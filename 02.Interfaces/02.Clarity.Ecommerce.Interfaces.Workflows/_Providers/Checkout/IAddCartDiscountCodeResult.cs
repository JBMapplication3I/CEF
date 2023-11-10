// <copyright file="IAddCartDiscountCodeResult.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddCartDiscountCodeResult interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for add cart discount code result.</summary>
    public interface IAddCartDiscountCodeResult
    {
        /// <summary>Gets or sets a value indicating whether the success.</summary>
        /// <value>True if success, false if not.</value>
        bool Success { get; set; }

        /// <summary>Gets or sets a value indicating whether the valid.</summary>
        /// <value>True if valid, false if not.</value>
        bool Valid { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        string? Message { get; set; }
    }
}
