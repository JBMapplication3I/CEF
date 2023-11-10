// <copyright file="IAmFilterableByProductModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByProductModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by product model.</summary>
    /// <typeparam name="TModel">Type of the product relationship model.</typeparam>
    public interface IAmFilterableByProductModel<TModel>
    {
        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        List<TModel>? Products { get; set; }
    }

    /// <summary>Interface for am filterable by product model.</summary>
    public interface IAmFilterableByProductModel
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        IProductModel? Product { get; set; }

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
