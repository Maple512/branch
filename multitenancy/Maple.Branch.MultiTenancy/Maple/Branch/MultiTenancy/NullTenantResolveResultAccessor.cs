// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.DependencyInjection;

namespace Maple.Branch.MultiTenancy
{
    public class NullTenantResolveResultAccessor : ITenantResolveResultAccessor, ISingletonObject
    {
        public TenantResolveResult? Result
        {
            get => null;
            set { }
        }
    }
}
