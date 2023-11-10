// <copyright file="SuggestResultBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the suggest result base class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.Collections.Generic;

    /// <summary>A suggest result base.</summary>
    public abstract class SuggestResultBase
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int? ID { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        public string? CustomKey { get; set; }

        /// <summary>Gets or sets URL of the seo.</summary>
        /// <value>The seo URL.</value>
        public string? SeoUrl { get; set; }

        /// <summary>Gets or sets the summary.</summary>
        /// <value>The summary.</value>
        public string? Summary { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
        public double Score { get; set; }

        /// <summary>Gets or sets the queryable attributes.</summary>
        /// <value>The queryable attributes.</value>
        public Dictionary<string, string>? QueryableAttributes { get; set; }
    }
}
