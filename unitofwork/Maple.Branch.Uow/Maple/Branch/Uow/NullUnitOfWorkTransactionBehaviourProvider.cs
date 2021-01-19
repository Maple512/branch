// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.DependencyInjection;

namespace Maple.Branch.Uow
{
    public class NullUnitOfWorkTransactionBehaviourProvider : IUnitOfWorkTransactionBehaviourProvider, ISingletonObject
    {
        public bool? IsTransactional => null;
    }
}
