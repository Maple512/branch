// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading;
using Maple.Branch.DependencyInjection;

namespace Maple.Branch.Uow
{
    public class AmbientUnitOfWork : IAmbientUnitOfWork, ISingletonObject
    {
        public IUnitOfWork? UnitOfWork => _currentUow.Value;

        private readonly AsyncLocal<IUnitOfWork?> _currentUow;

        public AmbientUnitOfWork()
        {
            _currentUow = new AsyncLocal<IUnitOfWork?>();
        }

        public void SetUnitOfWork(IUnitOfWork? unitOfWork)
        {
            _currentUow.Value = unitOfWork;
        }
    }
}
