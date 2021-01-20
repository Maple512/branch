// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Maple.Branch.Data.ConnectionStrings
{
    public class ConnectionStringDictionary : Dictionary<string, string?>
    {
        public const string ConnectionStringNameDefault = "Default";

        public string? Default
        {
            get => this.GetValueOrDefault(ConnectionStringNameDefault);
            set => this[ConnectionStringNameDefault] = value;
        }
    }
}
