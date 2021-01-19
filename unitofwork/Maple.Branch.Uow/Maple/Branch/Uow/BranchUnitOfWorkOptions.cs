// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Data;

namespace Maple.Branch.Uow
{
    public class BranchUnitOfWorkOptions : IBranchUnitOfWorkOptions
    {
        /// <summary>
        /// Default: false.
        /// </summary>
        public bool IsTransactional { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        public BranchUnitOfWorkOptions()
        {

        }

        public BranchUnitOfWorkOptions(
            bool isTransactional = false,
            IsolationLevel? isolationLevel = null,
            TimeSpan? timeout = null)
        {
            IsTransactional = isTransactional;
            IsolationLevel = isolationLevel;
            Timeout = timeout;
        }

        public BranchUnitOfWorkOptions Clone()
        {
            return new BranchUnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout
            };
        }
    }
}
