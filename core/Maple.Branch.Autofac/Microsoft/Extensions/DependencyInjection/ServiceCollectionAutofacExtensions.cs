// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac;
using Maple.Branch.Autofac;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionAutofacExtensions
    {
        public static BranchAutofacServiceProviderFactory AddAutofacServiceProviderFactory(this IServiceCollection services)
        {
            return services.AddAutofacServiceProviderFactory(new ContainerBuilder());
        }

        public static BranchAutofacServiceProviderFactory AddAutofacServiceProviderFactory(this IServiceCollection services, ContainerBuilder containerBuilder)
        {
            var factory = new BranchAutofacServiceProviderFactory(containerBuilder);

            services.AddSingleton(containerBuilder);
            services.AddSingleton((IServiceProviderFactory<ContainerBuilder>)factory);

            return factory;
        }
    }
}
