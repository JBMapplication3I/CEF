// <copyright file="CatModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category model class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    /// <summary>A data Model for the category.</summary>
    public class CatModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        public string? CustomKey { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        public string? DisplayName { get; set; }

        /// <summary>Gets or sets the identifier of the parent.</summary>
        /// <value>The identifier of the parent.</value>
        public int? ParentID { get; set; }
    }
}
