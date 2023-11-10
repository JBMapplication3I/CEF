// <copyright file="ZoneRateShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zone rate shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Zone
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A zone rate shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class ZoneRateShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => ZoneRateShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
                    "ERROR! Line Items are required to get Zone Rate shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Zone Rate shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get Zone Rate shipping rate quotes");
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
                    "ERROR! Origin is required to get Zone Rate shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get Zone Rate shipping rate quotes");
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items!, "ZoneRate");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            // Get the rates from the settings in JSON and deserialize into ShipmentRates
            var zoneSettings = (await Workflows.Settings.GetSettingByTypeNameAsync("ZoneRateShipping", contextProfileName).ConfigureAwait(false))
                .FirstOrDefault()?.Value;
            if (!Contract.CheckValidKey(zoneSettings))
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR: Zone settings not detected");
            }
            var zoneShippingRates = JsonConvert.DeserializeObject<List<ShipmentZone>>(zoneSettings!);
            var zone = zoneShippingRates!.SingleOrDefault(x => x.Countries!.Any(y => y == destination.Address!.CountryCode));
            Contract.RequiresNotNull(zone, "ERROR! We are unable to ship to the country provided");
            var rates = new List<IShipmentRate>();
            foreach (var shipmentRate in zone!.ShipmentRates!)
            {
                rates.Add(new ShipmentRate
                {
                    CarrierName = shipmentRate.CarrierName,
                    FullOptionName = shipmentRate.FullOptionName,
                    OptionCode = shipmentRate.OptionCode,
                    OptionName = shipmentRate.OptionName,
                    Rate = shipmentRate.Rate,
                });
            }
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID: cartID,
                    hash: hash,
                    currentShippingProvider: null,
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

        /*
        private long BuildSimpleRequestHash(
            IContactModel origin,
            IContactModel destination,
            string provider)
        {
            var toHash = (
                origin: (
                    origin.Address.Street1,
                    origin.Address.Street2,
                    origin.Address.Street3,
                    origin.Address.City,
                    origin.Address.RegionID,
                    origin.Address.CountryID,
                    origin.Address.PostalCode),
                destination: (
                    destination.Address.Street1,
                    destination.Address.Street2,
                    destination.Address.Street3,
                    destination.Address.City,
                    destination.Address.RegionID,
                    destination.Address.CountryID,
                    destination.Address.PostalCode));
            return Digest.Crc64(
                toHash,
                (x, writer) =>
                {
                    writer.Write(provider);
                    writer.Write(JsonConvert.SerializeObject(x.origin));
                    writer.Write(JsonConvert.SerializeObject(x.destination));
                });
        }
        */
    }
}
