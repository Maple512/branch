// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Maple.Branch.Collections;
using Maple.Branch.DynamicProxy;

namespace Maple.Branch.DependencyInjection
{
    public interface IInterceptorRegistredContext
    {
        ITypeList<IBranchInterceptor> Interceptors { get; }

        Type ImplementationType { get; }
    }
}
