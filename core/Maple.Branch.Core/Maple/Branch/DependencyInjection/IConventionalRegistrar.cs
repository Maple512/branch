// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.DependencyInjection
{
    public interface IConventionalRegistrar
    {
        void AddAssembly(IServiceCollection services, Assembly assembly);

        void AddTypes(IServiceCollection serivces, params Type[] types);

        void AddType(IServiceCollection services, Type type, ServiceLifetime? lifetime = null);
    }
}
