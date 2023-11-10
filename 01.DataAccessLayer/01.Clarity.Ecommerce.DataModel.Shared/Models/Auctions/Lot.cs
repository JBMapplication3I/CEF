// <copyright file="Lot.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the lot class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for auction.</summary>
    public interface ILot
        : INameableBase,
            IHaveAStatusBase<LotStatus>,
            IHaveATypeBase<LotType>,
            IAmFilterableByCategory<LotCategory>,
            IAmFilterableByProduct
    {
        #region Properties
        /// <summary>Gets or sets a value indicating whether this Lot is buy now available.</summary>
        /// <value>True if buy now available, false if not.</value>
        bool BuyNowAvailable { get; set; }

        /// <summary>Gets or sets a value indicating whether the prevent buy multiple.</summary>
        /// <value>True if prevent buy multiple, false if not.</value>
        bool PreventBuyMultiple { get; set; }

        /// <summary>Gets or sets a value indicating whether there was a no show for the pickup time.</summary>
        /// <value>True if no show, false if not.</value>
        bool NoShow { get; set; }

        /// <summary>Gets or sets the pickup time.</summary>
        /// <value>The pickup time.</value>
        DateTime? PickupTime { get; set; }

        /// <summary>Gets or sets the bidding reserve.</summary>
        /// <value>The bidding reserve.</value>
        decimal? BiddingReserve { get; set; }

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
        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        int? LotGroupID { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        LotGroup? LotGroup { get; set; }

        /// <summary>Gets or sets the identifier of the auction.</summary>
        /// <value>The identifier of the auction.</value>
        int AuctionID { get; set; }

        /// <summary>Gets or sets the auction.</summary>
        /// <value>The auction.</value>
        Auction? Auction { get; set; }

        /// <summary>Gets or sets the identifier of the pickup location.</summary>
        /// <value>The identifier of the pickup location.</value>
        int? PickupLocationID { get; set; }

        /// <summary>Gets or sets the pickup location.</summary>
        /// <value>The pickup location.</value>
        Contact? PickupLocation { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the bids.</summary>
        /// <value>The bids.</value>
        ICollection<Bid>? Bids { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Auctions", "Lot")]
    public class Lot : NameableBase, ILot
    {
        private ICollection<Bid>? bids;
        private ICollection<LotCategory>? categories;

        public Lot()
        {
            bids = new HashSet<Bid>();
            categories = new HashSet<LotCategory>();
        }

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual LotStatus? Status { get; set; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual LotType? Type { get; set; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Product)), DefaultValue(0)]
        public int ProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Product? Product { get; set; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<LotCategory>? Categories { get => categories; set => categories = value; }
        #endregion

        #region Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool BuyNowAvailable { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool PreventBuyMultiple { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool NoShow { get; set; } = false;

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? PickupTime { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? BiddingReserve { get; set; }

        /// <summary>Gets or sets the quantity available.</summary>
        /// <value>The quantity available.</value>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityAvailable { get; set; }

        /// <summary>Gets or sets the quantity sold.</summary>
        /// <value>The quantity sold.</value>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantitySold { get; set; }

        /// <summary>Gets the total number of quantity.</summary>
        /// <value>The total number of quantity.</value>
        [NotMapped, JsonIgnore, DefaultValue(null)]
        public decimal? TotalQuantity => QuantityAvailable ?? 0m + QuantitySold ?? 0m;
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(LotGroup)), DefaultValue(null)]
        public int? LotGroupID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual LotGroup? LotGroup { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Auction)), DefaultValue(null)]
        public int AuctionID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Auction? Auction { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PickupLocation)), DefaultValue(null)]
        public int? PickupLocationID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Contact? PickupLocation { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Bid>? Bids { get => bids; set => bids = value; }
        #endregion
    }
}
