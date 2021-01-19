// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using MapleClub.Utility;

namespace Maple.Branch.Uow
{
    public class UnitOfWorkEventArgs : EventArgs
    {
        /// <summary>
        /// Reference to the unit of work related to this event.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        public UnitOfWorkEventArgs([NotNull] IUnitOfWork unitOfWork)
        {
            UnitOfWork = Check.NotNull(unitOfWork, nameof(unitOfWork));
        }
    }
}
