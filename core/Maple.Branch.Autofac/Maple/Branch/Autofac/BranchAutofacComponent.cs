// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Maple.Branch.Castle;
using Maple.Branch.Componentization;

namespace Maple.Branch.Autofac
{
    [DependsOn(typeof(BranchCastleComponent))]
    public class BranchAutofacComponent : BranchComponent
    {
    }
}
