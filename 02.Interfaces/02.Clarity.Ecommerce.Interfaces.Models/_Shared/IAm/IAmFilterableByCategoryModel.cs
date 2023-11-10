// <copyright file="IAmFilterableByCategoryModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByCategoryModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by category model.</summary>
    /// <typeparam name="TModel">Type of the category relationship model.</typeparam>
    public interface IAmFilterableByCategoryModel<TModel>
    {
        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        List<TModel>? Categories { get; set; }
    }

    /// <summary>Interface for am filterable by category model.</summary>
    public interface IAmFilterableByCategoryModel
    {
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        ICategoryModel? Category { get; set; }

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
