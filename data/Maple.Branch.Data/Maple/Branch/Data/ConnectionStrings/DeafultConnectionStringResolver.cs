// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maple.Branch.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Maple.Branch.Data.ConnectionStrings
{
    public class DeafultConnectionStringResolver : IConnectionStringResolver, ITransientObject
    {
        protected ConnectionStringOptions Options { get; }

        public DeafultConnectionStringResolver(IOptionsSnapshot<ConnectionStringOptions> options)
        {
            Options = options.Value;
        }

        public virtual ValueTask<string?> ResolveAsync(string? connectionStringName = null)
        {
            return ValueTask.FromResult(ResolveInternal(connectionStringName));
        }

        private string? ResolveInternal(string? connectionStringName)
        {
            // Get module specific value if provided
            if (connectionStringName.NotNullOrEmpty())
            {
                var moduleConnString = Options.Connections.GetOrDefault(connectionStringName!);
                if (moduleConnString.NotNullOrEmpty())
                {
                    return moduleConnString!;
                }
            }

            // Get default value
            return Options.Connections.Default;
        }
    }
}
