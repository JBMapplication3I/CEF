// <copyright file="VendorManufacturer.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor manufacturer class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IVendorManufacturer
        : IAmAVendorRelationshipTableWhereVendorIsTheMaster<Manufacturer>,
            IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<Vendor>
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

    [SqlSchema("Vendors", "VendorManufacturer")]
    public class VendorManufacturer : Base, IVendorManufacturer
    {
        #region IAmARelationshipTable<Vendor, Manufacturer>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Vendor? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Manufacturer? Slave { get; set; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByVendor.VendorID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        Vendor? IAmFilterableByVendor.Vendor { get => Master; set => Master = value; }
        #endregion

        #region IAmAVendorRelationshipTableWhereVendorIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAVendorRelationshipTableWhereVendorIsTheMaster<Manufacturer>.VendorID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Vendor? IAmAVendorRelationshipTableWhereVendorIsTheMaster<Manufacturer>.Vendor { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByManufacturer
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByManufacturer.ManufacturerID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Manufacturer? IAmFilterableByManufacturer.Manufacturer { get => Slave; set => Slave = value; }
        #endregion

        #region IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<Vendor>.ManufacturerID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Manufacturer? IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<Vendor>.Manufacturer { get => Slave; set => Slave = value; }
        #endregion
    }
}
