// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Maple.Branch.Localization
{
    public interface ILanguageInfo
    {
        string CultureName { get; }

        string UiCultureName { get; }

        string DisplayName { get; }

        string? FlagIcon { get; }
    }
}
