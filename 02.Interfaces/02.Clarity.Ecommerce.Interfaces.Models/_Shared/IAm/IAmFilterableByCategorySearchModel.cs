// <copyright file="IAmFilterableByCategorySearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByCategorySearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by category search model.</summary>
    public interface IAmFilterableByCategorySearchModel
    {
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int? CategoryID { get; set; }

        /// <summary>Gets or sets the category key.</summary>
        /// <value>The category key.</value>
        string? CategoryKey { get; set; }

        /// <summary>Gets or sets the name of the category.</summary>
        /// <value>The name of the category.</value>
        string? CategoryName { get; set; }

        /// <summary>Gets or sets URL of the category SEO.</summary>
        /// <value>The category SEO URL.</value>
        string? CategorySeoUrl { get; set; }
    }
}
