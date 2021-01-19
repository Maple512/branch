// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Maple.Branch.DynamicProxy;

namespace Maple.Branch.Castle.DynamicProxy
{
    public class AsyncInterceptorAdapter<TInterceptor> : AsyncInterceptorBase
        where TInterceptor : IBranchInterceptor
    {
        private readonly TInterceptor _interceptor;

        public AsyncInterceptorAdapter(TInterceptor interceptor)
        {
            _interceptor = interceptor;
        }

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            await _interceptor.InterceptAsync(
                new MethodInvocationAdapter(invocation, proceedInfo, proceed)
            );
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var adapter = new MethodInvocationAdapterWithReturnValue<TResult>(invocation, proceedInfo, proceed);

            await _interceptor.InterceptAsync(
                adapter
            );

            return (TResult)adapter.ReturnValue!;
        }
    }
}
