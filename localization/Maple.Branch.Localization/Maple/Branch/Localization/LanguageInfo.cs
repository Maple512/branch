// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using MapleClub.Utility;

namespace Maple.Branch.Localization
{
    [Serializable]
    public class LanguageInfo : ILanguageInfo
    {
        [NotNull]
        public virtual string CultureName { get; protected set; } = default!;

        [NotNull]
        public virtual string UiCultureName { get; protected set; } = default!;

        [NotNull]
        public virtual string DisplayName { get; protected set; } = default!;

        public virtual string? FlagIcon { get; set; }

        public LanguageInfo(
            string cultureName,
            string? uiCultureName = null,
            string? displayName = null,
            string? flagIcon = null)
        {
            ChangeCultureInternal(cultureName, uiCultureName, displayName);
            FlagIcon = flagIcon;
        }

        public virtual void ChangeCulture(
            string cultureName,
            string? uiCultureName = null,
            string? displayName = null)
        {
            ChangeCultureInternal(cultureName, uiCultureName, displayName);
        }

        private void ChangeCultureInternal(
            [NotNull] string cultureName,
            string? uiCultureName,
            string? displayName)
        {
            CultureName = Check.NotNullOrWhiteSpace(cultureName, nameof(cultureName));

            UiCultureName = uiCultureName.NotNullOrWhiteSpace()
                ? uiCultureName!
                : cultureName;

            DisplayName = !displayName.IsNullOrWhiteSpace()
                ? displayName!
                : cultureName;
        }
    }
}
