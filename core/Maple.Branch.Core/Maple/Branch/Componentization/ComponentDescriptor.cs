// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Maple.Branch.Componentization
{
    public class ComponentDescriptor : IComponentDescriptor
    {
        public ComponentDescriptor(IBranchComponent instance)
        {
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
            Dependencies = new List<IComponentDescriptor>();
        }

        public Type Type { get; }

        public Assembly Assembly { get; }

        public IBranchComponent Instance { get; }

        public ICollection<IComponentDescriptor> Dependencies { get; }

        public void AddDependency(IComponentDescriptor descriptor)
        {
            Dependencies.AddIfNotContains(descriptor);
        }
    }
}
