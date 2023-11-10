// <copyright file="IDiscountCodeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountCodeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for discount code model.</summary>
    public partial interface IDiscountCodeModel
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }

        /// <summary>Gets or sets the identifier of the discount.</summary>
        /// <value>The identifier of the discount.</value>
        int DiscountID { get; set; }

        /// <summary>Gets or sets the discount key.</summary>
        /// <value>The discount key.</value>
        string? DiscountKey { get; set; }

        /// <summary>Gets or sets the name of the discount.</summary>
        /// <value>The name of the discount.</value>
        string? DiscountName { get; set; }

        /// <summary>Gets or sets the discount.</summary>
        /// <value>The discount.</value>
        IDiscountModel? Discount { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }
    }
}
