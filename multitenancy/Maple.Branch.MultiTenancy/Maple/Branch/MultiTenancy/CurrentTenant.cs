// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Maple.Branch.DependencyInjection;

namespace Maple.Branch.MultiTenancy
{
    public class CurrentTenant : ICurrentTenant, ITransientObject
    {
        private readonly ICurrentTenantAccessor _currentTenantAccessor;

        public CurrentTenant(ICurrentTenantAccessor currentTenantAccessor)
        {
            _currentTenantAccessor = currentTenantAccessor;
        }

        public string? Id => _currentTenantAccessor.Current?.Id;

        public string? Name => _currentTenantAccessor.Current?.Name;

        public IAsyncDisposable Change(ICurrentTenant tenant)
        {
            var parent = _currentTenantAccessor.Current;

            _currentTenantAccessor.Current = new TenantInfo(tenant.Id, tenant.Name);

            return new AsyncDisposableAction(() =>
            {
                _currentTenantAccessor.Current = parent;
            });
        }
    }
}
