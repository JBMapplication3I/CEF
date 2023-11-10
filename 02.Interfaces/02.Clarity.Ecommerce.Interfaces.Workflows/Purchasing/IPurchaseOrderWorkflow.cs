// <copyright file="IPurchaseOrderWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPurchaseOrderWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for purchase order workflow.</summary>
    public partial interface IPurchaseOrderWorkflow
    {
        /// <summary>Performs a checkout for the Purchase Order.</summary>
        /// <param name="cartID">            Identifier for the cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="vendors">           The vendors.</param>
        /// <returns>A Task.</returns>
        Task CheckoutAsync(
            int cartID,
            string? contextProfileName,
            List<int>? vendors = null);

        /// <summary>Creates from sales order.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="isDropShip">        True if this IPurchaseOrderWorkflow is drop ship.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new from sales order.</returns>
        Task<IPurchaseOrderModel> CreateFromSalesOrderAsync(
            ISalesOrderModel order,
            bool isDropShip,
            string? contextProfileName);
    }
}
