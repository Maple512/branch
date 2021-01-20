// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Maple.Branch.MultiTenancy
{
    public class TenantResolveResult
    {
        public string? TenantName { get; set; }

        public List<string> AppliedResolvers { get; } = new();
    }
}
