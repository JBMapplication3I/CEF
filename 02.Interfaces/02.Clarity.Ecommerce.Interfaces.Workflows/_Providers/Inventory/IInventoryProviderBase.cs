// <copyright file="IInventoryProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IInventoryProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Inventory
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for Inventory provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IInventoryProviderBase : IProviderBase
    {
        /// <summary>Calculates the inventory.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="productKey">        The product key.</param>
        /// <param name="accountKey">        The account key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated inventory wrapped in a CEFActionResponse.</returns>
        Task<CEFActionResponse<ICalculatedInventory>> CalculateInventoryAsync(
            int productID,
            string productKey,
            string? accountKey,
            string? contextProfileName);

        /// <summary>Bulk calculate inventory.</summary>
        /// <param name="productIDsAndKeys"> The product IDs and keys.</param>
        /// <param name="accountKey">        The account key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{Dictionary{int,ICalculatedInventory}}}.</returns>
        Task<CEFActionResponse<Dictionary<int, ICalculatedInventory>>> BulkCalculateInventoryAsync(
            List<(int productID, string productKey)>? productIDsAndKeys,
            string? accountKey,
            string? contextProfileName);

        /// <summary>Gets available inventory count.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The available inventory count wrapped in a CEFActionResponse.</returns>
        Task<CEFActionResponse<decimal>> GetAvailableInventoryCountAsync(
            int productID,
            string? contextProfileName);

        /// <summary>Gets available inventory counts in bulk.</summary>
        /// <param name="productIDs">        The product IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The available inventory counts in bulk.</returns>
        Task<CEFActionResponse<Dictionary<int, decimal>>> GetBulkAvailableInventoryCountAsync(
            List<int> productIDs,
            string? contextProfileName);

        /// <summary>Check has any available inventory.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{bool}}.</returns>
        Task<CEFActionResponse<bool>> CheckHasAnyAvailableInventoryAsync(
            int productID,
            string? contextProfileName);

        /// <summary>Bulk check has any available inventory.</summary>
        /// <param name="productIDs">        The product IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{Dictionary{int,bool}}}.</returns>
        Task<CEFActionResponse<Dictionary<int, bool>>> BulkCheckHasAnyAvailableInventoryAsync(
            List<int> productIDs,
            string? contextProfileName);

        /// <summary>Updates the inventory for product.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="quantityAllocated"> The quantity allocated.</param>
        /// <param name="quantityPreSold">   The quantity pre sold.</param>
        /// <param name="relevantLocationID">Identifier for the relevant location.</param>
        /// <param name="relevantHash">      The relevant hash.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UpdateInventoryForProductAsync(
            int productID,
            decimal? quantity,
            decimal? quantityAllocated,
            decimal? quantityPreSold,
            int? relevantLocationID,
            long? relevantHash,
            string? contextProfileName);

        /// <summary>Bulk update inventory for products.</summary>
        /// <param name="inventoryToPush">   The inventory to push.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> BulkUpdateInventoryForProductsAsync(
            List<ICalculatedInventory> inventoryToPush,
            string? contextProfileName);

        /// <summary>Resets the allocated inventory for product IDs.</summary>
        /// <param name="productIDs">        The product IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ResetAllocatedInventoryForProductIDsAsync(
            List<int> productIDs,
            string? contextProfileName);

        /// <summary>Resets the allocated inventory for all products.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ResetAllocatedInventoryForAllProductsAsync(string? contextProfileName);

        /// <summary>Caclulates the inventory for each UOM on the product.</summary>
        /// <param name="productID">The product identifier.</param>
        /// <param name="productKey">The product custom key.</param>
        /// <param name="accountKey">The account custom key.</param>
        /// <param name="contextProfileName">The name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{ICalculatedInventory[]?}}.</returns>
        Task<CEFActionResponse<ICalculatedInventory[]?>> CalculateInventoryForMultipleUOMsAsync(
            int productID,
            string productKey,
            string? accountKey,
            string? contextProfileName);
    }
}
