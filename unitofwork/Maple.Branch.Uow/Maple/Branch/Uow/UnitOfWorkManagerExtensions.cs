// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using MapleClub.Utility;

namespace Maple.Branch.Uow
{
    public static class UnitOfWorkManagerExtensions
    {
        public static IUnitOfWork Begin(
            [NotNull] this IUnitOfWorkManager unitOfWorkManager,
            bool requiresNew = false,
            bool isTransactional = false,
            IsolationLevel? isolationLevel = null,
            TimeSpan? timeout = null)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));

            return unitOfWorkManager.Begin(new BranchUnitOfWorkOptions
            {
                IsTransactional = isTransactional,
                IsolationLevel = isolationLevel,
                Timeout = timeout
            }, requiresNew);
        }

        public static void BeginReserved([NotNull] this IUnitOfWorkManager unitOfWorkManager, [NotNull] string reservationName)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));
            Check.NotNull(reservationName, nameof(reservationName));

            unitOfWorkManager.BeginReserved(reservationName, new BranchUnitOfWorkOptions());
        }

        public static void TryBeginReserved([NotNull] this IUnitOfWorkManager unitOfWorkManager, [NotNull] string reservationName)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));
            Check.NotNull(reservationName, nameof(reservationName));

            unitOfWorkManager.TryBeginReserved(reservationName, new BranchUnitOfWorkOptions());
        }
    }
}
