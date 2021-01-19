// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Maple.Branch.Componentization
{
    public static class ComponentHelper
    {
        public static List<Type> FindAllComponentTypes(Type startupModuleType)
        {
            var moduleTypes = new List<Type>();

            FillComponents(moduleTypes, startupModuleType);

            return moduleTypes;
        }

        public static IEnumerable<Type> GetAllDependedComponentTypes(Type componentType)
        {
            return componentType
                .GetCustomAttributes<DependsOnAttribute>()
                .SelectMany(m => m.DepenededTypes)
                .Distinct();
        }

        public static void Initialization(
            IEnumerable<IComponentDescriptor> componentDescriptors,
            IServiceCollection services,
            ILogger? logger)
        {
            var context = new ConfigureSerivcesContext(services);

            foreach (var descriptor in componentDescriptors)
            {
                if (descriptor.Instance is BranchComponent component)
                {
                    component.SetConfigureServicesContext(context);
                }
            }

            // PreConfigureServices
            foreach (var module in componentDescriptors)
            {
                try
                {
                    module.Instance.OnPreConfigureServices(context);

                    logger?.LogInformation($"-- Branch Component: {module.Type.Name}");
                }
                catch (Exception ex)
                {
                    throw new BranchException($"An error occurred during {nameof(IPreConfigureServices.OnPreConfigureServices)} phase of the component {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            // ConfigureServices
            foreach (var descriptor in componentDescriptors)
            {
                if (descriptor.Instance is BranchComponent component)
                {
                    services.AddAssembly(descriptor.Type.Assembly);
                }

                try
                {
                    descriptor.Instance.OnConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new BranchException($"An error occurred during {nameof(IBranchComponent.OnConfigureServices)} phase of the component {descriptor.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            // PostConfigureServices
            foreach (var module in componentDescriptors)
            {
                try
                {
                    module.Instance.OnPostConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new BranchException($"An error occurred during {nameof(IPostConfigureServices.OnPostConfigureServices)} phase of the component {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            // clear ConfigureServicesContext
            foreach (var descriptor in componentDescriptors)
            {
                if (descriptor.Instance is BranchComponent component)
                {
                    component.ClearConfigureServicesContext();
                }
            }
        }

        private static void FillComponents(List<Type> componentTypes, Type componentType)
        {
            if (componentTypes.Contains(componentType))
            {
                return;
            }

            componentTypes.Add(componentType);

            foreach (var dependedModuleType in GetAllDependedComponentTypes(componentType))
            {
                FillComponents(componentTypes, dependedModuleType);
            }
        }
    }
}
