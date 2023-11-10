// <copyright file="AddCartDiscountCodeResult.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the add cart discount code result class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>Encapsulates the result of an add cart discount code.</summary>
    /// <seealso cref="IAddCartDiscountCodeResult"/>
    public class AddCartDiscountCodeResult : IAddCartDiscountCodeResult
    {
        /// <inheritdoc/>
        public bool Success { get; set; }

        /// <inheritdoc/>
        public bool Valid { get; set; }

        /// <inheritdoc/>
        public string? Message { get; set; }
    }
}
