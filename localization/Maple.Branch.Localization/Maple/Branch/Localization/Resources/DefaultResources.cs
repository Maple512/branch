// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Maple.Branch.Localization.Defaults;

namespace Maple.Branch.Localization.Resources
{
    public class DefaultResources : DefaultLocalizationResource
    {
        public override IEnumerable<KeyCollection> DifineTranslations()
        {
            yield return new KeyCollection("DisplayName:Abp.Localization.DefaultLanguage")
            {
                "Default language",
                "默认语言"
            };

            yield return new KeyCollection("Description:Abp.Localization.DefaultLanguage")
            {
                "The default language of the application.",
                "应用程序的默认语言"
            };
        }

        public override IEnumerable<string> SupportCultureNames()
        {
            yield return "en";

            yield return "en";
        }
    }
}
