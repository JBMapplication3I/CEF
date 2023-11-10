// <copyright file="VendorAccount.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor Account class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IVendorAccount
        : IAmAVendorRelationshipTableWhereVendorIsTheMaster<Account>,
          IAmAnAccountRelationshipTableWhereAccountIsTheSlave<Vendor>
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

    [SqlSchema("Vendors", "VendorAccount")]
    public class VendorAccount : Base, IVendorAccount
    {
        #region IAmARelationshipTable<Vendor, Account>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Vendor? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use MasterID instead.", true)]
        int IAmFilterableByVendor.VendorID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use Master instead.", true)]
        Vendor? IAmFilterableByVendor.Vendor { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use MasterID instead.", true)]
        int IAmAVendorRelationshipTableWhereVendorIsTheMaster<Account>.VendorID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use Master instead.", true)]
        Vendor? IAmAVendorRelationshipTableWhereVendorIsTheMaster<Account>.Vendor { get => Master; set => Master = value; }

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
        int IAmAnAccountRelationshipTableWhereAccountIsTheSlave<Vendor>.AccountID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Account? IAmAnAccountRelationshipTableWhereAccountIsTheSlave<Vendor>.Account { get => Slave; set => Slave = value; }
        #endregion
    }
}
