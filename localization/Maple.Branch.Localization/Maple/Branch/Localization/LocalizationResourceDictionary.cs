// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Maple.Branch.Localization
{
    public class LocalizationResourceDictionary : Dictionary<Type, LocalizationResource>
    {
        public LocalizationResource Add<TResouce>(string? defaultCultureName = null)
        {
            return Add(typeof(TResouce), defaultCultureName);
        }

        public LocalizationResource Add(Type resourceType, string? defaultCultureName = null)
        {
            if (ContainsKey(resourceType))
            {
                throw new BranchException("This resource is already added before: " + resourceType.AssemblyQualifiedName);
            }

            return this[resourceType] = new LocalizationResource(resourceType, defaultCultureName);
        }

        public LocalizationResource Get<TResource>()
        {
            var resourceType = typeof(TResource);

            var resource = this.GetOrDefault(resourceType);
            if (resource == null)
            {
                throw new BranchException("Can not find a resource with given type: " + resourceType.AssemblyQualifiedName);
            }

            return resource;
        }
    }
}
