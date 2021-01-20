// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Maple.Branch.MultiTenancy
{
    public static class BranchMultiTenancyConstants
    {
        public const string ComponentName = "MultiTenancy";

        /// <summary>
        /// Value: <c>MultiTenancy</c>
        /// </summary>
        public const string ConfigurationRootName = ComponentName;

        /// <summary>
        /// Value: <c>Branch:MultiTenancy</c>
        /// </summary>
        public const string ConfigurationRootFullName = BranchConsants.ConfigurationRootName + ":" + ConfigurationRootName;
    }
}
