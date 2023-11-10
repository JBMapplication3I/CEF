// <copyright file="AccountPricePoint.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account price point class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAccountPricePoint
        : IAmAnAccountRelationshipTableWhereAccountIsTheMaster<PricePoint>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "AccountPricePoint")]
    public class AccountPricePoint : Base, IAccountPricePoint
    {
        #region IAmARelationshipTable<Account, PricePoint>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Account? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByAccount.AccountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmFilterableByAccount.Account { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAnAccountRelationshipTableWhereAccountIsTheMaster<PricePoint>.AccountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmAnAccountRelationshipTableWhereAccountIsTheMaster<PricePoint>.Account { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual PricePoint? Slave { get; set; }
        #endregion
    }
}
