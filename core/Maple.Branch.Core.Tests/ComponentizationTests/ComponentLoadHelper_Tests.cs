// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using Maple.Branch.Componentization;
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
    }

    internal class FirstComponent : BranchComponent
    {
    }

    [DependsOn(typeof(FirstComponent))]
    internal class SecondComponent : BranchComponent
    {
    }
}
