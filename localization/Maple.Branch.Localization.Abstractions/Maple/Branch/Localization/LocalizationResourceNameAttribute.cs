// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;

namespace Maple.Branch.Localization
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LocalizationResourceNameAttribute : Attribute
    {
        public string Name { get; }

        public LocalizationResourceNameAttribute(string name)
        {
            Name = name;
        }

        public static LocalizationResourceNameAttribute? GetOrNull(Type resourceType)
        {
            return resourceType
                .GetCustomAttributes(true)
                .OfType<LocalizationResourceNameAttribute>()
                .FirstOrDefault();
        }

        public static string GetName(Type resourceType)
        {
            return GetOrNull(resourceType)?.Name ?? resourceType.FullName!;
        }
    }
}
