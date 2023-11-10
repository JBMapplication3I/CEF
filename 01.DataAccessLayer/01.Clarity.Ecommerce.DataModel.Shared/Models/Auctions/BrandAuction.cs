// <copyright file="BrandAuction.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand brand class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IBrandAuction
        : IAmABrandRelationshipTableWhereBrandIsTheMaster<Auction>
    {
        /// <summary>Gets or sets a value indicating whether this IBrandAuction is visible in.</summary>
        /// <value>True if this IBrandAuction is visible in, false if not.</value>
        bool IsVisibleIn { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Auctions", "BrandAuction")]
    public class BrandAuction : Base, IBrandAuction
    {
        #region IAmARelationshipTable<Brand, Auction>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Brand? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByBrand.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmFilterableByBrand.Brand { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmABrandRelationshipTableWhereBrandIsTheMaster<Auction>.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmABrandRelationshipTableWhereBrandIsTheMaster<Auction>.Brand { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Auction? Slave { get; set; }
        #endregion

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsVisibleIn { get; set; }
    }
}
