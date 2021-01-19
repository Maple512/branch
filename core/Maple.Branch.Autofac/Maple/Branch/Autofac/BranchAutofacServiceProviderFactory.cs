// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MapleClub.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.Autofac
{
    public class BranchAutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly ContainerBuilder _builder;

        public BranchAutofacServiceProviderFactory(ContainerBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Creates a container builder from an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <param name="services">The collection of services</param>
        /// <returns>A container builder that can be used to create an <see cref="T:System.IServiceProvider" />.</returns>
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            _builder.Populate(services);

            return _builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            Check.NotNull(containerBuilder, nameof(containerBuilder));

            return new AutofacServiceProvider(containerBuilder.Build());
        }
    }
}
