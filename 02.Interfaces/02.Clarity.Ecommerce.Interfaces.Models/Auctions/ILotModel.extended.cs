// <copyright file="ILotModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ILotModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for lot model.</summary>
    public partial interface ILotModel
    {
        #region Properties
        /// <summary>Gets or sets the bidding reserve.</summary>
        /// <value>The bidding reserve.</value>
        decimal? BiddingReserve { get; set; }

        /// <summary>Gets or sets a value indicating whether this ILotModel is buy now available.</summary>
        /// <value>True if buy now available, false if not.</value>
        bool BuyNowAvailable { get; set; }

        /// <summary>Gets or sets a value indicating whether the prevent buy multiple.</summary>
        /// <value>True if prevent buy multiple, false if not.</value>
        bool PreventBuyMultiple { get; set; }

        /// <summary>Gets or sets a value indicating whether the no show.</summary>
        /// <value>True if no show, false if not.</value>
        bool NoShow { get; set; }

        /// <summary>Gets or sets the pickup time.</summary>
        /// <value>The pickup time.</value>
        DateTime? PickupTime { get; set; }

        /// <summary>Gets or sets the quantity available.</summary>
        /// <value>The quantity available.</value>
        decimal? QuantityAvailable { get; set; }

        /// <summary>Gets or sets the quantity sold.</summary>
        /// <value>The quantity sold.</value>
        decimal? QuantitySold { get; set; }

        /// <summary>Gets the total number of quantity.</summary>
        /// <value>The total number of quantity.</value>
        decimal? TotalQuantity { get; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the lot group.</summary>
        /// <value>The identifier of the lot group.</value>
        int? LotGroupID { get; set; }

        /// <summary>Gets or sets the lot group key.</summary>
        /// <value>The lot group key.</value>
        string? LotGroupKey { get; set; }

        /// <summary>Gets or sets the name of the lot group.</summary>
        /// <value>The name of the lot group.</value>
        string? LotGroupName { get; set; }

        /// <summary>Gets or sets the lot group.</summary>
        /// <value>The lot group.</value>
        ILotGroupModel? LotGroup { get; set; }

        /// <summary>Gets or sets the identifier of the auction.</summary>
        /// <value>The identifier of the auction.</value>
        int AuctionID { get; set; }

        /// <summary>Gets or sets the auction key.</summary>
        /// <value>The auction key.</value>
        string? AuctionKey { get; set; }

        /// <summary>Gets or sets the name of the auction.</summary>
        /// <value>The name of the auction.</value>
        string? AuctionName { get; set; }

        /// <summary>Gets or sets the auction.</summary>
        /// <value>The auction.</value>
        IAuctionModel? Auction { get; set; }

        /// <summary>Gets or sets the identifier of the pickup location.</summary>
        /// <value>The identifier of the pickup location.</value>
        int? PickupLocationID { get; set; }

        /// <summary>Gets or sets the pickup location key.</summary>
        /// <value>The pickup location key.</value>
        string? PickupLocationKey { get; set; }

        /// <summary>Gets or sets the pickup location.</summary>
        /// <value>The pickup location.</value>
        IContactModel? PickupLocation { get; set; }
        #endregion
    }
}
