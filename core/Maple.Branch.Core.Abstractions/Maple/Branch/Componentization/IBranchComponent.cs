// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Maple.Branch.Componentization
{
    public interface IBranchComponent : IPreConfigureServices, IConfigureServices, IPostConfigureServices
    {
        ConfigureSerivcesContext? Context { get; }
    }
}
