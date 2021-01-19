// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Maple.Branch.Localization
{
    public static class BranchLocalizationOptionsExtensions
    {
        public static BranchLocalizationOptions AddLanguagesMapOrUpdate(this BranchLocalizationOptions localizationOptions,
            string packageName, params KeyValue[] maps)
        {
            foreach (var map in maps)
            {
                AddOrUpdate(localizationOptions.LanguagesMap, packageName, map);
            }

            return localizationOptions;
        }

        public static string GetLanguagesMap(this BranchLocalizationOptions localizationOptions, string packageName,
            string language)
        {
            return localizationOptions.LanguagesMap.TryGetValue(packageName, out var maps)
                ? maps.FirstOrDefault(x => x.Key == language)?.Value ?? language
                : language;
        }

        public static string GetCurrentUICultureLanguagesMap(this BranchLocalizationOptions localizationOptions, string packageName)
        {
            return GetLanguagesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
        }

        public static BranchLocalizationOptions AddLanguageFilesMapOrUpdate(this BranchLocalizationOptions localizationOptions,
            string packageName, params KeyValue[] maps)
        {
            foreach (var map in maps)
            {
                AddOrUpdate(localizationOptions.LanguageFilesMap, packageName, map);
            }

            return localizationOptions;
        }

        public static string GetLanguageFilesMap(this BranchLocalizationOptions localizationOptions, string packageName,
            string language)
        {
            return localizationOptions.LanguageFilesMap.TryGetValue(packageName, out var maps)
                ? maps.FirstOrDefault(x => x.Key == language)?.Value ?? language
                : language;
        }

        public static string GetCurrentUICultureLanguageFilesMap(this BranchLocalizationOptions localizationOptions, string packageName)
        {
            return GetLanguageFilesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
        }

        private static void AddOrUpdate(IDictionary<string, List<KeyValue>> maps, string packageName, KeyValue value)
        {
            if (maps.TryGetValue(packageName, out var existMaps))
            {
                existMaps.GetOrAdd(x => x.Key == value.Key, () => value).Value = value.Value;
            }
            else
            {
                maps.Add(packageName, new List<KeyValue> { value });
            }
        }
    }
}
