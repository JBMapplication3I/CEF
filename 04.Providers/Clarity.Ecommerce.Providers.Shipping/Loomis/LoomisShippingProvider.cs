// <copyright file="LoomisShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the loomis shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Loomis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using JSConfigs;
    using LoomisService;
    using Models;
    using Utilities;

    /// <summary>The loomis shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class LoomisShippingProvider : ShippingProviderBase
    {
        /// <summary>The services code.</summary>
        private static readonly IDictionary<string, string> ServicesCode = new Dictionary<string, string>
        {
            { "GRD", "Loomis Ground" },
            { "EXG1800", "Loomis Express 18:00" },
            { "EXG1200", "Loomis Express 12:00" },
            { "EXG900", "Loomis Express 9:00" },
        };

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => LoomisShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Line Items are required to get Loomis shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get Loomis shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get Loomis shipping rate quotes");
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR(new List<IRateQuoteModel>(), "NOTE! No items in this cart need to ship");
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Origin is required to get Loomis shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Destination is required to get Loomis shipping rate quotes");
            }
            var packageArray = items
                .Select(item => new Package
                {
                    reported_weightSpecified = true,
                    reported_weight = item.Weight,
                    billed_weight = item.Weight,
                    dim_weight = item.Weight,
                })
                .ToArray();
            destination.Address!.PostalCode = destination.Address.PostalCode!.Replace(" ", string.Empty).Replace("-", string.Empty);
            origin.Address!.PostalCode = origin.Address.PostalCode!.Replace(" ", string.Empty).Replace("-", string.Empty);
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "Loomis");

            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            // Generate a Loomis Rate Request
            var request = GenerateRateRequest(origin, destination);
            request.shipment.packages = packageArray;
            // Run the Request
            GetRatesRs response;
            using (var service = new USSRatingServicePortTypeClient())
            {
                try
                {
                    response = service.getRates(request);
                }
                catch (Exception ex)
                {
                    await Logger.LogErrorAsync("Get Loomis Shipping Rates", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                    return new List<IRateQuoteModel>().WrapInFailingCEFAR($"Loomis GetRates: {ex.Message}");
                }
            }
            // Check for Errors
            if (Contract.CheckValidKey(response.error))
            {
                await Logger.LogErrorAsync("Get Loomis Shipping Rates", response.error, contextProfileName).ConfigureAwait(false);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"Loomis GetRates: {response.error}");
            }
            // Parse the response
            var rates = response.getRatesResult
                .Where(r => r?.shipment?.shipment_info_num != null)
                .Select(MapToShipmentRate)
                .ToList();
            // Check for Minimum Rate Amount requirements and apply if set
            if (LoomisShippingProviderConfig.UseDefaultMinimumPricing
                && decimal.TryParse(LoomisShippingProviderConfig.DefaultMinimumPrice, out var defaultMinimumPrice)
                && defaultMinimumPrice != 0)
            {
                foreach (var rate in rates.Where(x => x.Rate < defaultMinimumPrice))
                {
                    rate.Rate = defaultMinimumPrice;
                }
            }
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(cartID, hash, "Loomis", rates, contextProfileName).ConfigureAwait(false);
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
            throw new NotImplementedException();
        }

        /// <summary>Map to shipment rate.</summary>
        /// <param name="result">The result.</param>
        /// <returns>An IShipmentRate.</returns>
        private static IShipmentRate MapToShipmentRate(GetRatesResult result)
        {
            var rate = new ShipmentRate { CarrierName = "Loomis" };
            if (result.shipment.shipment_info_num[1].value != null)
            {
                // Total charges
                rate.Rate = result.shipment.shipment_info_num[1].value!.Value;
            }
            if (result.shipment.shipment_info_str[0].value == null)
            {
                return rate;
            }
            rate.OptionCode = result.shipment.shipment_info_str[1].value;
            rate.OptionName = ServicesCode.ContainsKey(rate.OptionCode)
                ? ServicesCode[rate.OptionCode]
                : string.Empty;
            return rate;
        }

        /// <summary>Query if 'destination' is residential.</summary>
        /// <param name="destination">Destination for the.</param>
        /// <param name="origin">     The origin.</param>
        /// <returns>true if residential, false if not.</returns>
        private static bool IsResidential(IAddressModel destination, IAddressModel origin)
        {
            return destination.CountryCode == origin.CountryCode
                && destination.CountryCode == CEFConfigDictionary.CountryCode;
        }

        /// <summary>Generates a request.</summary>
        /// <param name="origin">     The origin.</param>
        /// <param name="destination">Destination for the.</param>
        /// <returns>The request.</returns>
        private static GetRatesRq GenerateRateRequest(IContactModel origin, IContactModel destination) =>
            new()
            {
                user_id = LoomisShippingProviderConfig.UserName,
                password = LoomisShippingProviderConfig.Password,
                shipment = new()
                {
                    user_id = LoomisShippingProviderConfig.UserName,
                    courier = "L",
                    // delivery info
                    delivery_address_line_1 = destination.Address!.Street1,
                    delivery_address_line_2 = destination.Address.Street2,
                    delivery_address_line_3 = destination.Address.Street3,
                    delivery_city = destination.Address.City,
                    delivery_country = destination.Address.CountryCode,
                    delivery_email = destination.Email1,
                    delivery_name = destination.Email1,
                    delivery_postal_code = destination.Address.PostalCode?.Trim(),
                    delivery_province = destination.Address.RegionCode,
                    delivery_residential = IsResidential(destination.Address, origin.Address!),
                    dimension_unit = "I",
                    // origin info
                    pickup_address_line_1 = origin.Address!.Street1,
                    pickup_address_line_2 = origin.Address.Street2,
                    pickup_address_line_3 = origin.Address.Street3,
                    pickup_city = origin.Address.City,
                    pickup_email = origin.Email1,
                    pickup_name = origin.FullName ?? $"{origin.FirstName} {origin.LastName}",
                    pickup_phone = origin.Phone1,
                    pickup_postal_code = origin.Address.PostalCode!.Trim(),
                    pickup_province = origin.Address.RegionCode,
                    // package info
                    reported_weight_unit = "L",
                    service_type = "ALL",
                    shipper_num = LoomisShippingProviderConfig.AccountNumber,
                    shipping_date = DateExtensions.GenDateTime.ToString("yyyyMMdd"),
                },
            };
    }
}
