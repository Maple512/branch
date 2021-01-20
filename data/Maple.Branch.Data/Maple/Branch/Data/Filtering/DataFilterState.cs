// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maple.Branch.Data.Filtering
{
    public class DataFilterState
    {
        public DataFilterState(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public bool IsEnabled { get; set; }

        public DataFilterState Clone()
        {
            return new(IsEnabled);
        }
    }
}
