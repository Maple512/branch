// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using Maple.Branch.Collections;

namespace Maple.Branch.Localization
{
    public class BranchLocalizationOptions
    {
        public LocalizationResourceDictionary Resources { get; }

        /// <summary>
        /// Used as the default resource when resource was not specified on a localization operation.
        /// </summary>
        public Type? DefaultResourceType { get; set; }

        public ITypeList<ILocalizationResourceContributor> GlobalContributors { get; }

        public List<LanguageInfo> Languages { get; }

        public Dictionary<string, List<KeyValue>> LanguagesMap { get; }

        public Dictionary<string, List<KeyValue>> LanguageFilesMap { get; }

        public BranchLocalizationOptions()
        {
            Resources = new LocalizationResourceDictionary();
            GlobalContributors = new TypeList<ILocalizationResourceContributor>();
            Languages = new List<LanguageInfo>();
            LanguagesMap = new Dictionary<string, List<KeyValue>>();
            LanguageFilesMap = new Dictionary<string, List<KeyValue>>();
        }
    }
}
