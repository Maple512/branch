// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using MapleClub.Utility;

namespace Maple.Branch.Localization.Defaults
{
    public class KeyCollection : Collection<string>, ICollection<string>
    {
        public KeyCollection([NotNull] string key)
        {
            Key = Check.NotNull(key, nameof(key));
        }

        public string Key { get; }
    }
}
