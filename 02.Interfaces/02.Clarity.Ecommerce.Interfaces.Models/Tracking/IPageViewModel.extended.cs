// <copyright file="IPageViewModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPageViewModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for page view model.</summary>
    public partial interface IPageViewModel
    {
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        string? Title { get; set; }

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        string? URI { get; set; }

        /// <summary>Gets or sets the Date/Time of the viewed on.</summary>
        /// <value>The viewed on.</value>
        DateTime? ViewedOn { get; set; }

        /// <summary>Gets or sets the visit key.</summary>
        /// <value>The visit key.</value>
        string? VisitKey { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int? CategoryID { get; set; }

        /// <summary>Gets or sets the category key.</summary>
        /// <value>The category key.</value>
        string? CategoryKey { get; set; }

        /// <summary>Gets or sets the name of the category.</summary>
        /// <value>The name of the category.</value>
        string? CategoryName { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        ICategoryModel? Category { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int? ProductID { get; set; }

        /// <summary>Gets or sets the product key.</summary>
        /// <value>The product key.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the name of the product.</summary>
        /// <value>The name of the product.</value>
        string? ProductName { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        IProductModel? Product { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the page view events.</summary>
        /// <value>The page view events.</value>
        List<IPageViewEventModel>? PageViewEvents { get; set; }
        #endregion
    }
}
