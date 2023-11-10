// <copyright file="IndexableFilterBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexable filter base class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>An indexable filter base.</summary>
    public abstract class IndexableFilterBase
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        public string? Key { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }
    }
}
