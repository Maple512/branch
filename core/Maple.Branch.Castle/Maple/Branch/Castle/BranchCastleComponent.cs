// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Castle.DynamicProxy;
using Maple.Branch.Componentization;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.Castle
{
    public class BranchCastleComponent : BranchComponent
    {
        public override void OnConfigureServices(ConfigureSerivcesContext context)
        {
            context.Services.AddTransient(typeof(BranchAsyncDeterminationInterceptor<>));
        }
    }
}
