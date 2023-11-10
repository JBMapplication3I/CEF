// <copyright file="ShipToStoreShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship to store shipping provider class</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Providers.Shipping.ShipToStore
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using Models;

    /// <summary>A ship to store shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class ShipToStoreShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => ShipToStoreShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            string? contextProfileName)
        {
            if (salesItemIDs == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Line Items are required to get Internal Ship to Store shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Internal Ship to Store shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Internal Ship to Store shipping rate quotes");
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR<List<IRateQuoteModel>?>(
                    new(),
                    "NOTE! No items in this cart need to ship")!;
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Origin is required to get Internal Ship to Store shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get Internal Ship to Store shipping rate quotes");
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "ShipToStore");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            // Generate a FedEx Rate Request
            var rates = new List<IShipmentRate>
            {
                new ShipmentRate
                {
                    CarrierName = "Internal",
                    FullOptionName = "Internal Ship to Store",
                    OptionCode = "Ship-to-Store",
                    OptionName = "Ship to Store",
                    Rate = 0, // Free
                },
            };
            // Save the rates to the table to "cache" them with the hash we calculated above
            return (await SaveRateQuotesToTableAndReturnResultsAsync(
                        cartID: cartID,
                        hash: hash,
                        currentShippingProvider: "Internal",
                        rates: rates,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .WrapInPassingCEFAR();
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
