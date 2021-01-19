// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Extensions.Logging;

namespace Maple.Branch.Logging
{
    public interface IHasLogLevel
    {
        /// <summary>
        /// Log severity.
        /// </summary>
        LogLevel LogLevel { get; }
    }
}
