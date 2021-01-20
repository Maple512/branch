// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Maple.Branch.Data.ConnectionStrings;
using MapleClub.Utility;

namespace Maple.Branch.MultiTenancy.Configurations
{
    [Serializable]
    public class TenantConfiguration
    {
        public string Name { get; set; }

        public ConnectionStringDictionary ConnectionStrings { get; set; } = new();

        public TenantConfiguration()
        {

        }

        public TenantConfiguration([NotNull] string name)
        {
            Name = Check.NotNull(name, nameof(name));
        }
    }
}
