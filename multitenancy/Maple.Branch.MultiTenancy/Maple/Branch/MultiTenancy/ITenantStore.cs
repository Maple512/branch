// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Maple.Branch.MultiTenancy.Configurations;

namespace Maple.Branch.MultiTenancy
{
    public interface ITenantStore
    {
        ValueTask<TenantConfiguration?> FindByNameAsync([NotNull] string name);
    }
}
