// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Maple.Branch.Localization.Defaults
{
    public abstract class DefaultLocalizationResource
    {
        /// <summary>
        /// define all translation text's
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<KeyCollection> DifineTranslations();

        /// <summary>
        /// define support culture name's
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> SupportCultureNames();
    }
}
