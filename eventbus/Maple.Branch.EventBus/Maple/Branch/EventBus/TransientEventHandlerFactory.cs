// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maple.Branch.EventBus
{
    public class TransientNewEventHandlerFactory<THandler>
        : TransientEventHandlerFactory<THandler>
        where THandler : IIntegrationEventHandler, new()
    {
        protected override IIntegrationEventHandler? CreateHandler()
        {
            return new THandler();
        }
    }

    public class TransientEventHandlerFactory<THandler>
        : TransientEventHandlerFactory,
        IEventHandlerFactory
        where THandler : IIntegrationEventHandler
    {
        public TransientEventHandlerFactory() : base(typeof(THandler))
        {
        }
    }

    public class TransientEventHandlerFactory : IEventHandlerFactory
    {
        public TransientEventHandlerFactory(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public Type HandlerType { get; }

        public IEventHandlerDisposeWrapper GetHandler()
        {
            var handler = CreateHandler();

            if (handler == null)
            {
                // 
                throw new BranchException();
            }

            return new EventHandlerDisposeWrapper(handler, () =>
            {
                switch (handler)
                {
                    case IAsyncDisposable asyncDisposable:
                        return asyncDisposable.DisposeAsync();
                    case IDisposable disposable:
                        disposable.Dispose();
                        return ValueTask.CompletedTask;
                    default:
                        return ValueTask.CompletedTask;
                }
            });
        }

        public bool IsInFactories(List<IEventHandlerFactory> factories)
        {
            return factories
                   .OfType<TransientEventHandlerFactory>()
                   .Any(m => m.HandlerType == HandlerType);
        }

        protected virtual IIntegrationEventHandler? CreateHandler()
        {
            return (IIntegrationEventHandler?)Activator.CreateInstance(HandlerType);
        }
    }
}
