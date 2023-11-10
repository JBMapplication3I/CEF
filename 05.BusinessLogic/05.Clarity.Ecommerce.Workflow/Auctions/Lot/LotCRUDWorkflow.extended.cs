// <copyright file="LotCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account association workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;

    public partial class LotWorkflow
    {
        /// <inheritdoc/>
        public async Task<DateTime?> GetLastModifiedForByIDsResultAsync(
            List<int> lotIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Lots
                .AsNoTracking()
                .FilterByIDs(lotIDs)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .FirstAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<ILotModel>> GetByIDsAsync(
            List<int> lotIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // await CleanProductsAsync(productIDs.ToArray(), context).ConfigureAwait(false);
            var lots = new List<ILotModel>();
            foreach (var lotID in lotIDs)
            {
                var p = await GetAsync(lotID, contextProfileName).ConfigureAwait(false);
                if (p is null)
                {
                    continue;
                }
                lots.Add(p);
            }
            return lots;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<bool>> ValidateVinNumber(
            int productID,
            string vinNumber,
            string? contextProfileName)
        {
            var product = await Workflows.Products.GetAsync(
                    productID,
                    contextProfileName)
                .ConfigureAwait(false);
            if (product == null)
            {
                return CEFAR.FailingCEFAR<bool>("No product was found with the productID provided.");
            }
            var provider = RegistryLoaderWrapper.GetVinLookupProvider(contextProfileName);
            if (provider?.HasValidConfiguration != true)
            {
                return CEFAR.FailingCEFAR<bool>("No VIN Lookup Provider Configured.");
            }
            var result = await provider.ValidateVinAsync(vinNumber, contextProfileName).ConfigureAwait(false);
            if (result == null)
            {
                return CEFAR.FailingCEFAR<bool>("Result from ValidateVin was NULL.");
            }
            if (result.ActionSucceeded)
            {
                product.Description = vinNumber;
                await Workflows.Products.UpdateAsync(product, contextProfileName).ConfigureAwait(false);
            }
            return result;
        }
    }
}
