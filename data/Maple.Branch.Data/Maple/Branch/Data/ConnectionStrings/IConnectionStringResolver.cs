// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Maple.Branch.Data.ConnectionStrings
{
    public interface IConnectionStringResolver
    {
        ValueTask<string?> ResolveAsync(string? connectionStringName = null);
    }
}
