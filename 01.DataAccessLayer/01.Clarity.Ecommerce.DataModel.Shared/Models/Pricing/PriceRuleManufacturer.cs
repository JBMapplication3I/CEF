// <copyright file="PriceRuleManufacturer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule manufacturer class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for price rule manufacturer.</summary>
    public interface IPriceRuleManufacturer
        : IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<PriceRule>
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

    [SqlSchema("Pricing", "PriceRuleManufacturer")]
    public class PriceRuleManufacturer : Base, IPriceRuleManufacturer
    {
        #region IAmARelationshipTable<PriceRule, Manufacturer>
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
        public virtual Manufacturer? Slave { get; set; }
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
        int IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<PriceRule>.ManufacturerID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Manufacturer? IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<PriceRule>.Manufacturer { get => Slave; set => Slave = value; }
        #endregion
    }
}
