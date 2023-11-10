// <copyright file="IndexableVariantAttributesFilter.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexable variant attributes filter class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>An indexable variant attributes filter.</summary>
    public class IndexableVariantAttributesFilter
    {
        /// <summary>Gets or sets the ID of the variant.</summary>
        /// <value>The ID of the variant.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the JSON attributes of the variant.</summary>
        /// <value>The JSON attributes of the variant.</value>
        public string? JsonAttributes { get; set; }
    }
}
