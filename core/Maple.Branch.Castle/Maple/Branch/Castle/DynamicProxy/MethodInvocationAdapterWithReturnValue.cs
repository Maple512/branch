// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Maple.Branch.DynamicProxy;

namespace Maple.Branch.Castle.DynamicProxy
{
    public class MethodInvocationAdapterWithReturnValue<TResult> : MethodInvocationAdapterBase, IMethodInvocation
    {
        protected IInvocationProceedInfo ProceedInfo { get; }

        protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

        public MethodInvocationAdapterWithReturnValue(IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
            : base(invocation)
        {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        public override async ValueTask ProceedAsync()
        {
            ReturnValue = await Proceed(Invocation, ProceedInfo);
        }
    }
}
