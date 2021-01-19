// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maple.Branch.Validation
{
    public interface IHasValidationErrors
    {
        IList<ValidationResult> ValidationErrors { get; }
    }
}
