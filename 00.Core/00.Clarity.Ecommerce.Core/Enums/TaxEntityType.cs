// <copyright file="TaxEntityType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax entity type class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent tax entity types.</summary>
    public enum TaxEntityType
    {
        /// <summary>An enum constant representing the cart option.</summary>
        Cart,

        /// <summary>An enum constant representing the sales order option.</summary>
        SalesOrder,

        /// <summary>An enum constant representing the purchase order option.</summary>
        PurchaseOrder,

        /// <summary>An enum constant representing the return option.</summary>
        Return,
    }
}
