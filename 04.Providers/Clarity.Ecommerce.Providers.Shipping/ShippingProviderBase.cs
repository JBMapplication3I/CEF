// <copyright file="ShippingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment provider base class</summary>
namespace Clarity.Ecommerce.Providers.Shipping
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Shipping;
    using JSConfigs;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A shipment provider.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IShippingProviderBase"/>
    public abstract class ShippingProviderBase : ProviderBase, IShippingProviderBase
    {
        private static readonly Dictionary<string, string> ConversionsToCommonNames = new()
        {
            ["lbs"] = "lb",
            ["pound"] = "lb",
            ["pounds"] = "lb",
            ["ozs"] = "oz",
            ["ounce"] = "oz",
            ["ounces"] = "oz",
            ["kgs"] = "kg",
            ["kilogram"] = "kg",
            ["kilograms"] = "kg",
            ["gs"] = "g",
            ["gram"] = "g",
            ["grams"] = "g",
            ["mgs"] = "mg",
            ["milligram"] = "mg",
            ["milligrams"] = "mg",
            ["ton"] = "ton",
            ["tons"] = "ton",
            ["tonne"] = "t",
            ["tonnes"] = "t",
            ["feet"] = "ft",
            ["foot"] = "ft",
            ["yds"] = "yd",
            ["yard"] = "yd",
            ["yards"] = "yd",
            ["mms"] = "mm",
            ["millimeter"] = "mm",
            ["millimeters"] = "mm",
            ["cms"] = "cm",
            ["centimeter"] = "cm",
            ["centimeters"] = "cm",
            ["ms"] = "m",
            ["meter"] = "m",
            ["meters"] = "m",
            ["kms"] = "km",
            ["kilometer"] = "km",
            ["kilometers"] = "km",
        };

        private static readonly Dictionary<string, decimal> ConversionsToPounds = new()
        {
            ["lb"] = 1m,
            ["oz"] = 1m / 16m,
            ["kg"] = 1m / 0.453_592m,
            ["g"] = 1m / 453.592m,
            ["mg"] = 1m / 453_592m,
            ["ton"] = 1m / 2_000m, // US Short Ton | ton | t | 2,000 lbs
            ["t"] = 1m / 2_204.623m, // Metric Ton | tonne | MT | 2,204.623 lbs
        };

        private static readonly Dictionary<string, decimal> ConversionsToInches = new()
        {
            ["ft"] = 12m, // 12 inches
            ["yd"] = 36m, // 36 inches
            ["mm"] = 1m / 25.4m,
            ["cm"] = 1m / 2.54m,
            ["m"] = 39.370_079m, // 39.37 inches
            ["km"] = 39_370.079m,
        };

        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Shipping;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <summary>Combine packages.</summary>
        /// <param name="items">               The items to act on.</param>
        /// <param name="useDimensionalWeight">true to use dimensional weight.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>A List{RequestedPackageLineItem}.</returns>
        public static IProviderShipment? CombinePackages(
            IReadOnlyCollection<IProviderShipment>? items,
            bool useDimensionalWeight,
            string? contextProfileName)
        {
            if (items == null || items.Count == 0)
            {
                return null;
            }
            var combinedPackageWeight = 0m;
            var combinedPackageVolume = 0m;
            foreach (var item in items)
            {
                var weight = useDimensionalWeight
                    ? ConvertToPounds(item.DimensionalWeight, item.DimensionalWeightUnitOfMeasure)
                    : ConvertToPounds(item.Weight, item.WeightUnitOfMeasure);
                var quantity = (int)(item.PackageQuantity ?? 1);
                combinedPackageWeight += weight * quantity;
                combinedPackageVolume += Volume(item) * quantity;
            }
            var side = useDimensionalWeight
                ? (decimal?)Math.Pow((double)combinedPackageVolume, 1d / 3d)
                : null;
            var shipment = RegistryLoaderWrapper.GetInstance<IProviderShipment>(contextProfileName);
            shipment.ItemName = "Combined Package";
            shipment.ItemCode = items
                .Where(i => !string.IsNullOrWhiteSpace(i.ItemCode))
                .Select(i => i.ItemCode)
                .DefaultIfEmpty(string.Empty)
                .Aggregate((c, n) => $"{c};{n}");
            shipment.PackageQuantity = 1;
            shipment.Weight = combinedPackageWeight;
            shipment.Height = side;
            shipment.Depth = side;
            shipment.Width = side;
            return shipment;
        }

        /// <summary>Converts this ShippingProviderBase to the pounds.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <returns>The given data converted to the pounds.</returns>
        public static decimal ConvertToPounds(decimal value, string? unitOfMeasure)
        {
            var key = unitOfMeasure?.Trim().ToLower() ?? string.Empty;
            if (ConversionsToCommonNames.ContainsKey(key))
            {
                key = ConversionsToCommonNames[key];
            }
            if (!ConversionsToPounds.ContainsKey(key))
            {
                return value;
            }
            return value * ConversionsToPounds[key];
        }

        /// <summary>Converts this ShippingProviderBase to the inches.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <returns>The given data converted to the inches.</returns>
        public static decimal ConvertToInches(decimal value, string? unitOfMeasure)
        {
            var key = unitOfMeasure?.Trim().ToLower() ?? string.Empty;
            if (ConversionsToCommonNames.ContainsKey(key))
            {
                key = ConversionsToCommonNames[key];
            }
            if (!ConversionsToInches.ContainsKey(key))
            {
                return value;
            }
            return value * ConversionsToInches[key];
        }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<List<IShipmentRate>> GetBaseOrNetChargesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            bool useBaseCharge,
            string? contextProfileName);

        /// <summary>Reads the settings.</summary>
        /// <returns>The settings.</returns>
        protected static (bool companyLeadTimeEnabled,
            Dictionary<DayOfWeek, bool> dayEnabled,
            Dictionary<DayOfWeek, TimeSpan> startHours,
            Dictionary<DayOfWeek, TimeSpan> endHours,
            TimeSpan normal,
            TimeSpan expedited) ReadSettings()
        {
            // TODO@JTG: Cache the result of this function in static memory so it doesn't have to be read again
            var dayEnabled = new Dictionary<DayOfWeek, bool>
            {
                [DayOfWeek.Monday] = CEFConfigDictionary.ShippingCompanyLeadTimeEnabledMonday,
                [DayOfWeek.Tuesday] = CEFConfigDictionary.ShippingCompanyLeadTimeEnabledTuesday,
                [DayOfWeek.Wednesday] = CEFConfigDictionary.ShippingCompanyLeadTimeEnabledWednesday,
                [DayOfWeek.Thursday] = CEFConfigDictionary.ShippingCompanyLeadTimeEnabledThursday,
                [DayOfWeek.Friday] = CEFConfigDictionary.ShippingCompanyLeadTimeEnabledFriday,
                [DayOfWeek.Saturday] = CEFConfigDictionary.ShippingCompanyLeadTimeEnabledSaturday,
                [DayOfWeek.Sunday] = CEFConfigDictionary.ShippingCompanyLeadTimeEnabledSunday,
            };
            var startHours = new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursStartHourMonday,
                [DayOfWeek.Tuesday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursStartHourTuesday,
                [DayOfWeek.Wednesday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursStartHourWednesday,
                [DayOfWeek.Thursday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursStartHourThursday,
                [DayOfWeek.Friday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursStartHourFriday,
                [DayOfWeek.Saturday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursStartHourSaturday,
                [DayOfWeek.Sunday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursStartHourSunday,
            };
            var endHours = new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursEndHourMonday,
                [DayOfWeek.Tuesday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursEndHourTuesday,
                [DayOfWeek.Wednesday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursEndHourWednesday,
                [DayOfWeek.Thursday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursEndHourThursday,
                [DayOfWeek.Friday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursEndHourFriday,
                [DayOfWeek.Saturday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursEndHourSaturday,
                [DayOfWeek.Sunday] = CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursEndHourSunday,
            };
            return (CEFConfigDictionary.ShippingCompanyLeadTimeEnabled,
                dayEnabled,
                startHours,
                endHours,
                CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursNormal,
                CEFConfigDictionary.ShippingCompanyLeadTimeInBusinessHoursExpedited);
        }

        /// <summary>Gets existing rate quotes for hash.</summary>
        /// <param name="hash">              The hash.</param>
        /// <param name="cartID">            Identifier for the cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The existing rate quotes for hash.</returns>
        protected static List<IRateQuoteModel> GetExistingRateQuotesForHash(
            long hash,
            int cartID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // 17 = 5:00 PM, use as cutoff in that they must ask the server for next day target ships
            var now = DateTime.Now;
            var (_, dayEnabled, _, endHours, _, _) = ReadSettings();
            var dayToCheck = now;
            var daysBack = 0;
            while (!dayEnabled[dayToCheck.DayOfWeek])
            {
                daysBack++;
                dayToCheck = dayToCheck.AddDays(-1);
                if (daysBack > 7)
                {
                    // Invalid config, so just reset to 0 and step out
                    daysBack = 0;
                    break;
                }
            }
            var modifiedSince = now.TimeOfDay > endHours[dayToCheck.DayOfWeek]
                // Only since 5:00 PM today
                ? DateTime.Today.AddDays(-daysBack).Add(endHours[dayToCheck.DayOfWeek])
                // Up to 5:00 PM yesterday // TODO: Walk this back day by day
                : DateTime.Today.AddDays(-daysBack).Add(endHours[dayToCheck.DayOfWeek]).AddDays(-1);
            return context.RateQuotes
                .AsNoTracking()
                .Where(x => x.Active
                         && x.CartHash == hash
                         && x.CartID == cartID
                         && (x.UpdatedDate >= modifiedSince || x.CreatedDate >= modifiedSince))
                .Select(x => new
                {
                    // Base Properties
                    x.ID,
                    x.CustomKey,
                    x.Active,
                    x.CreatedDate,
                    x.UpdatedDate,
                    x.Hash,
                    x.JsonAttributes,
                    // NameableBase Properties
                    x.Name,
                    x.Description,
                    // RateQuote Properties
                    x.CartHash,
                    x.Rate,
                    x.RateTimestamp,
                    x.Selected,
                    x.EstimatedDeliveryDate,
                    x.TargetShippingDate,
                    // Related Objects
                    x.CartID,
                    x.ShipCarrierMethodID,
                    ShipCarrierMethodKey = x.ShipCarrierMethod!.CustomKey,
                    ShipCarrierMethodName = x.ShipCarrierMethod.Name,
                    x.ShipCarrierMethod.ShipCarrierID,
                    ShipCarrierKey = x.ShipCarrierMethod.ShipCarrier!.CustomKey,
                    ShipCarrierName = x.ShipCarrierMethod.ShipCarrier.Name,
                })
                .ToList()
                .Select(x =>
                {
                    var model = RegistryLoaderWrapper.GetInstance<IRateQuoteModel>(contextProfileName);
                    // Base Properties
                    model.ID = x.ID;
                    model.CustomKey = x.CustomKey;
                    model.Active = x.Active;
                    model.CreatedDate = x.CreatedDate;
                    model.UpdatedDate = x.UpdatedDate;
                    model.Hash = x.Hash;
                    model.SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary();
                    // NameableBase Properties
                    model.Name = x.Name;
                    model.Description = x.Description;
                    // RateQuote Properties
                    model.CartHash = x.CartHash;
                    model.Rate = x.Rate;
                    model.RateTimestamp = x.RateTimestamp;
                    model.Selected = x.Selected;
                    model.EstimatedDeliveryDate = x.EstimatedDeliveryDate;
                    model.TargetShippingDate = x.TargetShippingDate;
                    // Related Objects
                    model.CartID = x.CartID;
                    model.ShipCarrierMethodID = x.ShipCarrierMethodID;
                    model.ShipCarrierMethodKey = x.ShipCarrierMethodKey;
                    model.ShipCarrierMethodName = x.ShipCarrierMethodName;
                    model.ShipCarrierID = x.ShipCarrierID;
                    model.ShipCarrierKey = x.ShipCarrierKey;
                    model.ShipCarrierName = x.ShipCarrierName;
                    return model;
                })
                .ToList();
        }

        /// <summary>Saves a rate quotes to table and return results.</summary>
        /// <param name="cartID">                 Identifier for the cart.</param>
        /// <param name="hash">                   The hash.</param>
        /// <param name="currentShippingProvider">The current shipping provider.</param>
        /// <param name="rates">                  The rates to save.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>A List{IRateQuoteModel}.</returns>
        protected static async Task<List<IRateQuoteModel>> SaveRateQuotesToTableAndReturnResultsAsync(
            int cartID,
            long hash,
            string? currentShippingProvider,
            IEnumerable<IShipmentRate>? rates,
            string? contextProfileName,
            string? selectedRate = null)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var timestamp = DateExtensions.GenDateTime;
                var newEntities = new List<DataModel.RateQuote>();
                foreach (var rate in rates!)
                {
                    var carrierMethodID = await context.ShipCarrierMethods
                        .AsNoTracking()
                        .Where(x => x.Active
                            && x.CustomKey == rate.OptionCode
                            && x.ShipCarrier != null && x.ShipCarrier.Name == rate.CarrierName)
                        .Select(y => y.ID)
                        .FirstOrDefaultAsync();
                    if (carrierMethodID <= 0)
                    {
                        var carrierID = await context.ShipCarriers
                            .AsNoTracking()
                            .Where(x => x.Active && x.Name == rate.CarrierName)
                            .Select(y => y.ID)
                            .FirstOrDefaultAsync();
                        if (carrierID <= 0)
                        {
                            var newCarrier = new DataModel.ShipCarrier
                            {
                                Active = true,
                                CreatedDate = timestamp,
                                CustomKey = rate.CarrierName,
                                Name = rate.CarrierName,
                            };
                            context.ShipCarriers.Add(newCarrier);
                            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                            carrierID = newCarrier.ID;
                        }
                        var newCarrierMethod = new DataModel.ShipCarrierMethod
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            CustomKey = rate.OptionCode,
                            Name = rate.OptionName,
                            ShipCarrierID = carrierID,
                        };
                        context.ShipCarrierMethods.Add(newCarrierMethod);
                        await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                        carrierMethodID = newCarrierMethod.ID;
                    }
                    var dummy = RegistryLoaderWrapper.GetInstance<IRateQuoteModel>(contextProfileName);
                    dummy.SerializableAttributes = new();
                    if (Contract.CheckValidKey(rate.DeliveryDayOfWeek))
                    {
                        dummy.SerializableAttributes[nameof(rate.DeliveryDayOfWeek)] = new()
                        {
                            Key = nameof(rate.DeliveryDayOfWeek),
                            Value = rate.DeliveryDayOfWeek!,
                        };
                    }
                    if (Contract.CheckValidKey(rate.SignatureOption))
                    {
                        dummy.SerializableAttributes[nameof(rate.SignatureOption)] = new()
                        {
                            Key = nameof(rate.SignatureOption),
                            Value = rate.SignatureOption!,
                        };
                    }
                    if (Contract.CheckValidKey(string.Join(",", rate.AppliedAccessorials ?? Array.Empty<string>())))
                    {
                        dummy.SerializableAttributes[nameof(rate.AppliedAccessorials)] = new()
                        {
                            Key = nameof(rate.AppliedAccessorials),
                            Value = string.Join(",", rate.AppliedAccessorials!),
                        };
                    }
                    if (Contract.CheckValidKey(rate.EstimatedArrival?.ToString("O")))
                    {
                        dummy.SerializableAttributes[nameof(rate.EstimatedArrival)] = new()
                        {
                            Key = nameof(rate.EstimatedArrival),
                            Value = rate.EstimatedArrival!.Value.ToString("O")!,
                        };
                    }
                    if (Contract.CheckValidKey(rate.EstimatedArrivalMax?.ToString("O")))
                    {
                        dummy.SerializableAttributes[nameof(rate.EstimatedArrivalMax)] = new()
                        {
                            Key = nameof(rate.EstimatedArrivalMax),
                            Value = rate.EstimatedArrivalMax!.Value.ToString("O")!,
                        };
                    }
                    var toAdd = new DataModel.RateQuote
                    {
                        // Base Properties
                        CustomKey = $"{cartID}|{hash}|{rate.CarrierName}|{rate.OptionCode}",
                        Active = true,
                        CreatedDate = timestamp,
                        // NameableBase Properties
                        Name = rate.FullOptionName,
                        // RateQuote Properties
                        CartHash = hash,
                        Rate = rate.Rate,
                        RateTimestamp = timestamp,
                        Selected = Contract.CheckValidKey(selectedRate) && selectedRate == rate.FullOptionName ? true : false,
                        EstimatedDeliveryDate = rate.EstimatedArrival ?? timestamp.Date.AddDays(3),
                        TargetShippingDate = rate.TargetShipping ?? timestamp.Date.AddDays(1),
                        // Related Objects
                        CartID = cartID,
                        ShipCarrierMethodID = carrierMethodID,
                    };
                    await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(toAdd, dummy, contextProfileName).ConfigureAwait(false);
                    newEntities.Add(toAdd);
                }
                var oldRateQuotes = await context.RateQuotes
                    .Where(x => x.CartID == cartID
                        && x.ShipCarrierMethod!.ShipCarrier!.CustomKey == currentShippingProvider)
                    .ToListAsync()
                    .ConfigureAwait(false);
                foreach (var oldRateQuote in oldRateQuotes)
                {
                    oldRateQuote.Active = false;
                }
                newEntities.ForEach(x => context.RateQuotes.Add(x));
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
        }

        /// <summary>Hash the request parts of a shipment</summary>
        /// <param name="origin">     The package origin</param>
        /// <param name="destination">The package destination</param>
        /// <param name="items">      The items</param>
        /// <param name="provider">   The provider name</param>
        /// <returns>A hashed request.</returns>
        protected virtual long BuildSimpleRequestHash(
            IContactModel origin,
            IContactModel destination,
            List<IProviderShipment>? items,
            string provider)
        {
            var toHash = (
                items: items?.Where(x => x.Weight > 0),
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
                    if (x.items is not null)
                    {
                        writer.Write(JsonConvert.SerializeObject(x.items));
                    }
                    writer.Write(JsonConvert.SerializeObject(x.origin));
                    writer.Write(JsonConvert.SerializeObject(x.destination));
                });
        }

        /// <summary>Volumes the given item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>A decimal.</returns>
        private static decimal Volume(IProviderShipment item)
        {
            if (!item.Height.HasValue
                || !item.Depth.HasValue
                || !item.Width.HasValue)
            {
                return 0;
            }
            return Math.Round(item.Height.Value, 0)
                 * Math.Round(item.Depth.Value, 0)
                 * Math.Round(item.Width.Value, 0);
        }
    }
}
