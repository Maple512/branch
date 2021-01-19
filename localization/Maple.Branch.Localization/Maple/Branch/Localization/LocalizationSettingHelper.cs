// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using MapleClub.Utility;

namespace Maple.Branch.Localization
{
    // TODO: 功能跨界
    public static class LocalizationSettingHelper
    {
        /// <summary>
        /// Gets a setting value like "en-US;en" and returns as splitted values like ("en-US", "en").
        /// </summary>
        /// <param name="settingValue"></param>
        /// <returns></returns>
        public static (string cultureName, string uiCultureName) ParseLanguageSetting([NotNull] string settingValue)
        {
            Check.NotNull(settingValue, nameof(settingValue));

            if (!settingValue.Contains(";"))
            {
                return (settingValue, settingValue);
            }

            var splitted = settingValue.Split(';');

            return (splitted[0], splitted[1]);
        }
    }
}
