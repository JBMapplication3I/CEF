// <copyright file="PageViewModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page view model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>A ViewModel for the page.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IPageViewModel"/>
    public partial class PageViewModel
    {
        /// <inheritdoc/>
        public string? Title { get; set; }

        /// <inheritdoc/>
        public string? URI { get; set; }

        /// <inheritdoc/>
        public DateTime? ViewedOn { get; set; }

        /// <inheritdoc/>
        public string? VisitKey { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? CategoryID { get; set; }

        /// <inheritdoc/>
        public string? CategoryKey { get; set; }

        /// <inheritdoc/>
        public string? CategoryName { get; set; }

        /// <inheritdoc cref="IPageViewModel.Category"/>
        [JsonIgnore]
        public CategoryModel? Category { get; set; }

        /// <inheritdoc/>
        ICategoryModel? IPageViewModel.Category { get => Category; set => Category = (CategoryModel?)value; }

        /// <inheritdoc/>
        public int? ProductID { get; set; }

        /// <inheritdoc/>
        public string? ProductKey { get; set; }

        /// <inheritdoc/>
        public string? ProductName { get; set; }

        /// <inheritdoc cref="IPageViewModel.Product"/>
        [JsonIgnore]
        public ProductModel? Product { get; set; }

        /// <inheritdoc/>
        IProductModel? IPageViewModel.Product { get => Product; set => Product = (ProductModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IPageViewModel.PageViewEvents"/>
        [JsonIgnore]
        public List<PageViewEventModel>? PageViewEvents { get; set; }

        /// <inheritdoc/>
        List<IPageViewEventModel>? IPageViewModel.PageViewEvents { get => PageViewEvents?.ToList<IPageViewEventModel>(); set => PageViewEvents = value?.Cast<PageViewEventModel>().ToList(); }
        #endregion
    }
}
