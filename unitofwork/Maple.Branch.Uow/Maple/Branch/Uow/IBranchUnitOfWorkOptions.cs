// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Data;

namespace Maple.Branch.Uow
{
    public interface IBranchUnitOfWorkOptions
    {
        bool IsTransactional { get; }

        IsolationLevel? IsolationLevel { get; }

        TimeSpan? Timeout { get; }
    }
}
