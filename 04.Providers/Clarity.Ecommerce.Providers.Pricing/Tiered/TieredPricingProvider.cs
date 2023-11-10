// <copyright file="TieredPricingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tiered pricing provider class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Tiered
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using Models;
    using Utilities;

    /// <summary>A tiered pricing provider.</summary>
    /// <seealso cref="PricingProviderBase"/>
    public class TieredPricingProvider : PricingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => TieredPricingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<ICalculatedPrice> CalculatePriceAsync(
                IPricingFactoryProductModel factoryProduct,
                IPricingFactoryContextModel factoryContext,
                string? contextProfileName,
                bool? forCart = false)
        {
            return CalculateTieredPriceAsync(factoryProduct, factoryContext, contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                nameof(TieredPricingProvider)
                    + $":P={factoryProduct.ProductID}"
                    + $",PP={factoryContext.PricePoint}"
                    + $",U={factoryProduct.ProductUnitOfMeasure}"
                    + $",S={factoryContext.StoreID ?? 0}"
                    + $",S={factoryContext.FranchiseID ?? 0}"
                    + $",B={factoryContext.BrandID ?? 0}"
                    + $",Q={factoryContext.Quantity:0.00000}"
                    + $",D={DateTime.Today:yyyyMMdd}");
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    PriceKeyAltProvider = nameof(TieredPricingProvider),
                    PriceKeyAltProductID = $"{factoryProduct.ProductID}",
                    PriceKeyAltPricePoint = $"{factoryContext.PricePoint}",
                    PriceKeyAltUnitOfMeasure = $"{factoryProduct.ProductUnitOfMeasure}",
                    PriceKeyAltStoreID = $"{factoryContext.StoreID ?? 0}",
                    PriceKeyAltFranchiseID = $"{factoryContext.FranchiseID ?? 0}",
                    PriceKeyAltBrandID = $"{factoryContext.BrandID ?? 0}",
                    PriceKeyAltQuantity = $"{factoryContext.Quantity:0.00000}",
                    PriceKeyAltDate = $"{DateTime.Today:yyyyMMdd}",
                }));
        }

        /// <summary>Determine possible product price point models.</summary>
        /// <param name="productPricePoints">   The product price points.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="pricingFactoryProduct">The pricing factory product.</param>
        /// <param name="defaultPricePointKey"> The default price point key.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A List{IProductPricePointModel}.</returns>
        protected static async Task<List<IProductPricePointModel>> DeterminePossibleProductPricePointModelsAsync(
            IReadOnlyCollection<IProductPricePointModel> productPricePoints,
            IPricingFactoryContextModel pricingFactoryContext,
            IPricingFactoryProductModel pricingFactoryProduct,
            string defaultPricePointKey,
            string? contextProfileName)
        {
            var quantity = pricingFactoryContext.Quantity;
            var pricePointKey = pricingFactoryContext.PricePoint;
            var unitOfMeasure = pricingFactoryProduct.ProductUnitOfMeasure?.ToLower();
            var currencyID = pricingFactoryContext.CurrencyID;
            if (!Contract.CheckValidID(currencyID) && Contract.CheckValidKey(pricingFactoryContext.CurrencyKey))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                currencyID = await context.Currencies
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(pricingFactoryContext.CurrencyKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(currencyID))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                currencyID = await context.Currencies
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey("USD", true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            var storeID = pricingFactoryContext.StoreID;
            if (!Contract.CheckValidID(storeID) && Contract.CheckValidKey(pricingFactoryContext.StoreKey))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                storeID = await context.Stores
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(pricingFactoryContext.StoreKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            var franchiseID = pricingFactoryContext.FranchiseID;
            if (!Contract.CheckValidID(franchiseID) && Contract.CheckValidKey(pricingFactoryContext.FranchiseKey))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                franchiseID = await context.Franchises
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(pricingFactoryContext.FranchiseKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            var brandID = pricingFactoryContext.BrandID;
            if (!Contract.CheckValidID(brandID) && Contract.CheckValidKey(pricingFactoryContext.BrandKey))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                brandID = await context.Brands
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(pricingFactoryContext.BrandKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            if (quantity < 1)
            {
                quantity = 1;
            }
            if (Contract.CheckInvalidID(storeID))
            {
                storeID = null;
            }
            if (Contract.CheckInvalidID(franchiseID))
            {
                franchiseID = null;
            }
            if (Contract.CheckInvalidID(brandID))
            {
                brandID = null;
            }
            var now = DateExtensions.GenDateTime;
            // Best of points for that Product
            var possiblePoints = productPricePoints
                .Where(ppp => ppp.MinQuantity <= quantity
                           && ppp.MaxQuantity >= quantity
                           && (string.IsNullOrEmpty(unitOfMeasure) || ppp.UnitOfMeasure == unitOfMeasure)
                           && (!ppp.CurrencyID.HasValue && !currencyID.HasValue || ppp.CurrencyID == currencyID)
                           && (!ppp.StoreID.HasValue && !storeID.HasValue || ppp.StoreID == storeID)
                           && (!ppp.FranchiseID.HasValue && !franchiseID.HasValue || ppp.FranchiseID == franchiseID)
                           && (!ppp.BrandID.HasValue && !brandID.HasValue || ppp.BrandID == brandID)
                           && (!ppp.From.HasValue || ppp.From < now)
                           && (!ppp.To.HasValue || ppp.To > now)
                           && ppp.SlaveKey == pricePointKey)
                .ToList();
            if (!possiblePoints.Any())
            {
                // None Found, Resorting to ignoring Dates for this point key
                possiblePoints = productPricePoints
                    .Where(ppp => ppp.MinQuantity <= quantity
                               && ppp.MaxQuantity >= quantity
                               && (string.IsNullOrEmpty(unitOfMeasure) || ppp.UnitOfMeasure == unitOfMeasure)
                               && (!ppp.CurrencyID.HasValue && !currencyID.HasValue || ppp.CurrencyID == currencyID)
                               && (!ppp.StoreID.HasValue && !storeID.HasValue || ppp.StoreID == storeID)
                               && (!ppp.FranchiseID.HasValue && !franchiseID.HasValue || ppp.FranchiseID == franchiseID)
                               && (!ppp.BrandID.HasValue && !brandID.HasValue || ppp.BrandID == brandID)
                               && ppp.SlaveKey == pricePointKey)
                    .ToList();
            }
            if (!possiblePoints.Any())
            {
                // None Found, Resorting to ignoring Dates and Quantities for this point key
                possiblePoints = productPricePoints
                    .Where(ppp => (!ppp.CurrencyID.HasValue && !currencyID.HasValue || ppp.CurrencyID == currencyID)
                               && (string.IsNullOrEmpty(unitOfMeasure) || ppp.UnitOfMeasure == unitOfMeasure)
                               && (!ppp.StoreID.HasValue && !storeID.HasValue || ppp.StoreID == storeID)
                               && (!ppp.FranchiseID.HasValue && !franchiseID.HasValue || ppp.FranchiseID == franchiseID)
                               && (!ppp.BrandID.HasValue && !brandID.HasValue || ppp.BrandID == brandID)
                               && ppp.SlaveKey == pricePointKey)
                    .ToList();
            }
            if (!possiblePoints.Any())
            {
                // None Found, Resorting to best with DefaultPricePointKey
                possiblePoints = productPricePoints
                    .Where(ppp => ppp.MinQuantity <= quantity
                               && ppp.MaxQuantity >= quantity
                               && (string.IsNullOrEmpty(unitOfMeasure) || ppp.UnitOfMeasure == unitOfMeasure)
                               && (!ppp.CurrencyID.HasValue && !currencyID.HasValue || ppp.CurrencyID == currencyID)
                               && (!ppp.StoreID.HasValue && !storeID.HasValue || ppp.StoreID == storeID)
                               && (!ppp.FranchiseID.HasValue && !franchiseID.HasValue || ppp.FranchiseID == franchiseID)
                               && (!ppp.BrandID.HasValue && !brandID.HasValue || ppp.BrandID == brandID)
                               && (!ppp.From.HasValue || ppp.From < now)
                               && (!ppp.To.HasValue || ppp.To > now)
                               && ppp.SlaveKey == defaultPricePointKey)
                    .ToList();
            }
            if (!possiblePoints.Any())
            {
                // None Found, Resorting to DefaultPricePointKey ignoring Dates
                possiblePoints = productPricePoints
                    .Where(ppp => ppp.MinQuantity <= quantity
                               && ppp.MaxQuantity >= quantity
                               && (string.IsNullOrEmpty(unitOfMeasure) || ppp.UnitOfMeasure == unitOfMeasure)
                               && (!ppp.CurrencyID.HasValue && !currencyID.HasValue || ppp.CurrencyID == currencyID)
                               && (!ppp.StoreID.HasValue && !storeID.HasValue || ppp.StoreID == storeID)
                               && (!ppp.FranchiseID.HasValue && !franchiseID.HasValue || ppp.FranchiseID == franchiseID)
                               && (!ppp.BrandID.HasValue && !brandID.HasValue || ppp.BrandID == brandID)
                               && ppp.SlaveKey == defaultPricePointKey)
                    .ToList();
            }
            if (!possiblePoints.Any())
            {
                // None Found, Resorting to DefaultPricePointKey ignoring Dates and Quantities
                possiblePoints = productPricePoints
                    .Where(ppp => (string.IsNullOrEmpty(unitOfMeasure) || ppp.UnitOfMeasure == unitOfMeasure)
                               && (!ppp.CurrencyID.HasValue && !currencyID.HasValue || ppp.CurrencyID == currencyID)
                               && (!ppp.StoreID.HasValue && !storeID.HasValue || ppp.StoreID == storeID)
                               && (!ppp.FranchiseID.HasValue && !franchiseID.HasValue || ppp.FranchiseID == franchiseID)
                               && (!ppp.BrandID.HasValue && !brandID.HasValue || ppp.BrandID == brandID)
                               && ppp.SlaveKey == defaultPricePointKey)
                    .ToList();
            }
            if (!possiblePoints.Any())
            {
                // None Found, Resorting to Any of DefaultPricePointKey for that Product
                possiblePoints = productPricePoints
                    .Where(ppp => ppp.SlaveKey == defaultPricePointKey)
                    .ToList();
            }
            return !possiblePoints.Any() ? new() : possiblePoints;
        }

        /// <summary>Calculated pre-rounding price.</summary>
        /// <param name="pointPrice">          The point price.</param>
        /// <param name="pointPercentDiscount">The point percent discount.</param>
        /// <param name="priceBase">           The price base.</param>
        /// <param name="defaultMarkupRate">   The default markup rate.</param>
        /// <returns>A decimal.</returns>
        protected static decimal CalculatedPreRoundingPrice(
            decimal? pointPrice,
            decimal? pointPercentDiscount,
            decimal? priceBase,
            decimal defaultMarkupRate)
        {
            // Use manually set price (not calculated) if it is set
            if (pointPrice.HasValue)
            {
                return pointPrice.Value;
            }
            // Otherwise try to use Markup method, which requires priceBase to have a value
            if (!priceBase.HasValue)
            {
                return -1;
            }
            var markup = pointPercentDiscount.HasValue
                ? pointPercentDiscount.Value == 1
                    ? 0
                    : pointPercentDiscount.Value
                : defaultMarkupRate;
            return priceBase.Value / Math.Max(0.01m, 1m - markup);
        }

        /// <summary>Calculates the rounded price.</summary>
        /// <param name="preRoundingPrice">The pre rounding price.</param>
        /// <param name="roundHow">        The round how.</param>
        /// <param name="roundAmount">     The round amount.</param>
        /// <param name="countPerPackage"> The count per package.</param>
        /// <returns>The calculated rounded price.</returns>
        protected static decimal CalculateRoundedPrice(
            decimal preRoundingPrice,
            int? roundHow,
            int? roundAmount,
            decimal countPerPackage)
        {
            var roundedPrice = preRoundingPrice;
            roundHow ??= 1;
            roundAmount ??= 2;
            switch (roundHow.Value)
            {
                case 0: // 0=Don't Round
                {
                    roundedPrice = preRoundingPrice * countPerPackage;
                    break;
                }
                case 1: // 1=MultipleOf, like multiple of 2 or 0.01 (for cents)
                {
                    roundedPrice = Math.Ceiling(preRoundingPrice * countPerPackage * 100m) / 100m;
                    break;
                }
                case 2: // 2=EndsIn, like ends in 0 or 5
                {
                    roundedPrice = Math.Round(preRoundingPrice * countPerPackage / roundAmount.Value)
                        * roundAmount.Value;
                    break;
                }
            }
            return roundedPrice;
        }

        /// <summary>Calculates the tiered price.</summary>
        /// <param name="factoryProduct">    The factory product.</param>
        /// <param name="factoryContext">    Context for the pricing factory.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated tiered price.</returns>
        protected virtual async Task<ICalculatedPrice> CalculateTieredPriceAsync(
            IPricingFactoryProductModel? factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName)
        {
            if (factoryProduct is null)
            {
                return new CalculatedPrice(Name, -1);
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var ppp = context.ProductPricePoints
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductPricePointsByProductID(factoryProduct.ProductID);
            if (!await ppp.AnyAsync())
            {
                return new CalculatedPrice(Name, -1);
            }
            factoryProduct.ProductPricePoints = (await ppp
                .Select(x => new
                {
                    x.Price,
                    x.PercentDiscount,
                    x.MinQuantity,
                    x.MaxQuantity,
                    x.UnitOfMeasure,
                    x.CurrencyID,
                    x.StoreID,
                    x.FranchiseID,
                    x.BrandID,
                    x.From,
                    x.To,
                    PointKey = x.Slave!.CustomKey,
                    Rounding = x.PriceRoundingID == null
                        ? null
                        : new
                        {
                            x.PriceRounding!.RoundHow,
                            x.PriceRounding.RoundingAmount,
                        },
                })
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(x =>
                {
                    var model = RegistryLoaderWrapper.GetInstance<IProductPricePointModel>(contextProfileName);
                    model.Active = true;
                    model.Price = x.Price;
                    model.PercentDiscount = x.PercentDiscount;
                    model.MinQuantity = x.MinQuantity;
                    model.MaxQuantity = x.MaxQuantity;
                    model.UnitOfMeasure = x.UnitOfMeasure;
                    model.CurrencyID = x.CurrencyID;
                    model.StoreID = x.StoreID;
                    model.FranchiseID = x.FranchiseID;
                    model.BrandID = x.BrandID;
                    model.From = x.From;
                    model.To = x.To;
                    model.SlaveKey = x.PointKey;
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (x.Rounding == null)
                    {
                        return model;
                    }
                    model.PriceRounding = RegistryLoaderWrapper.GetInstance<IPriceRoundingModel>(contextProfileName);
                    model.PriceRounding.RoundHow = x.Rounding.RoundHow;
                    model.PriceRounding.RoundingAmount = x.Rounding.RoundingAmount;
                    return model;
                })
                .ToList();
            var countPerPackage = Math.Max(1m, factoryProduct.KitBaseQuantityPriceMultiplier ?? 1m);
            var possiblePoints = await DeterminePossibleProductPricePointModelsAsync(
                    factoryProduct.ProductPricePoints.ToList(),
                    factoryContext,
                    factoryProduct,
                    Workflows.PricingFactory.Settings.DefaultPricePointKey,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!possiblePoints.Any())
            {
                // No valid Points
                return new CalculatedPrice(Name, -1);
            }
            var productPricePoint = possiblePoints.First();
            var preRoundingPrice = CalculatedPreRoundingPrice(
                productPricePoint.Price,
                productPricePoint.PercentDiscount,
                factoryProduct.PriceBase,
                Workflows.PricingFactory.Settings.DefaultMarkupRate);
            var roundedPrice = CalculateRoundedPrice(
                preRoundingPrice,
                productPricePoint.PriceRounding?.RoundHow,
                productPricePoint.PriceRounding?.RoundingAmount,
                countPerPackage);
            return new CalculatedPrice(Name, (double)roundedPrice)
            {
                PriceKey = await GetPriceKeyAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                PriceKeyAlt = await GetPriceKeyAltAsync(factoryProduct, factoryContext).ConfigureAwait(false),
            };
        }
    }
}
