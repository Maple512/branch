// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Maple.Branch
{
    [Serializable]
    public class BranchException : Exception
    {
        public BranchException()
        {

        }

        public BranchException(string? message)
            : base(message)
        {

        }

        public BranchException(string? message, Exception? innerException)
            : base(message, innerException)
        {

        }

        public BranchException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
