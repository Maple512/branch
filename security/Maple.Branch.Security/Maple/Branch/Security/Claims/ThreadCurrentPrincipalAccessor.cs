// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Security.Claims;
using System.Threading;
using Maple.Branch.DependencyInjection;

namespace Maple.Branch.Security.Claims
{
    public class ThreadCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ISingletonObject
    {
        protected override ClaimsPrincipal GetClaimsPrincipal()
        {
            return (ClaimsPrincipal)Thread.CurrentPrincipal!;
        }
    }
}
