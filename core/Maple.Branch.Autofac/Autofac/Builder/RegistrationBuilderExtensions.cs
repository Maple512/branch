// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Maple.Branch.Castle.DynamicProxy;
using Maple.Branch.Collections;
using Maple.Branch.Componentization;
using Maple.Branch.DependencyInjection;
using Maple.Branch.DynamicProxy;

namespace Autofac.Builder
{
    public static class RegistrationBuilderExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> ConfigureAbpConventions<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
                IComponentContainer moduleContainer,
                InterceptorRegistrationActionList intercetproRegistrationActionList)
            where TActivatorData : ReflectionActivatorData
        {
            var serviceType = registrationBuilder.RegistrationData.Services.OfType<IServiceWithType>().FirstOrDefault()?.ServiceType;
            if (serviceType == null)
            {
                return registrationBuilder;
            }

            var implementationType = registrationBuilder.ActivatorData.ImplementationType;
            if (implementationType == null)
            {
                return registrationBuilder;
            }

            registrationBuilder = registrationBuilder.EnablePropertyInjection(moduleContainer, implementationType);
            registrationBuilder = registrationBuilder.InvokeRegistrationActions(intercetproRegistrationActionList, serviceType, implementationType);

            return registrationBuilder;
        }

        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InvokeRegistrationActions<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, InterceptorRegistrationActionList registrationActionList, Type serviceType, Type implementationType)
            where TActivatorData : ReflectionActivatorData
        {
            var serviceRegistredArgs = new InterceptorRegistredContext(serviceType, implementationType);

            foreach (var registrationAction in registrationActionList)
            {
                registrationAction.Invoke(serviceRegistredArgs);
            }

            if (serviceRegistredArgs.Interceptors.Any())
            {
                registrationBuilder = registrationBuilder.AddInterceptors(
                    serviceType,
                    serviceRegistredArgs.Interceptors
                );
            }

            return registrationBuilder;
        }

        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> EnablePropertyInjection<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
                IComponentContainer moduleContainer,
                Type implementationType)
            where TActivatorData : ReflectionActivatorData
        {
            //Enable Property Injection only for types in an assembly containing an AbpModule
            if (moduleContainer.Modules.Any(m => m.Assembly == implementationType.Assembly))
            {
                registrationBuilder = registrationBuilder.PropertiesAutowired();
            }

            return registrationBuilder;
        }

        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> AddInterceptors<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
            Type serviceType,
            ITypeList<IBranchInterceptor> interceptors)
            where TActivatorData : ReflectionActivatorData
        {
            if (serviceType.IsInterface)
            {
                registrationBuilder = registrationBuilder.EnableInterfaceInterceptors();
            }
            else
            {
                (registrationBuilder as IRegistrationBuilder<TLimit, ConcreteReflectionActivatorData, TRegistrationStyle>)?.EnableClassInterceptors();
            }

            foreach (var interceptor in interceptors)
            {
                registrationBuilder.InterceptedBy(
                    typeof(BranchAsyncDeterminationInterceptor<>).MakeGenericType(interceptor)
                );
            }

            return registrationBuilder;
        }
    }
}
