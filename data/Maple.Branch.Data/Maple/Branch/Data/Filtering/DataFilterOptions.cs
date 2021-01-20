// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Maple.Branch.Data.Filtering
{
    public class DataFilterOptions
    {
        public Dictionary<Type, DataFilterState> States { get; set; } = new();
    }
}
