// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Maple.Branch.MultiTenancy
{
    public class TenantResolveContext : ITenantResolveContext
    {
        public TenantResolveContext(IServiceProvider serviceProvder)
        {
            ServiceProvder = serviceProvder;
        }

        public IServiceProvider ServiceProvder { get; }

        public string? TenantName { get; }

        public bool Handled { get; }
    }
}
