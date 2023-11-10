// <copyright file="IndexableRegionModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexable region model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>A data Model for the indexable region.</summary>
    /// <seealso cref="IndexableFilterBase"/>
    public class IndexableRegionModel : IndexableFilterBase
    {
        /// <summary>Gets or sets the number of. </summary>
        /// <value>The count.</value>
        public long? DocCount { get; set; }
    }
}
