// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Maple.Branch.Uow
{
    public interface ITransactionApi : IAsyncDisposable
    {
        ValueTask CommitAsync();
    }
}
