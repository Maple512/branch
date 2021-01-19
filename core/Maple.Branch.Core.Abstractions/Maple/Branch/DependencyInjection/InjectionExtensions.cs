// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Maple.Branch.DependencyInjection
{
    public static class InjectionExtensions
    {
        public static bool IsInjectionType(this Type type)
        {
            return type.IsAssignableToBaseTypes(
                typeof(ITransientObject),
                typeof(ISingletonObject),
                typeof(IScopedObject)
                )
                || type.IsDefined<InjectionAttribute>();
        }
    }
}
