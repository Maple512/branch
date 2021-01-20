// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Componentization;
using Maple.Branch.Data.ConnectionStrings;
using Maple.Branch.Data.Filtering;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.Data
{
    public class BranchDataCommponent : BranchComponent
    {
        public override void OnConfigureServices(ConfigureSerivcesContext context)
        {
            var configuretion = context.Services.GetConfiguration();

            Configure<ConnectionStringOptions>(configuretion);

            context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
        }
    }
}
