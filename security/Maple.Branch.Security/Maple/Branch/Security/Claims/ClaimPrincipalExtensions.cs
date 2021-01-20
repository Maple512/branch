// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using MapleClub.Utility;

namespace Maple.Branch.Security.Claims
{
    public static class ClaimPrincipalExtensions
    {
        public static string? FindClaimValue(this ClaimsPrincipal principal, [NotNull] string type)
        {
            Check.NotNullOrEmpty(type, nameof(type));

            return principal.FindFirst(type)?.Value;
        }

        public static IEnumerable<string> FindClaimValues(this ClaimsPrincipal principal, [NotNull] string type)
        {
            Check.NotNullOrEmpty(type, nameof(type));

            return principal.FindAll(type).Select(m => m.Value);
        }
    }
}
