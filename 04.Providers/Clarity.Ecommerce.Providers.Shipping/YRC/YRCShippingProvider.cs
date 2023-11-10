// <copyright file="YRCShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tnt shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using Models;

    /// <summary>A YRC shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class YRCShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => YRCShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
                    "ERROR! Line Items are required to get YRC shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get YRC shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get YRC shipping rate quotes");
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
                    "ERROR! Origin is required to get YRC shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get YRC shipping rate quotes");
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "YRC");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            // Generate a YRC Rate Request
            var request = YRCResponseGenerator.CreateYRCRequest(items, origin, destination);
            var response = YRCResponseGenerator.GetYRCResponse(request);
            // Check for Errors
            if (!response.IsSuccess)
            {
                var errorMessage = response.Errors?.Length > 0
                    ? response.Errors[0].Message
                    : "The response code was invalid";
                await Logger.LogErrorAsync("Get YRC Shipping Rates", errorMessage, contextProfileName).ConfigureAwait(false);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"YRC GetRates: {errorMessage}");
            }
            // Parse the response
            var rates = ParseResponse(response);
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID: cartID,
                    hash: hash,
                    currentShippingProvider: "YRC",
                    rates: rates,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return results.WrapInPassingCEFAR();
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
        private static List<IShipmentRate> ParseResponse(YRCResponse response)
        {
            // Check for Minimum Rate Amount requirements and apply if set
            var rate = response.PageRoot?.BodyMain?.RateQuote?.RatedCharges != null
                ? (decimal)response.PageRoot.BodyMain.RateQuote.RatedCharges.TotalCharges / 100
                : 0m;
            if (YRCShippingProviderConfig.UseDefaultMinimumPricing
                && YRCShippingProviderConfig.DefaultMinimumPrice > 0
                && rate < YRCShippingProviderConfig.DefaultMinimumPrice)
            {
                rate = YRCShippingProviderConfig.DefaultMinimumPrice;
            }
            return new()
            {
                new ShipmentRate
                {
                    OptionCode = response.PageRoot?.ProgramId,
                    CarrierName = "YRC",
                    FullOptionName = "YRC Standard LTL",
                    OptionName = "YRC Standard LTL",
                    Rate = rate,
                },
            };
        }
    }
}
