// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MapleClub.Utility;

namespace Maple.Branch.Componentization
{
    public static class ComponentLoadHelper
    {
        public static IEnumerable<IComponentDescriptor> LoadBranchComponents<TStartupComponent>()
            where TStartupComponent : IBranchComponent
        {
            var type = typeof(TStartupComponent);

            var components = GetDescriptors(type);

            return SortByDependency(components, type);
        }

        public static IEnumerable<IComponentDescriptor> LoadBranchComponents([NotNull] Type startComponent)
        {
            Check.NotNull(startComponent, nameof(startComponent));

            var components = GetDescriptors(startComponent);

            return SortByDependency(components, startComponent);
        }

        private static IEnumerable<IComponentDescriptor> GetDescriptors(Type startupComponent)
        {
            var descriptors = new List<ComponentDescriptor>();

            // All components starting from the startup component
            foreach (var componentType in ComponentHelper.FindAllComponentTypes(startupComponent))
            {
                var component = (IBranchComponent)Activator.CreateInstance(componentType)!;

                var descriptor = new ComponentDescriptor(component);

                descriptors.Add(descriptor);
            }

            foreach (var descriptor in descriptors)
            {
                foreach (var dependeComponentType in ComponentHelper.GetAllDependedComponentTypes(descriptor.Type))
                {
                    var dependedComponent = descriptors.First(m => m.Type == dependeComponentType);

                    descriptor.AddDependency(dependedComponent);
                }
            }

            return descriptors.Cast<IComponentDescriptor>();
        }

        private static IEnumerable<IComponentDescriptor> SortByDependency(
            IEnumerable<IComponentDescriptor> components,
            Type startComponent)
        {
            var sortedModules = components.SortByDependencies(m => m.Dependencies);

            sortedModules.MoveItem(m => m.Type == startComponent, components.Count() - 1);

            return sortedModules;
        }
    }
}
