// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MapleClub.Utility;

namespace Maple.Branch.EventBus
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventNameAttribute : Attribute
    {
        public EventNameAttribute([NotNull] string name)
        {
            Name = Check.NotNullOrEmpty(name, nameof(name));
        }

        public string Name { get; }

        public static string? GetName([NotNull] Type type)
        {
            Check.NotNull(type, nameof(type));

            return type.GetCustomAttribute<EventNameAttribute>()?.Name;
        }
    }
}
