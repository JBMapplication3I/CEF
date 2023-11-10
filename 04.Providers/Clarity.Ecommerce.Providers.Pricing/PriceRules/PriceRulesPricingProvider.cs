// #define OVERDEBUG
// <copyright file="PriceRulesPricingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rules pricing provider class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.PriceRule
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using LinqKit;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A price rules pricing provider.</summary>
    /// <seealso cref="PricingProviderBase"/>
    public class PriceRulesPricingProvider : PricingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PriceRulesPricingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                nameof(PriceRulesPricingProvider)
                    + $":P={factoryProduct.ProductID}"
                    + $",PT={factoryProduct.ProductTypeID}"
                    + $",U={factoryProduct.ProductUnitOfMeasure}"
                    + $",CA={AggregateIDList(factoryProduct.CategoryIDs)}"
                    + $",M={AggregateIDList(factoryProduct.ManufacturerIDs)}"
                    + $",S={AggregateIDList(factoryProduct.StoreIDs)}"
                    + $",V={AggregateIDList(factoryProduct.VendorIDs)}"
                    + $",R={AggregateKeyList(factoryContext.UserRoles)}"
                    + $",CO={factoryContext.CountryID}"
                    + $",A={factoryContext.AccountID}"
                    + $",AT={factoryContext.AccountTypeID}"
                    + $",Q={factoryContext.Quantity:0.00000}"
                    + $",D={DateTime.Today:yyyyMMdd}");
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                GetPriceKeyAlt(
                    factoryProduct: factoryProduct,
                    factoryContext: factoryContext,
                    assocAccountUpLevel1ID: null,
                    assocAccountTypeUpLevel1ID: null,
                    assocAccountUpLevel2ID: null,
                    assocAccountTypeUpLevel2ID: null,
                    assocAccountUpLevel3ID: null,
                    assocAccountTypeUpLevel3ID: null,
                    assocAccountUpLevel4ID: null,
                    assocAccountTypeUpLevel4ID: null,
                    assocAccountUpLevel5ID: null,
                    assocAccountTypeUpLevel5ID: null));
        }

        /// <inheritdoc/>
        public override async Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName,
            bool? forCart = false)
        {
            var timestamp = DateExtensions.GenDateTime;
            var (relevantRules,
                 assocAccountUpLevel1ID,
                 assocAccountTypeUpLevel1ID,
                 assocAccountUpLevel2ID,
                 assocAccountTypeUpLevel2ID,
                 assocAccountUpLevel3ID,
                 assocAccountTypeUpLevel3ID,
                 assocAccountUpLevel4ID,
                 assocAccountTypeUpLevel4ID,
                 assocAccountUpLevel5ID,
                 assocAccountTypeUpLevel5ID)
                 = GetRelevantRules(factoryProduct, factoryContext, contextProfileName, timestamp);
            if (relevantRules?.Any(x => x.Active) != true)
            {
                return new CalculatedPrice(
                    Name,
                    (double)(factoryProduct.PriceBase ?? 0m),
                    (double?)factoryProduct.PriceSale);
            }
            var (usedRules, @base, sale) = ProcessRelevantRules(factoryProduct, relevantRules);
            return new CalculatedPrice(Name, (double)@base, (double?)sale)
            {
                PriceKey = await GetPriceKeyAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                PriceKeyAlt = GetPriceKeyAlt(
                    factoryProduct,
                    factoryContext,
                    assocAccountUpLevel1ID,
                    assocAccountTypeUpLevel1ID,
                    assocAccountUpLevel2ID,
                    assocAccountTypeUpLevel2ID,
                    assocAccountUpLevel3ID,
                    assocAccountTypeUpLevel3ID,
                    assocAccountUpLevel4ID,
                    assocAccountTypeUpLevel4ID,
                    assocAccountUpLevel5ID,
                    assocAccountTypeUpLevel5ID),
                RelevantRules = relevantRules.Cast<PriceRuleModel>().ToList(),
                UsedRules = usedRules?.Cast<PriceRuleModel>().ToList(),
            };
        }

        private static string GetPriceKeyAlt(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            int? assocAccountUpLevel1ID,
            int? assocAccountTypeUpLevel1ID,
            int? assocAccountUpLevel2ID,
            int? assocAccountTypeUpLevel2ID,
            int? assocAccountUpLevel3ID,
            int? assocAccountTypeUpLevel3ID,
            int? assocAccountUpLevel4ID,
            int? assocAccountTypeUpLevel4ID,
            int? assocAccountUpLevel5ID,
            int? assocAccountTypeUpLevel5ID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                PriceKeyAltProvider = nameof(PriceRulesPricingProvider),
                PriceKeyAltProductID = $"{factoryProduct.ProductID}",
                PriceKeyAltProductTypeID = $"{factoryProduct.ProductTypeID}",
                PriceKeyAltProductUnitOfMeasure = $"{factoryProduct.ProductUnitOfMeasure}",
                PriceKeyAltCategoryIDs = $"{AggregateIDList(factoryProduct.CategoryIDs)}",
                PriceKeyAltManufacturerIDs = $"{AggregateIDList(factoryProduct.ManufacturerIDs)}",
                PriceKeyAltStoreIDs = $"{AggregateIDList(factoryProduct.StoreIDs)}",
                PriceKeyAltVendorIDs = $"{AggregateIDList(factoryProduct.VendorIDs)}",
                PriceKeyAltUserRoles = $"{AggregateKeyList(factoryContext.UserRoles)}",
                PriceKeyAltCountryID = $"{factoryContext.CountryID}",
                PriceKeyAltAccountID = $"{factoryContext.AccountID}",
                PriceKeyAltAccountType = $"{factoryContext.AccountTypeID}",
                PriceKeyAltInheritedAccountLevel1ID = $"{assocAccountUpLevel1ID}",
                PriceKeyAltInheritedAccountTypeLevel1ID = $"{assocAccountTypeUpLevel1ID}",
                PriceKeyAltInheritedAccountLevel2ID = $"{assocAccountUpLevel2ID}",
                PriceKeyAltInheritedAccountTypeLevel2ID = $"{assocAccountTypeUpLevel2ID}",
                PriceKeyAltInheritedAccountLevel3ID = $"{assocAccountUpLevel3ID}",
                PriceKeyAltInheritedAccountTypeLevel3ID = $"{assocAccountTypeUpLevel3ID}",
                PriceKeyAltInheritedAccountLevel4ID = $"{assocAccountUpLevel4ID}",
                PriceKeyAltInheritedAccountTypeLevel4ID = $"{assocAccountTypeUpLevel4ID}",
                PriceKeyAltInheritedAccountLevel5ID = $"{assocAccountUpLevel5ID}",
                PriceKeyAltInheritedAccountTypeLevel5ID = $"{assocAccountTypeUpLevel5ID}",
                PriceKeyAltQuantity = $"{factoryContext.Quantity:0.00000}",
                PriceKeyAltDate = $"{DateTime.Today:yyyyMMdd}",
            });
        }

        private static decimal CalculatePriceBase(IPriceRuleModel rule, decimal basePrice, int? productID)
        {
            if (!rule.UsePriceBase)
            {
                return basePrice;
            }
            // ReSharper disable once InvertIf
            if (productID != null && rule.Products?.Any(x => x.Active) == true)
            {
                // look if price override
                var overrideRule = rule.Products
                    .Find(p => p.Active && p.OverridePrice && p.SlaveID == productID.Value);
                if (overrideRule != null)
                {
                    return overrideRule.OverrideBasePrice ?? basePrice;
                }
            }
            return ApplyRuleToPrice(rule, basePrice);
        }

        private static decimal? CalculatePriceSale(IPriceRuleModel rule, decimal? salePrice, int? productID)
        {
            if (rule.UsePriceBase || !salePrice.HasValue)
            {
                return null;
            }
            // ReSharper disable once InvertIf
            if (productID != null && rule.Products?.Any(x => x.Active) == true)
            {
                // look if price override
                var overrideRule = rule.Products
                    .Find(p => p.Active && p.OverridePrice && p.SlaveID == productID.Value);
                if (overrideRule != null)
                {
                    return overrideRule.OverrideSalePrice ?? salePrice;
                }
            }
            return ApplyRuleToPrice(rule, salePrice.Value);
        }

        private static decimal ApplyRuleToPrice(IPriceRuleModel rule, decimal price)
        {
            decimal ModPrice()
            {
                if (rule.IsMarkup)
                {
                    if (rule.IsPercentage)
                    {
                        return price * (1m + rule.PriceAdjustment / 100m);
                    }
                    return price + rule.PriceAdjustment;
                }
                if (rule.IsPercentage)
                {
                    return price * (1m - rule.PriceAdjustment / 100m);
                }
                return price - rule.PriceAdjustment;
            }
            // Don't allow it to go negative
            return Math.Max(0m, ModPrice());
        }

        // ReSharper disable once FunctionComplexityOverflow
        private static (
            List<IPriceRuleModel> rules,
            int? assocAccountUpLevel1ID,
            int? assocAccountTypeUpLevel1ID,
            int? assocAccountUpLevel2ID,
            int? assocAccountTypeUpLevel2ID,
            int? assocAccountUpLevel3ID,
            int? assocAccountTypeUpLevel3ID,
            int? assocAccountUpLevel4ID,
            int? assocAccountTypeUpLevel4ID,
            int? assocAccountUpLevel5ID,
            int? assocAccountTypeUpLevel5ID)
            GetRelevantRules(
                IPricingFactoryProductModel factoryProduct,
                IPricingFactoryContextModel factoryContext,
                string? contextProfileName,
                DateTime timestamp)
        {
            var relevantRules = new List<IPriceRuleModel>();
            var generalConstraints = PredicateBuilder.New<PriceRule>(true);
            // General Qualifying Constraints (all must match)
            generalConstraints.And(x => x.Active);
            generalConstraints.And(x => x.StartDate == null || x.StartDate <= timestamp);
            generalConstraints.And(x => x.EndDate == null || x.EndDate >= timestamp);
            generalConstraints.And(x => x.MinQuantity == null || x.MinQuantity <= factoryContext.Quantity);
            generalConstraints.And(x => x.MaxQuantity == null || x.MaxQuantity >= factoryContext.Quantity);
            generalConstraints.And(x => x.UnitOfMeasure == null || x.UnitOfMeasure == factoryProduct.ProductUnitOfMeasure);
            // Specific Qualifying Constraints (any one can match)
            var noneOrSameCountry = PredicateBuilder.New<PriceRule>(
                x => !x.PriceRuleCountries!.Any(y => y.Active)
                  || x.PriceRuleCountries!.Any(y => y.Active && y.SlaveID == factoryContext.CountryID));
            var noneOrSameProduct = PredicateBuilder.New<PriceRule>(
                x => !x.Products!.Any(y => y.Active)
                  || x.Products!.Any(y => y.Active && y.SlaveID == factoryProduct.ProductID));
            var noneOrSameProductType = PredicateBuilder.New<PriceRule>(
                x => !x.PriceRuleProductTypes!.Any(y => y.Active)
                  || x.PriceRuleProductTypes!.Any(y => y.Active && y.SlaveID == factoryProduct.ProductTypeID));
            int? assocAccountUpLevel1ID = null, assocAccountTypeUpLevel1ID = null,
                 assocAccountUpLevel2ID = null, assocAccountTypeUpLevel2ID = null,
                 assocAccountUpLevel3ID = null, assocAccountTypeUpLevel3ID = null,
                 assocAccountUpLevel4ID = null, assocAccountTypeUpLevel4ID = null,
                 assocAccountUpLevel5ID = null, assocAccountTypeUpLevel5ID = null;
            if (Contract.CheckValidID(factoryContext.AccountID))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                // Level 1
                assocAccountUpLevel1ID = context.AccountAssociations
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableBySlaveID<AccountAssociation, Account, Account>(
                        factoryContext.AccountID!.Value)
                    .Select(x => (int?)x.MasterID)
                    .FirstOrDefault();
                if (Contract.CheckValidID(assocAccountUpLevel1ID))
                {
                    assocAccountTypeUpLevel1ID = context.Accounts
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(assocAccountUpLevel1ID!.Value)
                        .Select(x => (int?)x.TypeID)
                        .SingleOrDefault();
                    // Level 2
                    assocAccountUpLevel2ID = context.AccountAssociations
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableBySlaveID<AccountAssociation, Account, Account>(
                            assocAccountUpLevel1ID)
                        .Select(x => (int?)x.MasterID)
                        .FirstOrDefault();
                    if (Contract.CheckValidID(assocAccountUpLevel2ID))
                    {
                        assocAccountTypeUpLevel2ID = context.Accounts
                            .AsNoTracking()
                            .FilterByActive(true)
                            .FilterByID(assocAccountUpLevel2ID!.Value)
                            .Select(x => (int?)x.TypeID)
                            .SingleOrDefault();
                        // Level 3
                        assocAccountUpLevel3ID = context.AccountAssociations
                            .AsNoTracking()
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableBySlaveID<AccountAssociation, Account, Account>(
                                assocAccountUpLevel2ID)
                            .Select(x => (int?)x.MasterID)
                            .FirstOrDefault();
                        if (Contract.CheckValidID(assocAccountUpLevel3ID))
                        {
                            assocAccountTypeUpLevel3ID = context.Accounts
                                .AsNoTracking()
                                .FilterByActive(true)
                                .FilterByID(assocAccountUpLevel3ID!.Value)
                                .Select(x => (int?)x.TypeID)
                                .SingleOrDefault();
                            // Level 4
                            assocAccountUpLevel4ID = context.AccountAssociations
                                .AsNoTracking()
                                .FilterByActive(true)
                                .FilterIAmARelationshipTableBySlaveID<AccountAssociation, Account, Account>(
                                    assocAccountUpLevel3ID)
                                .Select(x => (int?)x.MasterID)
                                .FirstOrDefault();
                            if (Contract.CheckValidID(assocAccountUpLevel4ID))
                            {
                                assocAccountTypeUpLevel4ID = context.Accounts
                                    .AsNoTracking()
                                    .FilterByActive(true)
                                    .FilterByID(assocAccountUpLevel4ID!.Value)
                                    .Select(x => (int?)x.TypeID)
                                    .SingleOrDefault();
                                // Level 5
                                assocAccountUpLevel5ID = context.AccountAssociations
                                    .AsNoTracking()
                                    .FilterByActive(true)
                                    .FilterIAmARelationshipTableBySlaveID<AccountAssociation, Account, Account>(
                                        assocAccountUpLevel4ID)
                                    .Select(x => (int?)x.MasterID)
                                    .FirstOrDefault();
                                if (Contract.CheckValidID(assocAccountUpLevel5ID))
                                {
                                    assocAccountTypeUpLevel5ID = context.Accounts
                                        .AsNoTracking()
                                        .FilterByActive(true)
                                        .FilterByID(assocAccountUpLevel5ID!.Value)
                                        .Select(x => (int?)x.TypeID)
                                        .SingleOrDefault();
                                }
                            }
                        }
                    }
                }
            }
            var noneOrSameAccount = PredicateBuilder.New<PriceRule>(
                x => !x.Accounts!.Any(y => y.Active)
                  || x.Accounts!
                        .Any(y => y.Active
                               && (y.SlaveID == factoryContext.AccountID
                                   || y.SlaveID == assocAccountUpLevel1ID
                                   || y.SlaveID == assocAccountUpLevel2ID
                                   || y.SlaveID == assocAccountUpLevel3ID
                                   || y.SlaveID == assocAccountUpLevel4ID
                                   || y.SlaveID == assocAccountUpLevel5ID)));
            var noneOrSameAccountType = PredicateBuilder.New<PriceRule>(
                x => !x.PriceRuleAccountTypes!.Any(y => y.Active)
                  || x.PriceRuleAccountTypes!
                        .Any(y => y.Active
                               && (y.SlaveID == factoryContext.AccountTypeID
                                   || y.SlaveID == assocAccountTypeUpLevel1ID
                                   || y.SlaveID == assocAccountTypeUpLevel2ID
                                   || y.SlaveID == assocAccountTypeUpLevel3ID
                                   || y.SlaveID == assocAccountTypeUpLevel4ID
                                   || y.SlaveID == assocAccountTypeUpLevel5ID)));

            if (!Contract.CheckValidID(factoryContext.UserID))
            {
                factoryContext.UserID = null;
            }
            var noneOrSameUserRole = PredicateBuilder.New<PriceRule>(x => !x.PriceRuleUserRoles!.Any(y => y.Active));
            noneOrSameUserRole = (factoryContext.UserRoles ?? new List<string>()).Where(Contract.CheckValidKey)
                .Aggregate(noneOrSameUserRole, (c, name) => c.Or(x => x.PriceRuleUserRoles!.Any(y => y.Active && y.RoleName == name)));

            var noneOrSameCategory = PredicateBuilder.New<PriceRule>(x => !x.PriceRuleCategories!.Any(y => y.Active));
            noneOrSameCategory = (factoryProduct.CategoryIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(noneOrSameCategory, (c, id) => c.Or(x => x.PriceRuleCategories!.Any(y => y.Active && y.SlaveID == id)));

            var noneOrSameManufacturer = PredicateBuilder.New<PriceRule>(x => !x.Manufacturers!.Any(y => y.Active));
            noneOrSameManufacturer = (factoryProduct.ManufacturerIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(noneOrSameManufacturer, (c, id) => c.Or(x => x.Manufacturers!.Any(y => y.Active && y.SlaveID == id)));

            var noneOrSameStore = PredicateBuilder.New<PriceRule>(x => !x.Stores!.Any(y => y.Active));
            noneOrSameStore = (factoryProduct.StoreIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(noneOrSameStore, (c, id) => c.Or(x => x.Stores!.Any(y => y.Active && y.SlaveID == id)));

            var noneOrSameVendor = PredicateBuilder.New<PriceRule>(x => !x.Vendors!.Any(y => y.Active));
            noneOrSameVendor = (factoryProduct.VendorIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(noneOrSameVendor, (c, id) => c.Or(x => x.Vendors!.Any(y => y.Active && y.SlaveID == id)));

            var matchesProductID = PredicateBuilder.New<PriceRule>(true)
                .And(x => x.Products!.Any(y => y.Active && y.SlaveID == factoryProduct.ProductID))
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var matchesProductTypeID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(x => x.PriceRuleProductTypes!.Any(y => y.Active && y.SlaveID == factoryProduct.ProductTypeID))
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var matchesCountryID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(x => x.PriceRuleCountries!.Any(y => y.Active && y.SlaveID == factoryContext.CountryID))
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var matchesAccountID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(x => x.Accounts!
                    .Any(y => y.Active
                           && (y.SlaveID == factoryContext.AccountID
                               || y.SlaveID == assocAccountUpLevel1ID
                               || y.SlaveID == assocAccountUpLevel2ID
                               || y.SlaveID == assocAccountUpLevel3ID
                               || y.SlaveID == assocAccountUpLevel4ID
                               || y.SlaveID == assocAccountUpLevel5ID)))
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var matchesAccountTypeID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(x => x.PriceRuleAccountTypes!
                    .Any(y => y.Active
                           && (y.SlaveID == factoryContext.AccountTypeID
                               || y.SlaveID == assocAccountTypeUpLevel1ID
                               || y.SlaveID == assocAccountTypeUpLevel2ID
                               || y.SlaveID == assocAccountTypeUpLevel3ID
                               || y.SlaveID == assocAccountTypeUpLevel4ID
                               || y.SlaveID == assocAccountTypeUpLevel5ID)))
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var sameManufacturer = PredicateBuilder.New<PriceRule>(false);
            sameManufacturer = (factoryProduct.ManufacturerIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(sameManufacturer, (c, id) => c.Or(x => x.Manufacturers!.Any(y => y.Active && y.SlaveID == id)));
            var matchesManufacturerID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(sameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var sameStore = PredicateBuilder.New<PriceRule>(false);
            sameStore = (factoryProduct.StoreIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(sameStore, (c, id) => c.Or(x => x.Stores!.Any(y => y.Active && y.SlaveID == id)));
            var matchesStoreID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(sameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var sameVendor = PredicateBuilder.New<PriceRule>(false);
            sameVendor = (factoryProduct.VendorIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(sameVendor, (c, id) => c.Or(x => x.Vendors!.Any(y => y.Active && y.SlaveID == id)));
            var matchesVendorID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(sameVendor)
                .And(noneOrSameCategory)
                .And(noneOrSameUserRole);

            var sameCategory = PredicateBuilder.New<PriceRule>(false);
            sameCategory = (factoryProduct.CategoryIDs ?? new List<int>()).Where(x => Contract.CheckValidID(x))
                .Aggregate(sameCategory, (c, id) => c.Or(x => x.PriceRuleCategories!.Any(y => y.Active && y.SlaveID == id)));
            var matchesCategoryID = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(sameCategory)
                .And(noneOrSameUserRole);

            var sameUserRole = PredicateBuilder.New<PriceRule>(false);
            sameUserRole = (factoryContext.UserRoles ?? new List<string>()).Where(Contract.CheckValidKey)
                .Aggregate(sameUserRole, (c, name) => c.Or(x => x.PriceRuleUserRoles!.Any(y => y.Active && y.RoleName == name)));
            var matchesUserRoleName = PredicateBuilder.New<PriceRule>(true)
                .And(noneOrSameProduct)
                .And(noneOrSameProductType)
                .And(noneOrSameCountry)
                .And(noneOrSameAccount)
                .And(noneOrSameAccountType)
                .And(noneOrSameManufacturer)
                .And(noneOrSameStore)
                .And(noneOrSameVendor)
                .And(noneOrSameCategory)
                .And(sameUserRole);

            var matchesAnon = PredicateBuilder.New<PriceRule>(x => x.IsOnlyForAnonymousUsers && !factoryContext.UserID.HasValue);

            var specificConstraints = PredicateBuilder.New<PriceRule>(false)
                .Or(matchesProductID)
                .Or(matchesProductTypeID)
                .Or(matchesCountryID)
                .Or(matchesAccountID)
                .Or(matchesAccountTypeID)
                .Or(matchesManufacturerID)
                .Or(matchesStoreID)
                .Or(matchesVendorID)
                .Or(matchesCategoryID)
                .Or(matchesUserRoleName)
                .Or(matchesAnon);

            var lookupFunc = PredicateBuilder.And(generalConstraints, specificConstraints).Compile();
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                // TODO: Replace each contains with the appropriate predicates
                var rules = context.PriceRules
                    .AsNoTracking()
#if OVERDEBUG
                    .Include(x => x.PriceRuleCountries)
                    .Include(x => x.PriceRuleAccounts)
                    .Include(x => x.PriceRuleAccountTypes)
                    .Include(x => x.PriceRuleProductTypes)
                    .Include(x => x.PriceRuleUserRoles)
#endif
                    .AsExpandable()
#if OVERDEBUG
                    .Where(x => x.Active)
                    .Where(x => !x.Products.Any(y => y.Active)
                             || x.Products.Any(y => y.Active && y.SlaveID == factoryProduct.ProductID))
#else
                    .Where(lookupFunc)
#endif
                    .Select(x => new
                    {
                        MatchID = x.ID,
                        MatchesByProductID = x.Products!.Any(y => y.Active && y.SlaveID == factoryProduct.ProductID),
                        MatchesByProductTypeID = x.PriceRuleProductTypes!.Any(y => y.Active && y.SlaveID == factoryProduct.ProductTypeID),
                        MatchesByCountryID = x.PriceRuleCountries!.Any(y => y.Active && y.SlaveID == factoryContext.CountryID),
                        MatchesByAccountID = x.Accounts!.Any(y => y.Active && (y.SlaveID == factoryContext.AccountID || y.SlaveID == assocAccountUpLevel1ID || y.SlaveID == assocAccountUpLevel2ID || y.SlaveID == assocAccountUpLevel3ID || y.SlaveID == assocAccountUpLevel4ID || y.SlaveID == assocAccountUpLevel5ID)),
                        MatchesByAccountTypeID = x.PriceRuleAccountTypes!.Any(y => y.Active && (y.SlaveID == factoryContext.AccountTypeID || y.SlaveID == assocAccountTypeUpLevel1ID || y.SlaveID == assocAccountTypeUpLevel2ID || y.SlaveID == assocAccountTypeUpLevel3ID || y.SlaveID == assocAccountTypeUpLevel4ID || y.SlaveID == assocAccountTypeUpLevel5ID)),
                        MatchesByManufacturerID = x.Manufacturers!.Any(y => y.Active && factoryProduct.ManufacturerIDs!.Contains(y.SlaveID)),
                        MatchesByStoreID = x.Stores!.Any(y => y.Active && factoryProduct.StoreIDs!.Contains(y.SlaveID)),
                        MatchesByVendorID = x.Vendors!.Any(y => y.Active && factoryProduct.VendorIDs!.Contains(y.SlaveID)),
                        MatchesByCategoryID = x.PriceRuleCategories!.Any(y => y.Active && factoryProduct.CategoryIDs!.Contains(y.SlaveID)),
                        MatchesByRoleName = x.IsOnlyForAnonymousUsers && !factoryContext.UserID.HasValue || x.PriceRuleUserRoles!.Any(y => y.Active && factoryContext.UserRoles!.Contains(y.RoleName!)),
#if OVERDEBUG
                        UnmatchesByProductID = x.Products.All(y => !y.Active || y.SlaveID != factoryProduct.ProductID),
                        UnmatchesByProductTypeID = x.PriceRuleProductTypes.Any(y => y.Active) && x.PriceRuleProductTypes.All(y => !y.Active || y.SlaveID != factoryProduct.ProductTypeID),
                        UnmatchesByCountryID = x.PriceRuleCountries.Any(y => y.Active) && x.PriceRuleCountries.All(y => !y.Active || y.SlaveID != factoryContext.CountryID),
                        UnmatchesByAccountID = x.Accounts.Any(y => y.Active) && x.Accounts.All(y => !y.Active || (y.SlaveID != factoryContext.AccountID && y.SlaveID != altAccountID)),
                        UnmatchesByAccountTypeID = x.PriceRuleAccountTypes.Any(y => y.Active) && x.PriceRuleAccountTypes.All(y => !y.Active || (y.SlaveID != factoryContext.AccountTypeID && y.SlaveID != altAccountTypeID)),
                        UnmatchesByManufacturerID = x.Manufacturers.Any(y => y.Active) && x.Manufacturers.All(y => !y.Active || !factoryProduct.ManufacturerIDs.Contains(y.SlaveID)),
                        UnmatchesByStoreID = x.Stores.Any(y => y.Active) && x.Stores.All(y => !y.Active || !factoryProduct.StoreIDs.Contains(y.SlaveID)),
                        UnmatchesByVendorID = x.Vendors.Any(y => y.Active) && x.Vendors.All(y => !y.Active || !factoryProduct.VendorIDs.Contains(y.SlaveID)),
                        UnmatchesByCategoryID = x.PriceRuleCategories.Any(y => y.Active) && x.PriceRuleCategories.All(y => !y.Active || !factoryProduct.CategoryIDs.Contains(y.SlaveID)),
                        UnmatchesByRoleName = !x.IsOnlyForAnonymousUsers && factoryContext.UserID.HasValue || x.PriceRuleUserRoles.All(y => !y.Active || !factoryContext.UserRoles.Contains(y.RoleName)),
#endif
                        Match = x,
                    })
                    .Select(x => new
                    {
                        x.MatchID,
                        x.MatchesByAccountID,
                        x.MatchesByAccountTypeID,
                        x.MatchesByCategoryID,
                        x.MatchesByCountryID,
                        x.MatchesByManufacturerID,
                        x.MatchesByProductID,
                        x.MatchesByProductTypeID,
                        x.MatchesByStoreID,
                        x.MatchesByVendorID,
#if OVERDEBUG
                        x.UnmatchesByAccountID,
                        x.UnmatchesByAccountTypeID,
                        x.UnmatchesByCategoryID,
                        x.UnmatchesByCountryID,
                        x.UnmatchesByManufacturerID,
                        x.UnmatchesByProductID,
                        x.UnmatchesByProductTypeID,
                        x.UnmatchesByStoreID,
                        x.UnmatchesByVendorID,
#endif
                        Score =
#if OVERDEBUG
                        x.UnmatchesByProductID || x.UnmatchesByProductTypeID || x.UnmatchesByCountryID
                             || x.UnmatchesByAccountID || x.UnmatchesByAccountTypeID || x.UnmatchesByStoreID
                             || x.UnmatchesByManufacturerID || x.UnmatchesByVendorID || x.UnmatchesByCategoryID
                            ? int.MinValue
                            :
#endif
                              (x.MatchesByProductID ? 1000 : 0)
                               + (x.MatchesByProductTypeID ? 500 : 0)
                               + (x.MatchesByCountryID ? 600 : 0)
                               + (x.MatchesByAccountID ? 800 : 0)
                               + (x.MatchesByAccountTypeID ? 400 : 0)
                               + (x.MatchesByManufacturerID ? 600 : 0)
                               + (x.MatchesByStoreID ? 600 : 0)
                               + (x.MatchesByVendorID ? 600 : 0)
                               + (x.MatchesByCategoryID ? 600 : 0)
                               + (x.MatchesByRoleName ? 800 : 0),
                        x.Match,
#if OVERDEBUG
                        x.Match.PriceRuleProductTypes,
                        x.Match.PriceRuleAccounts,
                        x.Match.PriceRuleAccountTypes,
                        x.Match.PriceRuleCountries,
                        x.Match.PriceRuleUserRoles,
#endif
                    })
                    .OrderByDescending(x => x.Match.IsExclusive) // Exclusives first
                    .ThenByDescending(x => x.Match.Priority ?? int.MinValue) // Exclusives have Priorities
                    .ThenBy(x => x.Match.IsPercentage) // Non-Percentages first
                    .ThenByDescending(x => x.Score)
#if OVERDEBUG
                    ;
                foreach (var item in rules)
                {
                    System.Diagnostics.Debug.WriteLine(
                        Newtonsoft.Json.JsonConvert.SerializeObject(
                            item/*,
                            SerializableAttributesDictionaryExtensions.JsonSettings*/));
                }
                var rules2 = rules
#endif
                    .Select(x => x.Match)
                    .Select(x => ModelMapperForPriceRule.MapPriceRuleModelFromEntityFull(x, contextProfileName))
                    .ToList();
#if OVERDEBUG
                relevantRules.AddRange(rules2);
#else
                relevantRules.AddRange(rules!);
#endif
            }
            return (
                relevantRules,
                assocAccountUpLevel1ID,
                assocAccountTypeUpLevel1ID,
                assocAccountUpLevel2ID,
                assocAccountTypeUpLevel2ID,
                assocAccountUpLevel3ID,
                assocAccountTypeUpLevel3ID,
                assocAccountUpLevel4ID,
                assocAccountTypeUpLevel4ID,
                assocAccountUpLevel5ID,
                assocAccountTypeUpLevel5ID);
        }

        private static (IPriceRuleModel? @base, IPriceRuleModel? sale) GetBestExclusivePriceRulesWithOverride(
            IReadOnlyCollection<IPriceRuleModel?> rules,
            int productID)
        {
            if (rules.Count == 1)
            {
                var rule = rules.First();
                if (rule is null)
                {
                    return (null, null);
                }
                if (rule.UsePriceBase)
                {
                    return (rule, null);
                }
                return (null, rule);
            }
            // Check if there is any price override
            var numberOfOverride = rules
                .Count(r => r == null || r.Products!.Any(pr => pr.OverridePrice && pr.SlaveID == productID));
            // if we find 1 override -- we use it
            if (numberOfOverride == 1)
            {
                var rule = rules.Single(r => r == null || r.Products!.Any(pr => pr.OverridePrice && pr.SlaveID == productID));
                if (rule is null)
                {
                    return (null, null);
                }
                if (rule.UsePriceBase)
                {
                    return (rule, null);
                }
                return (null, rule);
            }
            // if there is more than one -- we need to define the best price in the overrides
            if (numberOfOverride > 1)
            {
            }
            var overrideRules = rules
                .Where(r => r != null && r.Products!.Any(pr => pr.OverridePrice && pr.SlaveID == productID))
                .Cast<IPriceRuleModel>()
                .ToList();
            return DetermineBestOverridePrices(overrideRules);
        }

        private static (IPriceRuleModel? @base, IPriceRuleModel? sale) GetBestExclusivePriceRules(
            IReadOnlyCollection<IPriceRuleModel> rules)
        {
            switch (rules.Count)
            {
                case 1:
                {
                    var rule = rules.First();
                    if (rule.UsePriceBase)
                    {
                        return (rule, null);
                    }
                    return (null, rule);
                }
                default:
                {
                    // if more than one exclusive, return the one with the best price with the customer
                    return DetermineBestBaseAndSaleRulesByHighestPriority(rules.ToList());
                }
            }
        }

        private static (IPriceRuleModel? @base, IPriceRuleModel? sale) DetermineBestBaseAndSaleRulesByHighestPriority(
            IReadOnlyCollection<IPriceRuleModel> rules)
        {
            return (DetermineBestRuleByHighestPriority(rules, true),
                    DetermineBestRuleByHighestPriority(rules, false));
        }

        // ReSharper disable once UnusedMember.Local
        private static (IPriceRuleModel? @base, IPriceRuleModel? sale) DetermineBestBaseAndSaleRulesByLowestResultPrices(
            IReadOnlyCollection<IPriceRuleModel> rules)
        {
            return (DetermineBestRuleByLowestResultPrice(rules, true),
                    DetermineBestRuleByLowestResultPrice(rules, false));
        }

        private static (IPriceRuleModel? @base, IPriceRuleModel? sale) DetermineBestOverridePrices(
            IReadOnlyCollection<IPriceRuleModel> rules)
        {
            return (DetermineRuleWithBestOverridePrice(rules, true),
                    DetermineRuleWithBestOverridePrice(rules, false));
        }

        private static IPriceRuleModel? DetermineBestRuleByHighestPriority(
            IReadOnlyCollection<IPriceRuleModel> rules,
            bool useBase)
        {
            var highestPriorityRule = rules
                .Where(x => x.UsePriceBase == useBase)
                .OrderByDescending(x => x.Priority ?? int.MinValue)
                .FirstOrDefault();
            if (highestPriorityRule == null)
            {
                return null;
            }
            var priority = highestPriorityRule.Priority ?? int.MinValue;
            if (rules.Count(x => (x.Priority ?? int.MinValue) == priority) > 1)
            {
                // Determine best price between shared priority
                return DetermineBestRuleByLowestResultPrice(
                    rules.Where(x => (x.Priority ?? int.MinValue) == priority),
                    useBase);
            }
            return rules
                .Where(x => x.UsePriceBase == useBase)
                .OrderByDescending(x => x.Priority ?? int.MinValue)
                .FirstOrDefault();
        }

        private static IPriceRuleModel? DetermineBestRuleByLowestResultPrice(
            IEnumerable<IPriceRuleModel> rules, bool useBase)
        {
            decimal? currentBestPrice = null;
            IPriceRuleModel? bestRule = null;
            foreach (var rule in rules.Where(x => x.UsePriceBase == useBase))
            {
                var ret = ApplyRuleToPrice(rule, 100m);
                if (ret >= currentBestPrice)
                {
                    continue;
                }
                // Store just the delta from our example $100, not a final number
                currentBestPrice = ret - 100;
                bestRule = rule;
            }
            return bestRule;
        }

        private static IPriceRuleModel? DetermineRuleWithBestOverridePrice(
            IEnumerable<IPriceRuleModel> rules, bool useBase)
        {
            var currentBestPrice = -1m;
            IPriceRuleModel? bestRule = null;
            foreach (var rule in rules.Where(x => x.UsePriceBase == useBase))
            {
                var foundProductRule = rule.Products!.Find(p => p.OverridePrice);
                if (foundProductRule == null)
                {
                    continue;
                }
                var price = useBase ? foundProductRule.OverrideBasePrice : foundProductRule.OverrideSalePrice;
                if (price != null && bestRule == null)
                {
                    bestRule = rule;
                    currentBestPrice = price.Value;
                    continue;
                }
                if (price == null || price >= currentBestPrice)
                {
                    continue;
                }
                currentBestPrice = price.Value;
                bestRule = rule;
            }
            return bestRule;
        }

        private static (List<IPriceRuleModel?> usedRules, decimal @base, decimal? sale) ProcessRelevantRules(
            IPricingFactoryProductModel factoryProduct,
            IReadOnlyCollection<IPriceRuleModel?> relevantRules)
        {
            IPriceRuleModel? exclusiveRuleForPriceBase = null;
            IPriceRuleModel? exclusiveRuleForPriceSale = null;
            IPriceRuleModel? exclusiveRuleForPriceBaseWithOverride = null;
            IPriceRuleModel? exclusiveRuleForPriceSaleWithOverride = null;
            var exclusiveWithOverrideRules = relevantRules
                .Where(x => x is { IsExclusive: true } && x.Products!.Any(y => y.OverridePrice && y.SlaveID == factoryProduct.ProductID))
                .ToList();
            if (exclusiveWithOverrideRules.Count > 0)
            {
                (exclusiveRuleForPriceBaseWithOverride, exclusiveRuleForPriceSaleWithOverride) = GetBestExclusivePriceRulesWithOverride(
                    exclusiveWithOverrideRules,
                    factoryProduct.ProductID);
            }
            var exclusiveRulesWithoutOverrides = relevantRules
                .Where(x => x is { IsExclusive: true } && (x.Products!.Any(y => !y.OverridePrice && y.SlaveID == factoryProduct.ProductID) // this one and specifies not to override
                    || !x.Products!.Any())) // No products on the rule at all
                .Cast<IPriceRuleModel>()
                .ToList();
            if (exclusiveRulesWithoutOverrides.Count > 0)
            {
                (exclusiveRuleForPriceBase, exclusiveRuleForPriceSale) = GetBestExclusivePriceRules(
                    exclusiveRulesWithoutOverrides);
            }
            var basePrice = factoryProduct.PriceBase ?? 0;
            var salePrice = factoryProduct.PriceSale > 0 ? factoryProduct.PriceSale.Value : (decimal?)null;
            if (factoryProduct.KitBaseQuantityPriceMultiplier > 0
                && factoryProduct.KitBaseQuantityPriceMultiplier != 1m)
            {
                basePrice *= factoryProduct.KitBaseQuantityPriceMultiplier.Value;
                if (salePrice.HasValue)
                {
                    salePrice *= factoryProduct.KitBaseQuantityPriceMultiplier.Value;
                }
            }
            if (exclusiveRuleForPriceBaseWithOverride != null)
            {
                basePrice = CalculatePriceBase(exclusiveRuleForPriceBaseWithOverride, basePrice, factoryProduct.ProductID);
                if (factoryProduct.KitBaseQuantityPriceMultiplier > 0
                    && factoryProduct.KitBaseQuantityPriceMultiplier != 1m)
                {
                    basePrice *= factoryProduct.KitBaseQuantityPriceMultiplier.Value;
                }
            }
            else if (exclusiveRuleForPriceBase != null)
            {
                basePrice = CalculatePriceBase(exclusiveRuleForPriceBase, basePrice, factoryProduct.ProductID);
            }
            if (salePrice.HasValue)
            {
                if (exclusiveRuleForPriceSaleWithOverride != null)
                {
                    salePrice = CalculatePriceSale(exclusiveRuleForPriceSaleWithOverride, salePrice, factoryProduct.ProductID);
                    if (factoryProduct.KitBaseQuantityPriceMultiplier > 0
                        && factoryProduct.KitBaseQuantityPriceMultiplier != 1m)
                    {
                        salePrice *= factoryProduct.KitBaseQuantityPriceMultiplier.Value;
                    }
                }
                else if (exclusiveRuleForPriceSale != null)
                {
                    salePrice = CalculatePriceSale(exclusiveRuleForPriceSale, salePrice, factoryProduct.ProductID);
                }
            }
            if ((exclusiveRuleForPriceBaseWithOverride != null
                    || exclusiveRuleForPriceBase != null)
                && (!salePrice.HasValue
                    || exclusiveRuleForPriceSaleWithOverride != null
                    || exclusiveRuleForPriceSale != null))
            {
                // No further rule changes allowed
                return (new() { exclusiveRuleForPriceBaseWithOverride ?? exclusiveRuleForPriceBase, exclusiveRuleForPriceSaleWithOverride ?? exclusiveRuleForPriceSale, },
                    basePrice,
                    salePrice);
            }
            // At this point exclusive rules are no longer relevant, so filter them out, but don't modify base or sale
            // any further if one of them had an exclusive rule
            var nonExclusiveRules = relevantRules.Where(x => x is { IsExclusive: false }).ToList();
            foreach (var relevantRule in nonExclusiveRules)
            {
                if (relevantRule!.UsePriceBase
                    && exclusiveRuleForPriceBaseWithOverride == null
                    && exclusiveRuleForPriceBase == null)
                {
                    basePrice = ApplyRuleToPrice(relevantRule, basePrice);
                }
                else if (salePrice.HasValue
                    && !relevantRule.UsePriceBase
                    && exclusiveRuleForPriceSaleWithOverride == null
                    && exclusiveRuleForPriceSale == null)
                {
                    salePrice = ApplyRuleToPrice(relevantRule, salePrice.Value);
                }
            }
            return (nonExclusiveRules, basePrice, salePrice);
        }

        private static string AggregateIDList(IEnumerable<int>? ids)
        {
            return ids?.Select(x => x.ToString()).DefaultIfEmpty().Aggregate((c, n) => c + "," + n) ?? "0";
        }

        private static string AggregateKeyList(IEnumerable<string>? keys)
        {
            return keys?.DefaultIfEmpty().Aggregate((c, n) => c + "," + n) ?? "0";
        }
    }
}
