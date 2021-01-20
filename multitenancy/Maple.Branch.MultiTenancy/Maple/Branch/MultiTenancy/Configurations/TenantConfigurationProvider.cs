// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maple.Branch.DependencyInjection;

namespace Maple.Branch.MultiTenancy.Configurations
{
    public class TenantConfigurationProvider : ITenantConfigurationProvider, ITransientObject
    {
        protected virtual ITenantResolver TenantResolver { get; }
        protected virtual ITenantStore TenantStore { get; }
        protected virtual ITenantResolveResultAccessor TenantResolveResultAccessor { get; }

        public TenantConfigurationProvider(
            ITenantResolver tenantResolver,
            ITenantStore tenantStore,
            ITenantResolveResultAccessor tenantResolveResultAccessor)
        {
            TenantResolver = tenantResolver;
            TenantStore = tenantStore;
            TenantResolveResultAccessor = tenantResolveResultAccessor;
        }

        public virtual async ValueTask<TenantConfiguration?> GetOrNullAsync(bool saveResolveResult = false)
        {
            var resolveResult = await TenantResolver.ResolveAsync();

            if (saveResolveResult)
            {
                TenantResolveResultAccessor.Result = resolveResult;
            }

            TenantConfiguration? tenant = null;
            if (resolveResult.TenantName.NotNullOrEmpty())
            {
                tenant = await TenantStore.FindByNameAsync(resolveResult.TenantName!);

                if (tenant == null)
                {
                    throw new BriefException(
                        "There is no tenant with the tenant id or name: " + resolveResult.TenantName
                    );
                }
            }

            return tenant;
        }
    }
}
