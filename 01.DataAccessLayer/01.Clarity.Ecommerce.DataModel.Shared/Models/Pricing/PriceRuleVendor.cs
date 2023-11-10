// <copyright file="PriceRuleVendor.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule vendor class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IPriceRuleVendor
        : IAmAVendorRelationshipTableWhereVendorIsTheSlave<PriceRule>
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

    [SqlSchema("Pricing", "PriceRuleVendor")]
    public class PriceRuleVendor : Base, IPriceRuleVendor
    {
        #region IAmARelationshipTable<PriceRule, Vendor>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual PriceRule? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Vendor? Slave { get; set; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByVendor.VendorID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Vendor? IAmFilterableByVendor.Vendor { get => Slave; set => Slave = value; }
        #endregion

        #region IAmAVendorRelationshipTableWhereVendorIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAVendorRelationshipTableWhereVendorIsTheSlave<PriceRule>.VendorID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Vendor? IAmAVendorRelationshipTableWhereVendorIsTheSlave<PriceRule>.Vendor { get => Slave; set => Slave = value; }
        #endregion
    }
}
