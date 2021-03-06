// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Maple.Branch.MultiTenancy
{
    public interface ICurrentTenant
    {
        string? Id { get; }

        string? Name { get; }

        IAsyncDisposable Change(ICurrentTenant tenant);
    }
}
