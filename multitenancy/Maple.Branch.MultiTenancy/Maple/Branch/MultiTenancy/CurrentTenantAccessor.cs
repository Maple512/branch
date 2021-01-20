// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading;
using Maple.Branch.DependencyInjection;

namespace Maple.Branch.MultiTenancy
{
    public class CurrentTenantAccessor : ICurrentTenantAccessor, ISingletonObject
    {
        public TenantInfo? Current
        {
            get => _currentScope.Value;
            set => _currentScope.Value = value;
        }

        private readonly AsyncLocal<TenantInfo?> _currentScope;

        public CurrentTenantAccessor()
        {
            _currentScope = new AsyncLocal<TenantInfo?>();
        }
    }
}
