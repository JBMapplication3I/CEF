// <copyright file="TieredShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tiered pricing provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Tiered
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using Models;
    using Utilities;

    /// <summary>A tiered pricing provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class TieredShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => TieredShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Line Items are required to get UPS shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get UPS shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get UPS shipping rate quotes");
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR(new List<IRateQuoteModel>(), "NOTE! No items in this cart need to ship");
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Origin is required to get UPS shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Destination is required to get UPS shipping rate quotes");
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
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            /*
            // TODO: Generate a TimeInTransit request so we can see when packages would be delivered, probably have to
            // make TimeInTransit override Rate request's results, their docs say it adds logic to exclude more
            // expensive services that can't deliver any faster.
            if (decimal.Parse(rateRequest.Shipment.Package.First().PackageWeight.Weight) <= 0)
            {
                // No Weight
                const string Message = "There was no weight to use to calculate shipping.";
                Logger.LogWarning("UPSShippingProvider: GetRates", Message, contextProfileName);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"UPS GetRates: {Message}");
            }
            */
            // Run the Request
            // IShippingFactoryProductModel factoryProduct;
            // IShippingFactoryContextModel factoryContext;
            List<IShipmentRate>? rates = null; // TODO: await GetMethodsAsync(items, destination, origin, factoryProduct, factoryContext, contextProfileName).ConfigureAwait(false);
            // Check for Minimum Rate Amount requirements and apply if set
            var defaultMinimumPrice = TieredShippingProviderConfig.DefaultMinimumPrice;
            if (TieredShippingProviderConfig.UseDefaultMinimumPricing && defaultMinimumPrice != 0)
            {
                foreach (var rate in rates!.Where(x => x.Rate < defaultMinimumPrice))
                {
                    rate.Rate = defaultMinimumPrice;
                }
            }
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID: cartID,
                    hash: hash,
                    currentShippingProvider: "Tiered",
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
            throw new NotImplementedException();
        }

        /// <summary>Determine possible product price point models.</summary>
        /// <param name="productShipCarrierMethods">   The product price points.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="pricingFactoryProduct">The pricing factory product.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A List{IProductShipCarrierMethodModel}.</returns>
        protected static List<IProductShipCarrierMethodModel> DeterminePossibleProductShipCarrierMethodModels(
            IReadOnlyCollection<IProductShipCarrierMethodModel> productShipCarrierMethods,
            IShippingFactoryContextModel pricingFactoryContext,
            IShippingFactoryProductModel pricingFactoryProduct,
            string? contextProfileName)
        {
            var quantity = pricingFactoryContext.Quantity;
            var unitOfMeasure = pricingFactoryProduct.ProductUnitOfMeasure?.ToLower();
            var currencyID = pricingFactoryContext.CurrencyID;
            if (!Contract.CheckValidID(currencyID) && Contract.CheckValidKey(pricingFactoryContext.CurrencyKey))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                currencyID = context.Currencies
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(pricingFactoryContext.CurrencyKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault();
            }
            if (Contract.CheckInvalidID(currencyID))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                currencyID = context.Currencies
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey("USD", true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault();
            }
            var storeID = pricingFactoryContext.StoreID;
            if (!Contract.CheckValidID(storeID) && Contract.CheckValidKey(pricingFactoryContext.StoreKey))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                storeID = context.Stores
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(pricingFactoryContext.StoreKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault();
            }
            var brandID = pricingFactoryContext.BrandID;
            if (!Contract.CheckValidID(brandID) && Contract.CheckValidKey(pricingFactoryContext.BrandKey))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                brandID = context.Brands
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(pricingFactoryContext.BrandKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault();
            }
            if (quantity < 1)
            {
                quantity = 1;
            }
            if (Contract.CheckInvalidID(storeID))
            {
                storeID = null;
            }
            if (Contract.CheckInvalidID(brandID))
            {
                brandID = null;
            }
            var now = DateExtensions.GenDateTime;
            // Best of qualifying methods for that Product
            var possibleMethods = productShipCarrierMethods
                .Where(pscm => pscm.MinQuantity <= quantity
                            && pscm.MaxQuantity >= quantity
                            && (!pscm.From.HasValue || pscm.From < now)
                            && (!pscm.To.HasValue || pscm.To > now)
                            && (string.IsNullOrEmpty(unitOfMeasure) || pscm.UnitOfMeasure == unitOfMeasure)
                            && (!pscm.CurrencyID.HasValue && !currencyID.HasValue || pscm.CurrencyID == currencyID)
                            && (!pscm.StoreID.HasValue && !storeID.HasValue || pscm.StoreID == storeID)
                            && (!pscm.BrandID.HasValue && !brandID.HasValue || pscm.BrandID == brandID))
                .ToList();
            if (!possibleMethods.Any())
            {
                // None Found, Resorting to ignoring Dates but checking Quantities
                possibleMethods = productShipCarrierMethods
                    .Where(pscm => pscm.MinQuantity == quantity
                                && pscm.MaxQuantity == quantity
                                && (string.IsNullOrEmpty(unitOfMeasure) || pscm.UnitOfMeasure == unitOfMeasure)
                                && (!pscm.CurrencyID.HasValue && !currencyID.HasValue || pscm.CurrencyID == currencyID)
                                && (!pscm.StoreID.HasValue && !storeID.HasValue || pscm.StoreID == storeID)
                                && (!pscm.BrandID.HasValue && !brandID.HasValue || pscm.BrandID == brandID))
                    .ToList();
            }
            if (!possibleMethods.Any())
            {
                // None Found, Resorting to ignoring Quantities but checking Dates
                possibleMethods = productShipCarrierMethods
                    .Where(pscm => (!pscm.From.HasValue || pscm.From < now)
                                && (!pscm.To.HasValue || pscm.To > now)
                                && (string.IsNullOrEmpty(unitOfMeasure) || pscm.UnitOfMeasure == unitOfMeasure)
                                && (!pscm.CurrencyID.HasValue && !currencyID.HasValue || pscm.CurrencyID == currencyID)
                                && (!pscm.StoreID.HasValue && !storeID.HasValue || pscm.StoreID == storeID)
                                && (!pscm.BrandID.HasValue && !brandID.HasValue || pscm.BrandID == brandID))
                    .ToList();
            }
            if (!possibleMethods.Any())
            {
                // None Found, Resorting to ignoring Dates and Quantities
                possibleMethods = productShipCarrierMethods
                    .Where(pscm => (string.IsNullOrEmpty(unitOfMeasure) || pscm.UnitOfMeasure == unitOfMeasure)
                                && (!pscm.CurrencyID.HasValue && !currencyID.HasValue || pscm.CurrencyID == currencyID)
                                && (!pscm.StoreID.HasValue && !storeID.HasValue || pscm.StoreID == storeID)
                                && (!pscm.BrandID.HasValue && !brandID.HasValue || pscm.BrandID == brandID))
                    .ToList();
            }
            return !possibleMethods.Any()
                ? new()
                : possibleMethods;
        }

        /// <summary>Calculated pre-rounding price.</summary>
        /// <param name="productShipCarrierMethod">The product price point.</param>
        /// <returns>A decimal.</returns>
        protected static decimal CalculatedPreRoundingPrice(IProductShipCarrierMethodModel productShipCarrierMethod)
        {
            // Use manually set price (not calculated) if it is set
            return productShipCarrierMethod.Price;
        }

        /// <summary>Calculates the rounded price.</summary>
        /// <param name="preRoundingPrice">The pre-rounding price.</param>
        /// <returns>The calculated rounded price.</returns>
        protected static decimal CalculateRoundedPrice(decimal preRoundingPrice)
        {
            return Math.Ceiling(preRoundingPrice * 100m) / 100m;
        }

        /// <summary>Calculates the tiered price.</summary>
        /// <param name="factoryProduct">    The factory product.</param>
        /// <param name="factoryContext">    Context for the pricing factory.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated tiered price.</returns>
        protected virtual Task<decimal> CalculateTieredRatesAsync(
            IShippingFactoryProductModel factoryProduct,
            IShippingFactoryContextModel factoryContext,
            string? contextProfileName)
        {
            if (factoryProduct?.ProductShipCarrierMethods == null || !factoryProduct.ProductShipCarrierMethods.Any())
            {
                return Task.FromResult(-1m);
            }
            var possibleMethods = DeterminePossibleProductShipCarrierMethodModels(
                factoryProduct.ProductShipCarrierMethods.ToList(),
                factoryContext,
                factoryProduct,
                contextProfileName);
            if (!possibleMethods.Any())
            {
                // No valid Points
                return Task.FromResult(-1m);
            }
            var productShipCarrierMethod = possibleMethods.First();
            var preRoundingPrice = CalculatedPreRoundingPrice(productShipCarrierMethod);
            var roundedPrice = CalculateRoundedPrice(preRoundingPrice);
            return Task.FromResult(roundedPrice);
        }
    }
}
