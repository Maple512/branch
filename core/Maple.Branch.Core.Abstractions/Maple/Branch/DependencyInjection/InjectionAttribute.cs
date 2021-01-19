// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.DependencyInjection
{
    /// <summary>
    /// 用于描述对象的注入行为
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectionAttribute : Attribute
    {
        public InjectionAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        /// <inheritdoc cref="ServiceLifetime"/>
        public ServiceLifetime Lifetime { get; }

        /// <summary>
        /// 尝试在DI容器中注册
        /// </summary>
        public bool TryRegister { get; }

        /// <summary>
        /// 替换DI容器中第一个相同的对象
        /// </summary>
        public bool ReplaceService { get; }
    }
}
