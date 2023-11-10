// <copyright file="BrandStore.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand store class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IBrandStore
        : IAmABrandRelationshipTableWhereBrandIsTheMaster<Store>,
            IAmAStoreRelationshipTableWhereStoreIsTheSlave<Brand>
    {
        /// <summary>Gets or sets a value indicating whether this record is visible in the owner.</summary>
        /// <value>True if this record is visible in the owner, false if not.</value>
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

    [SqlSchema("Brands", "BrandStore")]
    public class BrandStore : Base, IBrandStore
    {
        #region IAmAStoreRelationshipTable<Brand>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Brand? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Store? Slave { get; set; }

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByBrand.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmFilterableByBrand.Brand { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmABrandRelationshipTableWhereBrandIsTheMaster<Store>.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmABrandRelationshipTableWhereBrandIsTheMaster<Store>.Brand { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByStore.StoreID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Store? IAmFilterableByStore.Store { get => Slave; set => Slave = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAStoreRelationshipTableWhereStoreIsTheSlave<Brand>.StoreID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Store? IAmAStoreRelationshipTableWhereStoreIsTheSlave<Brand>.Store { get => Slave; set => Slave = value; }
        #endregion
        #endregion

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsVisibleIn { get; set; }
    }
}
