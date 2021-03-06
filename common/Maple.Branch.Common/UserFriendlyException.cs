// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Maple.Branch
{
    /// <inheritdoc cref="IUserFriendlyException"/>
    [Serializable]
    public class UserFriendlyException : BranchException, IUserFriendlyException
    {
        public UserFriendlyException(
            string? message = null,
            Exception? innerException = null)
            : base(
                  message,
                  innerException)
        {
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
