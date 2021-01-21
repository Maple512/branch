// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Maple.Branch.EventBus
{
    public class SingletonEventHandlerFactory : IEventHandlerFactory
    {
        public SingletonEventHandlerFactory(IIntegrationEventHandler instance)
        {
            Instance = instance;
        }

        public IIntegrationEventHandler Instance { get; }

        public IEventHandlerDisposeWrapper GetHandler()
        {
            return new EventHandlerDisposeWrapper(Instance);
        }

        public bool IsInFactories(List<IEventHandlerFactory> factories)
        {
            return factories.OfType<SingletonEventHandlerFactory>()
                .Any(m => m.Instance == Instance);
        }
    }
}
