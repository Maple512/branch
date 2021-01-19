// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MapleClub.Utility;

namespace Maple.Branch.Localization
{
    public class LocalizationResource
    {
        [NotNull]
        public Type ResourceType { get; }

        [NotNull]
        public string ResourceName => LocalizationResourceNameAttribute.GetName(ResourceType);

        public string? DefaultCultureName { get; set; }

        [NotNull]
        public LocalizationResourceContributorList Contributors { get; }

        [NotNull]
        public List<Type> BaseResourceTypes { get; }

        public LocalizationResource(
            [NotNull] Type resourceType,
             string? defaultCultureName = null,
             ILocalizationResourceContributor? initialContributor = null)
        {
            ResourceType = Check.NotNull(resourceType, nameof(resourceType));
            DefaultCultureName = defaultCultureName;

            BaseResourceTypes = new List<Type>();
            Contributors = new LocalizationResourceContributorList();

            if (initialContributor != null)
            {
                Contributors.Add(initialContributor);
            }

            AddBaseResourceTypes();
        }

        protected virtual void AddBaseResourceTypes()
        {
            var descriptors = ResourceType
                .GetCustomAttributes(true)
                .OfType<IInheritedResourceTypesProvider>();

            foreach (var descriptor in descriptors)
            {
                foreach (var baseResourceType in descriptor.GetInheritedResourceTypes())
                {
                    BaseResourceTypes.AddIfNotContains(baseResourceType);
                }
            }
        }
    }
}
