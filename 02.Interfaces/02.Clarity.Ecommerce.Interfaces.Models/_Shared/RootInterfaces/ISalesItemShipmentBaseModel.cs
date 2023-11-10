// <copyright file="ISalesItemShipmentBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesItemShipmentBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for sales item shipment base model.</summary>
    public interface ISalesItemShipmentBaseModel
        : IAmARelationshipTableBaseModel<IShipmentModel>
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal Quantity { get; set; }
    }
}
