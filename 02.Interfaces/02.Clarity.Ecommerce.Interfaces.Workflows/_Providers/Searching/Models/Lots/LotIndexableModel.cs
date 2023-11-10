// <copyright file="LotIndexableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Lot indexable model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>A data Model for the category indexable.</summary>
    /// <seealso cref="IndexableModelBase"/>
    public class LotIndexableModel : IndexableModelBase
    {
        /// <summary>Gets or sets the number of bids.</summary>
        /// <value>The number of bids.</value>
        public int BidCount { get; set; }
    }
}
