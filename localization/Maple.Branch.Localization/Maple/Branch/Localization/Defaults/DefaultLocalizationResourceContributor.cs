// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Maple.Branch.Localization.Defaults
{
    public abstract class DefaultLocalizationResourceContributor : ILocalizationResourceContributor
    {
        private Dictionary<string, ILocalizationDictionary>? _dictionaries;

        protected BranchLocalizationOptions? Options { get; private set; }

        public void Initialize(LocalizationResourceInitializationContext context)
        {
            Options = context.ServiceProvider.GetRequiredService<IOptions<BranchLocalizationOptions>>().Value;
        }

        public LocalizedString? GetOrNull(string cultureName, string name)
        {
            return GetDictionaries().GetOrDefault(cultureName)?.GetOrNull(name);
        }

        public void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary)
        {
            GetDictionaries().GetOrDefault(cultureName)?.Fill(dictionary);
        }

        private Dictionary<string, ILocalizationDictionary> GetDictionaries()
        {
            var dictionaries = _dictionaries;
            if (dictionaries != null)
            {
                return dictionaries;
            }

            dictionaries = _dictionaries = CreateDictionaries();

            return dictionaries;
        }

        private Dictionary<string, ILocalizationDictionary> CreateDictionaries()
        {
            if (Options == null)
            {
                throw new System.ArgumentNullException($"This Options must be not null: {nameof(BranchLocalizationOptions)}");
            }

            var dictionaries = new Dictionary<string, ILocalizationDictionary>();

            var resource = Options.Resources
                .Where(m => m.Key.IsAssignableTo(typeof(DefaultLocalizationResource)))
                .OfType<DefaultLocalizationResource>()
                .FirstOrDefault();

            if (resource == null)
            {
                return new();
            }

            var cultures = resource.SupportCultureNames();

            for (var i = 0; i < cultures.Count(); i++)
            {
                var culture = cultures.ElementAt(i);
                var dictionary = CreateDictionaryFromFileContent(i, culture, resource);

                if (dictionaries.ContainsKey(dictionary.CultureName))
                {
                    throw new BranchException($"{resource.GetType().AssemblyQualifiedName} dictionary has a culture name '{dictionary.CultureName}' which is already defined!");
                }

                dictionaries[culture] = dictionary;
            }

            return dictionaries;
        }

        protected virtual ILocalizationDictionary CreateDictionaryFromFileContent(
            int cultureIndex,
            string cultureName,
            DefaultLocalizationResource resource)
        {
            var translations = resource.DifineTranslations();

            var dictionary = new Dictionary<string, LocalizedString>();
            var dublicateNames = new List<string>();
            foreach (var item in translations)
            {
                if (dictionary.GetOrDefault(item.Key) != null)
                {
                    dublicateNames.Add(item.Key);
                }

                dictionary[item.Key] = new LocalizedString(item.Key, item[cultureIndex]);
            }

            return new StaticLocalizationDictionary(cultureName, dictionary);
        }
    }
}
