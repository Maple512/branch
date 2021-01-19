// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Maple.Branch.DynamicProxy
{
    public interface IMethodInvocation
    {
        object[] Arguments { get; }

        IReadOnlyDictionary<string, object> ArgumentsDictionary { get; }

        Type[] GenericArguments { get; }

        object TargetObject { get; }

        MethodInfo Method { get; }

        object? ReturnValue { get; set; }

        ValueTask ProceedAsync();
    }
}
