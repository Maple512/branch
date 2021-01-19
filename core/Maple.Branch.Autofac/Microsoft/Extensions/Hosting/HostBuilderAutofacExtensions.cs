// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac;
using Maple.Branch.Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderAutofacExtensions
    {
        public static IHostBuilder UseAutofac(this IHostBuilder hostBuilder)
        {
            var containerBuilder = new ContainerBuilder();

            return hostBuilder.ConfigureServices((_, services) =>
            {
                services.AddSingleton(containerBuilder);
            })
                .UseServiceProviderFactory(new BranchAutofacServiceProviderFactory(containerBuilder));
        }
    }
}
