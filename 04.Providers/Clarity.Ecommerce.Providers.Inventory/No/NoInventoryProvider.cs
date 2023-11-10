// <copyright file="NoInventoryProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the no inventory provider class</summary>
namespace Clarity.Ecommerce.Providers.Inventory.No
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using ServiceStack;

    /// <summary>A No inventory provider.</summary>
    /// <seealso cref="InventoryProviderBase"/>
    public class NoInventoryProvider : InventoryProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => NoInventoryProviderConfig.IsValid(IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override Task<CEFActionResponse<ICalculatedInventory>> CalculateInventoryAsync(
            int productID,
            string productKey,
            string? accountKey,
            string? contextProfileName)
        {
            var calculated = RegistryLoaderWrapper.GetInstance<ICalculatedInventory>(contextProfileName);
            calculated.ProductID = productID;
            calculated.IsUnlimitedStock = true;
            return Task.FromResult(calculated.WrapInPassingCEFAR()!)!;
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<Dictionary<int, ICalculatedInventory>>> BulkCalculateInventoryAsync(
            List<(int productID, string productKey)>? productIDsAndKeys,
            string? accountKey,
            string? contextProfileName)
        {
            return Task.FromResult(
                productIDsAndKeys
                    .ToDictionary(x => new KeyValuePair<int, ICalculatedInventory>(
                        x.productID,
                        new CalculatedInventory { ProductID = x.productID, IsUnlimitedStock = true }))
                    .WrapInPassingCEFAR()!)!;
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<decimal>> GetAvailableInventoryCountAsync(
            int productID,
            string? contextProfileName)
        {
            return Task.FromResult(0m.WrapInPassingCEFAR());
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<Dictionary<int, decimal>>> GetBulkAvailableInventoryCountAsync(
            List<int> productIDs,
            string? contextProfileName)
        {
            return Task.FromResult(
                productIDs.ToDictionary(x => new KeyValuePair<int, decimal>(x, 0m)).WrapInPassingCEFAR()!)!;
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<bool>> CheckHasAnyAvailableInventoryAsync(
            int productID,
            string? contextProfileName)
        {
            return Task.FromResult(false.WrapInPassingCEFAR());
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<Dictionary<int, bool>>> BulkCheckHasAnyAvailableInventoryAsync(
            List<int> productIDs,
            string? contextProfileName)
        {
            return Task.FromResult(
                productIDs.ToDictionary(x => new KeyValuePair<int, bool>(x, false)).WrapInPassingCEFAR()!)!;
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> UpdateInventoryForProductAsync(
            int productID,
            decimal? quantity,
            decimal? quantityAllocated,
            decimal? quantityPreSold,
            int? relevantLocationID,
            long? relevantHash,
            string? contextProfileName)
        {
            return Task.FromResult(CEFAR.PassingCEFAR());
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> BulkUpdateInventoryForProductsAsync(
            List<ICalculatedInventory> inventoryToPush,
            string? contextProfileName)
        {
            return Task.FromResult(CEFAR.PassingCEFAR());
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ResetAllocatedInventoryForProductIDsAsync(
            List<int> productIDs,
            string? contextProfileName)
        {
            return Task.FromResult(CEFAR.PassingCEFAR());
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ResetAllocatedInventoryForAllProductsAsync(string? contextProfileName)
        {
            return Task.FromResult(CEFAR.PassingCEFAR());
        }

        public override Task<CEFActionResponse<ICalculatedInventory[]?>> CalculateInventoryForMultipleUOMsAsync(int productID, string productKey, string? accountKey, string? contextProfileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
