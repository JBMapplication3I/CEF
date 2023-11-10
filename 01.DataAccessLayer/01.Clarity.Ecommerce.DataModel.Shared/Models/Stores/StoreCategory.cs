// <copyright file="StoreCategory.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store category class, which describes
// a relationship between a Store and a Category</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IStoreCategory
        : IAmAStoreRelationshipTableWhereStoreIsTheMaster<Category>,
            IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Store>
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
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Stores", "StoreCategory")]
    public class StoreCategory : Base, IStoreCategory
    {
        #region Store as Master
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Store? Master { get; set; }
        #endregion

        #region Category as Slave
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual Category? Slave { get; set; }
        #endregion

        #region IAmAStoreRelationshipTableWhereStoreIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAStoreRelationshipTableWhereStoreIsTheMaster<Category>.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmAStoreRelationshipTableWhereStoreIsTheMaster<Category>.Store { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByStore.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmFilterableByStore.Store { get => Master; set => Master = value; }
        #endregion

        #region IAmACategoryRelationshipTableWhereCategoryIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Store>.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Category? IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Store>.Category { get => Slave; set => Slave = value; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByCategory.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Category? IAmFilterableByCategory.Category { get => Slave; set => Slave = value; }
        #endregion

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsVisibleIn { get; set; }
    }
}
