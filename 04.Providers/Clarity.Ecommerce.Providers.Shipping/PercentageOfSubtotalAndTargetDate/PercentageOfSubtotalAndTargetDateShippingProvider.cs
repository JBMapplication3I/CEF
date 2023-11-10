// <copyright file="PercentageOfSubtotalAndTargetDateShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the percentage of subtotal and target date shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.PercentageOfSubtotalAndTargetDate
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using JSConfigs;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A flat rate shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class PercentageOfSubtotalAndTargetDateShippingProvider : ShippingProviderBase
    {
        /// <summary>The standard shipping in days.</summary>
        private const int StandardShippingDays = 3;

        /// <summary>The expedited shipping in days.</summary>
        private const int ExpeditedShippingDays = 2;

        /// <summary>The rush shipping in days.</summary>
        private const int RushShippingDays = 1;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PercentageOfSubtotalAndTargetDateShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            string? contextProfileName)
        {
            // var cart = Workflows.Carts.Get(cartID, contextProfileName);
            ICartModel cart;
            IAccount? account = null;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var cartEntity = await context.Carts
                    .AsNoTracking()
                    .Where(x => x.ID == cartID)
                    .SingleAsync()
                    .ConfigureAwait(false);
                if (cartEntity == null
                    || string.IsNullOrWhiteSpace(cartEntity.Type!.Name)
                    || !cartEntity.SessionID.HasValue
                    || !cartEntity.UserID.HasValue)
                {
                    return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                        $"ERROR! Cart Entity with ID {cartID} NOT FOUND -- required to get shipping rate quotes");
                }
                var user = await Workflows.Users.GetAsync(cartEntity.UserID.Value, contextProfileName).ConfigureAwait(false);
                var accountID = await Workflows.Accounts.GetIDByUserIDAsync(cartEntity.UserID.Value, contextProfileName).ConfigureAwait(false);
                account = (await context.Accounts
                        .AsNoTracking()
                        .FilterByID(accountID)
                        .Select(x => new
                        {
                            x.ID,
                            x.CustomKey,
                            x.TypeID,
                            x.JsonAttributes,
                            APPs = x.AccountPricePoints!
                                .Where(y => y.Active)
                                .Select(y => new { y.Active, y.Slave!.CustomKey }),
                        })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new Account
                    {
                        ID = x.ID,
                        CustomKey = x.CustomKey,
                        TypeID = x.TypeID,
                        JsonAttributes = x.JsonAttributes,
                        AccountPricePoints = x.APPs
                            .Select(y => new AccountPricePoint { Active = true, CustomKey = y.CustomKey })
                            .ToList(),
                    })
                    .SingleOrDefault();
                var pricingFactoryContext = RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>(contextProfileName);
                pricingFactoryContext.UserID = user!.ID;
                pricingFactoryContext.UserKey = user.CustomKey;
                pricingFactoryContext.CountryID = user.Contact?.Address?.CountryID;
                if (account != null)
                {
                    pricingFactoryContext.AccountID = account.ID;
                    pricingFactoryContext.AccountKey = account.CustomKey;
                    pricingFactoryContext.AccountTypeID = account.TypeID;
                    pricingFactoryContext.PricePoint = account.AccountPricePoints
                            ?.SingleOrDefault()
                            ?.CustomKey
                        ?? CEFConfigDictionary.PricingProviderTieredDefaultPricePointKey
                        ?? "WEB";
                }
                pricingFactoryContext.CurrencyID = user.CurrencyID;
                pricingFactoryContext.SessionID = cartEntity.SessionID.Value;
                var taxesProvider = await RegistryLoaderWrapper.GetTaxProviderAsync(contextProfileName).ConfigureAwait(false);
                var (response, _) = await Workflows.Carts.SessionGetAsync(
                        lookupKey: new SessionCartBySessionAndTypeLookupKey(
                            sessionID: cartEntity.SessionID.Value,
                            typeKey: cartEntity.Type.CustomKey!,
                            userID: cartEntity.UserID,
                            accountID: cartEntity.AccountID,
                            brandID: cartEntity.BrandID,
                            franchiseID: cartEntity.FranchiseID,
                            storeID: cartEntity.StoreID),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                        $"ERROR! Cart with ID {cartID} NOT VALID -- required to get shipping rate quotes");
                }
                cart = response.Result!;
            }
            if (cart == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    $"ERROR! Cart with ID {cartID} NOT FOUND -- required to get shipping rate quotes");
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
            if (items!.Count == 0)
            {
                return CEFAR.PassingCEFAR<List<IRateQuoteModel>?>(new(), "NOTE! No items in this cart need to ship");
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Origin is required to get shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>(
                    "ERROR! Destination is required to get shipping rate quotes");
            }
            var subTotal = cart.Totals!.SubTotal;
            DayOfWeek? preferredDeliveryDay = null;
            if (account != null)
            {
                preferredDeliveryDay = account.SerializableAttributes!.TryGetValue("PreferredDeliveryDay", out var attr)
                    && attr != null
                        ? (DayOfWeek?)Enum.Parse(typeof(DayOfWeek), attr.Value)
                        : null;
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(
                origin,
                destination,
                subTotal,
                preferredDeliveryDay?.ToString(),
                nameof(PercentageOfSubtotalAndTargetDateShippingProvider));
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Count > 0)
            {
                return existingRateQuotes.WrapInPassingCEFAR(
                    "NOTE! These rate quotes were previously provided and stored.");
            }
            var rates = new List<IShipmentRate>();
            var isUSAddress = destination.Address!.CountryCode == "US" || destination.Address.CountryCode == "USA";
            var isContinentalUSAddress = isUSAddress
                && destination.Address.RegionCode != "AK"
                && destination.Address.RegionCode != "HI";
            var orderDate = DateTime.Now; // TODO: Orders after 5PM considered start next business day?
            var earliestShipDate = NextBusinessDay(isUSAddress, orderDate);
            var requestedShipDate = preferredDeliveryDay.HasValue
                ? NextBusinessDayWithPreferredDeliveryDay(isUSAddress, preferredDeliveryDay.Value)
                : NextBusinessDay(isUSAddress, DateTime.Today);
            var targetDeliveryDate = preferredDeliveryDay.HasValue
                ? NextBusinessDay(isUSAddress, requestedShipDate.AddDays(-1))
                : earliestShipDate;
            /*
            var targetDeliveryDate = cart.RequestedShipDate.HasValue
                // Customer manually overridden value via UI for this cart only
                ? NextBusinessDay(cart.RequestedShipDate.Value.AddDays(-1))
                : preferredDeliveryDay.HasValue
                    // Customer's account-level
                    ? NextBusinessDay(preferredDeliveryDay.Value)
                    : earliestShipDate;
            */
            // Standard Rate
            ShipmentRate standardRate;
            if (isContinentalUSAddress)
            {
                standardRate = BuildShipmentRate(
                    $"Standard US Continental (3 Day) - {RatePercent(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercentStandardUSContinental)}",
                    "FedEx",
                    "Standard US Continental (3 Day)",
                    "STD-USC",
                    MinimumShippingRate(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercentStandardUSContinental * subTotal),
                    StandardShippingDays,
                    true,
                    earliestShipDate);
            }
            else if (isUSAddress)
            {
                standardRate = BuildShipmentRate(
                    $"Standard US Non-Continental (3 Day) - {RatePercent(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercentStandardUSNonContinental)}",
                    "FedEx",
                    "Standard US Non-Continental (3 Day)",
                    "STD-USNC",
                    MinimumShippingRate(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercentStandardUSNonContinental * subTotal),
                    StandardShippingDays,
                    true,
                    earliestShipDate);
            }
            else
            {
                standardRate = BuildShipmentRate(
                    $"Standard Outside US - {RatePercent(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercentStandardUSNonContinental)}",
                    "FedEx",
                    "Standard Outside US",
                    "STD-INTL",
                    MinimumShippingRate(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercentStandardUSNonContinental * subTotal),
                    StandardShippingDays,
                    false,
                    earliestShipDate);
            }
            if (preferredDeliveryDay.HasValue)
            {
                rates.Add(
                    BuildShipmentRatePreferred(
                        standardRate,
                        StandardShippingDays,
                        isUSAddress,
                        earliestShipDate,
                        targetDeliveryDate));
            }
            rates.Add(standardRate);
            // ExpeditedShippingDays Day Rate
            rates.Add(BuildShipmentRate(
                $"Expedited US ({ExpeditedShippingDays} Day) - {RatePercent(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercent2Day)}",
                "FedEx",
                $"Expedited US ({ExpeditedShippingDays} Day)",
                "2DAY",
                MinimumShippingRate(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercent2Day * subTotal),
                ExpeditedShippingDays,
                isUSAddress,
                earliestShipDate,
                earliestShipDate));
            // 1 Day Rate
            rates.Add(BuildShipmentRate(
                $"Rush US ({RushShippingDays} Day) - {RatePercent(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercent1Day)}",
                "FedEx",
                $"Rush US ({RushShippingDays} Day)",
                "1DAY",
                MinimumShippingRate(PercentageOfSubtotalAndTargetDateShippingProviderConfig.RatePercent1Day * subTotal),
                1,
                isUSAddress,
                earliestShipDate,
                earliestShipDate));
            // Save the rates to the table to "cache" them with the hash we calculated above
            return (await SaveRateQuotesToTableAndReturnResultsAsync(
                        cartID,
                        hash,
                        nameof(PercentageOfSubtotalAndTargetDateShippingProvider),
                        rates.OrderBy(x => x.Rate).ThenBy(x => x.OptionName),
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
            throw new NotImplementedException();
        }

        /// <summary>Minimum shipping rate.</summary>
        /// <param name="rate">The rate.</param>
        /// <returns>A decimal.</returns>
        private static decimal MinimumShippingRate(decimal rate)
        {
            return Math.Max(PercentageOfSubtotalAndTargetDateShippingProviderConfig.MinimumShippingRate, rate);
        }

        /// <summary>Next business day with preferred delivery day.</summary>
        /// <param name="checkUSHoliday">    True to check us holiday.</param>
        /// <param name="preferredDayOfWeek">The preferred day of week.</param>
        /// <returns>A DateTime.</returns>
        private static DateTime NextBusinessDayWithPreferredDeliveryDay(
            bool checkUSHoliday,
            DayOfWeek preferredDayOfWeek)
        {
            var tomorrow = DateTime.Today.AddDays(1);
            var addDays = 0;
            var nextBusinessDay = tomorrow;
            while (!IsBusinessDay(nextBusinessDay, checkUSHoliday) || nextBusinessDay.DayOfWeek != preferredDayOfWeek)
            {
                nextBusinessDay = tomorrow.AddDays(++addDays);
            }
            return tomorrow.AddDays(addDays);
        }

        /// <summary>Next business day.</summary>
        /// <param name="checkUSHoliday">True to check us holiday.</param>
        /// <param name="startDateTime"> The start date time.</param>
        /// <returns>A DateTime.</returns>
        private static DateTime NextBusinessDay(bool checkUSHoliday, DateTime? startDateTime = null)
        {
            var tomorrow = (startDateTime ?? DateTime.Today).AddDays(1);
            var addDays = 0;
            var nextBusinessDay = tomorrow;
            while (!IsBusinessDay(nextBusinessDay, checkUSHoliday))
            {
                nextBusinessDay = tomorrow.AddDays(++addDays);
            }
            return tomorrow.AddDays(addDays);
        }

        /// <summary>Next business day.</summary>
        /// <param name="checkUSHoliday">  True to check us holiday.</param>
        /// <param name="dayOfWeekToMatch">A match specifying the day of week to.</param>
        /// <returns>A DateTime.</returns>
        // ReSharper disable once UnusedMember.Local
        private static DateTime NextBusinessDay(bool checkUSHoliday, DayOfWeek dayOfWeekToMatch)
        {
            var tomorrow = DateTime.Today.AddDays(1);
            while (tomorrow.DayOfWeek != dayOfWeekToMatch)
            {
                tomorrow = NextBusinessDay(checkUSHoliday, tomorrow);
            }
            return tomorrow;
        }

        /// <summary>Query if 'date' is business day.</summary>
        /// <param name="date">          The date Date/Time.</param>
        /// <param name="checkUSHoliday">True to check us holiday.</param>
        /// <returns>True if business day, false if not.</returns>
        private static bool IsBusinessDay(DateTime date, bool checkUSHoliday)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday
                && (!checkUSHoliday || !IsUSFederalHoliday(date));
        }

        /// <summary>Query if 'date' is us federal holiday.</summary>
        /// <param name="date">The date Date/Time.</param>
        /// <returns>True if us federal holiday, false if not.</returns>
        private static bool IsUSFederalHoliday(DateTime date)
        {
            // to ease typing
            var nthWeekDay = (int)Math.Ceiling(date.Day / 7.0d);
            var dayName = date.DayOfWeek;
            var isThursday = dayName == DayOfWeek.Thursday;
            var isFriday = dayName == DayOfWeek.Friday;
            var isMonday = dayName == DayOfWeek.Monday;
            var isWeekend = dayName is DayOfWeek.Saturday or DayOfWeek.Sunday;
            // New Years Day (Jan 1, or preceding Friday/following Monday if weekend)
            if (date.Month == 12 && date.Day == 31 && isFriday
                || date.Month == 1 && date.Day == 1 && !isWeekend
                || date.Month == 1 && date.Day == 2 && isMonday)
            {
                return true;
            }
            // MLK day (3rd monday in January)
            if (date.Month == 1 && isMonday && nthWeekDay == 3)
            {
                return true;
            }
            // President's Day (3rd Monday in February)
            if (date.Month == 2 && isMonday && nthWeekDay == 3)
            {
                return true;
            }
            // Memorial Day (Last Monday in May)
            if (date.Month == 5 && isMonday && date.AddDays(7).Month == 6)
            {
                return true;
            }
            // Independence Day (July 4, or preceding Friday/following Monday if weekend)
            if (date.Month == 7 && date.Day == 3 && isFriday
                || date.Month == 7 && date.Day == 4 && !isWeekend
                || date.Month == 7 && date.Day == 5 && isMonday)
            {
                return true;
            }
            // Labor Day (1st Monday in September)
            if (date.Month == 9 && isMonday && nthWeekDay == 1)
            {
                return true;
            }
            // Columbus Day (2nd Monday in October)
            if (date.Month == 10 && isMonday && nthWeekDay == 2)
            {
                return true;
            }
            // Veteran's Day (November 11, or preceding Friday/following Monday if weekend))
            if (date.Month == 11 && date.Day == 10 && isFriday
                || date.Month == 11 && date.Day == 11 && !isWeekend
                || date.Month == 11 && date.Day == 12 && isMonday)
            {
                return true;
            }
            // Thanksgiving Day (4th Thursday in November)
            if (date.Month == 11 && isThursday && nthWeekDay == 4)
            {
                return true;
            }
            // Christmas Day (December 25, or preceding Friday/following Monday if weekend))
            if (date.Month == 12 && date.Day == 24 && isFriday
                || date.Month == 12 && date.Day == 25 && !isWeekend
                || date.Month == 12 && date.Day == 26 && isMonday)
            {
                return true;
            }
            return false;
        }

        /// <summary>Query if 'date' is delivery day.</summary>
        /// <param name="date">The date Date/Time.</param>
        /// <returns>True if delivery day, false if not.</returns>
        private static bool IsDeliveryDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>Latest date time.</summary>
        /// <param name="date1">The date 1 Date/Time.</param>
        /// <param name="date2">The date 2 Date/Time.</param>
        /// <returns>A DateTime.</returns>
        private static DateTime LatestDateTime(DateTime date1, DateTime date2)
        {
            return date1 > date2 ? date1 : date2;
        }

        /// <summary>Estimated delivery.</summary>
        /// <param name="checkUSHoliday">      True to check us holiday.</param>
        /// <param name="earliestShippingDate">The earliest shipping date.</param>
        /// <param name="shippingDays">        The shipping in days.</param>
        /// <param name="targetDeliveryDate">  The target delivery date.</param>
        /// <returns>A DateTime.</returns>
        private static (DateTime EstimatedDeliveryDate, DateTime TargetShippingDate) EstimatedDelivery(
            bool checkUSHoliday,
            DateTime earliestShippingDate,
            int shippingDays,
            DateTime? targetDeliveryDate = null)
        {
            var noDeliveryDays = 0;
            for (var i = 1; i <= shippingDays; i++)
            {
                if (!IsDeliveryDay(earliestShippingDate.AddDays(i)))
                {
                    noDeliveryDays++;
                }
            }
            var totalShippingDays = shippingDays + noDeliveryDays;
            var estimatedDeliveryDate = earliestShippingDate.AddDays(totalShippingDays);
            if (targetDeliveryDate.HasValue)
            {
                estimatedDeliveryDate = LatestDateTime(estimatedDeliveryDate, targetDeliveryDate.Value);
            }
            var targetShippingDate = estimatedDeliveryDate.AddDays(totalShippingDays * -1);
            targetShippingDate = targetShippingDate > earliestShippingDate ? targetShippingDate : earliestShippingDate;
            while (!IsBusinessDay(targetShippingDate, checkUSHoliday) && targetShippingDate >= earliestShippingDate)
            {
                targetShippingDate = targetShippingDate.AddDays(-1); // Try to ship a day earlier
            }
            return (estimatedDeliveryDate, targetShippingDate);
        }

        /// <summary>Rate percent.</summary>
        /// <param name="rateDecimal">The rate decimal.</param>
        /// <returns>A string.</returns>
        private static string RatePercent(decimal rateDecimal)
        {
            return $"{rateDecimal:P0}";
        }

        /// <summary>Builds shipment rate.</summary>
        /// <param name="fullOptionName">    Name of the full option.</param>
        /// <param name="carrierName">       Name of the carrier.</param>
        /// <param name="optionName">        Name of the option.</param>
        /// <param name="optionCode">        The option code.</param>
        /// <param name="rate">              The rate.</param>
        /// <param name="shippingDays">      The shipping in days.</param>
        /// <param name="checkUSHoliday">    True to check us holiday.</param>
        /// <param name="earliestShipDate">  The earliest ship date.</param>
        /// <param name="targetDeliveryDate">The target delivery date.</param>
        /// <returns>A ShipmentRate.</returns>
        private static ShipmentRate BuildShipmentRate(
            string fullOptionName,
            string carrierName,
            string optionName,
            string optionCode,
            decimal rate,
            int shippingDays,
            bool checkUSHoliday,
            DateTime earliestShipDate,
            DateTime? targetDeliveryDate = null)
        {
            var (estimatedDeliveryDate, targetShippingDate) = EstimatedDelivery(
                checkUSHoliday,
                earliestShipDate,
                shippingDays,
                targetDeliveryDate);
            return new()
            {
                FullOptionName = fullOptionName,
                CarrierName = carrierName,
                OptionName = optionName,
                OptionCode = optionCode,
                Rate = rate,
                EstimatedArrival = estimatedDeliveryDate,
                TargetShipping = targetShippingDate,
            };
        }

        /// <summary>Builds shipment rate preferred.</summary>
        /// <param name="shipmentRate">      The shipment rate.</param>
        /// <param name="shippingDays">      The shipping in days.</param>
        /// <param name="checkUSHoliday">    True to check us holiday.</param>
        /// <param name="earliestShipDate">  The earliest ship date.</param>
        /// <param name="targetDeliveryDate">The target delivery date.</param>
        /// <returns>A ShipmentRate.</returns>
        private static ShipmentRate BuildShipmentRatePreferred(
            IShipmentRate shipmentRate,
            int shippingDays,
            bool checkUSHoliday,
            DateTime earliestShipDate,
            DateTime targetDeliveryDate)
        {
            return BuildShipmentRate(
                $"{shipmentRate.FullOptionName} - PREFERRED DELIVERY DAY",
                shipmentRate.CarrierName!,
                $"{shipmentRate.OptionName} - PREFERRED DELIVERY DAY",
                $"{shipmentRate.OptionCode}-PDD",
                shipmentRate.Rate,
                shippingDays,
                checkUSHoliday,
                earliestShipDate,
                targetDeliveryDate);
        }

        /// <summary>Builds simple request hash.</summary>
        /// <param name="origin">              The origin.</param>
        /// <param name="destination">         Destination for the.</param>
        /// <param name="subTotal">            The sub total.</param>
        /// <param name="preferredDeliveryDay">The preferred delivery day.</param>
        /// <param name="provider">            The provider.</param>
        /// <returns>An ulong.</returns>
        private static long BuildSimpleRequestHash(
           IContactModel origin,
           IContactModel destination,
           decimal subTotal,
           string? preferredDeliveryDay,
           string provider)
        {
            var toHash = (
                subTotal,
                preferredDeliveryDay,
                origin: (
                    origin.Address?.Street1,
                    origin.Address?.Street2,
                    origin.Address?.Street3,
                    origin.Address?.City,
                    origin.Address?.RegionID,
                    origin.Address?.CountryID,
                    origin.Address?.PostalCode),
                destination: (
                    destination.Address?.Street1,
                    destination.Address?.Street2,
                    destination.Address?.Street3,
                    destination.Address?.City,
                    destination.Address?.RegionID,
                    destination.Address?.CountryID,
                    destination.Address?.PostalCode));
            return Digest.Crc64(
                toHash,
                (x, writer) =>
                {
                    // TODO: More specific data points for hashing
                    writer.Write(provider);
                    writer.Write(JsonConvert.SerializeObject(x.subTotal));
                    writer.Write(JsonConvert.SerializeObject(x.preferredDeliveryDay));
                    writer.Write(JsonConvert.SerializeObject(x.origin));
                    writer.Write(JsonConvert.SerializeObject(x.destination));
                });
        }
    }
}
