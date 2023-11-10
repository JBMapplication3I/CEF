// <copyright file="ICategorySearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICategorySearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for category search model.</summary>
    /// <seealso cref="IHaveATypeBaseSearchModel"/>
    public partial interface ICategorySearchModel
    {
        /// <summary>Gets or sets the categories the exclude product belongs to.</summary>
        /// <value>The exclude product categories.</value>
        bool? ExcludeProductCategories { get; set; }

        /// <summary>Gets or sets the parent seourl.</summary>
        /// <value>The parent seourl.</value>
        string? ParentSEOURL { get; set; }

        /// <summary>Gets or sets the identifier of the has products under another category.</summary>
        /// <value>The identifier of the has products under another category.</value>
        int? HasProductsUnderAnotherCategoryID { get; set; }

        /// <summary>Gets or sets the has products under another category key.</summary>
        /// <value>The has products under another category key.</value>
        string? HasProductsUnderAnotherCategoryKey { get; set; }

        /// <summary>Gets or sets the name of the has products under another category.</summary>
        /// <value>The name of the has products under another category.</value>
        string? HasProductsUnderAnotherCategoryName { get; set; }

        /// <summary>Gets or sets the child JSON attributes.</summary>
        /// <value>The child JSON attributes.</value>
        Dictionary<string, string?[]?>? ChildJsonAttributes { get; set; }

        /// <summary>Gets or sets the current roles.</summary>
        /// <value>The current roles.</value>
        string?[]? CurrentRoles { get; set; }
    }
}
