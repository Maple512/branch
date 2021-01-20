// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maple.Branch.Data.ConnectionStrings;
using Maple.Branch.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Maple.Branch.MultiTenancy.ConnectionStrings
{
    [Injection(ReplaceService = true)]
    public class MultiTenantConnectionStringResolver : DeafultConnectionStringResolver
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IServiceProvider _serviceProvider;

        public MultiTenantConnectionStringResolver(
            IOptionsSnapshot<ConnectionStringOptions> options,
            ICurrentTenant currentTenant,
            IServiceProvider serviceProvider)
            : base(options)
        {
            _currentTenant = currentTenant;
            _serviceProvider = serviceProvider;
        }

        public override async ValueTask<string?> ResolveAsync(string? connectionStringName = null)
        {
            // No current tenant, fallback to default logic
            if (_currentTenant.Name.IsNullOrEmpty())
            {
                return await base.ResolveAsync(connectionStringName);
            }

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var tenantStore = serviceScope
                    .ServiceProvider
                    .GetRequiredService<ITenantStore>();

                var tenant = await tenantStore.FindByNameAsync(_currentTenant.Name!);

                if (tenant?.ConnectionStrings == null)
                {
                    return await base.ResolveAsync(connectionStringName);
                }

                // Requesting default connection string
                if (connectionStringName == null)
                {
                    return tenant.ConnectionStrings.Default ??
                           Options.Connections.Default;
                }

                // Requesting specific connection string
                var connString = tenant.ConnectionStrings.GetOrDefault(connectionStringName);
                if (connString != null)
                {
                    return connString;
                }

                /* Requested a specific connection string, but it's not specified for the tenant.
                 * - If it's specified in options, use it.
                 * - If not, use tenant's default conn string.
                 */

                var connStringInOptions = Options.Connections.GetOrDefault(connectionStringName);
                if (connStringInOptions != null)
                {
                    return connStringInOptions;
                }

                return tenant.ConnectionStrings.Default ??
                       Options.Connections.Default;
            }
        }
    }
}
