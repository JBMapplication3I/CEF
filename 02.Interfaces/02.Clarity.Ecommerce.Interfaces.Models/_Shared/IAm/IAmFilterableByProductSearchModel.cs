// <copyright file="IAmFilterableByProductSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByProductSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by product search model.</summary>
    public interface IAmFilterableByProductSearchModel
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int? ProductID { get; set; }

        /// <summary>Gets or sets the product key.</summary>
        /// <value>The product key.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the name of the product.</summary>
        /// <value>The name of the product.</value>
        string? ProductName { get; set; }

        /// <summary>Gets or sets the SEO URL of the product.</summary>
        /// <value>The SEO URL of the product.</value>
        string? ProductSeoUrl { get; set; }
    }
}
