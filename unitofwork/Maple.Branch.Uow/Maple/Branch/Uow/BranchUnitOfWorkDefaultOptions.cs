// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Data;

namespace Maple.Branch.Uow
{
    public class BranchUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Default value: <see cref="UnitOfWorkTransactionBehavior.Auto"/>.
        /// </summary>
        public UnitOfWorkTransactionBehavior TransactionBehavior { get; set; } = UnitOfWorkTransactionBehavior.Auto;

        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        internal BranchUnitOfWorkOptions Normalize(BranchUnitOfWorkOptions options)
        {
            if (options.IsolationLevel == null)
            {
                options.IsolationLevel = IsolationLevel;
            }

            if (options.Timeout == null)
            {
                options.Timeout = Timeout;
            }

            return options;
        }

        public bool CalculateIsTransactional(bool autoValue)
        {
            return TransactionBehavior switch
            {
                UnitOfWorkTransactionBehavior.Enabled => true,
                UnitOfWorkTransactionBehavior.Disabled => false,
                UnitOfWorkTransactionBehavior.Auto => autoValue,
                _ => throw new NotImplementedException("Not implemented TransactionBehavior value: " + TransactionBehavior),
            };
        }
    }
}
