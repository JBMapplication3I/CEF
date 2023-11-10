// <copyright file="InventoryProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Inventory provider base class</summary>
namespace Clarity.Ecommerce.Providers.Inventory
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Inventory;
    using Models;

    /// <summary>An Inventory provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IInventoryProviderBase"/>
    public abstract class InventoryProviderBase : ProviderBase, IInventoryProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Inventory;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<ICalculatedInventory>> CalculateInventoryAsync(
            int productID,
            string productKey,
            string? accountKey,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<Dictionary<int, ICalculatedInventory>>> BulkCalculateInventoryAsync(
            List<(int productID, string productKey)>? productIDsAndKeys,
            string? accountKey,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<decimal>> GetAvailableInventoryCountAsync(
            int productID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<Dictionary<int, decimal>>> GetBulkAvailableInventoryCountAsync(
            List<int> productIDs,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<bool>> CheckHasAnyAvailableInventoryAsync(
            int productID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<Dictionary<int, bool>>> BulkCheckHasAnyAvailableInventoryAsync(
            List<int> productIDs,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> UpdateInventoryForProductAsync(
            int productID,
            decimal? quantity,
            decimal? quantityAllocated,
            decimal? quantityPreSold,
            int? relevantLocationID,
            long? relevantHash,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> BulkUpdateInventoryForProductsAsync(
            List<ICalculatedInventory> inventoryToPush,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ResetAllocatedInventoryForProductIDsAsync(
            List<int> productIDs,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ResetAllocatedInventoryForAllProductsAsync(string? contextProfileName);

        public abstract Task<CEFActionResponse<ICalculatedInventory[]?>> CalculateInventoryForMultipleUOMsAsync(int productID, string productKey, string? accountKey, string? contextProfileName);
    }
}
