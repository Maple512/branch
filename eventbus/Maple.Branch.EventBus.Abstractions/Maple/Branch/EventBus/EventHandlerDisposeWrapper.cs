// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Maple.Branch.EventBus
{
    public class EventHandlerDisposeWrapper : IEventHandlerDisposeWrapper
    {
        public IIntegrationEventHandler EventHandler { get; }

        private readonly Func<ValueTask>? _dispose;

        public EventHandlerDisposeWrapper(
            IIntegrationEventHandler eventHandler,
            Func<ValueTask>? dispose = null)
        {
            EventHandler = eventHandler;
            _dispose = dispose;
        }

        public ValueTask DisposeAsync()
        {
            return _dispose != null ? _dispose.Invoke() : ValueTask.CompletedTask;
        }
    }
}
