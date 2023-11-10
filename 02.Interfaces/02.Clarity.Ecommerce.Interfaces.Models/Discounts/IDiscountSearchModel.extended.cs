// <copyright file="IDiscountSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for discount search model.</summary>
    public partial interface IDiscountSearchModel
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }
    }
}
