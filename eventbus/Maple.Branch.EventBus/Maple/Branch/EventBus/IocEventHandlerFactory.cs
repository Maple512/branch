// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.EventBus
{
    public class IocEventHandlerFactory : IEventHandlerFactory, IAsyncDisposable
    {
        public IocEventHandlerFactory(Type handlerType, IServiceScopeFactory scopeFactory)
        {
            HandlerType = handlerType;
            ScopeFactory = scopeFactory;
        }

        public Type HandlerType { get; }

        protected IServiceScopeFactory ScopeFactory { get; }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public IEventHandlerDisposeWrapper GetHandler()
        {
            var scope = ScopeFactory.CreateScope();

            return new EventHandlerDisposeWrapper(
                (IIntegrationEventHandler)scope.ServiceProvider.GetRequiredService(HandlerType),
                () =>
                {
                    scope.Dispose();
                    return ValueTask.CompletedTask;
                });
        }

        public bool IsInFactories(List<IEventHandlerFactory> factories)
        {
            return factories.OfType<IocEventHandlerFactory>()
                .Any(m => m.HandlerType == HandlerType);
        }
    }
}
