// <copyright file="IMenuCategoryModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMenuCategoryModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for menu category model.</summary>
    public interface IMenuCategoryModel
    {
        /// <summary>Gets or sets the id.</summary>
        /// <value>The id.</value>
        int? ID { get; set; }

        /// <summary>Gets or sets a value indicating whether the category has children.</summary>
        /// <value>The has children indicator.</value>
        bool HasChildren { get; set; }

        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        string? CustomKey { get; set; }

        /// <summary>Gets or sets the seo url.</summary>
        /// <value>The seo url.</value>
        string? SeoUrl { get; set; }

        /// <summary>Gets or sets the name of the primary image file.</summary>
        /// <value>The primary image file.</value>
        string? PrimaryImageFileName { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the menu category children.</summary>
        /// <value>The menu category children.</value>
        List<IMenuCategoryModel>? Children { get; set; }
    }
}
