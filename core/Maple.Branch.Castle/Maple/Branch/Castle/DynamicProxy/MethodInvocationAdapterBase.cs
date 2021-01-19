// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Maple.Branch.DynamicProxy;

namespace Maple.Branch.Castle.DynamicProxy
{
    public abstract class MethodInvocationAdapterBase : IMethodInvocation
    {
        public object[] Arguments => Invocation.Arguments;

        public IReadOnlyDictionary<string, object> ArgumentsDictionary => _lazyArgumentsDictionary.Value;
        private readonly Lazy<IReadOnlyDictionary<string, object>> _lazyArgumentsDictionary;

        public Type[] GenericArguments => Invocation.GenericArguments;

        public object TargetObject => Invocation.InvocationTarget ?? Invocation.MethodInvocationTarget;

        public MethodInfo Method => Invocation.MethodInvocationTarget ?? Invocation.Method;

        public object? ReturnValue { get; set; }

        protected IInvocation Invocation { get; }

        protected MethodInvocationAdapterBase(IInvocation invocation)
        {
            Invocation = invocation;
            _lazyArgumentsDictionary = new Lazy<IReadOnlyDictionary<string, object>>(GetArgumentsDictionary);
        }

        public abstract ValueTask ProceedAsync();

        private IReadOnlyDictionary<string, object> GetArgumentsDictionary()
        {
            var dict = new Dictionary<string, object>();

            var methodParameters = Method.GetParameters();
            for (var i = 0; i < methodParameters.Length; i++)
            {
                var key = methodParameters[i].Name;
                if (key != null)
                {
                    dict[key] = Invocation.Arguments[i];
                }
            }

            return dict;
        }
    }
}
