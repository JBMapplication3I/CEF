// <copyright file="UPSShipmentLocationExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UPS shipment location extensions class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.UPS
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Shipping;
    using UPSPackageRateService;
    using Utilities;

    /// <summary>A shipment location extensions.</summary>
    public static class UPSShipmentLocationExtensions
    {
        /// <summary>An IAddressModel extension method that converts a location to the ups address type.</summary>
        /// <param name="location">The location to act on.</param>
        /// <returns>location as an AddressType.</returns>
        public static ShipAddressType ToUPSAddressType(this IContactModel? location)
        {
            Contract.RequiresNotNull(location?.Address);
            var addressType = new ShipAddressType();
            if (!string.IsNullOrWhiteSpace(location!.Address!.Street1))
            {
                var addressStreets = new List<string> { location.Address.Street1! };
                if (!string.IsNullOrWhiteSpace(location.Address.Street2))
                {
                    addressStreets.Add(location.Address.Street2!);
                }
                if (!string.IsNullOrWhiteSpace(location.Address.Street3))
                {
                    addressStreets.Add(location.Address.Street3!);
                }
                addressType.AddressLine = addressStreets.ToArray();
            }
            if (!string.IsNullOrWhiteSpace(location.Address.City))
            {
                addressType.City = location.Address.City;
            }
            if (!string.IsNullOrWhiteSpace(location.Address.PostalCode))
            {
                addressType.PostalCode = location.Address.PostalCode;
            }
            if (!string.IsNullOrWhiteSpace(location.Address.RegionCode))
            {
                addressType.StateProvinceCode = location.Address.RegionCode;
            }
            if (!string.IsNullOrWhiteSpace(location.Address.CountryCode))
            {
                addressType.CountryCode = location.Address.CountryCode!.Replace("USA", "US");
            }
            return addressType;
        }

        /// <summary>An IAddressModel extension method that converts a location to the ups ship to address type.</summary>
        /// <param name="location">The location to act on.</param>
        /// <returns>location as a ShipToAddressType.</returns>
        public static ShipToAddressType ToUPSShipToAddressType(this IContactModel? location)
        {
            Contract.RequiresNotNull(location?.Address);
            var addressType = new ShipToAddressType();
            if (!string.IsNullOrWhiteSpace(location!.Address!.Street1))
            {
                var addressStreets = new List<string> { location.Address.Street1! };
                if (!string.IsNullOrWhiteSpace(location.Address.Street2))
                {
                    addressStreets.Add(location.Address.Street2!);
                }
                if (!string.IsNullOrWhiteSpace(location.Address.Street3))
                {
                    addressStreets.Add(location.Address.Street3!);
                }
                addressType.AddressLine = addressStreets.ToArray();
            }
            if (!string.IsNullOrWhiteSpace(location.Address.City))
            {
                addressType.City = location.Address.City;
            }
            if (!string.IsNullOrWhiteSpace(location.Address.PostalCode))
            {
                addressType.PostalCode = location.Address.PostalCode;
            }
            if (!string.IsNullOrWhiteSpace(location.Address.RegionCode))
            {
                addressType.StateProvinceCode = location.Address.RegionCode;
            }
            if (!string.IsNullOrWhiteSpace(location.Address.CountryCode))
            {
                addressType.CountryCode = location.Address.CountryCode!.Replace("USA", "US");
            }
            return addressType;
        }

        /// <summary>An <see cref="IList{IProviderShipment}"/> extension method that converts the items to the UPS
        /// package type array.</summary>
        /// <param name="items">                                 The items to act on.</param>
        /// <param name="combinePackagesWhenGettingShippingRate">true to combine packages when getting shipping rate.</param>
        /// <param name="useDimensionalWeight">                  true to use dimensional weight.</param>
        /// <returns>items as a PackageType[].</returns>
        public static PackageType[] ToUPSPackageTypeArray(
            this IList<IProviderShipment> items,
            bool combinePackagesWhenGettingShippingRate,
            bool useDimensionalWeight)
        {
            Contract.RequiresNotNull(items);
            var packages = combinePackagesWhenGettingShippingRate
                ? CombinePackages(items, useDimensionalWeight)
                : GetPackages(items, useDimensionalWeight);
            return packages.ToArray();
        }

        /// <summary>A RatedShipmentType[] extension method that converts the rates to a shipment rates.</summary>
        /// <param name="rates">The rates to act on.</param>
        /// <param name="rateIncludeList">specific rate to keep.</param>
        /// <returns>rates as a List{IShipmentRate}.</returns>
        public static List<IShipmentRate> ToShipmentRates(this RatedShipmentType[] rates, string[]? rateIncludeList)
        {
            var upsRates = new List<IShipmentRate>();
            foreach (var rate in Contract.RequiresNotNull(rates))
            {
                if (Contract.CheckNotEmpty(rateIncludeList) && !rateIncludeList!.Contains(rate.Service.Code))
                {
                    continue;
                }
                var arrival = DateExtensions.GenDateTime;
                var hasDeliveryTime = Contract.CheckValidKey(rate.GuaranteedDelivery?.DeliveryByTime)
                    && DateTime.TryParse(rate.GuaranteedDelivery!.DeliveryByTime, out arrival);
                if (hasDeliveryTime && arrival.Hour == 0 && arrival.Minute == 0)
                {
                    // It says midnight, but they aren't actually specifying, so assign 23:59
                    arrival = arrival.AddHours(23).AddMinutes(59);
                }
                var optionName = rate.Service.Code.GetMethodName();
                var amount = decimal.Parse(rate.TotalCharges.MonetaryValue);
                upsRates.Add(new ShipmentRate
                {
                    CarrierName = "UPS",
                    Rate = amount,
                    FullOptionName = $"{optionName} ({amount:C2})",
                    OptionName = optionName,
                    OptionCode = rate.Service.Code,
                    EstimatedArrival = hasDeliveryTime ? arrival : null,
                });
            }
            return upsRates;
        }

        private static List<PackageType> GetPackages(IEnumerable<IProviderShipment> items, bool useDimensionalWeight)
        {
            return items.Select(t => ToPackageType(t, useDimensionalWeight, true)).ToList();
        }

        private static List<PackageType> CombinePackages(IList<IProviderShipment> items, bool useDimensionalWeight)
        {
            var firstItem = items.FirstOrDefault();
            if (firstItem == null)
            {
                return new();
            }
            var weights = 0m;
            var volume = 0m;
            foreach (var item in items)
            {
                var weight = useDimensionalWeight
                    ? ShippingProviderBase.ConvertToPounds(item.DimensionalWeight, item.DimensionalWeightUnitOfMeasure)
                    : ShippingProviderBase.ConvertToPounds(item.Weight, item.WeightUnitOfMeasure);
                var quantity = (int)(item.PackageQuantity ?? 1);
                weights += weight * quantity;
                volume += Volume(item) * quantity;
            }
            ////item.Height.HasValue ? (float) Math.Pow(quantity, 1f/3f) * (float) item.Height.Value : 0;
            firstItem.ItemName = "Combined Package";
            firstItem.ItemCode = string.Join(";", items.Where(i => !string.IsNullOrWhiteSpace(i.ItemCode)).Select(i => i.ItemCode));
            firstItem.PackageQuantity = 1;
            firstItem.Weight = Math.Round(weights, 2, MidpointRounding.AwayFromZero);
            var side = useDimensionalWeight ? (decimal?)Math.Pow((double)volume, 1d / 3d) : null;
            firstItem.Height = side;
            firstItem.Depth = side;
            firstItem.Width = side;
            return new() { firstItem.ToPackageType(useDimensionalWeight) };
        }

        private static decimal Volume(IProviderShipment item)
        {
            if (!(item.Height.HasValue && item.Depth.HasValue && item.Width.HasValue))
            {
                return 0;
            }
            return Math.Round(item.Height.Value, 0)
                * Math.Round(item.Depth.Value, 0)
                * Math.Round(item.Width.Value, 0);
        }

        private static PackageType ToPackageType(this IProviderShipment item, bool useDimensionalWeight, bool convertWeight = false)
        {
            return new()
            {
                PackagingType = new() { Code = "02" },
                PackageWeight = new()
                {
                    UnitOfMeasurement = new() { Code = "LBS" }, // It's always converted to LBS
                    Weight = (convertWeight ? Math.Round(item.Weight, 2) : item.Weight).ToString(CultureInfo.InvariantCulture),
                },
                Dimensions = useDimensionalWeight ? ItemDimensions(item) : null, // Added to support Dimensional Weight calculations
            };
        }

        private static DimensionsType? ItemDimensions(IProviderShipment item)
        {
            if (!(item.Height.HasValue && item.Depth.HasValue && item.Width.HasValue))
            {
                return null;
            }
            return new()
            {
                UnitOfMeasurement = new() { Code = "IN" }, // Always converted to Inches
                Length = Math.Round(ShippingProviderBase.ConvertToInches(item.Depth.Value, item.DepthUnitOfMeasure), 0).ToString(CultureInfo.InvariantCulture),
                Width = Math.Round(ShippingProviderBase.ConvertToInches(item.Width.Value, item.WidthUnitOfMeasure), 0).ToString(CultureInfo.InvariantCulture),
                Height = Math.Round(ShippingProviderBase.ConvertToInches(item.Height.Value, item.HeightUnitOfMeasure), 0).ToString(CultureInfo.InvariantCulture),
            };
        }

        /// <summary>A string extension method that gets method name.</summary>
        /// <param name="code">The code to act on.</param>
        /// <returns>The method name.</returns>
        private static string? GetMethodName(this string code)
        {
            var translations = new List<KeyValuePair<string, string>>
            {
                new("01", "UPS Next Day Air"),
                new("02", "UPS 2nd Day Air"),
                new("03", "UPS Ground"),
                new("12", "UPS 3 Day Select"),
                new("13", "UPS Next Day Air Saver"),
                new("14", "UPS Next Day Air Early AM"),
                new("59", "UPS 2nd Day Air AM"),
                new("07", "UPS Worldwide Express"),
                new("08", "UPS Worldwide Expedited"),
                new("11", "UPS Standard"),
                new("54", "UPS Worldwide Express Plus"),
                new("65", "UPS Saver"),
                new("82", "UPS Today Standard"),
                new("83", "UPS Today Dedicated Courier"),
                new("84", "UPS Today Intercity"),
                new("85", "UPS Today Express"),
                new("86", "UPS Today Express Saver"),
                new("96", "UPS Worldwide Express Freight"),
            };
            return translations.Where(m => m.Key == code).Select(s => s.Value).FirstOrDefault();
        }
    }
}
