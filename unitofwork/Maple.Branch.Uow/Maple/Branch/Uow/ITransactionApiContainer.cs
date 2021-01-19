// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Maple.Branch.Uow
{
    public interface ITransactionApiContainer
    {
        ITransactionApi? FindTransactionApi([NotNull] string key);

        void AddTransactionApi([NotNull] string key, [NotNull] ITransactionApi api);

        ITransactionApi GetOrAddTransactionApi([NotNull] string key, [NotNull] Func<ITransactionApi> factory);
    }
}
