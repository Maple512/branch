// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Maple.Branch.EventBus.InMemory
{
    public sealed class NullInMemoryEventBus : IInMemoryEventBus
    {
        public static NullInMemoryEventBus Instance { get; } = new();

        public ValueTask PublishAsync<TEvent>(TEvent eventData) where TEvent : IntegrationEvent
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask PublishAsync(Type eventType, object eventData)
        {
            return ValueTask.CompletedTask;
        }

        public IAsyncDisposable Subscribe<TEvent>(IInMemoryEventHandler<TEvent> handler) where TEvent : IntegrationEvent
        {
            return NullAsyncDisposableAction.Instance;
        }

        public IAsyncDisposable Subscribe<TEvent>(Func<TEvent, ValueTask> action) where TEvent : IntegrationEvent
        {
            return NullAsyncDisposableAction.Instance;
        }

        public IAsyncDisposable Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler, new()
        {
            return NullAsyncDisposableAction.Instance;
        }

        public IAsyncDisposable Subscribe(Type eventType, IIntegrationEventHandler handler)
        {
            return NullAsyncDisposableAction.Instance;
        }

        public IAsyncDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {
            return NullAsyncDisposableAction.Instance;
        }

        public IAsyncDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            return NullAsyncDisposableAction.Instance;
        }

        public void Unsubscribe<TEvent>([NotNull] Func<TEvent, ValueTask> action) where TEvent : IntegrationEvent
        {
        }

        public void Unsubscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent
        {
        }

        public void Unsubscribe(Type eventType, IIntegrationEventHandler handler)
        {
        }

        public void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : IntegrationEvent
        {
        }

        public void Unsubscribe(Type eventType, IEventHandlerFactory factory)
        {
        }

        public void UnsubscribeAll<TEvent>() where TEvent : IntegrationEvent
        {
        }

        public void UnsubscribeAll(Type eventType)
        {
        }
    }
}
