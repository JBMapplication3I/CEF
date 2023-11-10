﻿// <copyright file="AccountUsageBalance.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account usage balance class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for account.</summary>
    public interface IAccountUsageBalance
        : IAmAProductRelationshipTableWhereProductIsTheSlave<Account>,
          IAmAnAccountRelationshipTableWhereAccountIsTheMaster<Product>
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        int Quantity { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "AccountUsageBalance")]
    public class AccountUsageBalance : Base, IAccountUsageBalance
    {
        #region IAmAProductRelationshipTableWhereProductIsTheSlave<Account>
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Account? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByAccount.AccountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmFilterableByAccount.Account { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAnAccountRelationshipTableWhereAccountIsTheMaster<Product>.AccountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmAnAccountRelationshipTableWhereAccountIsTheMaster<Product>.Account { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? Slave { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheSlave<Account>.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheSlave<Account>.Product { get => Slave; set => Slave = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Slave; set => Slave = value; }
        #endregion

        /// <inheritdoc/>
        public int Quantity { get; set; }
    }
}
