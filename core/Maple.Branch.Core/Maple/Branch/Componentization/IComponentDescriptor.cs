// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Maple.Branch.Componentization
{
    public interface IComponentDescriptor
    {
        Type Type { get; }

        Assembly Assembly { get; }

        IBranchComponent Instance { get; }

        ICollection<IComponentDescriptor> Dependencies { get; }
    }
}
