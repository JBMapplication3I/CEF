// <copyright file="IUpdateInventoryForProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUpdateInventoryForProduct interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for update inventory for product.</summary>
    public interface IUpdateInventoryForProduct
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int ID { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal? Quantity { get; set; }

        /// <summary>Gets or sets the quantity allocated.</summary>
        /// <value>The quantity allocated.</value>
        decimal? QuantityAllocated { get; set; }

        /// <summary>Gets or sets the quantity pre sold.</summary>
        /// <value>The quantity pre sold.</value>
        decimal? QuantityPreSold { get; set; }

        /// <summary>Gets or sets the identifier of the relevant location.</summary>
        /// <value>The identifier of the relevant location.</value>
        int? RelevantLocationID { get; set; }

        /// <summary>Gets or sets the relevant hash.</summary>
        /// <value>The relevant hash.</value>
        long? RelevantHash { get; set; }
    }
}
