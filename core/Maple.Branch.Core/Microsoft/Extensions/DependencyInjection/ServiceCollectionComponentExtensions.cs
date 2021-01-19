// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Componentization;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionComponentExtensions
    {
        public static IServiceCollection AddComponent<TStartupComponent>(this IServiceCollection services)
            where TStartupComponent : IBranchComponent
        {
            var logger = services.GetSingletonInstanceOrNull<ILogger<IBranchComponent>>();

            var components = ComponentLoadHelper.LoadBranchComponents(typeof(TStartupComponent));

            services.AddSingleton<IComponentContainer>(new ComponentContainer(components));

            ComponentHelper.Initialization(components, services, logger);

            return services;
        }
    }
}
