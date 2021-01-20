// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maple.Branch.MultiTenancy
{
    public static class CurrentTenantExtensions
    {
        public static MultiTenancySides GetMultiTenancySides(this ICurrentTenant currentTenant)
        {
            return currentTenant.Id.IsNullOrEmpty()
                ? MultiTenancySides.Host
                : MultiTenancySides.Tenant;
        }
    }
}
