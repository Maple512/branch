// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Componentization;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.Uow
{
    public class UnitOfWorkComponent : BranchComponent
    {
        public override void OnConfigureServices(ConfigureSerivcesContext context)
        {
            context.Services.OnRegistred(UnitOfWorkInterceptorRegistrar.RegisterIfNeeded);
        }
    }
}
