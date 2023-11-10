// <copyright file="AccountContact.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account contact class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IAccountContact
        : INameableBase,
          IAmAContactRelationshipTable<Account, AccountContact>,
          IAmAnAccountRelationshipTableWhereAccountIsTheMaster<Contact>
    {
        /// <summary>Gets or sets a value indicating whether this IAccountContact is primary.</summary>
        /// <value>True if this IAccountContact is primary, false if not.</value>
        bool IsPrimary { get; set; }

        /// <summary>Gets or sets a value indicating whether this IAccountContact is billing.</summary>
        /// <value>True if this IAccountContact is billing, false if not.</value>
        bool IsBilling { get; set; }

        /// <summary>Gets or sets a value indicating whether the transmitted to erp.</summary>
        /// <value>True if transmitted to erp, false if not.</value>
        bool TransmittedToERP { get; set; }

        /// <summary>Gets or sets the end date of the account contact.</summary>
        /// <value>The EndDate.</value>
        DateTime? EndDate { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "AccountContact")]
    public class AccountContact : NameableBase, IAccountContact
    {
        #region IAmAContactRelationshipTable<Account>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Account? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual Contact? Slave { get; set; }

        #region IAmAnAccountRelationshipTableWhereAccountIsTheMaster<>
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAnAccountRelationshipTableWhereAccountIsTheMaster<Contact>.AccountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmAnAccountRelationshipTableWhereAccountIsTheMaster<Contact>.Account { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByAccount
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByAccount.AccountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmFilterableByAccount.Account { get => Master; set => Master = value; }
        #endregion

        #region IHaveAContactBase
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IHaveAContactBase.ContactID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Contact? IHaveAContactBase.Contact { get => Slave; set => Slave = value; }
        #endregion
        #endregion

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsPrimary { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsBilling { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool TransmittedToERP { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDate { get; set; }
    }
}
