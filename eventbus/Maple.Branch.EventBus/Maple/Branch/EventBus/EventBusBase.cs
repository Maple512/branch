// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Maple.Branch.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.EventBus
{
    public abstract class EventBusBase : IEventBus
    {
        protected EventBusBase(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
        }

        protected IServiceScopeFactory ServiceScopeFactory { get; }

        #region Publish

        public virtual ValueTask PublishAsync<TEvent>(TEvent eventData) where TEvent : IntegrationEvent
        {
            return PublishAsync(typeof(TEvent), eventData);
        }

        public abstract ValueTask PublishAsync(Type eventType, object eventData);

        #endregion Publish

        #region Subscribe

        public virtual IAsyncDisposable Subscribe<TEvent>(Func<TEvent, ValueTask> handler) where TEvent : IntegrationEvent
        {
            return Subscribe(typeof(TEvent), new ActionEventHandler<TEvent>(handler));
        }

        public virtual IAsyncDisposable Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler, new()
        {
            return Subscribe(typeof(TEvent), new TransientNewEventHandlerFactory<THandler>());
        }

        public virtual IAsyncDisposable Subscribe(Type eventType, IIntegrationEventHandler handler)
        {
            return Subscribe(eventType, new SingletonEventHandlerFactory(handler));
        }

        public virtual IAsyncDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {
            return Subscribe(typeof(TEvent), factory);
        }

        public abstract IAsyncDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

        #endregion Subscribe

        #region Unsubscribe

        public abstract void Unsubscribe<TEvent>([NotNull] Func<TEvent, ValueTask> action) where TEvent : IntegrationEvent;

        public virtual void Unsubscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent
        {
            Unsubscribe(typeof(TEvent), handler);
        }

        public abstract void Unsubscribe(Type eventType, IIntegrationEventHandler handler);

        public virtual void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : IntegrationEvent
        {
            Unsubscribe(typeof(TEvent), factory);
        }

        public abstract void Unsubscribe(Type eventType, IEventHandlerFactory factory);

        public virtual void UnsubscribeAll<TEvent>() where TEvent : IntegrationEvent
        {
            UnsubscribeAll(typeof(TEvent));
        }

        public abstract void UnsubscribeAll(Type eventType);

        #endregion Unsubscribe

        public virtual async Task TriggerHandlersAsync(Type eventType, object eventData)
        {
            var exceptions = new List<Exception>();

            await TriggerHandlersAsync(eventType, eventData, exceptions);

            if (exceptions.Any())
            {
                if (exceptions.Count == 1)
                {
                    exceptions[0].ReThrow();
                }

                throw new AggregateException("More than one error has occurred while triggering the event: " + eventType, exceptions);
            }
        }

        protected virtual async Task TriggerHandlersAsync(Type eventType, object eventData, List<Exception> exceptions)
        {
            foreach (var handlerFactories in GetHandlerFactories(eventType))
            {
                foreach (var handlerFactory in handlerFactories.EventHandlerFactories)
                {
                    await TriggerHandlerAsync(handlerFactory, handlerFactories.EventType, eventData, exceptions);
                }
            }

            // TODO: 待检验
            // Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
            //if (eventType.GetTypeInfo().IsGenericType
            //    && eventType.GetGenericArguments().Length == 1)
            //{
            //    var genericArg = eventType.GetGenericArguments()[0];
            //    var baseArg = genericArg.GetTypeInfo().BaseType;
            //    if (baseArg != null)
            //    {
            //        var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(baseArg);
            //        var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
            //        var baseEventData = Activator.CreateInstance(baseEventType, constructorArgs);
            //        await PublishAsync(baseEventType, baseEventData);
            //    }
            //}
        }

        protected virtual void SubscribeHandlers(ITypeList<IIntegrationEventHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                var interfaces = handler.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IIntegrationEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                    {
                        continue;
                    }

                    var genericArgs = @interface.GetGenericArguments();
                    if (genericArgs.Length == 1)
                    {
                        Subscribe(genericArgs[0], new IocEventHandlerFactory(handler, ServiceScopeFactory));
                    }
                }
            }
        }

        protected abstract IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType);

        protected virtual async Task TriggerHandlerAsync(IEventHandlerFactory asyncHandlerFactory, Type eventType, object eventData, List<Exception> exceptions)
        {
            await using (var eventHandlerWrapper = asyncHandlerFactory.GetHandler())
            {
                try
                {
                    var handlerType = eventHandlerWrapper.EventHandler.GetType();

                    if (handlerType.IsAssignableTo(typeof(IIntegrationEventHandler<>)))
                    {
                        var method = typeof(IIntegrationEventHandler<>)
                            .MakeGenericType(eventType)
                            .GetMethod(
                                nameof(IIntegrationEventHandler<IntegrationEvent>.HanldeAsync),
                                new[] { eventType }
                            );

                        await (ValueTask)method!.Invoke(eventHandlerWrapper.EventHandler, new[] { eventData })!;
                    }
                    else
                    {
                        throw new BranchException("The object instance is not an event handler. Object type: " + handlerType.AssemblyQualifiedName);
                    }
                }
                catch (TargetInvocationException ex)
                {
                    exceptions.Add(ex.InnerException);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
        }

        protected class EventTypeWithEventHandlerFactories
        {
            public Type EventType { get; }

            public List<IEventHandlerFactory> EventHandlerFactories { get; }

            public EventTypeWithEventHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories)
            {
                EventType = eventType;
                EventHandlerFactories = eventHandlerFactories;
            }
        }
    }
}
