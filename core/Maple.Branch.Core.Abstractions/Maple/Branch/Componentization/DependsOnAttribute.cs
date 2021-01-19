// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Maple.Branch.Componentization
{
    /// <summary>
    /// 组件的依赖描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// 组件的依赖描述
        /// </summary>
        /// <param name="dependedTypes">该组件依赖的其它组件</param>
        public DependsOnAttribute(params Type[] dependedTypes)
        {
            var notComponents = dependedTypes.Where(m => m.NotBranchComponent());
            if (notComponents.Any())
            {
                throw new NotSupportedException($"None of these types are {nameof(IBranchComponent)}: {notComponents.Select(m => m.Name).JoinAsString(", ")}");
            }

            DepenededTypes = dependedTypes;
        }

        public Type[] DepenededTypes { get; }
    }
}
