// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Maple.Branch.EventBus.InMemory
{
    public interface IInMemoryEventHandler<in TEvent> : IIntegrationEventHandler<TEvent>
        where TEvent : IntegrationEvent
    {

    }
}
