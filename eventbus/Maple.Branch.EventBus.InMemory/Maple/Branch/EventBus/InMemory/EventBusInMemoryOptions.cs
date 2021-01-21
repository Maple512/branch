// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Collections;

namespace Maple.Branch.EventBus.InMemory
{
    public class EventBusInMemoryOptions
    {
        public ITypeList<IIntegrationEventHandler> Handlers { get; } = new TypeList<IIntegrationEventHandler>();
    }
}
