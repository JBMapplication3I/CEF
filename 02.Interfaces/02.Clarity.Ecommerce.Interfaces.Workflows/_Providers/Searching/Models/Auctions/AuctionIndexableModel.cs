// <copyright file="AuctionIndexableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Auction indexable model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System;

    /// <summary>A data Model for the Auction indexable.</summary>
    /// <seealso cref="IndexableModelBase"/>
    public class AuctionIndexableModel : IndexableModelBase
    {
        /// <summary>Gets or sets the number of bids.</summary>
        /// <value>The number of bids.</value>
        public int BidCount { get; set; }

        /// <summary>Gets or sets the Date/Time of the closes at.</summary>
        /// <value>The closes at.</value>
        public DateTime? ClosesAt { get; set; }
    }
}
