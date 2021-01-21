// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Maple.Branch.EventBus
{
    public interface IEventHandlerFactory
    {
        IEventHandlerDisposeWrapper GetHandler();

        bool IsInFactories(List<IEventHandlerFactory> factories);
    }
}
