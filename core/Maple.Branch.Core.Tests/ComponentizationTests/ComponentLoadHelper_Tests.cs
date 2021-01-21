// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using Maple.Branch.Componentization;
using Maple.Branch.Core.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Maple.Branch.Core.Tests.ComponentizationTests
{
    public class ComponentLoadHelper_Tests
    {
        [Fact]
        public void Should_Load_Components_By_Dependency_Order()
        {
            var components = ComponentLoadHelper.LoadBranchComponents<SecondComponent>();

            components.Count().ShouldBe(2);

            components.First().Type.ShouldBe(typeof(FirstComponent));
            components.Last().Type.ShouldBe(typeof(SecondComponent));
        }

        [Fact]
        public void Test()
        {
            var services = new ServiceCollection();

            services.AddComponent<SecondComponent>();

            var serviceProvider = services.BuildServiceProvider();

            var a = serviceProvider.GetRequiredService<ITestService>();
        }
    }

    internal class FirstComponent : BranchComponent
    {
    }

    [DependsOn(typeof(FirstComponent), typeof(BranchCoreTestBaseComponent))]
    internal class SecondComponent : BranchComponent
    {
    }
}
