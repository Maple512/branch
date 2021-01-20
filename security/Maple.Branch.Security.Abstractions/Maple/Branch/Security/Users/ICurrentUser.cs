// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Maple.Branch.Security.Users
{
    public interface ICurrentUser
    {
        string? Id { get; }

        string? Name { get; }

        string? Email { get; }

        IEnumerable<string>? Roles { get; }

        bool IsInRole(string role);
    }
}
