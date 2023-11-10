// <copyright file="IProductInventoryLocationSectionWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductInventoryLocationSectionWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for product inventory location section workflow.</summary>
    public partial interface IProductInventoryLocationSectionWorkflow
    {
        /// <summary>Creates a many.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> CreateManyAsync(List<IProductInventoryLocationSectionModel> models, string? contextProfileName);

        /// <summary>Updates the many described by models.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> UpdateManyAsync(List<IProductInventoryLocationSectionModel> models, string? contextProfileName);

        /// <summary>Resets the inventory by store.</summary>
        /// <param name="storeKey">          The store key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ResetInventoryByStoreAsync(string storeKey, string? contextProfileName);

        /// <summary>Check kit component inventory.</summary>
        /// <param name="kitProductID">      Identifier for the kit product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> CheckKitComponentInventoryAsync(int? kitProductID, string? contextProfileName);

        /// <summary>Gets kit component inventory.</summary>
        /// <param name="kitProductID">      Identifier for the kit product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The kit component inventory.</returns>
        Task<(decimal? inventory, bool allowBackOrder, bool allowPreSale, DateTime? preSaleEnd)> GetKitComponentInventoryAsync(
            int? kitProductID,
            string? contextProfileName);

        /// <summary>Check product inventory.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> CheckProductInventoryAsync(int? productID, string? contextProfileName);

        /// <summary>Gets product inventory.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product inventory.</returns>
        Task<(decimal? inventory, bool allowBackOrder, bool allowPreSale, DateTime? preSaleEnd)> GetProductInventoryAsync(
            int? productID,
            string? contextProfileName);

        /// <summary>Searches the catalogs in this collection.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process the catalogs in this collection.</returns>
        Task<IEnumerable<IProductInventoryLocationSectionModel>> SearchForCatalogAsync(
            IProductInventoryLocationSectionSearchModel search,
            string? contextProfileName);

        /// <summary>Gets closest warehouse by region code.</summary>
        /// <param name="regionCode">        The region code.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The closest warehouse by region code.</returns>
        IProductInventoryLocationSectionModel? GetClosestWarehouseByRegionCode(
            string regionCode,
            int productID,
            string? contextProfileName);

        /// <summary>Gets warehouse contact.</summary>
        /// <param name="regionCode">        The region code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The warehouse contact.</returns>
        IContactModel? GetWarehouseContact(string regionCode, string? contextProfileName);
    }
}
