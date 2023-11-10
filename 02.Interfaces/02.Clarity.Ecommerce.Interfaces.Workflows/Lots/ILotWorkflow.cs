// <copyright file="ILotWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ILotWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Providers.Payments;

    /// <summary>Interface for lot workflow.</summary>
    public partial interface ILotWorkflow
    {
        /// <summary>Gets the last modified for by IDs result.</summary>
        /// <param name="lotIDs">            The lot IDs.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="isVendorAdmin">     True if this request is for a vendor admin.</param>
        /// <param name="vendorAdminID">     Identifier for the vendor admin.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by IDs result.</returns>
        Task<DateTime?> GetLastModifiedForByIDsResultAsync(
            List<int> lotIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName);

        /// <summary>Gets by IDs.</summary>
        /// <param name="lotIDs">            The lot IDs.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="isVendorAdmin">     True if this request is for a vendor admin.</param>
        /// <param name="vendorAdminID">     Identifier for the vendor admin.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by IDs.</returns>
        Task<List<ILotModel>> GetByIDsAsync(
            List<int> lotIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName);

        /// <summary>Validates the VIN number provided.</summary>
        /// <param name="productID">         The product ID.</param>
        /// <param name="vinNumber">         The VIN Number.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The validation result wrapped in a CEFActionResponse.</returns>
        Task<CEFActionResponse<bool>> ValidateVinNumber(
            int productID,
            string vinNumber,
            string? contextProfileName);
    }
}
