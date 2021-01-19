// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;

namespace Maple.Branch.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork? Current { get; }

        IUnitOfWork Begin([NotNull] BranchUnitOfWorkOptions options, bool requiresNew = false);

        IUnitOfWork Reserve([NotNull] string reservationName, bool requiresNew = false);

        void BeginReserved([NotNull] string reservationName, [NotNull] BranchUnitOfWorkOptions options);

        bool TryBeginReserved([NotNull] string reservationName, [NotNull] BranchUnitOfWorkOptions options);
    }
}
