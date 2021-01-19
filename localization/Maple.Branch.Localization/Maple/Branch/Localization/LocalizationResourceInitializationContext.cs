// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Maple.Branch.Localization
{
    public class LocalizationResourceInitializationContext
    {
        public LocalizationResource Resource { get; }

        public IServiceProvider ServiceProvider { get; }

        public LocalizationResourceInitializationContext(LocalizationResource resource, IServiceProvider serviceProvider)
        {
            Resource = resource;
            ServiceProvider = serviceProvider;
        }
    }
}
