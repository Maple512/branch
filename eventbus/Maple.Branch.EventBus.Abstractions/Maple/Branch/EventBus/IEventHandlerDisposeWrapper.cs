// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Maple.Branch.EventBus
{
    public interface IEventHandlerDisposeWrapper : IAsyncDisposable
    {
        IIntegrationEventHandler EventHandler { get; }
    }
}
