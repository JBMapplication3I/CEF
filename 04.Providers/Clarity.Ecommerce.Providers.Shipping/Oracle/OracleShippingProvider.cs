// <copyright file="OracleShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Oracle
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using Models;

    /// <summary>An oracle shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class OracleShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => OracleShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            string? contextProfileName)
        {
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Oracle shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Oracle shipping rate quotes");
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR(new List<IRateQuoteModel>(), "NOTE! No items in this cart need to ship");
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Origin is required to get Oracle shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get Oracle shipping rate quotes");
            }
            //// Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "Oracle");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes.WrapInPassingCEFAR(
                    "NOTE! These rate quotes were previously provided and stored.");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cartEntity = await context.Carts
               .AsNoTracking()
               .Where(x => x.ID == cartID)
               .SingleAsync()
               .ConfigureAwait(false);
            // Generate an Oracle Rate Request
            var request = OracleResponseGenerator.CreateOracleRequest(origin, destination, cartEntity.SubtotalItems);
            var response = await OracleResponseGenerator.GetOracleResponseAsync(request).ConfigureAwait(false);
            // Parse the response
            var rates = ParseResponse(response);
            // Save the rates to the table to "cache" them with the hash we calculated above
            return (await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID,
                    hash,
                    nameof(OracleShippingProvider),
                    rates,
                    contextProfileName)
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

        /// <summary>Parse response.</summary>
        /// <param name="response">The response.</param>
        /// <returns>A List{IShipmentRate}.</returns>
        private static List<IShipmentRate> ParseResponse(OracleResponse response)
        {
            // Check for Minimum Rate Amount requirements and apply if set
            var rate = response?.ShippingTax?.FirstOrDefault()?.TaxRate;
            if (OracleShippingProviderConfig.UseDefaultMinimumPricing
                && decimal.TryParse(OracleShippingProviderConfig.DefaultMinimumPrice, out var defaultMinimumPrice)
                && defaultMinimumPrice != 0
                && rate < defaultMinimumPrice)
            {
                rate = defaultMinimumPrice;
            }
            return new()
            {
                new ShipmentRate
                {
                    CarrierName = "Oracle",
                    FullOptionName = "Oracle Standard",
                    OptionName = "Oracle Standard",
                    Rate = rate ?? 0m,
                },
            };
        }
    }
}
