// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Maple.Branch.Localization
{
    public interface ILocalizationDictionary
    {
        string CultureName { get; }

        LocalizedString? GetOrNull(string name);

        void Fill(Dictionary<string, LocalizedString> dictionary);
    }
}
