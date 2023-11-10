// <copyright file="RateByAttributesShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Rate By Attribute shipping provider class</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Providers.Shipping.RateByAttributes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A Rate By Attribute shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class RateByAttributesShippingProvider : ShippingProviderBase
    {
        private const string ShippingOptionSelectedAttributeName = "ShippingOptionSelected";

        private const string Tier1PriceAttributeName = "Tier1Price";
        private const string Tier2PriceAttributeName = "Tier2Price";
        private const string Tier3PriceAttributeName = "Tier3Price";
        private const string Tier4PriceAttributeName = "Tier4Price";
        private const string Tier5PriceAttributeName = "Tier5Price";

        private const string Tier1RateAttributeName = "Tier1Rate";
        private const string Tier2RateAttributeName = "Tier2Rate";
        private const string Tier3RateAttributeName = "Tier3Rate";
        private const string Tier4RateAttributeName = "Tier4Rate";
        private const string Tier5RateAttributeName = "Tier5Rate";

        private const string ProductRateAttributeName = "ProductRate";

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => RateByAttributesShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
            #region Error Checking
            if (salesItemIDs == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Line Items are required to get Rate By Attribute shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Rate By Attribute shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Rate By Attribute shipping rate quotes");
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
                    "ERROR! Origin is required to get Rate By Attribute shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get Rate By Attribute shipping rate quotes");
            }
            #endregion

            var rates = new List<IShipmentRate>();

            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "RateByAttributes");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes.WrapInPassingCEFAR(
                    "NOTE! These rate quotes were previously provided and stored.");
            }

            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var rateInfo = await (
                from ci in context.CartItems
                join p in context.Products on ci.ProductID equals p.ID
                join sp in context.StoreProducts on p.ID equals sp.SlaveID
                join s in context.Stores on sp.MasterID equals s.ID
                where p.Active && sp.Active && s.Active && ci.Active && ci.MasterID == cartID
                select new
                {
                    sJsonAttributes = s.JsonAttributes,
                    pJsonAttributes = p.JsonAttributes,
                    ciPrice = ci.UnitSoldPrice,
                    ciQuantity = ci.TotalQuantity,
                })
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            decimal rate = 0;
            SerializableAttributesDictionary? storeAttributes = null;
            if (rateInfo.Count == 0 || rateInfo[0].sJsonAttributes == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Store " + nameof(ShippingOptionSelectedAttributeName) + " not set.");
            }
            storeAttributes = rateInfo[0].sJsonAttributes.DeserializeAttributesDictionary();
            if (!storeAttributes.ContainsKey(ShippingOptionSelectedAttributeName)
                || string.IsNullOrEmpty(storeAttributes[ShippingOptionSelectedAttributeName].Value))
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Store " + nameof(ShippingOptionSelectedAttributeName) + " not set.");
            }
            #region Find Store Price/Rate Tiers
            var storePriceRatesByPriceTier = new List<(decimal, decimal)>();
            if (storeAttributes[ShippingOptionSelectedAttributeName].Value.ToLower() == "rate table")
            {
                if (storeAttributes.ContainsKey(Tier1PriceAttributeName)
                    && storeAttributes.ContainsKey(Tier1RateAttributeName)
                    && decimal.TryParse(storeAttributes[Tier1PriceAttributeName].Value, out var tier1Price)
                    && tier1Price > 0
                    && storeAttributes.ContainsKey(Tier1RateAttributeName)
                    && decimal.TryParse(storeAttributes[Tier1PriceAttributeName].Value, out var tier1Rate))
                {
                    storePriceRatesByPriceTier.Add((tier1Price, tier1Rate));
                }
                if (storePriceRatesByPriceTier.Count == 1 && storeAttributes.ContainsKey(Tier2PriceAttributeName)
                    && storeAttributes.ContainsKey(Tier2RateAttributeName)
                    && decimal.TryParse(storeAttributes[Tier2PriceAttributeName].Value, out var tier2Price)
                    && tier2Price > 0
                    && decimal.TryParse(storeAttributes[Tier2PriceAttributeName].Value, out var tier2Rate))
                {
                    storePriceRatesByPriceTier.Add((tier2Price, tier2Rate));
                }
                if (storePriceRatesByPriceTier.Count == 2 && storeAttributes.ContainsKey(Tier3PriceAttributeName)
                    && storeAttributes.ContainsKey(Tier3RateAttributeName)
                    && decimal.TryParse(storeAttributes[Tier3PriceAttributeName].Value, out var tier3Price)
                    && tier3Price > 0
                    && decimal.TryParse(storeAttributes[Tier3PriceAttributeName].Value, out var tier3Rate))
                {
                    storePriceRatesByPriceTier.Add((tier3Price, tier3Rate));
                }
                if (storePriceRatesByPriceTier.Count == 3 && storeAttributes.ContainsKey(Tier4PriceAttributeName)
                    && storeAttributes.ContainsKey(Tier4RateAttributeName)
                    && decimal.TryParse(storeAttributes[Tier4PriceAttributeName].Value, out var tier4Price)
                    && tier4Price > 0
                    && decimal.TryParse(storeAttributes[Tier4PriceAttributeName].Value, out var tier4Rate))
                {
                    storePriceRatesByPriceTier.Add((tier4Price, tier4Rate));
                }
                if (storePriceRatesByPriceTier.Count == 4 && storeAttributes.ContainsKey(Tier5PriceAttributeName)
                    && storeAttributes.ContainsKey(Tier5RateAttributeName)
                    && decimal.TryParse(storeAttributes[Tier5PriceAttributeName].Value, out var tier5Price)
                    && tier5Price > 0
                    && decimal.TryParse(storeAttributes[Tier5PriceAttributeName].Value, out var tier5Rate))
                {
                    storePriceRatesByPriceTier.Add((tier5Price, tier5Rate));
                }
            }
            #endregion
            switch (storeAttributes[ShippingOptionSelectedAttributeName].Value.ToLower())
            {
                case "per item":
                {
                    for (var i = 0; i < rateInfo.Count; i++)
                    {
                        var productAttributes = rateInfo[i].pJsonAttributes.DeserializeAttributesDictionary();
                        if (productAttributes.ContainsKey(ProductRateAttributeName)
                            && decimal.TryParse(storeAttributes[ProductRateAttributeName].Value, out var productRate))
                        {
                            rate += productRate * rateInfo[i].ciQuantity;
                        }
                        else
                        {
                            return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                                "ERROR! Product " + nameof(ProductRateAttributeName) + " not set.");
                        }
                    }
                    break;
                }
                case "rate table":
                {
                    var orderSum = rateInfo.Sum(x => x.ciPrice * x.ciQuantity);
                    var found = false;
                    for (var j = 0; !found && j < storePriceRatesByPriceTier.Count; j++)
                    {
                        if (orderSum <= storePriceRatesByPriceTier[j].Item1 || storePriceRatesByPriceTier.Count == j + 1)
                        {
                            rate += storePriceRatesByPriceTier[j].Item2;
                            found = true;
                        }
                    }
                    break;
                }
                case "free":
                {
                    rate = 0;
                    break;
                }
                default:
                {
                    return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                        "ERROR! Store " + nameof(ShippingOptionSelectedAttributeName) + " not set.");
                }
            }
            // Get the rates from the settings in JSON and deserialize into ShipmentRates
            var setting = (await Workflows.Settings.GetSettingByTypeNameAsync("RateByAttributes", contextProfileName).ConfigureAwait(false))
                .FirstOrDefault()
                ?.Value;
            var shipmentRates = Contract.CheckValidKey(setting)
                ? JsonConvert.DeserializeObject<ShipmentRates>(setting!)!
                : new()
                {
                    Domestic = new()
                    {
                        new()
                        {
                            FullOptionName = "Standard Rate " + rate.ToString("C2"),
                            CarrierName = "Standard Carrier",
                            OptionName = "Standard",
                            OptionCode = "STD",
                            Rate = rate,
                            EstimatedArrival = null,
                        },
                    },
                };
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
            rates.AddRange(customerRates.Select(x => new ShipmentRate
            {
                CarrierName = x.CarrierName,
                FullOptionName = x.FullOptionName,
                OptionCode = x.OptionCode,
                OptionName = x.OptionName,
                Rate = x.Rate,
            }));
            // Save the rates to the table to "cache" them with the hash we calculated above
            return (await SaveRateQuotesToTableAndReturnResultsAsync(
                        cartID: cartID,
                        hash: hash,
                        currentShippingProvider: nameof(RateByAttributesShippingProvider),
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
            throw new NotImplementedException();
        }
    }
}
