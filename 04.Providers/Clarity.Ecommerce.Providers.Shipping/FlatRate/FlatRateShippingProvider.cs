// <copyright file="FlatRateShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat rate shipping provider class</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Providers.Shipping.FlatRate
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Shipping;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A flat rate shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class FlatRateShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => FlatRateShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

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
                    "ERROR! Line Items are required to get Flat Rate shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Flat Rate shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Flat Rate shipping rate quotes");
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
                    "ERROR! Origin is required to get Flat Rate shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get Flat Rate shipping rate quotes");
            }
            var itemCount = GetItemCount(items);
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "FlatRate");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes.WrapInPassingCEFAR(
                    "NOTE! These rate quotes were previously provided and stored.");
            }
            // Get the rates from the settings in JSON and deserialize into ShipmentRates
            //var setting = (await Workflows.Settings.GetSettingByTypeNameAsync("FlatRateShipping", contextProfileName).ConfigureAwait(false))
            //    .FirstOrDefault()
            //    ?.Value;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var billingOnCartAttributes = (await context.Carts
                    .AsNoTracking()
                    .FilterByID(cartID)
                    .Select(x => x.BillingContact!.JsonAttributes ?? string.Empty)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false))
                .DeserializeAttributesDictionary();
            string? selectedShippingRate = null;
            if (destination.SerializableAttributes.TryGetValue("defaultShipMethod", out var contactShipping)
                && Contract.CheckValidKey(contactShipping.Value))
            {
                selectedShippingRate = contactShipping.Value;
            }
            if (Contract.CheckNotEmpty(billingOnCartAttributes)
                && billingOnCartAttributes.TryGetValue("defaultShipMethod", out var billToShipping)
                && Contract.CheckAnyInvalidKey(selectedShippingRate))
            {
                selectedShippingRate = billToShipping.Value;
            }
            var rawShipmentRates = (await context.Settings
                    .Where(x => x.Type!.Name == "FlatRateShipping")
                    .ToListAsync()
                .ConfigureAwait(false))
                .Select(x => new ShipmentRate
                {
                    FullOptionName = x.CustomKey,
                    CarrierName = x.CustomKey!.ToLower().Contains("fedex")
                        ? "FedEx"
                        : "JBM",
                    OptionName = x.CustomKey,
                    OptionCode = x.CustomKey,
                    Rate = 0.0m,
                    EstimatedArrival = null,
                })
                .ToList();
            var shipmentRates = new ShipmentRates
            {
                Domestic = Contract.CheckNotEmpty(rawShipmentRates)
                    ? rawShipmentRates
                    : new()
                    {
                        new()
                    {
                        FullOptionName = "Standard Our Carrier $9.99",
                        CarrierName = "Our Carrier",
                        OptionName = "Standard",
                        OptionCode = "STD",
                        Rate = 9.99m,
                        EstimatedArrival = null,
                    },
                        new()
                        {
                            FullOptionName = "Overnight Our Carrier $24.99",
                            CarrierName = "Our Carrier",
                            OptionName = "Overnight",
                            OptionCode = "1DAY",
                            Rate = 24.99m,
                            EstimatedArrival = null,
                        },
                    },
            };
            var rates = new List<IShipmentRate>();
            List<ShipmentRate> customerRates;
            switch (destination.Address!.CountryCode)
            {
                case "US":
                case "USA":
                {
                    customerRates = shipmentRates.Domestic ?? new List<ShipmentRate>();
                    break;
                }
                case "CA":
                case "CAN":
                {
                    customerRates = shipmentRates.Canada ?? new List<ShipmentRate>();
                    break;
                }
                default:
                {
                    customerRates = shipmentRates.International ?? new List<ShipmentRate>();
                    break;
                }
            }
            rates.AddRange(await Task.WhenAll(
                    customerRates.Select(async x => new ShipmentRate
                    {
                        CarrierName = x.CarrierName,
                        FullOptionName = x.FullOptionName,
                        OptionCode = x.OptionCode,
                        OptionName = x.OptionName,
                        Rate = 0.0m,
                    }))
                .ConfigureAwait(false));
            // Save the rates to the table to "cache" them with the hash we calculated above
            return (await SaveRateQuotesToTableAndReturnResultsAsync(
                        cartID: cartID,
                        hash: hash,
                        currentShippingProvider: nameof(FlatRateShippingProvider),
                        rates: rates,
                        contextProfileName: contextProfileName,
                        selectedRate: selectedShippingRate)
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
            throw new NotImplementedException();
        }

        /// <summary>Gets the number of items in the cart from a list of IProviderShipment items.</summary>
        /// <param name="items">List of IProviderShipment items.</param>
        /// <returns>The number of items in the cart.</returns>
        private static decimal GetItemCount(IEnumerable<IProviderShipment>? items)
        {
            // get the total package count
            return items?.Sum(item => item.PackageQuantity ?? 0m) ?? 0m;
        }

        /// <summary>Gets the number of items in the cart from a list of IProviderShipment items.</summary>
        /// <param name="itemCount">         Number of items in the cart.</param>
        /// <param name="rate">              The rate for the specified shipment term.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The rate based on the package threshold and number of items in the cart.</returns>
        private static async Task<decimal> GetRateByPackageThresholdAsync(decimal itemCount, decimal rate, string? contextProfileName)
        {
            // calculate the rate based on the number of packages and the threshold
            var rateMultiplier = 1m;
            if (decimal.TryParse(
                    (await Workflows.Settings.GetSettingByTypeNameAsync("PackageThreshold", contextProfileName).ConfigureAwait(false)).FirstOrDefault()?.Value,
                    out var threshold)
                && threshold != 0)
            {
                rateMultiplier = Math.Ceiling(itemCount / threshold);
            }
            return rate * rateMultiplier;
        }
    }
}
