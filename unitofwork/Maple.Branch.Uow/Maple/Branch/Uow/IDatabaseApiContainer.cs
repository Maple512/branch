// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Maple.Branch.Uow
{
    public interface IDatabaseApiContainer
    {
        IServiceProvider ServiceProvider { get; }

        IDatabaseApi? FindDatabaseApi([NotNull] string key);

        void AddDatabaseApi([NotNull] string key, [NotNull] IDatabaseApi api);

        IDatabaseApi GetOrAddDatabaseApi([NotNull] string key, [NotNull] Func<IDatabaseApi> factory);
    }
}
