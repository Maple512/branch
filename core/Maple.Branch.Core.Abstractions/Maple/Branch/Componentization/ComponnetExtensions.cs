// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Maple.Branch.Componentization
{
    public static class ComponnetExtensions
    {
        public static bool IsBranchComponent(this Type type)
        {
            return type.IsAssignableTo(typeof(IBranchComponent));
        }

        public static bool NotBranchComponent(this Type type)
        {
            return !type.IsAssignableTo(typeof(IBranchComponent));
        }
    }
}
