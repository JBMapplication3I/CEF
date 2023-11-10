// <copyright file="FedExShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FedEx shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.FedEx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
#if NET5_0_OR_GREATER
    using FedExRateService5;
#else
    using FedExRateService;
#endif
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using JSConfigs;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A FedEx shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class FedExShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => FedExShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel? origin,
            IContactModel? destination,
            bool expedited,
            string? contextProfileName)
        {
            if (salesItemIDs == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Line Items are required to get FedEx shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get FedEx shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider must return successfully");
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR<List<IRateQuoteModel>?>(new(), "NOTE! No items in this cart need to ship");
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Origin is required to get FedEx shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Destination is required to get FedEx shipping rate quotes");
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var toHash = (
                items: items!.ToFedExLineItemArray(
                    FedExShippingProviderConfig.CombinePackagesWhenGettingShippingRate,
                    FedExShippingProviderConfig.UseDimensionalWeight,
                    contextProfileName),
                origin: origin.ToFedExAddress(),
                destination: destination.ToFedExAddress());
            var hash = Digest.Crc64(
                toHash,
                (x, writer) =>
                {
                    // TODO: More specific data points for hashing
                    writer.Write("FedEx");
                    writer.Write(JsonConvert.SerializeObject(x.items));
                    writer.Write(JsonConvert.SerializeObject(x.origin));
                    writer.Write(JsonConvert.SerializeObject(x.destination));
                });
            var existingRateQuotes = FedExShippingProviderConfig.ForceNoCacheForTesting
                ? new()
                : GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            var ratesList = new List<IShipmentRate>();
            var originLocations = new List<string>();
            if (CEFConfigDictionary.GetClosestWarehouseWithStock)
            {
                foreach (var item in items!)
                {
                    var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    var product = context.Products.FirstOrDefault(x => x.CustomKey == item.ItemCode);
                    if (product == null)
                    {
                        continue;
                    }
                    var closestSection = Workflows.ProductInventoryLocationSections.GetClosestWarehouseByRegionCode(
                        origin.Address!.RegionCode!,
                        product.ID,
                        contextProfileName);
                    if (closestSection != null)
                    {
                        origin = Workflows.ProductInventoryLocationSections.GetWarehouseContact(
                            closestSection.Slave!.InventoryLocationKey!,
                            contextProfileName);
                        toHash.origin = origin!.ToFedExAddress();
                        if (originLocations.Contains(origin!.Address!.RegionCode!))
                        {
                            continue;
                        }
                        // Generate a FedEx Rate Request
                        var (request, shipTime) = GenerateRateRequest(toHash.items!, toHash.origin, toHash.destination, expedited);
                        // Run the Request
                        ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                        ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                        // 1.2+ is the only thing that should be allowed
                        RateReply response;
#if NET5_0_OR_GREATER
                        using (var service = new RatePortTypeClient())
#else
                        using (var service = new RateService())
#endif
                        {
                            try
                            {
                                if (FedExShippingProviderConfig.UseProduction)
                                {
#if NET5_0_OR_GREATER
                                    // TODO@NET5: Figure out how to assign the web service in Net 5, have to use a Connected Service
                                    // Most likely need to use a nuget package instead of this process
                                    // service.Uri = new Uri("https://ws.fedex.com:443/web-services");
#else
                                    service.Url = "https://ws.fedex.com:443/web-services";
#endif
                                }
                                var proxyProvider = RegistryLoaderWrapper.GetProxyProvider(contextProfileName);
                                if (proxyProvider != null && proxyProvider.Name != "NoProxyProvider")
                                {
#if NET5_0_OR_GREATER
                                    // TODO@NET5: Figure out how to assign a proxy to a connected service in Net 5, have to use a Connected Service
                                    // Most likely need to use a nuget package instead of this process
                                    // var webRequest = await proxyProvider.CreateWebProxyAsync().ConfigureAwait(false);
                                    // service.Proxy = webRequest.Result;
#else
                                    var webRequest = await proxyProvider.CreateWebProxyAsync().ConfigureAwait(false);
                                    service.Proxy = webRequest.Result;
#endif
                                }
#if NET5_0_OR_GREATER
                                response = (await service.getRatesAsync(request).ConfigureAwait(false)).RateReply;
#else
                                response = service.getRates(request);
#endif
                            }
                            catch (Exception ex)
                            {
                                await Logger.LogErrorAsync(
                                        "Get FedEx Shipping Rates exception",
                                        ex.Message,
                                        ex,
                                        contextProfileName)
                                    .ConfigureAwait(false);
                                continue;
                            }
                        }
                        // Check for Errors
                        if (response.HighestSeverity != NotificationSeverityType.SUCCESS
                            && response.HighestSeverity != NotificationSeverityType.NOTE
                            && response.HighestSeverity != NotificationSeverityType.WARNING)
                        {
                            await Logger.LogErrorAsync(
                                    name: "Get FedEx Shipping Rates Error",
                                    message: "The response was invalid",
                                    forceEmail: false,
                                    ex: null,
                                    data: JsonConvert.SerializeObject(new { Request = request, Response = response }),
                                    contextProfileName: contextProfileName)
                                .ConfigureAwait(false);
                            continue;
                        }
                        if (response.HighestSeverity == NotificationSeverityType.WARNING
                            && response.Notifications.Any()
                            && Contract.CheckValidKey(response.Notifications[0].Code)
                            && response.RateReplyDetails?.Any() != true)
                        {
                            continue;
                        }
                        // Parse the response
                        var rates = response.RateReplyDetails.ToShippingRate(FedExShippingProviderConfig.RateTypeIncludeList, true, shipTime);
                        // Check for Minimum Rate Amount requirements and apply if set
                        if (FedExShippingProviderConfig.UseDefaultMinimumPricing
                            && FedExShippingProviderConfig.DefaultMinimumPrice != 0)
                        {
                            foreach (var rate in rates.Where(x => x.Rate < FedExShippingProviderConfig.DefaultMinimumPrice))
                            {
                                rate.Rate = FedExShippingProviderConfig.DefaultMinimumPrice;
                            }
                        }
                        foreach (var rate in rates)
                        {
                            var shipmentRate = ratesList.FirstOrDefault(x => x.OptionCode == rate.OptionCode);
                            if (shipmentRate != null)
                            {
                                shipmentRate.Rate += rate.Rate;
                            }
                            else
                            {
                                ratesList.Add(rate);
                            }
                        }
                        originLocations.Add(origin.Address.RegionCode!);
                    }
                }
            }
            else
            {
                // Generate a FedEx Rate Request
                var (request, shipTime) = GenerateRateRequest(toHash.items!, toHash.origin, toHash.destination, expedited);
                // Run the Request
                ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                // 1.2+ is the only thing that should be allowed
                RateReply response;
#if NET5_0_OR_GREATER
                using (var service = new RatePortTypeClient())
#else
                using (var service = new RateService())
#endif
                {
                    try
                    {
                        if (FedExShippingProviderConfig.UseProduction)
                        {
#if NET5_0_OR_GREATER
                            // TODO@NET5: Figure out how to assign the web service in Net 5, have to use a Connected Service
                            // Most likely need to use a nuget package instead of this process
                            // service.Uri = new Uri("https://ws.fedex.com:443/web-services");
#else
                            service.Url = "https://ws.fedex.com:443/web-services";
#endif
                        }
                        var proxyProvider = RegistryLoaderWrapper.GetProxyProvider(contextProfileName);
                        if (proxyProvider != null)
                        {
#if NET5_0_OR_GREATER
                            // TODO@NET5: Figure out how to assign a proxy to a connected service in Net 5, have to use a Connected Service
                            // Most likely need to use a nuget package instead of this process
                            // var webRequest = await proxyProvider.CreateWebProxyAsync().ConfigureAwait(false);
                            // service.Proxy = webRequest.Result;
#else
                            var webRequest = await proxyProvider.CreateWebProxyAsync().ConfigureAwait(false);
                            service.Proxy = webRequest.Result;
#endif
                        }
#if NET5_0_OR_GREATER
                        response = (await service.getRatesAsync(request).ConfigureAwait(false)).RateReply;
#else
                        response = service.getRates(request);
#endif
                    }
                    catch (Exception ex)
                    {
                        await Logger.LogErrorAsync("Get FedEx Shipping Rates", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                        return new List<IRateQuoteModel>().WrapInFailingCEFAR($"FedEx GetRates: {ex.Message}");
                    }
                }
                // Check for Errors
                if (response.HighestSeverity != NotificationSeverityType.SUCCESS
                    && response.HighestSeverity != NotificationSeverityType.NOTE
                    && response.HighestSeverity != NotificationSeverityType.WARNING)
                {
                    await Logger.LogErrorAsync("Get FedEx Shipping Rates", "The response was invalid", contextProfileName).ConfigureAwait(false);
                    return new List<IRateQuoteModel>().WrapInFailingCEFAR("FedEx GetRates: The response was invalid");
                }
                if (response.HighestSeverity == NotificationSeverityType.WARNING
                    && response.Notifications.Any()
                    && Contract.CheckValidKey(response.Notifications[0].Code)
                    && response.RateReplyDetails?.Any() != true)
                {
                    return new List<IRateQuoteModel>().WrapInFailingCEFAR(
                        $"FedEx Service Call Warning: {response.Notifications[0].Code}: {response.Notifications[0].Message}");
                }
                // Parse the response
                var rates = response.RateReplyDetails.ToShippingRate(FedExShippingProviderConfig.RateTypeIncludeList, false, shipTime);
                // Check for Minimum Rate Amount requirements and apply if set
                if (FedExShippingProviderConfig.UseDefaultMinimumPricing
                    && FedExShippingProviderConfig.DefaultMinimumPrice != 0)
                {
                    foreach (var rate in rates.Where(x => x.Rate < FedExShippingProviderConfig.DefaultMinimumPrice))
                    {
                        rate.Rate = FedExShippingProviderConfig.DefaultMinimumPrice;
                    }
                }
                ratesList = rates;
            }
            if (ratesList != null && ratesList.Any() && FedExShippingProviderConfig.IncludeFreeShipping)
            {
                var cart = await Workflows.Carts.GetAsync(cartID, contextProfileName).ConfigureAwait(false);
                if (cart != null
                    && cart.SalesItems!.Sum(x => x.ExtendedPrice) >= FedExShippingProviderConfig.AmountToIncludeFreeShipping)
                {
                    var groundRate = ratesList.FirstOrDefault(x => x.OptionCode == "FEDEX_GROUND");
                    if (groundRate != null)
                    {
                        ratesList.Insert(
                            0,
                            new ShipmentRate
                            {
                                Rate = 0,
                                OptionCode = "FREE_SHIPPING",
                                OptionName = "Free Shipping",
                                FullOptionName = "FedEx Ground Free Shipping ($0.00)",
                                EstimatedArrival = groundRate.EstimatedArrival,
                                EstimatedArrivalMax = groundRate.EstimatedArrivalMax,
                                CarrierName = groundRate.CarrierName,
                                DeliveryDayOfWeek = groundRate.DeliveryDayOfWeek,
                                TargetShipping = groundRate.TargetShipping,
                                SignatureOption = groundRate.SignatureOption,
                            });
                    }
                }
            }
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID: cartID,
                    hash: hash,
                    currentShippingProvider: "FedEx",
                    rates: ratesList,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return results.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<List<IShipmentRate>> GetBaseOrNetChargesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            bool useBaseCharge,
            string? contextProfileName)
        {
            var ratesList = new List<IShipmentRate>();
            if (salesItemIDs == null)
            {
                return ratesList;
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return ratesList;
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return ratesList;
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return ratesList;
            }
            // Validate the addresses
            if (origin == null)
            {
                return ratesList;
            }
            if (destination == null)
            {
                return ratesList;
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var toHash = (
                items: items!.ToFedExLineItemArray(
                    FedExShippingProviderConfig.CombinePackagesWhenGettingShippingRate,
                    FedExShippingProviderConfig.UseDimensionalWeight,
                    contextProfileName),
                origin: origin.ToFedExAddress(),
                destination: destination.ToFedExAddress());
            var originLocations = new List<string>();
            foreach (var item in items!)
            {
                var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var product = context.Products.FirstOrDefault(x => x.CustomKey == item.ItemCode);
                if (product == null)
                {
                    continue;
                }
                var closestSection = Workflows.ProductInventoryLocationSections.GetClosestWarehouseByRegionCode(
                    origin.Address!.RegionCode!,
                    product.ID,
                    contextProfileName);
                if (closestSection != null)
                {
                    origin = Workflows.ProductInventoryLocationSections.GetWarehouseContact(
                        closestSection.Slave!.InventoryLocationKey!,
                        contextProfileName)!;
                    toHash.origin = origin.ToFedExAddress();
                    if (originLocations.Contains(origin.Address!.RegionCode!))
                    {
                        continue;
                    }
                    // Generate a FedEx Rate Request
                    var (request, shipTime) = GenerateRateRequest(toHash.items!, toHash.origin, toHash.destination, expedited);
                    // Run the Request
                    ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                    ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                    // 1.2+ is the only thing that should be allowed
                    RateReply response;
#if NET5_0_OR_GREATER
                    using (var service = new RatePortTypeClient())
#else
                    using (var service = new RateService())
#endif
                    {
                        try
                        {
                            if (FedExShippingProviderConfig.UseProduction)
                            {
#if NET5_0_OR_GREATER
                                // TODO@NET5: Figure out how to assign the web service in Net 5, have to use a Connected Service
                                // Most likely need to use a nuget package instead of this process
                                // service.Uri = new Uri("https://ws.fedex.com:443/web-services");
#else
                                service.Url = "https://ws.fedex.com:443/web-services";
#endif
                            }
                            var proxyProvider = RegistryLoaderWrapper.GetProxyProvider(contextProfileName);
                            if (proxyProvider != null && proxyProvider.Name != "NoProxyProvider")
                            {
#if NET5_0_OR_GREATER
                                // TODO@NET5: Figure out how to assign a proxy to a connected service in Net 5, have to use a Connected Service
                                // Most likely need to use a nuget package instead of this process
                                // var webRequest = await proxyProvider.CreateWebProxyAsync().ConfigureAwait(false);
                                // service.Proxy = webRequest.Result;
#else
                                var webRequest = await proxyProvider.CreateWebProxyAsync().ConfigureAwait(false);
                                service.Proxy = webRequest.Result;
#endif
                            }
#if NET5_0_OR_GREATER
                            response = (await service.getRatesAsync(request).ConfigureAwait(false)).RateReply;
#else
                            response = service.getRates(request);
#endif
                        }
                        catch (Exception ex)
                        {
                            await Logger.LogErrorAsync("Get FedEx Shipping Rates", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                            continue;
                        }
                    }
                    // Check for Errors
                    if (response.HighestSeverity != NotificationSeverityType.SUCCESS
                        && response.HighestSeverity != NotificationSeverityType.NOTE
                        && response.HighestSeverity != NotificationSeverityType.WARNING)
                    {
                        await Logger.LogErrorAsync("Get FedEx Shipping Rates", "The response was invalid", contextProfileName).ConfigureAwait(false);
                        continue;
                    }
                    if (response.HighestSeverity == NotificationSeverityType.WARNING
                        && response.Notifications.Any()
                        && Contract.CheckValidKey(response.Notifications[0].Code)
                        && response.RateReplyDetails?.Any() != true)
                    {
                        continue;
                    }
                    // Parse the response
                    var rates = response.RateReplyDetails.ToShippingRate(FedExShippingProviderConfig.RateTypeIncludeList, useBaseCharge, shipTime);
                    // Check for Minimum Rate Amount requirements and apply if set
                    if (FedExShippingProviderConfig.UseDefaultMinimumPricing
                        && FedExShippingProviderConfig.DefaultMinimumPrice != 0)
                    {
                        foreach (var rate in rates.Where(x => x.Rate < FedExShippingProviderConfig.DefaultMinimumPrice))
                        {
                            rate.Rate = FedExShippingProviderConfig.DefaultMinimumPrice;
                        }
                    }
                    foreach (var rate in rates)
                    {
                        var existingRate = ratesList.FirstOrDefault(x => x.OptionCode == rate.OptionCode);
                        if (existingRate != null)
                        {
                            existingRate.OptionName += $" ${rate.Rate:0.00} ({origin.Address.RegionCode}) + ";
                            existingRate.Rate += rate.Rate;
                        }
                        else
                        {
                            rate.OptionName += $" ${rate.Rate:0.00} ({origin.Address.RegionCode}) + ";
                            ratesList.Add(rate);
                        }
                    }
                    originLocations.Add(origin.Address.RegionCode!);
                }
            }
            foreach (var rate in ratesList)
            {
                rate.OptionName = rate.OptionName![0..^2];
                rate.OptionName += $"= ${rate.Rate:0.00}";
            }
            return ratesList;
        }

        /// <summary>Generates a request.</summary>
        /// <param name="items">      The items.</param>
        /// <param name="origin">     The origin.</param>
        /// <param name="destination">Destination for the.</param>
        /// <param name="isExpedited">True if this request is expedited.</param>
        /// <returns>The request.</returns>
        private static (RateRequest request, DateTime shipTime) GenerateRateRequest(
            RequestedPackageLineItem[] items,
            Address origin,
            Address destination,
            bool isExpedited)
        {
            var now = DateExtensions.GenDateTime;
            var shipTime = now;
            var (companyLeadTimeEnabled, dayEnabled, startHours, endHours, normal, expedited) = ReadSettings();
            if (companyLeadTimeEnabled)
            {
                void SetShipTimeToStartTime(DayOfWeek dayOfWeek)
                {
                    shipTime = shipTime
                        .Subtract(shipTime.TimeOfDay)
                        .Add(startHours[dayOfWeek]);
                }
                void SetShipTimeToTomorrow()
                {
                    var tomorrow = shipTime.AddDays(1);
                    while (!dayEnabled[tomorrow.DayOfWeek])
                    {
                        tomorrow = tomorrow.AddDays(1);
                        if (tomorrow - shipTime > TimeSpan.FromDays(7))
                        {
                            // Invalid config, so just reset to 1 and step out
                            tomorrow = shipTime.AddDays(1);
                            break;
                        }
                    }
                    shipTime = tomorrow;
                }
                var hoursCounted = TimeSpan.Zero;
                var hoursToCount = isExpedited ? expedited : normal;
                while (true)
                {
                    if (!dayEnabled[shipTime.DayOfWeek])
                    {
                        // Non-working day, skip ahead to next working day
                        SetShipTimeToTomorrow();
                        SetShipTimeToStartTime(shipTime.DayOfWeek);
                    }
                    else if (shipTime.TimeOfDay < startHours[shipTime.DayOfWeek])
                    {
                        // We have to bump the to the start hour for today (it's early morning)
                        SetShipTimeToStartTime(shipTime.DayOfWeek);
                    }
                    else if (shipTime.TimeOfDay > endHours[shipTime.DayOfWeek])
                    {
                        // We are passed today's afternoon cutoff, have to bump to the next shippable day's start time
                        SetShipTimeToTomorrow();
                        SetShipTimeToStartTime(shipTime.DayOfWeek);
                    }
                    // We should now be inside a valid point to start counting hours based on normal or expedited
                    var hoursRemaining = hoursToCount - hoursCounted;
                    if (hoursRemaining <= TimeSpan.Zero)
                    {
                        // We've counted all we need to count and can get out of this loop
                        break;
                    }
                    var hoursToEndOfDay = endHours[shipTime.DayOfWeek] - shipTime.TimeOfDay;
                    if (hoursRemaining < hoursToEndOfDay)
                    {
                        // We're good for inside today's window, just add the rest and exit the loop
                        shipTime = shipTime.Add(hoursRemaining);
                        break;
                    }
                    // There are not enough hours left in the day to contain this shipment, have to move on to the end
                    // of the day so it goes into tomorrow. Extra minute ensures it's beyond it, won't have a
                    // meaningful impact in final numbers
                    hoursCounted = hoursCounted.Add(hoursToEndOfDay).Add(TimeSpan.FromMinutes(1));
                    shipTime = shipTime.Add(hoursCounted);
                }
            }
            return (new()
            {
                WebAuthenticationDetail = new()
                {
                    UserCredential = new()
                    {
                        Key = FedExShippingProviderConfig.UserName,
                        Password = FedExShippingProviderConfig.Password,
                    },
                },
                ClientDetail = new()
                {
                    AccountNumber = FedExShippingProviderConfig.AccountNumber,
                    MeterNumber = FedExShippingProviderConfig.MeterNumber,
                    ////Region = ExpressRegionCode.US, // TODO: Auto-switch between US, CA, LAC, EMEA, APAC
                    ////Localization = new Localization { LanguageCode = "EN", LocaleCode = "US" }
                },
                RequestedShipment = new()
                {
                    ShipTimestamp = shipTime, // Shipping date and time (when the package will be given to the shipper)
                    ShipTimestampSpecified = true,
                    DropoffType = DropoffType.REGULAR_PICKUP,
                    ServiceTypeSpecified = false,
                    // Packaging type FEDEX_BOX, FEDEX_PAK, FEDEX_TUBE, YOUR_PACKAGING, ...
                    PackagingType = PackagingType.YOUR_PACKAGING,
                    PackagingTypeSpecified = true,
                    PackageCount = items.Length.ToString(),
                    Shipper = new() { Address = origin },
                    Recipient = new() { Address = destination },
                    RequestedPackageLineItems = items,
                },
                TransactionDetail = new()
                {
                    // This is a reference field for the customer. Any value can be used and will be provided in the response
                    CustomerTransactionId = "***Rate Request using VC#***",
                },
                Version = new(),
                ReturnTransitAndCommit = true,
                ReturnTransitAndCommitSpecified = true,
            }, shipTime);
        }
    }
}
