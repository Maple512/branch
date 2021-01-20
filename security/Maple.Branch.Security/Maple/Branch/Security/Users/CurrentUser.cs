// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Maple.Branch.DependencyInjection;
using Maple.Branch.Security.Claims;
using Microsoft.Extensions.Options;

namespace Maple.Branch.Security.Users
{
    public class CurrentUser : ICurrentUser, ITransientObject
    {
        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
        private readonly UserClaimOptions _options;

        public CurrentUser(ICurrentPrincipalAccessor currentPrincipalAccessor,
            IOptions<UserClaimOptions> options)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
        }

        public string? Id => _currentPrincipalAccessor.Principal.FindClaimValue(_options.UserId);

        public string? Name => _currentPrincipalAccessor.Principal.FindClaimValue(_options.UserName);

        public string? Email => _currentPrincipalAccessor.Principal.FindClaimValue(_options.Email);

        public IEnumerable<string>? Roles => _currentPrincipalAccessor.Principal.FindClaimValues(_options.Role);

        public bool IsInRole(string role) => _currentPrincipalAccessor.Principal
            .FindClaimValues(_options.Role)
            .Any(m => m.Equals(role, StringComparison.OrdinalIgnoreCase));
    }
}
