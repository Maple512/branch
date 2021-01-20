// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Maple.Branch.MultiTenancy
{
    public class TenantInfo
    {
        public TenantInfo(string? id = null, string? name = null)
        {
            Id = id;
            Name = name;
        }

        public string? Id { get; }

        public string? Name { get; }
    }
}
