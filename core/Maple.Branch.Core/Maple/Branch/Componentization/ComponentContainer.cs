// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;

namespace Maple.Branch.Componentization
{
    public class ComponentContainer : IComponentContainer
    {
        public ComponentContainer(IEnumerable<IComponentDescriptor> modules)
        {
            Modules = modules.ToImmutableList();
        }

        public IReadOnlyList<IComponentDescriptor> Modules { get; }
    }
}
