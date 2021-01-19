// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Reflection;
using Maple.Branch.DependencyInjection;
using Maple.Branch.DynamicProxy;

namespace Maple.Branch.Uow
{
    public static class UnitOfWorkInterceptorRegistrar
    {
        public static void RegisterIfNeeded(IInterceptorRegistredContext context)
        {
            if (ShouldIntercept(context.ImplementationType))
            {
                context.Interceptors.Add<UnitOfWorkInterceptor>();
            }
        }

        private static bool ShouldIntercept(Type type)
        {
            return !DynamicProxyIgnoreTypes.Contains(type)
                && UnitOfWorkHelper.IsUnitOfWorkType(type.GetTypeInfo());
        }
    }
}
