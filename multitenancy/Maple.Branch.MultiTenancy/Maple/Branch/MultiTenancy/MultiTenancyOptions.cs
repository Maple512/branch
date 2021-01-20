// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Maple.Branch.MultiTenancy.Configurations;

namespace Maple.Branch.MultiTenancy
{
    public class MultiTenancyOptions
    {
        public MultiTenancyOptions()
        {
            Tenants = Array.Empty<TenantConfiguration>();
        }

        public bool IsEnabled { get; set; }

        public TenantConfiguration[] Tenants { get; set; }
    }
}
