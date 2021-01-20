// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Componentization;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.MultiTenancy
{
    /// <summary>
    /// Multi-Tenancy Component
    /// </summary>
    /// <remarks>
    /// Options:
    /// <list type="number">
    /// <item>
    /// <see cref="MultiTenancyOptions"/>. appsettings.json path: <c>Branch:MultiTenancy</c>
    /// </item>
    /// <item><see cref="TenantResolveOptions"/></item>
    /// </list>
    /// </remarks>
    public class BranchMultiTenancyComponent : BranchComponent
    {
        public override void OnConfigureServices(ConfigureSerivcesContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<MultiTenancyOptions>(configuration.GetSection(BranchMultiTenancyConstants.ConfigurationRootFullName));
        }
    }
}
