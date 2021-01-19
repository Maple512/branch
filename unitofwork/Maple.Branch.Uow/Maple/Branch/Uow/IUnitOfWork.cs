// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Maple.Branch.Uow
{
    public interface IUnitOfWork : IDatabaseApiContainer, ITransactionApiContainer, IAsyncDisposable
    {
        Guid Id { get; }

        Dictionary<string, object> Items { get; }

        // TODO: Switch to OnFailed (sync) and OnDisposed (sync) methods to be compatible with OnCompleted
        event EventHandler<UnitOfWorkFailedEventArgs>? Failed;

        event EventHandler<UnitOfWorkEventArgs>? Disposed;

        IBranchUnitOfWorkOptions? Options { get; }

        IUnitOfWork? Outer { get; }

        bool IsReserved { get; }

        bool IsDisposed { get; }

        bool IsCompleted { get; }

        string? ReservationName { get; }

        void SetOuter(IUnitOfWork? outer);

        void Initialize([NotNull] BranchUnitOfWorkOptions options);

        void Reserve([NotNull] string reservationName);

        ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);

        ValueTask CompleteAsync(CancellationToken cancellationToken = default);

        ValueTask RollbackAsync(CancellationToken cancellationToken = default);

        void OnCompleted(Func<ValueTask> handler);
    }
}
