// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.Componentization
{
    public class ConfigureSerivcesContext
    {
        public ConfigureSerivcesContext(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
