// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using MapleClub.Utility;
using Microsoft.Extensions.Logging;

namespace Maple.Branch
{
    /// <summary>
    /// 用户友好异常
    /// </summary>
    /// <remarks>直接将异常消息传递出去（支持本地化翻译）</remarks>
    [Serializable]
    public class UserFriendlyException : BusinessException, IUserFriendlyException
    {
        public string? LocalizedKey { get; private set; }

        public UserFriendlyException(
            string? message = null,
            Exception? innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(
                  message,
                  innerException,
                  logLevel)
        {
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// 应用本地化
        /// </summary>
        /// <remarks>使用本方法，以使用异常本地化，<see cref="Exception.Message"/>将被本地化取代</remarks>
        /// <param name="localizeKey">本地化文本-键</param>
        /// <returns></returns>
        public UserFriendlyException WithLocalized([NotNull] string localizeKey)
        {
            LocalizedKey = Check.NotNullOrWhiteSpace(localizeKey, nameof(localizeKey));

            return this;
        }
    }
}
