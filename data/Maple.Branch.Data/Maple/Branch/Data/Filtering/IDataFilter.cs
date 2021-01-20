// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maple.Branch.Data.Filtering
{
    public interface IDataFilter<TFilter>
        where TFilter : class
    {
        IAsyncDisposable Enable();

        IAsyncDisposable Disable();

        bool IsEnabled { get; }
    }

    public interface IDataFilter
    {
        IAsyncDisposable Enable<TFilter>() where TFilter : class;

        IAsyncDisposable Disable<TFilter>() where TFilter : class;

        bool IsEnabled<TFilter>() where TFilter : class;
    }
}
