// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Security.Claims;

namespace Maple.Branch.Security.Claims
{
    public static class BranchClaimTypes
    {
        /// <summary>
        /// Default: <see cref="ClaimTypes.NameIdentifier"/>
        /// </summary>
        public const string UserId  = ClaimTypes.NameIdentifier;

        /// <summary>
        /// Default: <see cref="ClaimTypes.Name"/>
        /// </summary>
        public const string UserName  = ClaimTypes.Name;

        /// <summary>
        /// Default: <see cref="ClaimTypes.Email"/>
        /// </summary>
        public const string Email  = ClaimTypes.Email;

        /// <summary>
        /// Default: <see cref="ClaimTypes.Role"/>
        /// </summary>
        public const string Role = ClaimTypes.Role;
    }
}
