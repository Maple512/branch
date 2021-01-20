// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Maple.Branch.DependencyInjection;
using Maple.Branch.MultiTenancy.Configurations;
using MapleClub.Utility;
using Microsoft.Extensions.Options;

namespace Maple.Branch.MultiTenancy
{
    public class TenantStore : ITenantStore, ITransientObject
    {
        private readonly MultiTenancyOptions _options;

        public TenantStore(IOptionsSnapshot<MultiTenancyOptions> options)
        {
            _options = options.Value;
        }

        public ValueTask<TenantConfiguration?> FindByNameAsync([NotNull] string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));

            var result = _options.Tenants?.FirstOrDefault(t => t.Name == name);

            return ValueTask.FromResult(result);
        }
    }
}
