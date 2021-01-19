// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using Maple.Branch.Componentization;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.TestBase
{
    public abstract class IntegratedTest<TStartupComponent> : IAsyncDisposable
        where TStartupComponent : IBranchComponent
    {
        protected IntegratedTest()
        {
            var services = new ServiceCollection();

            services.AddComponent<TStartupComponent>();

            ServiceProvider = services.BuildServiceProvider();
            ServiceScope = ServiceProvider.CreateScope();
        }

        protected IServiceProvider ServiceProvider { get; }

        protected IServiceScope ServiceScope { get; }

        public ValueTask DisposeAsync()
        {
            ServiceScope.Dispose();

            return ValueTask.CompletedTask;
        }
    }
}
