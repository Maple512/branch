// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Componentization;
using Maple.Branch.Localization.Defaults;

namespace Maple.Branch.Localization
{
    public class BranchLocalizationComponent : BranchComponent
    {
        public override void OnConfigureServices(ConfigureSerivcesContext context)
        {
            BranchStringLocalizerFactory.Replace(context.Services);

            Configure<BranchLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DefaultLocalizationResource>("en");
            });
        }
    }
}
