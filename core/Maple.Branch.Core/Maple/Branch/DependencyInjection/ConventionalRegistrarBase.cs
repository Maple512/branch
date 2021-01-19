// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.DependencyInjection
{
    public abstract class ConventionalRegistrarBase : IConventionalRegistrar
    {
        public void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(type =>
                type != null
                && type.IsClass
                && !type.IsAbstract
                && !type.IsGenericType
                && type.IsInjectionType());

            AddTypes(services, types.ToArray());
        }

        public abstract void AddType(IServiceCollection services, Type type, ServiceLifetime? lifetime = null);

        public void AddTypes(IServiceCollection serivces, params Type[] types)
        {
            foreach (var type in types)
            {
                AddType(serivces, type);
            }
        }
    }
}
