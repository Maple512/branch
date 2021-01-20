// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Maple.Branch
{
    /// <inheritdoc cref="IBriefException"/>
    [Serializable]
    public class BriefException : BranchException,
        IBriefException
    {
        public BriefException(
            string? message = null,
            Exception? innerException = null)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public BriefException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
