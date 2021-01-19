// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace Maple.Branch.Uow
{
    public interface ISupportsSavingChanges
    {
        ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
