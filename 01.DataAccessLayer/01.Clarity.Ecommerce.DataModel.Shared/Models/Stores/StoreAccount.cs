// <copyright file="StoreAccount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store account class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IStoreAccount
        : IAmAStoreRelationshipTableWhereStoreIsTheMaster<Account>,
            IAmAnAccountRelationshipTableWhereAccountIsTheSlave<Store>
    {
        /// <summary>Gets or sets a value indicating whether this IStoreAccount has access to store.</summary>
        /// <value>True if this IStoreAccount has access to store, false if not.</value>
        bool HasAccessToStore { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the price point.</summary>
        /// <value>The identifier of the price point.</value>
        int? PricePointID { get; set; }

        /// <summary>Gets or sets the price point.</summary>
        /// <value>The price point.</value>
        PricePoint? PricePoint { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Stores", "StoreAccount")]
    public class StoreAccount : Base, IStoreAccount
    {
        #region IAmARelationshipTable<Store, Account>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Store? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByStore.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmFilterableByStore.Store { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAStoreRelationshipTableWhereStoreIsTheMaster<Account>.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmAStoreRelationshipTableWhereStoreIsTheMaster<Account>.Store { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Account? Slave { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByAccount.AccountID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Account? IAmFilterableByAccount.Account { get => Slave; set => Slave = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAnAccountRelationshipTableWhereAccountIsTheSlave<Store>.AccountID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Account? IAmAnAccountRelationshipTableWhereAccountIsTheSlave<Store>.Account { get => Slave; set => Slave = value; }
        #endregion

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HasAccessToStore { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PricePoint))]
        public int? PricePointID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual PricePoint? PricePoint { get; set; }
        #endregion
    }
}
