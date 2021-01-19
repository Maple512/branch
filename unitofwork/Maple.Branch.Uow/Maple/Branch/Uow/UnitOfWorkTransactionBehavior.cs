// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Maple.Branch.Uow
{
    /// <summary>
    /// uow事务行为：自动，启用，禁用
    /// </summary>
    public enum UnitOfWorkTransactionBehavior
    {
        /// <summary>
        /// 自动启动/关闭事务
        /// </summary>
        Auto,

        /// <summary>
        /// 启动事务
        /// </summary>
        Enabled,

        /// <summary>
        /// 禁用事务
        /// </summary>
        Disabled
    }
}
