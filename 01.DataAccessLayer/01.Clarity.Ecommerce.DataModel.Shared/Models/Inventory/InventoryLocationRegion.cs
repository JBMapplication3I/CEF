// <copyright file="InventoryLocationRegion.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location region class, which
// describes a relationship between an InventoryLocation and a Region</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Clarity.Ecommerce.DataModel;

    public interface IInventoryLocationRegion
        : IAmARelationshipTable<InventoryLocation, Region>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Inventory", "InventoryLocationRegion")]
    public class InventoryLocationRegion : Base, IInventoryLocationRegion
    {
        #region Inventory Location as Master
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual InventoryLocation? Master { get; set; }
        #endregion

        #region Region as Slave
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave))]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual Region? Slave { get; set; }
        #endregion
    }
}
