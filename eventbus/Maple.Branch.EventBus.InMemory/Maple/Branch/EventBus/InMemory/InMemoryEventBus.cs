// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MapleClub.Utility;
using MapleClub.Utility.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Maple.Branch.EventBus.InMemory
{
    public class InMemoryEventBus : EventBusBase, IInMemoryEventBus
    {
        private readonly EventBusInMemoryOptions _options;
        public ILogger Logger { get; set; }
        protected ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }

        public InMemoryEventBus(
            IOptions<EventBusInMemoryOptions> options,
            IServiceScopeFactory serviceScope) : base(serviceScope)
        {
            _options = options.Value;
            Logger = NullLogger<InMemoryEventBus>.Instance;
            HandlerFactories = new();

            SubscribeHandlers(_options.Handlers);
        }

        public override async ValueTask PublishAsync(Type eventType, object eventData)
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

        public override IAsyncDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories =>
                {
                    if (!factory.IsInFactories(factories))
                    {
                        factories.Add(factory);
                    }
                }
                );

            return new EventHandlerFactoryUnregistrar(this, eventType, factory);
        }

        public IAsyncDisposable Subscribe<TEvent>(IInMemoryEventHandler<TEvent> handler) where TEvent : IntegrationEvent
        {
            return Subscribe(typeof(TEvent), handler);
        }

        public override void Unsubscribe<TEvent>([NotNull] Func<TEvent, ValueTask> action)
        {
            Check.NotNull(action, nameof(action));

            GetOrCreateHandlerFactories(typeof(TEvent))
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                        {
                            var singleInstanceFactory = factory as SingletonEventHandlerFactory;
                            if (singleInstanceFactory == null)
                            {
                                return false;
                            }

                            var actionHandler = singleInstanceFactory.Instance as ActionEventHandler<TEvent>;
                            if (actionHandler == null)
                            {
                                return false;
                            }

                            return actionHandler.Handler == action;
                        });
                });
        }

        public override void Unsubscribe(Type eventType, IIntegrationEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                            factory is SingletonEventHandlerFactory &&
                            (factory as SingletonEventHandlerFactory)!.Instance == handler
                    );
                });
        }

        public override void Unsubscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
        }

        public override void UnsubscribeAll(Type eventType)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
        }

        protected override IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

            foreach (var handlerFactory in HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
            {
                handlerFactoryList.Add(new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
            }

            return handlerFactoryList.ToArray();
        }

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            return HandlerFactories.GetOrAdd(eventType, (type) => new List<IEventHandlerFactory>());
        }

        private static bool ShouldTriggerEventForHandler(Type targetEventType, Type handlerEventType)
        {
            // Should trigger same type
            if (handlerEventType == targetEventType)
            {
                return true;
            }

            // Should trigger for inherited types
            if (handlerEventType.IsAssignableFrom(targetEventType))
            {
                return true;
            }

            return false;
        }
    }
}
