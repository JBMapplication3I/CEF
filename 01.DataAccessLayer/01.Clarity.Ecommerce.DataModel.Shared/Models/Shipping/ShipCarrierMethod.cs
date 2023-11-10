// <copyright file="ShipCarrierMethod.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship carrier method class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IShipCarrierMethod : INameableBase
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the ship carrier.</summary>
        /// <value>The identifier of the ship carrier.</value>
        int ShipCarrierID { get; set; }

        /// <summary>Gets or sets the ship carrier.</summary>
        /// <value>The ship carrier.</value>
        ShipCarrier? ShipCarrier { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the product ship carrier methods.</summary>
        /// <value>The product ship carrier methods.</value>
        ICollection<ProductShipCarrierMethod>? ProductShipCarrierMethods { get; set; }
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

    [SqlSchema("Shipping", "ShipCarrierMethod")]
    public class ShipCarrierMethod : NameableBase, IShipCarrierMethod
    {
        private ICollection<ProductShipCarrierMethod>? productShipCarrierMethods;

        public ShipCarrierMethod()
        {
            productShipCarrierMethods = new HashSet<ProductShipCarrierMethod>();
        }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ShipCarrier)), DefaultValue(0)]
        public int ShipCarrierID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual ShipCarrier? ShipCarrier { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductShipCarrierMethod>? ProductShipCarrierMethods { get => productShipCarrierMethods; set => productShipCarrierMethods = value; }
        #endregion
    }
}
