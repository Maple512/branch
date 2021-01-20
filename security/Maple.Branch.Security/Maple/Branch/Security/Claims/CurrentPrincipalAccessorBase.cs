// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Security.Claims;
using System.Threading;

namespace Maple.Branch.Security.Claims
{
    public abstract class CurrentPrincipalAccessorBase : ICurrentPrincipalAccessor
    {
        public ClaimsPrincipal Principal => _currentPrincipal.Value ?? GetClaimsPrincipal();

        private readonly AsyncLocal<ClaimsPrincipal> _currentPrincipal = new AsyncLocal<ClaimsPrincipal>();

        protected abstract ClaimsPrincipal GetClaimsPrincipal();

        public virtual IAsyncDisposable Change(ClaimsPrincipal principal)
        {
            var parent = Principal;

            _currentPrincipal.Value = principal;

            return new AsyncDisposableAction(() =>
            {
                _currentPrincipal.Value = parent;
            });
        }
    }
}
