// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Maple.Branch.DynamicProxy
{
    public abstract class BranchInterceptor : IBranchInterceptor
    {
        public abstract ValueTask InterceptAsync(IMethodInvocation method);
    }
}
