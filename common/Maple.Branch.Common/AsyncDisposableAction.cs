// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MapleClub.Utility;

namespace Maple.Branch
{
    public sealed class AsyncDisposableAction : IAsyncDisposable
    {
        private readonly Action _action;

        public AsyncDisposableAction([NotNull] Action action)
        {
            _action = Check.NotNull(action, nameof(action));
        }

        public ValueTask DisposeAsync()
        {
            _action.Invoke();

            return ValueTask.CompletedTask;
        }
    }
}
