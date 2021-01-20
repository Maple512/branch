// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionConfiguretionExtensions
    {
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            var hostBuilderContext = services.GetSingletonInstanceOrNull<HostBuilderContext>();
            if(hostBuilderContext?.Configuration != null)
            {
                return hostBuilderContext.Configuration;
            }

            return services.GetSingletonInstance<IConfiguration>();
        }
    }
}
