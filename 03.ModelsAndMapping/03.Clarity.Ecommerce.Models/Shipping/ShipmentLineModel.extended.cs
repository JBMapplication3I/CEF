// <autogenerated>
// <copyright file="ShipmentLineModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Model Interfaces generated to provide base setups</summary>
// <remarks>This file was auto-generated by Models.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#pragma warning disable 618 // Ignore Obsolete warnings
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    /// <summary>A data transfer model for Shipment Line.</summary>
    public partial class ShipmentLineModel
        : BaseModel
            , IShipmentLineModel
    {
        /// <inheritdoc/>
        public string? Sku { get; set; }

        /// <inheritdoc/>
        public int? ProductID { get; set; }

        /// <inheritdoc/>
        public ProductModel? Product { get; set; }

        /// <inheritdoc/>
        IProductModel? IShipmentLineModel.Product { get => Product; set => Product = (ProductModel?)value; }

        /// <inheritdoc/>
        public decimal? Quantity { get; set; }

        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public int? ShipmentID { get; set; }

        /// <inheritdoc/>
        public ShipmentModel? Shipment { get; set; }

        /// <inheritdoc/>
        IShipmentModel? IShipmentLineModel.Shipment { get => Shipment; set => Shipment = (ShipmentModel?)value; }
    }
}
