// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Security.Claims;

namespace Maple.Branch.Security.Users
{
    public class UserClaimOptions
    {
        /// <summary>
        /// Default: <see cref="BranchClaimTypes.UserId"/>
        /// </summary>
        public string UserId { get; set; } = BranchClaimTypes.UserId;

        /// <summary>
        /// Default: <see cref="BranchClaimTypes.UserName"/>
        /// </summary>
        public string UserName { get; set; } = BranchClaimTypes.UserName;

        /// <summary>
        /// Default: <see cref="BranchClaimTypes.Email"/>
        /// </summary>
        public string Email { get; set; } = BranchClaimTypes.Email;

        /// <summary>
        /// Default: <see cref="BranchClaimTypes.Role"/>
        /// </summary>
        public string Role { get; set; } = BranchClaimTypes.Role;
    }
}
