// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Maple.Branch.Collections;
using Maple.Branch.DynamicProxy;
using MapleClub.Utility;

namespace Maple.Branch.DependencyInjection
{
    public class InterceptorRegistredContext : IInterceptorRegistredContext
    {
        public virtual ITypeList<IBranchInterceptor> Interceptors { get; }

        public virtual Type ServiceType { get; }

        public virtual Type ImplementationType { get; }

        public InterceptorRegistredContext([NotNull] Type serviceType, [NotNull] Type implementationType)
        {
            ServiceType = Check.NotNull(serviceType, nameof(serviceType));
            ImplementationType = Check.NotNull(implementationType, nameof(implementationType));

            Interceptors = new TypeList<IBranchInterceptor>();
        }
    }
}
