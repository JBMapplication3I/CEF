// <copyright file="LotModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ILotModel interface</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.ComponentModel;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>Interface for lot model.</summary>
    public partial class LotModel
    {
        #region Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? BiddingReserve { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool BuyNowAvailable { get; set; }

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool PreventBuyMultiple { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool NoShow { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? PickupTime { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityAvailable { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantitySold { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public decimal? TotalQuantity => QuantityAvailable ?? 0m + QuantitySold ?? 0m;
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? LotGroupID { get; set; }

        /// <inheritdoc/>
        public string? LotGroupKey { get; set; }

        /// <inheritdoc/>
        public string? LotGroupName { get; set; }

        /// <inheritdoc cref="ILotModel.LotGroup"/>
        public LotGroupModel? LotGroup { get; set; }

        /// <inheritdoc/>
        ILotGroupModel? ILotModel.LotGroup { get => LotGroup; set => LotGroup = (LotGroupModel?)value; }

        /// <inheritdoc/>
        public int AuctionID { get; set; }

        /// <inheritdoc/>
        public string? AuctionKey { get; set; }

        /// <inheritdoc/>
        public string? AuctionName { get; set; }

        /// <inheritdoc cref="ILotModel.Auction"/>
        public AuctionModel? Auction { get; set; }

        /// <inheritdoc/>
        IAuctionModel? ILotModel.Auction { get => Auction; set => Auction = (AuctionModel?)value; }

        /// <inheritdoc/>
        public int? PickupLocationID { get; set; }

        /// <inheritdoc/>
        public string? PickupLocationKey { get; set; }

        /// <inheritdoc cref="ILotModel.PickupLocation"/>
        public ContactModel? PickupLocation { get; set; }

        /// <inheritdoc/>
        IContactModel? ILotModel.PickupLocation { get; set; }
        #endregion
    }
}
