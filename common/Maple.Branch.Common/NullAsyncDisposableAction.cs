// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Maple.Branch
{
    public struct NullAsyncDisposableAction : IAsyncDisposable
    {
        public static NullAsyncDisposableAction Instance { get; } = new NullAsyncDisposableAction();

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}
