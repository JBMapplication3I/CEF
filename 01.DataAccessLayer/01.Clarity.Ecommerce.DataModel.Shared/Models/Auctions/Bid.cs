// <copyright file="Bid.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bid class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for auction.</summary>
    public interface IBid
        : IHaveAStatusBase<BidStatus>,
            IAmFilterableByUser
    {
        #region Properties
        /// <summary>Gets or sets a value indicating whether this bid won the listing/lot.</summary>
        /// <value>True if won the listing/lot, false if not.</value>
        bool Won { get; set; }

        /// <summary>Gets or sets the maximum bid.</summary>
        /// <value>The maximum bid.</value>
        decimal? MaxBid { get; set; }

        /// <summary>Gets or sets the current bid.</summary>
        /// <value>The current bid.</value>
        decimal? CurrentBid { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the lot.</summary>
        /// <value>The identifier of the lot.</value>
        int? LotID { get; set; }

        /// <summary>Gets or sets the lot.</summary>
        /// <value>The lot.</value>
        Lot? Lot { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Auctions", "Bid")]
    public class Bid : Base, IBid
    {
        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual BidStatus? Status { get; set; }
        #endregion

        #region IAmFilterableByUser Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(0)]
        public int UserID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual User? User { get; set; }
        #endregion

        #region Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool Won { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaxBid { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? CurrentBid { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Lot)), DefaultValue(null)]
        public int? LotID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Lot? Lot { get; set; }
        #endregion
    }
}
