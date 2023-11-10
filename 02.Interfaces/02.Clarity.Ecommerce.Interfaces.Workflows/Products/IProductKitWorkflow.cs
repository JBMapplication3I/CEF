// <copyright file="IProductKitWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductKitWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for product kit workflow.</summary>
    public interface IProductKitWorkflow
    {
        /// <summary>Enumerates kit component bom full in this collection.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process kit component bom full in this collection.</returns>
        Task<IEnumerable<IProductModel>> KitComponentBOMFullAsync(int id, string? contextProfileName);

        /// <summary>Enumerates kit component bom full in this collection.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process kit component bom full in this collection.</returns>
        Task<IEnumerable<IProductModel>> KitComponentBOMFullAsync(string key, string? contextProfileName);

        /// <summary>Enumerates kit component bom up in this collection.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process kit component bom up in this collection.</returns>
        Task<IEnumerable<IProductModel>> KitComponentBOMUpAsync(int id, string? contextProfileName);

        /// <summary>Enumerates kit component bom up in this collection.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process kit component bom up in this collection.</returns>
        Task<IEnumerable<IProductModel>> KitComponentBOMUpAsync(string key, string? contextProfileName);

        /// <summary>Enumerates kit component bom down in this collection.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process kit component bom down in this collection.</returns>
        Task<IEnumerable<IProductModel>> KitComponentBOMDownAsync(int id, string? contextProfileName);

        /// <summary>Enumerates kit component bom down in this collection.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process kit component bom down in this collection.</returns>
        Task<IEnumerable<IProductModel>> KitComponentBOMDownAsync(string key, string? contextProfileName);

        /// <summary>Assemble kit inventory.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="locationSectionID"> Identifier for the location section.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> AssembleKitInventoryAsync(int id, decimal? quantity, int? locationSectionID, string? contextProfileName);

        /// <summary>Assemble kit inventory.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="locationSectionID"> Identifier for the location section.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> AssembleKitInventoryAsync(string key, decimal? quantity, int? locationSectionID, string? contextProfileName);

        /// <summary>Break kit inventory apart.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="locationSectionID"> Identifier for the location section.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> BreakKitInventoryApartAsync(int id, decimal? quantity, int? locationSectionID, string? contextProfileName);

        /// <summary>Break kit inventory apart.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="locationSectionID"> Identifier for the location section.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> BreakKitInventoryApartAsync(string key, decimal? quantity, int? locationSectionID, string? contextProfileName);

        /// <summary>Reassemble broken kit inventory.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="locationSectionID"> Identifier for the location section.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> ReassembleBrokenKitInventoryAsync(int id, decimal? quantity, int? locationSectionID, string? contextProfileName);

        /// <summary>Reassemble broken kit inventory.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="locationSectionID"> Identifier for the location section.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> ReassembleBrokenKitInventoryAsync(string key, decimal? quantity, int? locationSectionID, string? contextProfileName);
    }
}
