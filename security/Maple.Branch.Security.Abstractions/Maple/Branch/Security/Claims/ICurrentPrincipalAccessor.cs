// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Security.Claims;

namespace Maple.Branch.Security.Claims
{
    public interface ICurrentPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }

        IAsyncDisposable Change(ClaimsPrincipal principal);
    }
}
