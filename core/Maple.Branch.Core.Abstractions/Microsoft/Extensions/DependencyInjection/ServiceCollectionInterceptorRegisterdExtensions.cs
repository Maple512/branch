// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Maple.Branch.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionInterceptorRegisterdExtensions
    {
        public static void OnRegistred(this IServiceCollection services, Action<IInterceptorRegistredContext> registrationAction)
        {
            GetOrCreateInterceptorRegistrationActionList(services).Add(registrationAction);
        }

        public static InterceptorRegistrationActionList GetInterceptorRegistrationActionList(this IServiceCollection services)
        {
            return GetOrCreateInterceptorRegistrationActionList(services);
        }

        private static InterceptorRegistrationActionList GetOrCreateInterceptorRegistrationActionList(IServiceCollection services)
        {
            var actionList = services.GetSingletonInstanceOrNull<InterceptorRegistrationActionList>();

            if (actionList == null)
            {
                actionList = new InterceptorRegistrationActionList();
                services.AddSingleton(actionList);
            }

            return actionList;
        }
    }
}
