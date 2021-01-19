// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Extensions.Localization;

namespace Maple.Branch.Localization
{
    public interface IBranchStringLocalizerFactoryWithDefaultResourceSupport
    {
        IStringLocalizer? CreateDefaultOrNull();
    }
}
