// <copyright file="CombinedShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the combined shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Combined
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using Models;

    /// <summary>A combined shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class CombinedShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => CombinedShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            string? contextProfileName)
        {
            CEFActionResponse<List<IRateQuoteModel>?> response;
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Combined shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return itemsResult.ChangeFailingCEFARType<List<IRateQuoteModel>?>();
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR(new List<IRateQuoteModel>(), "NOTE! No items in this cart need to ship");
            }
            var combinedPackage = CombinePackages(items, false, contextProfileName);
            if (combinedPackage is null || combinedPackage.Weight < 70)
            {
                var upsProvider = new UPS.UPSShippingProvider();
                response = await upsProvider.GetRateQuotesAsync(cartID, salesItemIDs, origin, destination, expedited, contextProfileName).ConfigureAwait(false);
            }
            else
            {
                var fedexProvider = new FedEx.FedExShippingProvider();
                response = await fedexProvider.GetRateQuotesAsync(cartID, salesItemIDs, origin, destination, expedited, contextProfileName).ConfigureAwait(false);
            }
            return response;
        }

        /// <inheritdoc/>
        public override Task<List<IShipmentRate>> GetBaseOrNetChargesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            bool useBaseCharge,
            string? contextProfileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
