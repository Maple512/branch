// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Maple.Branch.DependencyInjection
{
    public class DefaultConventionalRegistrar : ConventionalRegistrarBase
    {
        public override void AddType(IServiceCollection services, Type type, ServiceLifetime? lifetime = null)
        {
            var injectionAttr = type.GetCustomAttribute<InjectionAttribute>();
            lifetime ??= injectionAttr?.Lifetime ?? GetServiceLifetimeFromAssignedTo(type);
            if (lifetime == null)
            {
                return;
            }

            var exposeTypes = GetExposedServiceTypes(type);

            foreach (var exposeType in exposeTypes)
            {
                var serviceDescriptor = CreateServiceDescriptor(
                    type,
                    exposeType,
                    exposeTypes,
                    lifetime.Value
                );

                if (injectionAttr?.ReplaceService == true)
                {
                    services.Replace(serviceDescriptor);
                }
                else if (injectionAttr?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }
            }
        }

        protected virtual IEnumerable<Type> GetExposedServiceTypes(Type type)
        {
            var interfaces = type.GetTypeInfo().GetInterfaces()
                .Where(m => !m.IsIn(typeof(ITransientObject), typeof(ISingletonObject), typeof(IScopedObject)))
                .ToList();

            interfaces.AddLast(type);

            return interfaces;
        }

        protected virtual ServiceDescriptor CreateServiceDescriptor(
            Type implementationType,
            Type exposeType,
            IEnumerable<Type> exposeTypes,
            ServiceLifetime lifetime)
        {
            if (lifetime.IsIn(ServiceLifetime.Singleton, ServiceLifetime.Scoped))
            {
                var redirectedType = GetRedirectedTypeOrNull(implementationType, exposeType, exposeTypes);

                if (redirectedType != null)
                {
                    return ServiceDescriptor.Describe(exposeType, implementationType, lifetime);
                }
            }

            return ServiceDescriptor.Describe(exposeType, implementationType, lifetime);
        }

        protected virtual Type? GetRedirectedTypeOrNull(
            Type implementationType,
            Type exposeType,
            IEnumerable<Type> exposeTypes)
        {
            if (exposeTypes.Count() < 2 || exposeType == implementationType)
            {
                return null;
            }

            if (exposeTypes.Contains(implementationType))
            {
                return implementationType;
            }

            return exposeTypes.FirstOrDefault(type => type.IsAssignableTo(exposeType));
        }

        protected virtual ServiceLifetime? GetServiceLifetimeFromAssignedTo(Type type)
        {
            if (type.IsAssignableTo(typeof(ITransientObject)))
            {
                return ServiceLifetime.Transient;
            }

            if (type.IsAssignableTo(typeof(ISingletonObject)))
            {
                return ServiceLifetime.Singleton;
            }

            if (type.IsAssignableTo(typeof(IScopedObject)))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }
    }
}
