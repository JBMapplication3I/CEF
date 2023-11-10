// <copyright file="IAmFilterableByBrandSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByBrandSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by brand search model.</summary>
    public interface IAmFilterableByBrandSearchModel
    {
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        int? BrandID { get; set; }

        /// <summary>Gets or sets the brand identifier include null.</summary>
        /// <value>The brand identifier include null.</value>
        bool? BrandIDIncludeNull { get; set; }

        /// <summary>Gets or sets the brand key.</summary>
        /// <value>The brand key.</value>
        string? BrandKey { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        string? BrandName { get; set; }

        /// <summary>Gets or sets the brand name strict.</summary>
        /// <value>The brand name strict.</value>
        bool? BrandNameStrict { get; set; }

        /// <summary>Gets or sets the brand name include null.</summary>
        /// <value>The brand name include null.</value>
        bool? BrandNameIncludeNull { get; set; }

        /// <summary>Gets or sets the identifier of the brand category.</summary>
        /// <value>The identifier of the brand category.</value>
        int? BrandCategoryID { get; set; }
    }
}
