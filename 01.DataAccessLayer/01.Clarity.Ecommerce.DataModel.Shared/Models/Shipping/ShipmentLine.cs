// <copyright file="ShipmentLine.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment line class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IShipmentLine : IBase
    {
        /// <summary>Gets or sets the Sku.</summary>
        /// <value>The Sku.</value>
        string? Sku { get; set; }

        /// <summary>Gets or sets the ProductID.</summary>
        /// <value>The product ID.</value>
        int? ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        Product? Product { get; set; }

        /// <summary>Gets or sets the quantity shipped.</summary>
        /// <value>The quantity.</value>
        decimal? Quantity { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        string? Description { get; set; }

        /// <summary>Gets or sets the shipment ID.</summary>
        /// <value>The shipment ID.</value>
        int? ShipmentID { get; set; }

        /// <summary>Gets or sets the shipment.</summary>
        /// <value>The shipment.</value>
        Shipment? Shipment { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Shipping", "ShipmentLine")]
    public class ShipmentLine : Base, IShipmentLine
    {
        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? Sku { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Product)), DefaultValue(null)]
        public int? ProductID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DontMapInEver]
        public virtual Product? Product { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? Quantity { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? Description { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Shipment)), DefaultValue(null)]
        public int? ShipmentID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual Shipment? Shipment { get; set; }
    }
}
