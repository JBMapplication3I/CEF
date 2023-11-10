// <copyright file="PagedResultsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF API interface</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Collections.Generic;

    /// <summary>A paged results.</summary>
    /// <typeparam name="TResultModel">Type of the result model.</typeparam>
    public abstract class PagedResultsBase<TResultModel>
    {
        /// <summary>Gets or sets the total number of pages.</summary>
        /// <value>The total number of pages.</value>
        public int TotalPages { get; set; }

        /// <summary>Gets or sets the current page.</summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }

        /// <summary>Gets or sets the number of totals.</summary>
        /// <value>The total number of count.</value>
        public int TotalCount { get; set; }

        /// <summary>Gets or sets the number of currents.</summary>
        /// <value>The number of currents.</value>
        public int CurrentCount { get; set; }

        /// <summary>Gets or sets the sorts.</summary>
        /// <value>The sorts.</value>
        public Sort[]? Sorts { get; set; }

        /// <summary>Gets or sets the groups.</summary>
        /// <value>The groups.</value>
        public Grouping[]? Groupings { get; set; }

        /// <summary>Gets or sets the results.</summary>
        /// <value>The results.</value>
        public List<TResultModel>? Results { get; set; }
    }
}
