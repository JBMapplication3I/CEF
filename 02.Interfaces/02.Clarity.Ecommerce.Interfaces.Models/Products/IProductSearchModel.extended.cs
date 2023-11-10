// <copyright file="IProductSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for product search model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    public partial interface IProductSearchModel : IAmVendorAdminModified, IAmManufacturerAdminModified
    {
        /// <summary>Gets or sets the identifier of any ancestor category.</summary>
        /// <value>The identifier of any ancestor category.</value>
        int? HasAnyAncestorCategoryID { get; set; }

        /// <summary>Gets or sets the categories the parent belongs to.</summary>
        /// <value>The parent categories.</value>
        int[]? ParentCategories { get; set; }

        /// <summary>Gets or sets the search term.</summary>
        /// <value>The search term.</value>
        string? SearchTerm { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        decimal? Price { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        decimal? Weight { get; set; }

        /// <summary>Gets or sets the name of the category type.</summary>
        /// <value>The name of the category type.</value>
        string? CategoryTypeName { get; set; }

        /// <summary>Gets or sets the keywords.</summary>
        /// <value>The keywords.</value>
        string? Keywords { get; set; }

        /// <summary>Gets or sets the comparison IDs.</summary>
        /// <value>The comparison IDs.</value>
        int[]? ComparisonIDs { get; set; }

        /// <summary>Gets or sets the product IDs.</summary>
        /// <value>The product IDs.</value>
        int[]? ProductIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the brand categories.</summary>
        /// <value>A list of identifiers of the brand categories.</value>
        int[]? BrandCategoryIDs { get; set; }

        /// <summary>Gets or sets the category JSON attributes.</summary>
        /// <value>The category JSON attributes.</value>
        Dictionary<string, string?[]?>? CategoryJsonAttributes { get; set; }

        /// <summary>Gets or sets a context for the pricing factory.</summary>
        /// <value>The pricing factory context.</value>
        IPricingFactoryContextModel? PricingFactoryContext { get; set; }
    }
}
