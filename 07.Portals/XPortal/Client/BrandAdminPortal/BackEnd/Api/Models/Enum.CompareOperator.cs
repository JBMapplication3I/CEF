// <copyright file="CompareOperator.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the compare operator class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    /// <summary>Values that represent compare operators.</summary>
    public enum CompareOperator
    {
        /// <summary>An enum constant representing the undefined option.</summary>
        Undefined,

        /// <summary>An enum constant representing the less than option.</summary>
        LessThan,

        /// <summary>An enum constant representing the less than or equal to option.</summary>
        LessThanOrEqualTo,

        /// <summary>An enum constant representing the greater than option.</summary>
        GreaterThan,

        /// <summary>An enum constant representing the greater than or equal to option.</summary>
        GreaterThanOrEqualTo,
    }
}
