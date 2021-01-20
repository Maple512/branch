// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Maple.Branch.MultiTenancy.Configurations
{
    public interface ITenantConfigurationProvider
    {
        ValueTask<TenantConfiguration?> GetOrNullAsync(bool saveResolveResult = false);
    }
}
