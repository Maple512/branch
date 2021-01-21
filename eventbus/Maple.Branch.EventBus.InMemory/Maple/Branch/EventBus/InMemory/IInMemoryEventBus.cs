// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Maple.Branch.EventBus.InMemory
{
    public interface IInMemoryEventBus : IEventBus
    {
        IAsyncDisposable Subscribe<TEvent>(IInMemoryEventHandler<TEvent> handler)
               where TEvent : IntegrationEvent;

    }
}
