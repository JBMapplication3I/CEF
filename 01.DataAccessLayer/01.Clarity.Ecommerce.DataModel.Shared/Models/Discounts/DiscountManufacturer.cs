// <copyright file="DiscountManufacturer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount manufacturer class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IDiscountManufacturer
        : IAmADiscountFilterRelationshipTable<Manufacturer>,
            IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<Discount>
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

    [SqlSchema("Discounts", "DiscountManufacturer")]
    public class DiscountManufacturer : Base, IDiscountManufacturer
    {
        #region IAmADiscountFilterRelationshipTable<Store>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual Discount? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
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
        int IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<Discount>.ManufacturerID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Manufacturer? IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<Discount>.Manufacturer { get => Slave; set => Slave = value; }
        #endregion
    }
}
