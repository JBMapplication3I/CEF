// <copyright file="FedExExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FedEx extensions class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.FedEx
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
#if NET5_0_OR_GREATER
    using FedExRateService5;
#else
    using FedExRateService;
#endif
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Shipping;
    using Utilities;

    /// <summary>A FedEx extensions.</summary>
    public static class FedExExtensions
    {
        /// <summary>An <see cref="IReadOnlyCollection{IProviderShipment}"/> extension method that converts the items to
        /// a FedEx line item list.</summary>
        /// <param name="items">                                 The items to act on.</param>
        /// <param name="combinePackagesWhenGettingShippingRate">true to combine packages when getting shipping rate.</param>
        /// <param name="useDimensionalWeight">                  true to use dimensional weight.</param>
        /// <param name="contextProfileName">                    Name of the context profile.</param>
        /// <returns>items converted to an array of <see cref="RequestedPackageLineItem"/>.</returns>
        public static RequestedPackageLineItem[]? ToFedExLineItemArray(
            this IReadOnlyCollection<IProviderShipment> items,
            bool combinePackagesWhenGettingShippingRate,
            bool useDimensionalWeight,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(items);
            return (combinePackagesWhenGettingShippingRate
                    ? CombinePackages(items, useDimensionalWeight, contextProfileName)
                    : GetPackageLineItems(items, useDimensionalWeight))
                ?.ToArray();
        }

        /// <summary>An <see cref="IContactModel"/> extension method that converts a location to a FedEx address.</summary>
        /// <param name="location">The location to act on.</param>
        /// <returns>The <see cref="IContactModel"/> converted to an <see cref="Address"/>.</returns>
        internal static Address ToFedExAddress(this IContactModel location)
        {
            Contract.RequiresNotNull(
                location?.Address,
                $"ERROR! {nameof(ToFedExAddress)} can not process a null {nameof(IContactModel)} to an {nameof(Address)}");
            var address = new Address();
            if (Contract.CheckValidKey(location!.Address!.Street1))
            {
                var streetAddresses = new List<string> { location.Address.Street1! };
                if (Contract.CheckValidKey(location.Address.Street2))
                {
                    streetAddresses.Add(location.Address.Street2!);
                }
                if (Contract.CheckValidKey(location.Address.Street3))
                {
                    streetAddresses.Add(location.Address.Street3!);
                }
                address.StreetLines = streetAddresses.ToArray();
            }
            if (Contract.CheckValidKey(location.Address.City))
            {
                address.City = location.Address.City;
            }
            if (Contract.CheckValidKey(location.Address.RegionCode))
            {
                address.StateOrProvinceCode = location.Address.RegionCode;
            }
            if (Contract.CheckValidKey(location.Address.PostalCode))
            {
                address.PostalCode = location.Address.PostalCode;
            }
            if (Contract.CheckValidKey(location.Address.CountryCode))
            {
                address.CountryCode = location.Address.CountryCode!.Replace("USA", "US");
            }
            return address;
        }

        /// <summary>Am array of <see cref="RateReplyDetail"/> extension method that converts the rates to a shipping
        /// rate.</summary>
        /// <param name="rates">          The rates to act on.</param>
        /// <param name="rateIncludeList">List of rate includes.</param>
        /// <param name="useBaseCharge">  True to use base charge.</param>
        /// <param name="shipTime">       The ship time.</param>
        /// <returns>rates converted to a <see cref="List{IShipmentRate}"/>.</returns>
        internal static List<IShipmentRate> ToShippingRate(
            this RateReplyDetail[]? rates,
            string[]? rateIncludeList,
            bool useBaseCharge,
            DateTime shipTime)
        {
            if (rates == null)
            {
                return new();
            }
            var providerRates = new List<IShipmentRate>();
            if (useBaseCharge)
            {
                providerRates.AddRange(
                    from rate in rates
                    from shipment in rate.RatedShipmentDetails
                    let optionName = rate.ServiceTypeSpecified ? rate.ServiceType.GetMethodName() : "Unknown Option"
                    let amount = shipment.ShipmentRateDetail.TotalBaseCharge.Amount
                    where !rateIncludeList!.Any() || rateIncludeList!.Contains(rate.ServiceType.ToString())
                    select new ShipmentRate
                    {
                        CarrierName = "FedEx",
                        Rate = amount,
                        FullOptionName = $"{optionName} (${amount})",
                        OptionName = optionName,
                        OptionCode = rate.ServiceType.ToString(),
                        TargetShipping = shipTime,
                        EstimatedArrival = rate.DeliveryTimestampSpecified
                            ? rate.DeliveryTimestamp
                            : rate.TransitTimeSpecified
                                ? DateExtensions.GenDateTime.Date.AddDays(rate.TransitTime.ToDays())
                                : DateExtensions.GenDateTime.Date.AddDays(7),
                        EstimatedArrivalMax = rate.MaximumTransitTimeSpecified
                            ? DateExtensions.GenDateTime.Date.AddDays(rate.MaximumTransitTime.ToDays())
                            : null,
                        DeliveryDayOfWeek = rate.DeliveryDayOfWeekSpecified ? rate.DeliveryDayOfWeek.ToString() : "Not Specified",
                        AppliedAccessorials = rate.AppliedOptions?.Select(x => x.ToString()).ToArray(),
                        SignatureOption = rate.SignatureOptionSpecified ? rate.SignatureOption.ToString() : "Not Specified",
                    });
            }
            else
            {
                providerRates.AddRange(
                   from rate in rates
                   from shipment in rate.RatedShipmentDetails
                   let optionName = rate.ServiceTypeSpecified ? rate.ServiceType.GetMethodName() : "Unknown Option"
                   let amount = shipment.ShipmentRateDetail.TotalNetCharge.Amount
                   where rateIncludeList == null || !rateIncludeList.Any() || rateIncludeList.Contains(rate.ServiceType.ToString())
                   select new ShipmentRate
                   {
                       CarrierName = "FedEx",
                       Rate = amount,
                       FullOptionName = $"{optionName} (${amount})",
                       OptionName = optionName,
                       OptionCode = rate.ServiceType.ToString(),
                       TargetShipping = shipTime,
                       EstimatedArrival = rate.DeliveryTimestampSpecified
                           ? rate.DeliveryTimestamp
                           : rate.TransitTimeSpecified
                               ? DateExtensions.GenDateTime.Date.AddDays(rate.TransitTime.ToDays())
                               : DateExtensions.GenDateTime.Date.AddDays(7),
                       EstimatedArrivalMax = rate.MaximumTransitTimeSpecified
                           ? DateExtensions.GenDateTime.Date.AddDays(rate.MaximumTransitTime.ToDays())
                           : null,
                       DeliveryDayOfWeek = rate.DeliveryDayOfWeekSpecified ? rate.DeliveryDayOfWeek.ToString() : "Not Specified",
                       AppliedAccessorials = rate.AppliedOptions?.Select(x => x.ToString()).ToArray(),
                       SignatureOption = rate.SignatureOptionSpecified ? rate.SignatureOption.ToString() : "Not Specified",
                   });
            }
            return providerRates;
        }

        /// <summary>Gets package line items.</summary>
        /// <param name="items">               The items to act on.</param>
        /// <param name="useDimensionalWeight">true to use dimensional weight.</param>
        /// <returns>The package line items.</returns>
        private static List<RequestedPackageLineItem> GetPackageLineItems(
            IEnumerable<IProviderShipment> items,
            bool useDimensionalWeight)
        {
            return items
                .Select((t, index) => t.ToRequestedPackageLineItem(index + 1, useDimensionalWeight))
                .ToList();
        }

        /// <summary>Combine packages.</summary>
        /// <param name="items">               The items to act on.</param>
        /// <param name="useDimensionalWeight">true to use dimensional weight.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>A List{RequestedPackageLineItem}.</returns>
        private static List<RequestedPackageLineItem>? CombinePackages(
            IReadOnlyCollection<IProviderShipment> items,
            bool useDimensionalWeight,
            string? contextProfileName)
        {
            if (Contract.CheckEmpty(items))
            {
                return new();
            }
            var combinedPackage = ShippingProviderBase.CombinePackages(items, useDimensionalWeight, contextProfileName);
            if (combinedPackage is null)
            {
                return null;
            }
            return new()
            {
                combinedPackage.ToRequestedPackageLineItem(1, useDimensionalWeight),
            };
        }

        /// <summary>An IProviderShipment extension method that converts this FedExExtensions to a
        /// requested package line item.</summary>
        /// <param name="item">                The item.</param>
        /// <param name="sequenceNumber">      The sequence number.</param>
        /// <param name="useDimensionalWeight">true to use dimensional weight.</param>
        /// <returns>The given data converted to a RequestedPackageLineItem.</returns>
        private static RequestedPackageLineItem ToRequestedPackageLineItem(
            this IProviderShipment item,
            int sequenceNumber,
            bool useDimensionalWeight)
        {
            Contract.RequiresNotNull(item);
            return new()
            {
                SequenceNumber = sequenceNumber.ToString(), // package sequence number
                GroupPackageCount = "1",
                Weight = new()
                {
                    Units = WeightUnits.LB,
                    UnitsSpecified = true,
                    ValueSpecified = true,
                    Value = ShippingProviderBase.ConvertToPounds(
                        item.Weight,
                        useDimensionalWeight ? item.DimensionalWeightUnitOfMeasure : item.WeightUnitOfMeasure),
                },
                Dimensions = useDimensionalWeight ? ItemDimensions(item) : null,
            };
        }

        /// <summary>Item dimensions.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The Dimensions.</returns>
        private static Dimensions? ItemDimensions(IProviderShipment item)
        {
            if (!(item.Height.HasValue && item.Depth.HasValue && item.Width.HasValue))
            {
                return null;
            }
            return new()
            {
                Units = LinearUnits.IN,
                UnitsSpecified = true,
                Length = ConvertToInchesRoundAndToString(item.Depth.Value, item.DepthUnitOfMeasure),
                Width = ConvertToInchesRoundAndToString(item.Width.Value, item.WidthUnitOfMeasure),
                Height = ConvertToInchesRoundAndToString(item.Height.Value, item.HeightUnitOfMeasure),
            };
        }

        /// <summary>Converts this FedExExtensions to the inches round and to string.</summary>
        /// <param name="value">The value.</param>
        /// <param name="uofm"> The current unit of measure.</param>
        /// <returns>The given data converted to the inches round and to string.</returns>
        private static string ConvertToInchesRoundAndToString(decimal value, string? uofm)
        {
            return Math.Round(ShippingProviderBase.ConvertToInches(value, uofm), 0)
                .ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>A ServiceType extension method that gets method name.</summary>
        /// <param name="type">The type to act on.</param>
        /// <returns>The method name.</returns>
        private static string GetMethodName(this ServiceType type)
        {
            const string Prefix = "FedEx ";
            switch (type)
            {
                case ServiceType.EUROPE_FIRST_INTERNATIONAL_PRIORITY:
                {
                    return Prefix + "Europe First International Priority";
                }
                case ServiceType.FEDEX_1_DAY_FREIGHT:
                {
                    return Prefix + "1 Day Freight";
                }
                case ServiceType.FEDEX_2_DAY:
                {
                    return Prefix + "2 Day";
                }
                case ServiceType.FEDEX_2_DAY_AM:
                {
                    return Prefix + "2 Day AM";
                }
                case ServiceType.FEDEX_2_DAY_FREIGHT:
                {
                    return Prefix + "2 Day Freight";
                }
                case ServiceType.FEDEX_3_DAY_FREIGHT:
                {
                    return Prefix + "3 Day Freight";
                }
                case ServiceType.FEDEX_DISTANCE_DEFERRED:
                {
                    return Prefix + "Distance Deferred";
                }
                case ServiceType.FEDEX_EXPRESS_SAVER:
                {
                    return Prefix + "Express Saver";
                }
                case ServiceType.FEDEX_FIRST_FREIGHT:
                {
                    return Prefix + "First Freight";
                }
                case ServiceType.FEDEX_FREIGHT_ECONOMY:
                {
                    return Prefix + "Freight Economy";
                }
                case ServiceType.FEDEX_FREIGHT_PRIORITY:
                {
                    return Prefix + "Freight Priority";
                }
                case ServiceType.FEDEX_GROUND:
                {
                    return Prefix + "Ground";
                }
                case ServiceType.FEDEX_NEXT_DAY_AFTERNOON:
                {
                    return Prefix + "Next Day Afternoon";
                }
                case ServiceType.FEDEX_NEXT_DAY_EARLY_MORNING:
                {
                    return Prefix + "Next Day Early Morning";
                }
                case ServiceType.FEDEX_NEXT_DAY_END_OF_DAY:
                {
                    return Prefix + "Next Day End of Day";
                }
                case ServiceType.FEDEX_NEXT_DAY_FREIGHT:
                {
                    return Prefix + "Next Day Freight";
                }
                case ServiceType.FEDEX_NEXT_DAY_MID_MORNING:
                {
                    return Prefix + "Next Day Freight";
                }
                case ServiceType.FIRST_OVERNIGHT:
                {
                    return Prefix + "Overnight";
                }
                case ServiceType.GROUND_HOME_DELIVERY:
                {
                    return Prefix + "Home Delivery";
                }
                case ServiceType.INTERNATIONAL_ECONOMY_FREIGHT:
                {
                    return Prefix + "International Economy Freight";
                }
                case ServiceType.INTERNATIONAL_FIRST:
                {
                    return Prefix + "International First";
                }
                case ServiceType.INTERNATIONAL_PRIORITY:
                {
                    return Prefix + "International Priority";
                }
                case ServiceType.INTERNATIONAL_PRIORITY_FREIGHT:
                {
                    return Prefix + "International Priority Freight";
                }
                case ServiceType.PRIORITY_OVERNIGHT:
                {
                    return Prefix + "Priority Freight";
                }
                case ServiceType.SAME_DAY:
                {
                    return Prefix + "Same Day";
                }
                case ServiceType.SAME_DAY_CITY:
                {
                    return Prefix + "Same Day City";
                }
                case ServiceType.SMART_POST:
                {
                    return Prefix + "Smart Post";
                }
                case ServiceType.STANDARD_OVERNIGHT:
                {
                    return Prefix + "Standard Overnight";
                }
                case ServiceType.INTERNATIONAL_ECONOMY:
                {
                    return Prefix + "International Economy";
                }
                default:
                {
                    return string.Empty;
                }
            }
        }

        private static int ToDays(this TransitTimeType value)
        {
            switch (value)
            {
                case TransitTimeType.TWENTY_DAYS:
                {
                    return 20;
                }
                case TransitTimeType.NINETEEN_DAYS:
                {
                    return 19;
                }
                case TransitTimeType.EIGHTEEN_DAYS:
                {
                    return 18;
                }
                case TransitTimeType.SEVENTEEN_DAYS:
                {
                    return 17;
                }
                case TransitTimeType.SIXTEEN_DAYS:
                {
                    return 16;
                }
                case TransitTimeType.FIFTEEN_DAYS:
                {
                    return 15;
                }
                case TransitTimeType.FOURTEEN_DAYS:
                {
                    return 14;
                }
                case TransitTimeType.THIRTEEN_DAYS:
                {
                    return 13;
                }
                case TransitTimeType.TWELVE_DAYS:
                {
                    return 12;
                }
                case TransitTimeType.ELEVEN_DAYS:
                {
                    return 11;
                }
                case TransitTimeType.TEN_DAYS:
                {
                    return 10;
                }
                case TransitTimeType.NINE_DAYS:
                {
                    return 9;
                }
                case TransitTimeType.EIGHT_DAYS:
                {
                    return 8;
                }
                case TransitTimeType.SEVEN_DAYS:
                {
                    return 7;
                }
                case TransitTimeType.SIX_DAYS:
                {
                    return 6;
                }
                case TransitTimeType.FIVE_DAYS:
                {
                    return 5;
                }
                case TransitTimeType.FOUR_DAYS:
                {
                    return 4;
                }
                case TransitTimeType.THREE_DAYS:
                {
                    return 3;
                }
                case TransitTimeType.TWO_DAYS:
                {
                    return 2;
                }
                case TransitTimeType.ONE_DAY:
                {
                    return 1;
                }
                case TransitTimeType.UNKNOWN:
                {
                    return 21;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }
    }
}
