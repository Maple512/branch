// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Maple.Branch.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Maple.Branch.Data.Filtering
{
    public class DataFilter : IDataFilter, ISingletonObject
    {
        private readonly ConcurrentDictionary<Type, object> _filters;
        private readonly IServiceProvider _serviceProvider;

        public DataFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _filters = new();
        }

        public IAsyncDisposable Disable<TFilter>() where TFilter : class
        {
            return GetFilter<TFilter>().Disable();
        }

        public IAsyncDisposable Enable<TFilter>() where TFilter : class
        {
            return GetFilter<TFilter>().Enable();
        }

        public bool IsEnabled<TFilter>() where TFilter : class
        {
            return GetFilter<TFilter>().IsEnabled;
        }

        private IDataFilter<TFilter> GetFilter<TFilter>() where TFilter : class
        {
            return (IDataFilter<TFilter>)_filters.GetOrAdd(typeof(TFilter),
                () => _serviceProvider.GetRequiredService<IDataFilter<TFilter>>());
        }
    }

    public class DataFilter<TFilter> : IDataFilter<TFilter>
        where TFilter : class
    {
        public bool IsEnabled
        {
            get
            {
                EnsureInitialized();
                return _filter.Value!.IsEnabled;
            }
        }

        private readonly DataFilterOptions _options;

        private readonly AsyncLocal<DataFilterState> _filter;

        public DataFilter(IOptions<DataFilterOptions> options)
        {
            _options = options.Value;
            _filter = new AsyncLocal<DataFilterState>();
        }

        public IAsyncDisposable Enable()
        {
            if (IsEnabled)
            {
                return NullAsyncDisposableAction.Instance;
            }

            _filter.Value!.IsEnabled = true;

            return new AsyncDisposableAction(() => Disable());
        }

        public IAsyncDisposable Disable()
        {
            if (!IsEnabled)
            {
                return NullAsyncDisposableAction.Instance;
            }

            _filter.Value!.IsEnabled = false;

            return new AsyncDisposableAction(() => Enable());
        }

        private void EnsureInitialized()
        {
            if (_filter.Value != null)
            {
                return;
            }

            _filter.Value = _options.States.GetOrDefault(typeof(TFilter))?.Clone()
                ?? new DataFilterState(true);
        }
    }
}
