// <copyright file="IProductCategorySelectorModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductCategorySelectorModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for product category selector model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public interface IProductCategorySelectorModel : INameableBaseModel
    {
        /// <summary>Gets or sets URL of the SEO.</summary>
        /// <value>The SEO URL.</value>
        string? SeoUrl { get; set; }

        /// <summary>Gets or sets the identifier of the parent.</summary>
        /// <value>The identifier of the parent.</value>
        int? ParentID { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the has children.</summary>
        /// <value>The has children.</value>
        bool? HasChildren { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProductCategorySelectorModel is self selected.</summary>
        /// <value>True if this IProductCategorySelectorModel is self selected, false if not.</value>
        bool IsSelfSelected { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProductCategorySelectorModel is child selected.</summary>
        /// <value>True if this IProductCategorySelectorModel is child selected, false if not.</value>
        bool IsChildSelected { get; set; }

        /// <summary>Gets or sets the children.</summary>
        /// <value>The children.</value>
        List<IProductCategorySelectorModel>? Children { get; set; }
    }
}
