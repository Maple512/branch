// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Maple.Branch.EventBus
{
    public class EventHandlerFactoryUnregistrar : IAsyncDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandlerFactory _factory;

        public EventHandlerFactoryUnregistrar(IEventBus eventBus, Type eventType, IEventHandlerFactory factory)
        {
            _eventBus = eventBus;
            _eventType = eventType;
            _factory = factory;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(_eventType, _factory);
        }

        public ValueTask DisposeAsync()
        {
            _eventBus.Unsubscribe(_eventType, _factory);

            return ValueTask.CompletedTask;
        }
    }
}
