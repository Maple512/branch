// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Maple.Branch.EventBus
{
    public interface IIntegrationEventHandler<in TIntegartionEvent> : IIntegrationEventHandler
        where TIntegartionEvent : IntegrationEvent
    {
        ValueTask HanldeAsync(TIntegartionEvent @event);
    }

    public interface IIntegrationEventHandler
    {

    }
}
