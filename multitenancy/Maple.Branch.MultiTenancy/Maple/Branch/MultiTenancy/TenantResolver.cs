// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using Maple.Branch.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Maple.Branch.MultiTenancy
{
    public class TenantResolver : ITenantResolver, ITransientObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TenantResolveOptions _options;

        public TenantResolver(IOptions<TenantResolveOptions> options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public virtual async ValueTask<TenantResolveResult> ResolveAsync()
        {
            var result = new TenantResolveResult();

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new TenantResolveContext(serviceScope.ServiceProvider);

                foreach (var tenantResolver in _options.TenantResolves)
                {
                    await tenantResolver.ResolveAsync(context);

                    result.AppliedResolvers.Add(tenantResolver.Name);

                    if (context.Handled)
                    {
                        result.TenantName = context.TenantName;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
