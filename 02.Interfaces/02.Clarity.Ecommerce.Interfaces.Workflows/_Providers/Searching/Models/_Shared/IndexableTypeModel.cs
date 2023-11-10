// <copyright file="IndexableTypeModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexable type model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>A data Model for the indexable type.</summary>
    /// <seealso cref="IndexableFilterBase"/>
    public class IndexableTypeModel : IndexableFilterBase
    {
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        public int SortOrder { get; set; }

        /// <summary>Gets or sets the number of. </summary>
        /// <value>The count.</value>
        public long Count { get; set; }
    }
}
