// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Maple.Branch.Localization
{
    public static class LanguageInfoExtensions
    {
        public static T? FindByCulture<T>(
            [NotNull] this IEnumerable<T> languages,
            [NotNull] string cultureName,
             string? uiCultureName = null)
            where T : class, ILanguageInfo
        {
            if (uiCultureName == null)
            {
                uiCultureName = cultureName;
            }

            var languageList = languages.ToList();

            return languageList.FirstOrDefault(l => l.CultureName == cultureName && l.UiCultureName == uiCultureName)
                   ?? languageList.FirstOrDefault(l => l.CultureName == cultureName)
                   ?? languageList.FirstOrDefault(l => l.UiCultureName == uiCultureName);
        }
    }
}
