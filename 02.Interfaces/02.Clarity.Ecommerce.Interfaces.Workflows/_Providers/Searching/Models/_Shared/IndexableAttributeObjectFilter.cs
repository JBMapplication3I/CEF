// <copyright file="IndexableAttributeObjectFilter.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexable attribute object filter class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>An indexable attribute object filter.</summary>
    public class IndexableAttributeObjectFilter
    {
        /// <summary>Gets or sets the identifier of the General Attribute that this value is for.</summary>
        /// <value>The identifier of the General Attribute that this value is for.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the Key of the General Attribute that this value is for.</summary>
        /// <value>The Key of the General Attribute that this value is for.</value>
        public string? Key { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public string? Value { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        public string? UofM { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        public int? SortOrder { get; set; }
    }
}
