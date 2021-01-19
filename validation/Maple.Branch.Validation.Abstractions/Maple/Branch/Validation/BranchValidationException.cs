// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Maple.Branch.Logging;
using Microsoft.Extensions.Logging;

namespace Maple.Branch.Validation
{
    /// <summary>
    /// 验证异常
    /// </summary>
    public class BranchValidationException : BranchException, IHasValidationErrors, IHasLogLevel
    {
        public IList<ValidationResult> ValidationErrors { get; } = new List<ValidationResult>();

        public LogLevel LogLevel { get; set; } = LogLevel.Warning;

        public BranchValidationException(string message)
            : base(message)
        {
        }

        public BranchValidationException(IList<ValidationResult> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public BranchValidationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public BranchValidationException(string message, IList<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }

        public BranchValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
