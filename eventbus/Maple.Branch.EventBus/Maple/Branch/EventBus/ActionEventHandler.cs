// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Maple.Branch.EventBus
{
    public class ActionEventHandler<TEvent> : IIntegrationEventHandler<TEvent>
        where TEvent : IntegrationEvent
    {
        public ActionEventHandler(Func<TEvent, ValueTask> handler)
        {
            Handler = handler;
        }

        public Func<TEvent, ValueTask> Handler { get; }

        public ValueTask HanldeAsync(TEvent @event)
        {
            return Handler(@event);
        }
    }
}
