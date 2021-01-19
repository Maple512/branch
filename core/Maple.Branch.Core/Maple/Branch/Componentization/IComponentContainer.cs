// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Maple.Branch.Componentization
{
    public interface IComponentContainer
    {
        IReadOnlyList<IComponentDescriptor> Modules { get; }
    }
}
