// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace System
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToBaseTypes(this Type type, params Type[] types)
        {
            foreach (var item in types)
            {
                if (type.IsAssignableTo(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsDefined<T>(this Type type)
        {
            return type.IsDefined(typeof(T));
        }
    }
}
