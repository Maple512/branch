// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Localization;

namespace Maple.Branch
{
    /// <summary>
    /// 用户友好异常
    /// </summary>
    public interface IUserFriendlyException : IBusinessException, IHasLocalizedKey
    {
    }
}
