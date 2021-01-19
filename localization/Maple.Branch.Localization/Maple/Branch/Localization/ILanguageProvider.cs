// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maple.Branch.Localization
{
    public interface ILanguageProvider
    {
        ValueTask<IReadOnlyList<LanguageInfo>> GetLanguagesAsync();
    }
}
