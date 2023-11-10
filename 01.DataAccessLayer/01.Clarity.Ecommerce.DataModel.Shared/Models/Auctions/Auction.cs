// <copyright file="Auction.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the auction class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for auction.</summary>
    public interface IAuction
        : INameableBase,
            IHaveATypeBase<AuctionType>,
            IHaveAStatusBase<AuctionStatus>,
            IHaveANullableContactBase,
            IAmFilterableByCategory<AuctionCategory>,
            IAmFilterableByBrand<BrandAuction>,
            IAmFilterableByFranchise<FranchiseAuction>,
            IAmFilterableByStore<StoreAuction>
    {
        #region Properties
        /// <summary>Gets or sets the Date/Time of the opens at.</summary>
        /// <value>The opens at.</value>
        DateTime? OpensAt { get; set; }

        /// <summary>Gets or sets the Date/Time of the closes at.</summary>
        /// <value>The closes at.</value>
        DateTime? ClosesAt { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the lots.</summary>
        /// <value>The lots.</value>
        ICollection<Lot>? Lots { get; set; }
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

    [SqlSchema("Auctions", "Auction")]
    public class Auction : NameableBase, IAuction
    {
        private ICollection<Lot>? lots;
        private ICollection<BrandAuction>? brands;
        private ICollection<StoreAuction>? stores;
        private ICollection<AuctionCategory>? categories;
        private ICollection<FranchiseAuction>? franchises;

        public Auction()
        {
            lots = new HashSet<Lot>();
            brands = new HashSet<BrandAuction>();
            stores = new HashSet<StoreAuction>();
            categories = new HashSet<AuctionCategory>();
            franchises = new HashSet<FranchiseAuction>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual AuctionType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual AuctionStatus? Status { get; set; }
        #endregion

        #region IHaveANullableContactBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Contact? Contact { get; set; }
        #endregion

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandAuction>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AuctionCategory>? Categories { get => categories; set => categories = value; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseAuction>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreAuction>? Stores { get => stores; set => stores = value; }
        #endregion

        #region Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? OpensAt { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? ClosesAt { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Lot>? Lots { get => lots; set => lots = value; }
        #endregion
    }
}
