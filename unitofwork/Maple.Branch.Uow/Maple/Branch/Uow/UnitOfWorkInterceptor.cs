// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using Maple.Branch.DependencyInjection;
using Maple.Branch.DynamicProxy;
using Microsoft.Extensions.Options;

namespace Maple.Branch.Uow
{
    public class UnitOfWorkInterceptor : BranchInterceptor, ITransientObject
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUnitOfWorkTransactionBehaviourProvider _transactionBehaviourProvider;
        private readonly BranchUnitOfWorkDefaultOptions _defaultOptions;

        public UnitOfWorkInterceptor(
            IUnitOfWorkManager unitOfWorkManager,
            IOptions<BranchUnitOfWorkDefaultOptions> options,
            IUnitOfWorkTransactionBehaviourProvider transactionBehaviourProvider)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _transactionBehaviourProvider = transactionBehaviourProvider;
            _defaultOptions = options.Value;
        }

        public override async ValueTask InterceptAsync(IMethodInvocation invocation)
        {
            if (!UnitOfWorkHelper.IsUnitOfWorkMethod(invocation.Method, out var unitOfWorkAttribute))
            {
                await invocation.ProceedAsync();
                return;
            }

            var options = CreateOptions(invocation, unitOfWorkAttribute);

            //Trying to begin a reserved UOW by AbpUnitOfWorkMiddleware
            if (_unitOfWorkManager.TryBeginReserved(UnitOfWork.UnitOfWorkReservationName, options))
            {
                await invocation.ProceedAsync();
                return;
            }

            await using var uow = _unitOfWorkManager.Begin(options);

            await invocation.ProceedAsync();

            await uow.CompleteAsync();
        }

        private BranchUnitOfWorkOptions CreateOptions(IMethodInvocation invocation, UnitOfWorkAttribute? unitOfWorkAttribute)
        {
            var options = new BranchUnitOfWorkOptions();

            unitOfWorkAttribute?.SetOptions(options);

            if (unitOfWorkAttribute?.IsTransactional == null)
            {
                options.IsTransactional = _defaultOptions.CalculateIsTransactional(
                    autoValue: _transactionBehaviourProvider.IsTransactional
                               ?? !invocation.Method.Name.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase)
                );
            }

            return options;
        }
    }
}
