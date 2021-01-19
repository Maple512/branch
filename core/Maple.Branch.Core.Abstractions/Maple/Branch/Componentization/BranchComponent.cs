// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.Branch.Componentization
{
    public abstract class BranchComponent : IBranchComponent
    {
        public ConfigureSerivcesContext? Context { get; private set; }

        public void OnPreConfigureServices(ConfigureSerivcesContext context)
        {

        }

        public virtual void OnConfigureServices(ConfigureSerivcesContext context)
        {

        }

        public void OnPostConfigureServices(ConfigureSerivcesContext context)
        {

        }

        public void SetConfigureServicesContext(ConfigureSerivcesContext context)
        {
            Context = context;
        }

        public void ClearConfigureServicesContext()
        {
            Context = null;
        }

        #region Configure Options

        protected virtual void Configure<TOptions>(IConfiguration configuration)
            where TOptions : class
        {
            Context?.Services.Configure<TOptions>(configuration);
        }

        protected virtual void Configure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context?.Services.Configure(configureOptions);
        }

        protected virtual void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions : class
        {
            Context?.Services.Configure<TOptions>(name, configuration);
        }

        protected virtual void Configure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context?.Services.Configure(name, configureOptions);
        }

        protected virtual void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            Context?.Services.Configure<TOptions>(configuration, configureBinder);
        }

        protected virtual void Configure<TOptions>(string name, IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            Context?.Services.Configure<TOptions>(name, configuration, configureBinder);
        }

        protected virtual void PostConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context?.Services.PostConfigure(configureOptions);
        }

        protected virtual void PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context?.Services.PostConfigureAll(configureOptions);
        }

        #endregion Configure Options
    }
}
