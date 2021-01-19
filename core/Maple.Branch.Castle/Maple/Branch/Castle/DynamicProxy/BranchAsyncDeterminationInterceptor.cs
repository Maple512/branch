// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Castle.DynamicProxy;
using Maple.Branch.DynamicProxy;

namespace Maple.Branch.Castle.DynamicProxy
{
    public class BranchAsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
        where TInterceptor : IBranchInterceptor
    {
        public BranchAsyncDeterminationInterceptor(TInterceptor abpInterceptor)
            : base(new AsyncInterceptorAdapter<TInterceptor>(abpInterceptor))
        {

        }
    }
}
