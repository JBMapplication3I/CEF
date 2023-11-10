// <copyright file="InventoryLocationSection.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location section class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IInventoryLocationSection : INameableBase
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the inventory location.</summary>
        /// <value>The identifier of the inventory location.</value>
        int InventoryLocationID { get; set; }

        /// <summary>Gets or sets the inventory location.</summary>
        /// <value>The inventory location.</value>
        InventoryLocation? InventoryLocation { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the product inventory location sections.</summary>
        /// <value>The product inventory location sections.</value>
        ICollection<ProductInventoryLocationSection>? ProductInventoryLocationSections { get; set; }

        /// <summary>Gets or sets the shipments.</summary>
        /// <value>The shipments.</value>
        ICollection<Shipment>? Shipments { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Inventory", "InventoryLocationSection")]
    public class InventoryLocationSection : NameableBase, IInventoryLocationSection
    {
        private ICollection<ProductInventoryLocationSection>? productInventoryLocationSections;
        private ICollection<Shipment>? shipments;

        public InventoryLocationSection()
        {
            productInventoryLocationSections = new HashSet<ProductInventoryLocationSection>();
            shipments = new HashSet<Shipment>();
        }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(InventoryLocation)), DefaultValue(null)]
        public int InventoryLocationID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual InventoryLocation? InventoryLocation { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductInventoryLocationSection>? ProductInventoryLocationSections { get => productInventoryLocationSections; set => productInventoryLocationSections = value; }

        #region Don't map these out
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Shipment>? Shipments { get => shipments; set => shipments = value; }
        #endregion
        #endregion
    }
}
