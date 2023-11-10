// <copyright file="UPSShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ups shipping provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.UPS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Shipping;
    using UPSPackageRateService;

    /// <summary>The ups shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class UPSShippingProvider : ShippingProviderBase
    {
        /// <summary>The default request option.</summary>
        private const string DefaultRequestOption = "Shop";

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => UPSShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets the service.</summary>
        /// <value>The service.</value>
        private RatePortTypeClient Service { get; } = new();

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int>? salesItemIDs,
            IContactModel? origin,
            IContactModel? destination,
            bool expedited,
            string? contextProfileName)
        {
            if (salesItemIDs == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Line Items are required to get UPS shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get UPS shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Packaging provider is required to get UPS shipping rate quotes");
            }
            var items = itemsResult.Result!;
            if (!items.Any())
            {
                return CEFAR.PassingCEFAR(new List<IRateQuoteModel>(), "NOTE! No items in this cart need to ship")!;
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Origin is required to get UPS shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get UPS shipping rate quotes");
            }
            destination.Address!.CountryCode = destination.Address.CountryCode == "USA"
                ? destination.Address.CountryCode = "US"
                : destination.Address.CountryCode;
            origin.Address!.CountryCode = origin.Address.CountryCode == "USA"
                ? origin.Address.CountryCode = "US"
                : origin.Address.CountryCode;
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "UPS");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.")!;
            }
            // Generate a UPS Rate Request
            // TODO: Generate a TimeInTransit request so we can see when packages would be delivered, probably have to
            // make TimeInTransit override Rate request's results, their docs say it adds logic to exclude more
            // expensive services that can't deliver any faster.
            var rateRequest = GenerateRateRequest(items, destination, origin);
            if (decimal.Parse(rateRequest.Shipment.Package.First().PackageWeight.Weight) <= 0)
            {
                // No Weight
                const string Message = "There was no weight to use to calculate shipping.";
                Logger.LogWarning("UPSShippingProvider: GetRates", Message, contextProfileName);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"UPS GetRates: {Message}")!;
            }
            // Run the Request
            RateResponse response;
            try
            {
                response = Service.ProcessRate(UPSShippingProviderConfig.Security, rateRequest);
            }
            catch (System.ServiceModel.FaultException<ErrorDetailType[]> ex1)
            {
                await Logger.LogErrorAsync("UPSShippingProvider: GetRates", ex1.Message, ex1, contextProfileName).ConfigureAwait(false);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"UPS Service Call Error: {ex1.Message}")!;
            }
            catch (Exception ex2)
            {
                await Logger.LogErrorAsync("UPSShippingProvider: GetRates", ex2.Message, ex2, contextProfileName).ConfigureAwait(false);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"UPS Error: {ex2.Message}")!;
            }
            // Check for Errors
            // TODO: Check UPS Response for Errors
            // Parse the response
            var rates = response.RatedShipment.ToShipmentRates(UPSShippingProviderConfig.RateTypeIncludeList);
            // Check for Minimum Rate Amount requirements and apply if set
            var defaultMinimumPrice = UPSShippingProviderConfig.DefaultMinimumPrice;
            if (UPSShippingProviderConfig.UseDefaultMinimumPricing && defaultMinimumPrice != 0)
            {
                foreach (var rate in rates.Where(x => x.Rate < defaultMinimumPrice))
                {
                    rate.Rate = defaultMinimumPrice;
                }
            }
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID: cartID,
                    hash: hash,
                    currentShippingProvider: "UPS",
                    rates: rates,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return results.WrapInPassingCEFAR()!;
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

        /// <summary>Generates a request.</summary>
        /// <param name="items">      The items.</param>
        /// <param name="destination">Destination for the.</param>
        /// <param name="origin">     The origin.</param>
        /// <returns>The request.</returns>
        private static RateRequest GenerateRateRequest(
            IList<IProviderShipment> items,
            IContactModel destination,
            IContactModel origin)
        {
            return new()
            {
                Request = new()
                {
                    RequestOption = new[]
                    {
                        DefaultRequestOption,
                    },
                },
                Shipment = new()
                {
                    Shipper = new()
                    {
                        ShipperNumber = UPSShippingProviderConfig.ShipperNumber,
                        Address = origin.ToUPSAddressType(),
                    },
                    ShipFrom = new() { Address = origin.ToUPSAddressType() },
                    ShipTo = new() { Address = destination.ToUPSShipToAddressType() },
                    Package = items.ToUPSPackageTypeArray(
                        UPSShippingProviderConfig.CombinePackagesWhenGettingShippingRate,
                        UPSShippingProviderConfig.UseDimensionalWeight),
                },
            };
        }
    }
}
