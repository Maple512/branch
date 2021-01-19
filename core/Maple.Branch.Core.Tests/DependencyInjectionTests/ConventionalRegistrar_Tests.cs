// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using Maple.Branch.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Maple.Branch.Core.Tests.DependencyInjectionTests
{
    public class ConventionalRegistrar_Tests
    {
        [Fact]
        public void Should_Use_Custom_Conventions_If_Added()
        {
            //Arrange
            var services = new ServiceCollection();

            //Act
            services.AddConventionalRegistrar(new MyCustomConventionalRegistrar());
            services.AddTypes(typeof(MyCustomClass), typeof(MyClass), typeof(MyNonRegisteredClass));

            //Assert
            services.First(m => m.ImplementationType == typeof(MyClass))
                .Lifetime.ShouldBe(ServiceLifetime.Transient);

            services.First(m => m.ImplementationType == typeof(MyCustomClass))
                .Lifetime.ShouldBe(ServiceLifetime.Singleton);

            services.FirstOrDefault(m => m.ImplementationType == typeof(MyNonRegisteredClass))
                .ShouldBeNull();
        }

        public class MyCustomConventionalRegistrar : ConventionalRegistrarBase
        {
            public override void AddType(IServiceCollection services, Type type, ServiceLifetime? lifetime = null)
            {
                if (type == typeof(MyClass))
                {
                    services.AddSingleton<MyCustomClass>();
                }
            }
        }

        public class MyCustomClass
        {

        }

        public class MyNonRegisteredClass
        {

        }

        private interface Inter1
        {

        }

        private interface Inter2 : Inter1
        {

        }

        public class MyClass : Inter2, ITransientObject
        {

        }
    }
}
