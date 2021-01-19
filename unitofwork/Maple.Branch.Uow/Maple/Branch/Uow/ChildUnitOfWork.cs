// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MapleClub.Utility;

namespace Maple.Branch.Uow
{
    internal class ChildUnitOfWork : IUnitOfWork
    {
        public Guid Id => _parent.Id;

        public IBranchUnitOfWorkOptions? Options => _parent.Options;

        public IUnitOfWork? Outer => _parent.Outer;

        public bool IsReserved => _parent.IsReserved;

        public bool IsDisposed => _parent.IsDisposed;

        public bool IsCompleted => _parent.IsCompleted;

        public string? ReservationName => _parent.ReservationName;

        public event EventHandler<UnitOfWorkFailedEventArgs>? Failed;
        public event EventHandler<UnitOfWorkEventArgs>? Disposed;

        public IServiceProvider ServiceProvider => _parent.ServiceProvider;

        public Dictionary<string, object> Items => _parent.Items;

        private readonly IUnitOfWork _parent;

        public ChildUnitOfWork([NotNull] IUnitOfWork parent)
        {
            _parent = Check.NotNull(parent, nameof(parent));

            _parent.Failed += (sender, args) => { Failed.InvokeSafely(sender, args); };
            _parent.Disposed += (sender, args) => { Disposed.InvokeSafely(sender, args); };
        }

        public void SetOuter(IUnitOfWork? outer)
        {
            _parent.SetOuter(outer);
        }

        public void Initialize([NotNull] BranchUnitOfWorkOptions options)
        {
            Check.NotNull(options, nameof(options));

            _parent.Initialize(options);
        }

        public void Reserve([NotNull] string reservationName)
        {
            Check.NotNullOrEmpty(reservationName, nameof(reservationName));

            _parent.Reserve(reservationName);
        }

        public ValueTask SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _parent.SaveChangesAsync(cancellationToken);
        }

        public ValueTask CompleteAsync(CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask RollbackAsync(CancellationToken cancellationToken = default)
        {
            return _parent.RollbackAsync(cancellationToken);
        }

        public void OnCompleted(Func<ValueTask> handler)
        {
            _parent.OnCompleted(handler);
        }

        public IDatabaseApi? FindDatabaseApi([NotNull] string key)
        {
            Check.NotNullOrEmpty(key, nameof(key));

            return _parent.FindDatabaseApi(key);
        }

        public void AddDatabaseApi([NotNull] string key, [NotNull] IDatabaseApi api)
        {
            Check.NotNullOrEmpty(key, nameof(key));
            Check.NotNull(api, nameof(api));

            _parent.AddDatabaseApi(key, api);
        }

        public IDatabaseApi GetOrAddDatabaseApi([NotNull] string key, [NotNull] Func<IDatabaseApi> factory)
        {
            Check.NotNullOrEmpty(key, nameof(key));
            Check.NotNull(factory, nameof(factory));

            return _parent.GetOrAddDatabaseApi(key, factory);
        }

        public ITransactionApi? FindTransactionApi([NotNull] string key)
        {
            Check.NotNullOrEmpty(key, nameof(key));

            return _parent.FindTransactionApi(key);
        }

        public void AddTransactionApi([NotNull] string key, [NotNull] ITransactionApi api)
        {
            Check.NotNullOrEmpty(key, nameof(key));
            Check.NotNull(api, nameof(api));

            _parent.AddTransactionApi(key, api);
        }

        public ITransactionApi GetOrAddTransactionApi([NotNull] string key, [NotNull] Func<ITransactionApi> factory)
        {
            Check.NotNullOrEmpty(key, nameof(key));
            Check.NotNull(factory, nameof(factory));

            return _parent.GetOrAddTransactionApi(key, factory);
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public override string ToString()
        {
            return $"[UnitOfWork {Id}]";
        }
    }
}
