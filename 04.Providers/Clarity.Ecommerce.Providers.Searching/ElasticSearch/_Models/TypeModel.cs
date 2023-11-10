// <copyright file="TypeModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the type model class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    /// <summary>A data Model for the category.</summary>
    public class TypeModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        public int SortOrder { get; set; }
    }
}
