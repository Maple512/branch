// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Maple.Branch.DependencyInjection;
using MapleClub.Utility;
using Microsoft.Extensions.Options;

namespace Maple.Branch.Uow
{
    public class UnitOfWork : IUnitOfWork, ITransientObject
    {
        public const string UnitOfWorkReservationName = "_ActionUnitOfWork";

        public Guid Id { get; } = Guid.NewGuid();

        public IBranchUnitOfWorkOptions? Options { get; private set; }

        public IUnitOfWork? Outer { get; private set; }

        public bool IsReserved { get; set; }

        public bool IsDisposed { get; private set; }

        public bool IsCompleted { get; private set; }

        public string? ReservationName { get; set; }

        protected List<Func<ValueTask>> CompletedHandlers { get; } = new List<Func<ValueTask>>();

        public event EventHandler<UnitOfWorkFailedEventArgs>? Failed;
        public event EventHandler<UnitOfWorkEventArgs>? Disposed;

        public IServiceProvider ServiceProvider { get; }

        public Dictionary<string, object> Items { get; }

        private readonly Dictionary<string, IDatabaseApi> _databaseApis;
        private readonly Dictionary<string, ITransactionApi> _transactionApis;
        private readonly BranchUnitOfWorkDefaultOptions _defaultOptions;

        private Exception? _exception;
        private bool _isCompleting;
        private bool _isRolledback;

        public UnitOfWork(IServiceProvider serviceProvider, IOptions<BranchUnitOfWorkDefaultOptions> options)
        {
            ServiceProvider = serviceProvider;
            _defaultOptions = options.Value;

            _databaseApis = new Dictionary<string, IDatabaseApi>();
            _transactionApis = new Dictionary<string, ITransactionApi>();

            Items = new Dictionary<string, object>();
        }

        public virtual void Initialize([NotNull] BranchUnitOfWorkOptions options)
        {
            Check.NotNull(options, nameof(options));

            if (Options != null)
            {
                throw new BranchException("This unit of work is already initialized before!");
            }

            Options = _defaultOptions.Normalize(options.Clone());
            IsReserved = false;
        }

        public virtual void Reserve([NotNull] string reservationName)
        {
            Check.NotNull(reservationName, nameof(reservationName));

            ReservationName = reservationName;
            IsReserved = true;
        }

        public virtual void SetOuter(IUnitOfWork? outer)
        {
            Outer = outer;
        }

        public virtual async ValueTask SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var databaseApi in GetAllActiveDatabaseApis())
            {
                if (databaseApi is ISupportsSavingChanges changes)
                {
                    await changes.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public IReadOnlyList<IDatabaseApi> GetAllActiveDatabaseApis()
        {
            return _databaseApis.Values.ToImmutableList();
        }

        public IReadOnlyList<ITransactionApi> GetAllActiveTransactionApis()
        {
            return _transactionApis.Values.ToImmutableList();
        }

        public virtual async ValueTask CompleteAsync(CancellationToken cancellationToken = default)
        {
            if (_isRolledback)
            {
                return;
            }

            PreventMultipleComplete();

            try
            {
                _isCompleting = true;
                await SaveChangesAsync(cancellationToken);
                await CommitTransactionsAsync();
                IsCompleted = true;
                await OnCompletedAsync();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        public virtual async ValueTask RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_isRolledback)
            {
                return;
            }

            _isRolledback = true;

            await RollbackAllAsync(cancellationToken);
        }

        public IDatabaseApi? FindDatabaseApi([NotNull] string key)
        {
            return _databaseApis.GetOrDefault(key);
        }

        public void AddDatabaseApi([NotNull] string key, [NotNull] IDatabaseApi api)
        {
            Check.NotNull(key, nameof(key));
            Check.NotNull(api, nameof(api));

            if (_databaseApis.ContainsKey(key))
            {
                throw new BranchException("There is already a database API in this unit of work with given key: " + key);
            }

            _databaseApis.Add(key, api);
        }

        public IDatabaseApi GetOrAddDatabaseApi([NotNull] string key, [NotNull] Func<IDatabaseApi> factory)
        {
            Check.NotNull(key, nameof(key));
            Check.NotNull(factory, nameof(factory));

            return _databaseApis.GetOrAdd(key, factory);
        }

        public ITransactionApi? FindTransactionApi([NotNull] string key)
        {
            Check.NotNull(key, nameof(key));

            return _transactionApis.GetOrDefault(key);
        }

        public void AddTransactionApi([NotNull] string key, [NotNull] ITransactionApi api)
        {
            Check.NotNull(key, nameof(key));
            Check.NotNull(api, nameof(api));

            if (_transactionApis.ContainsKey(key))
            {
                throw new BranchException("There is already a transaction API in this unit of work with given key: " + key);
            }

            _transactionApis.Add(key, api);
        }

        public ITransactionApi GetOrAddTransactionApi([NotNull] string key, [NotNull] Func<ITransactionApi> factory)
        {
            Check.NotNull(key, nameof(key));
            Check.NotNull(factory, nameof(factory));

            return _transactionApis.GetOrAdd(key, factory);
        }

        public void OnCompleted(Func<ValueTask> handler)
        {
            CompletedHandlers.Add(handler);
        }

        protected virtual async ValueTask OnCompletedAsync()
        {
            foreach (var handler in CompletedHandlers)
            {
                await handler.Invoke();
            }
        }

        protected virtual void OnFailed()
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(this, _exception, _isRolledback));
        }

        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this, new UnitOfWorkEventArgs(this));
        }

        public virtual async ValueTask DisposeAsync()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            await DisposeTransactionsAsync();

            if (!IsCompleted || _exception != null)
            {
                OnFailed();
            }

            OnDisposed();
        }

        private async ValueTask DisposeTransactionsAsync()
        {
            foreach (var transactionApi in GetAllActiveTransactionApis())
            {
                try
                {
                    await transactionApi.DisposeAsync();
                }
                catch
                {
                }
            }
        }

        private void PreventMultipleComplete()
        {
            if (IsCompleted || _isCompleting)
            {
                throw new BranchException("Complete is called before!");
            }
        }

        protected virtual void RollbackAll()
        {
            foreach (var databaseApi in GetAllActiveDatabaseApis())
            {
                try
                {
                    (databaseApi as ISupportsRollback)?.Rollback();
                }
                catch { }
            }

            foreach (var transactionApi in GetAllActiveTransactionApis())
            {
                try
                {
                    (transactionApi as ISupportsRollback)?.Rollback();
                }
                catch { }
            }
        }

        protected virtual async ValueTask RollbackAllAsync(CancellationToken cancellationToken)
        {
            foreach (var databaseApi in GetAllActiveDatabaseApis())
            {
                if (databaseApi is ISupportsRollback rollback)
                {
                    try
                    {
                        await rollback.RollbackAsync(cancellationToken);
                    }
                    catch { }
                }
            }

            foreach (var transactionApi in GetAllActiveTransactionApis())
            {
                if (transactionApi is ISupportsRollback rollback)
                {
                    try
                    {
                        await rollback.RollbackAsync(cancellationToken);
                    }
                    catch { }
                }
            }
        }

        protected virtual async ValueTask CommitTransactionsAsync()
        {
            foreach (var transaction in GetAllActiveTransactionApis())
            {
                await transaction.CommitAsync();
            }
        }

        public override string ToString()
        {
            return $"[UnitOfWork {Id}]";
        }
    }
}
