// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Maple.Branch.EventBus
{
    public interface IEventBus
    {
        #region Publish

        ValueTask PublishAsync<TEvent>(TEvent eventData)
            where TEvent : IntegrationEvent;

        ValueTask PublishAsync(Type eventType, object eventData);

        #endregion Publish

        #region Subscribe

        /// <summary>
        /// action handler
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        IAsyncDisposable Subscribe<TEvent>(Func<TEvent, ValueTask> action)
            where TEvent : IntegrationEvent;

        /// <summary>
        /// transient handler
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        IAsyncDisposable Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler, new();

        /// <summary>
        /// singleton handler
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        IAsyncDisposable Subscribe(Type eventType, IIntegrationEventHandler handler);

        /// <summary>
        /// handler factory
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        IAsyncDisposable Subscribe<TEvent>(IEventHandlerFactory factory)
            where TEvent : class;

        /// <summary>
        /// handler factory
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        IAsyncDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

        #endregion Subscribe

        #region Unsubscribe

        void Unsubscribe<TEvent>([NotNull] Func<TEvent, ValueTask> action)
            where TEvent : IntegrationEvent;

        void Unsubscribe<TEvent>(IIntegrationEventHandler<TEvent> handler)
            where TEvent : IntegrationEvent;

        void Unsubscribe(Type eventType, IIntegrationEventHandler handler);

        void Unsubscribe<TEvent>(IEventHandlerFactory factory)
            where TEvent : IntegrationEvent;

        void Unsubscribe(Type eventType, IEventHandlerFactory factory);

        void UnsubscribeAll<TEvent>()
            where TEvent : IntegrationEvent;

        void UnsubscribeAll(Type eventType);

        #endregion Unsubscribe
    }
}
