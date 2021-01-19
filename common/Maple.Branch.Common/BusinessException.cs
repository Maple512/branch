// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;
using Maple.Branch.Logging;
using Microsoft.Extensions.Logging;

namespace Maple.Branch
{
    /// <summary>
    /// 业务异常
    /// </summary>
    /// <remarks>传递经过处理的消息</remarks>
    [Serializable]
    public class BusinessException : BranchException,
        IBusinessException,
        IHasLogLevel
    {
        /// <inheritdoc cref="Microsoft.Extensions.Logging.LogLevel"/>
        public LogLevel LogLevel { get; set; }

        public BusinessException(
            string? message = null,
            Exception? innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, innerException)
        {
            LogLevel = logLevel;
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public BusinessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public BusinessException WithData(string name, object value)
        {
            Data[name] = value;

            return this;
        }
    }
}
