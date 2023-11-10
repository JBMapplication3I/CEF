// <copyright file="DoMockingSetupForContextRunnerPricing.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner pricing class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerPricingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Price Points
            if (DoAll || DoPricing || DoPricePointTable)
            {
                RawPricePoints = new()
                {
                    await CreateADummyPricePointAsync(id: 1, key: "WEB", name: "Web", desc: "desc", displayName: "Web").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 2, key: "FT", name: "Full Truck", desc: "desc", displayName: "Full Truck").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 3, key: "HT", name: "Half Truck", desc: "desc", displayName: "Half Truck").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 4, key: "LTL", name: "Less than Truck-Load", desc: "desc", displayName: "Less than Truck-Load").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 5, key: "RETAIL", name: "Retail", desc: "desc", displayName: "Retail").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 54, key: "ZORK1-2M", name: "Zork 1-2M", desc: "desc", displayName: "Zork 1-2M").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 55, key: "ZORK20-49M", name: "Zork 20-49M", desc: "desc", displayName: "Zork 20-49M").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 56, key: "ZORK3-5M", name: "Zork 3-5M", desc: "desc", displayName: "Zork 3-5M").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 57, key: "ZORK50M+", name: "Zork 50M+", desc: "desc", displayName: "Zork 50M+").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 58, key: "ZORK6-19M", name: "Zork 6-19M", desc: "desc", displayName: "Zork 6-19M").ConfigureAwait(false),
                    await CreateADummyPricePointAsync(id: 500, key: "WHOLESALE", name: "Wholesale", desc: "desc", displayName: "Wholesale").ConfigureAwait(false),
                };
                await InitializeMockSetPricePointsAsync(mockContext, RawPricePoints).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Roundings
            if (DoAll || DoPricing || DoPriceRoundingTable)
            {
                RawPriceRoundings = new()
                {
                    // 0200-ANGBT-BFUT-IA
                    await CreateADummyPriceRoundingAsync(id: 007204, key: "PRICE-ROUNDING-01", currencyKey: "USD", pricePointKey: "FT", productKey: "0200-ANGBT-BFUT-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 007205, key: "PRICE-ROUNDING-02", currencyKey: "USD", pricePointKey: "HT", productKey: "0200-ANGBT-BFUT-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 007206, key: "PRICE-ROUNDING-03", currencyKey: "USD", pricePointKey: "LTL", productKey: "0200-ANGBT-BFUT-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 007207, key: "PRICE-ROUNDING-04", currencyKey: "USD", pricePointKey: "RETAIL", productKey: "0200-ANGBT-BFUT-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 007208, key: "PRICE-ROUNDING-05", currencyKey: "USD", pricePointKey: "WEB", productKey: "0200-ANGBT-BFUT-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    // 0200-ANGRF-OPERA-VSI-IA
                    await CreateADummyPriceRoundingAsync(id: 007209, key: "PRICE-ROUNDING-06", currencyKey: "USD", pricePointKey: "FT", productKey: "0200-ANGRF-OPERA-VSI-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 007210, key: "PRICE-ROUNDING-07", currencyKey: "USD", pricePointKey: "HT", productKey: "0200-ANGRF-OPERA-VSI-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 007211, key: "PRICE-ROUNDING-08", currencyKey: "USD", pricePointKey: "LTL", productKey: "0200-ANGRF-OPERA-VSI-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 007212, key: "PRICE-ROUNDING-09", currencyKey: "USD", pricePointKey: "WEB", productKey: "0200-ANGRF-OPERA-VSI-IA", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    // ZORK-BLACK
                    await CreateADummyPriceRoundingAsync(id: 006442, key: "PRICE-ROUNDING-10", currencyKey: "USD", pricePointKey: "LTL", productKey: "ZORK-BLACK", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 006443, key: "PRICE-ROUNDING-11", currencyKey: "USD", pricePointKey: "WEB", productKey: "ZORK-BLACK", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 006444, key: "PRICE-ROUNDING-12", currencyKey: "USD", pricePointKey: "ZORK1-2M", productKey: "ZORK-BLACK", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 006445, key: "PRICE-ROUNDING-13", currencyKey: "USD", pricePointKey: "ZORK20-49M", productKey: "ZORK-BLACK", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 006446, key: "PRICE-ROUNDING-14", currencyKey: "USD", pricePointKey: "ZORK3-5M", productKey: "ZORK-BLACK", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 006447, key: "PRICE-ROUNDING-15", currencyKey: "USD", pricePointKey: "ZORK50M+", productKey: "ZORK-BLACK", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 006448, key: "PRICE-ROUNDING-16", currencyKey: "USD", pricePointKey: "ZORK6-19M", productKey: "ZORK-BLACK", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "CS").ConfigureAwait(false),
                    // Knee Pro-Tec™ Patellar Tendon Strap
                    await CreateADummyPriceRoundingAsync(id: 500001, key: "PRICE-ROUNDING-17", currencyKey: "USD", pricePointKey: "WEB", productKey: "100{Size}{Location}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 500002, key: "PRICE-ROUNDING-18", currencyKey: "USD", pricePointKey: "WHOLESALE", productKey: "100{Size}{Location}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    // Back of Knee Compression Wrap
                    await CreateADummyPriceRoundingAsync(id: 500003, key: "PRICE-ROUNDING-19", currencyKey: "USD", pricePointKey: "WEB", productKey: "1700F{Location}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 500004, key: "PRICE-ROUNDING-20", currencyKey: "USD", pricePointKey: "WHOLESALE", productKey: "1700F{Location}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    // Short Sleeve™ Knee Support
                    await CreateADummyPriceRoundingAsync(id: 500005, key: "PRICE-ROUNDING-21", currencyKey: "USD", pricePointKey: "WEB", productKey: "600{Size}{Location}{Package Type}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 500006, key: "PRICE-ROUNDING-22", currencyKey: "USD", pricePointKey: "WHOLESALE", productKey: "600{Size}{Location}{Package Type}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    // 3D Flat Premium Knee Support
                    await CreateADummyPriceRoundingAsync(id: 500007, key: "PRICE-ROUNDING-23", currencyKey: "USD", pricePointKey: "WEB", productKey: "740{Size}-{Location}{Package Type}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyPriceRoundingAsync(id: 500008, key: "PRICE-ROUNDING-24", currencyKey: "USD", pricePointKey: "WHOLESALE", productKey: "740{Size}-{Location}{Package Type}", roundHow: 1, roundingAmount: 2, roundTo: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                };
                await InitializeMockSetPriceRoundingsAsync(mockContext, RawPriceRoundings).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rules
            if (DoAll || DoPricing || DoPriceRuleTable)
            {
                RawPriceRules = new()
                {
                    // Base Price Rules
                    await CreateADummyPriceRuleAsync(id: 01, key: "Base-By-Account",            name: "Base by Account",         desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 01.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 02, key: "Base-By-Account-Type",       name: "Base by Account Type",    desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 02.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 03, key: "Base-By-Category",           name: "Base by Category",        desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 03.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 04, key: "Base-By-Manufacturer",       name: "Base by Manufacturer",    desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 04.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 05, key: "Base-By-Product",            name: "Base by Product",         desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 05.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 06, key: "Base-By-Product-Type",       name: "Base by Product Type",    desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 06.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 07, key: "Base-By-Store",              name: "Base by Store",           desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 07.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 08, key: "Base-By-Vendor",             name: "Base by Vendor",          desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 08.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 09, key: "Base-By-UserRole-CGA",       name: "Base by User Role-CGA",   desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 09.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 10, key: "Base-By-UserRole-CSA",       name: "Base by User Role-CSA",   desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 10.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 11, key: "Base-By-UserRole-CU",        name: "Base by User Role-CU",    desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 11.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 12, key: "Base-By-Anonymous",          name: "Base by Anonymous",       desc: "desc", isOnlyForAnonymousUsers: true,  isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 12.00m).ConfigureAwait(false),
                    // Sale Price Rules
                    await CreateADummyPriceRuleAsync(id: 13, key: "Sale-By-Account",            name: "Sale by Account",         desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 01.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 14, key: "Sale-By-Account-Type",       name: "Sale by Account Type",    desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 02.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 15, key: "Sale-By-Category",           name: "Sale by Category",        desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 03.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 16, key: "Sale-By-Manufacturer",       name: "Sale by Manufacturer",    desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 04.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 17, key: "Sale-By-Product",            name: "Sale by Product",         desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 05.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 18, key: "Sale-By-Product-Type",       name: "Sale by Product Type",    desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 06.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 19, key: "Sale-By-Store",              name: "Sale by Store",           desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 07.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 20, key: "Sale-By-Vendor",             name: "Sale by Vendor",          desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 08.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 21, key: "Sale-By-UserRole-CGA",       name: "Sale by User Role-CGA",   desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 09.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 22, key: "Sale-By-UserRole-CSA",       name: "Sale by User Role-CSA",   desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 10.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 23, key: "Sale-By-UserRole-CU",        name: "Sale by User Role-CU",    desc: "desc", isPercentage: true,  isMarkup: true, priceAdjustment: 11.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 24, key: "Sale-By-Anonymous",          name: "Sale by Anonymous",       desc: "desc", isOnlyForAnonymousUsers: true,  isPercentage: true,  isMarkup: true, priceAdjustment: 12.00m).ConfigureAwait(false),
                    // Exclusive Price Rule (no product starting value overrides)
                    await CreateADummyPriceRuleAsync(id: 25, key: "Base-By-Exclusive",          name: "Exclusive Base for 1152", desc: "desc", isExclusive: true, isMarkup: true, usePriceBase: true,  priceAdjustment: 20.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 26, key: "Sale-By-Exclusive",          name: "Exclusive Sale for 1152", desc: "desc", isExclusive: true, isMarkup: true, priceAdjustment: 19.01m).ConfigureAwait(false),
                    // Exclusive Price Rules with Override of Product starting value
                    await CreateADummyPriceRuleAsync(id: 27, key: "Base-By-Exclusive-Override", name: "Exclusive Base for 0969", desc: "desc", isExclusive: true, isMarkup: true, usePriceBase: true,  priceAdjustment: 00.00m).ConfigureAwait(false),
                    await CreateADummyPriceRuleAsync(id: 28, key: "Sale-By-Exclusive-Override", name: "Exclusive Sale for 0969", desc: "desc", isExclusive: true, isMarkup: true, priceAdjustment: 00.00m).ConfigureAwait(false),
                    // Base price Rule Country
                    await CreateADummyPriceRuleAsync(id: 29, key: "Base-By-Country",            name: "Base by Country",         desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 13.00m).ConfigureAwait(false),
                    // Sale price Rule Country
                    await CreateADummyPriceRuleAsync(id: 30, key: "Sale-By-Country",            name: "Sale by Country",         desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 13.00m).ConfigureAwait(false),
                    // Base price Rule Franchise
                    await CreateADummyPriceRuleAsync(id: 31, key: "Base-By-Franchise",          name: "Base by Franchise",       desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 13.00m).ConfigureAwait(false),
                    // Sale price Rule Franchise
                    await CreateADummyPriceRuleAsync(id: 32, key: "Sale-By-Franchise",          name: "Sale by Franchise",       desc: "desc", isPercentage: true,  isMarkup: true, usePriceBase: true,  priceAdjustment: 13.00m).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRulesAsync(mockContext, RawPriceRules).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Accounts
            if (DoAll || DoPricing || DoPriceRuleAccountTable)
            {
                var index = 0;
                RawPriceRuleAccounts = new()
                {
                    await CreateADummyPriceRuleAccountAsync(id: ++index, key: "Account-Base", masterID: 01).ConfigureAwait(false),
                    await CreateADummyPriceRuleAccountAsync(id: ++index, key: "Account-Sale", masterID: 13).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleAccountsAsync(mockContext, RawPriceRuleAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Account Types
            if (DoAll || DoPricing || DoPriceRuleAccountTypeTable)
            {
                var index = 0;
                RawPriceRuleAccountTypes = new()
                {
                    await CreateADummyPriceRuleAccountTypeAsync(id: ++index, key: "AccountType-Base", masterID: 02).ConfigureAwait(false),
                    await CreateADummyPriceRuleAccountTypeAsync(id: ++index, key: "AccountType-Sale", masterID: 14).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleAccountTypesAsync(mockContext, RawPriceRuleAccountTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Brands
            if (DoAll || DoPricing || DoPriceRuleBrandTable)
            {
                var index = 0;
                RawPriceRuleBrands = new()
                {
                    await CreateADummyPriceRuleBrandAsync(id: ++index, key: "Brand-Base", masterID: 01).ConfigureAwait(false),
                    await CreateADummyPriceRuleBrandAsync(id: ++index, key: "Brand-Sale", masterID: 02).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleBrandsAsync(mockContext, RawPriceRuleBrands).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Categories
            if (DoAll || DoPricing || DoPriceRuleCategoryTable)
            {
                var index = 0;
                RawPriceRuleCategories = new()
                {
                    await CreateADummyPriceRuleCategoryAsync(id: ++index, key: "Category-Base", masterID: 03).ConfigureAwait(false),
                    await CreateADummyPriceRuleCategoryAsync(id: ++index, key: "Category-Sale", masterID: 15).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleCategoriesAsync(mockContext, RawPriceRuleCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Countries
            if (DoAll || DoPricing || DoPriceRuleCountryTable)
            {
                var index = 0;
                RawPriceRuleCountries = new()
                {
                    await CreateADummyPriceRuleCountryAsync(id: ++index, key: "Country-Base", masterID: 29).ConfigureAwait(false),
                    await CreateADummyPriceRuleCountryAsync(id: ++index, key: "Country-Sale", masterID: 30).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleCountriesAsync(mockContext, RawPriceRuleCountries).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Franchises
            if (DoAll || DoPricing || DoPriceRuleFranchiseTable)
            {
                var index = 0;
                RawPriceRuleFranchises = new()
                {
                    await CreateADummyPriceRuleFranchiseAsync(id: ++index, key: "Franchise-Base", masterID: 31).ConfigureAwait(false),
                    await CreateADummyPriceRuleFranchiseAsync(id: ++index, key: "Franchise-Sale", masterID: 32).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleFranchisesAsync(mockContext, RawPriceRuleFranchises).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Manufacturers
            if (DoAll || DoPricing || DoPriceRuleManufacturerTable)
            {
                var index = 0;
                RawPriceRuleManufacturers = new()
                {
                    await CreateADummyPriceRuleManufacturerAsync(id: ++index, key: "Manufacturer-Base", masterID: 04).ConfigureAwait(false),
                    await CreateADummyPriceRuleManufacturerAsync(id: ++index, key: "Manufacturer-Sale", masterID: 16).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleManufacturersAsync(mockContext, RawPriceRuleManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Product
            if (DoAll || DoPricing || DoPriceRuleProductTable)
            {
                var index = 0;
                RawPriceRuleProducts = new()
                {
                    await CreateADummyPriceRuleProductAsync(id: ++index, key: "Product-1151-Base", masterID: 05, slaveID: 1151).ConfigureAwait(false),
                    await CreateADummyPriceRuleProductAsync(id: ++index, key: "Product-1151-Sale", masterID: 17, slaveID: 1151).ConfigureAwait(false),
                    await CreateADummyPriceRuleProductAsync(id: ++index, key: "Product-1152-Base", masterID: 25, slaveID: 1152).ConfigureAwait(false),
                    await CreateADummyPriceRuleProductAsync(id: ++index, key: "Product-1152-Sale", masterID: 26, slaveID: 1152).ConfigureAwait(false),
                    await CreateADummyPriceRuleProductAsync(id: ++index, key: "Product-0969-Base", masterID: 27, slaveID: 0969, overridePrice: true,  overrideBasePrice: 0.18999m).ConfigureAwait(false),
                    await CreateADummyPriceRuleProductAsync(id: ++index, key: "Product-0969-Sale", masterID: 28, slaveID: 0969, overridePrice: true,     overrideSalePrice: 0.18899m).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleProductsAsync(mockContext, RawPriceRuleProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Product Types
            if (DoAll || DoPricing || DoPriceRuleProductTypeTable)
            {
                var index = 0;
                RawPriceRuleProductTypes = new()
                {
                    await CreateADummyPriceRuleProductTypeAsync(id: ++index, key: "ProductType-Base", masterID: 06).ConfigureAwait(false),
                    await CreateADummyPriceRuleProductTypeAsync(id: ++index, key: "ProductType-Sale", masterID: 18).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleProductTypesAsync(mockContext, RawPriceRuleProductTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Stores
            if (DoAll || DoPricing || DoPriceRuleStoreTable)
            {
                var index = 0;
                RawPriceRuleStores = new()
                {
                    await CreateADummyPriceRuleStoreAsync(id: ++index, key: "Store-Base", masterID: 07).ConfigureAwait(false),
                    await CreateADummyPriceRuleStoreAsync(id: ++index, key: "Store-Sale", masterID: 19).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleStoresAsync(mockContext, RawPriceRuleStores).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule User Roles
            if (DoAll || DoPricing || DoPriceRuleUserRoleTable)
            {
                var index = 0;
                RawPriceRuleUserRoles = new()
                {
                    await CreateADummyPriceRuleUserRoleAsync(id: ++index, key: "Role-CGA-Base", roleName: "CEF Global Administrator", priceRuleID: 09).ConfigureAwait(false),
                    await CreateADummyPriceRuleUserRoleAsync(id: ++index, key: "Role-CSA-Base", roleName: "CEF Store Administrator",  priceRuleID: 10).ConfigureAwait(false),
                    await CreateADummyPriceRuleUserRoleAsync(id: ++index, key: "Role-CU-Base",  roleName: "CEF User",                 priceRuleID: 11).ConfigureAwait(false),
                    await CreateADummyPriceRuleUserRoleAsync(id: ++index, key: "Role-CGA-Sale", roleName: "CEF Global Administrator", priceRuleID: 21).ConfigureAwait(false),
                    await CreateADummyPriceRuleUserRoleAsync(id: ++index, key: "Role-CSA-Sale", roleName: "CEF Store Administrator",  priceRuleID: 22).ConfigureAwait(false),
                    await CreateADummyPriceRuleUserRoleAsync(id: ++index, key: "Role-C-SaleU",  roleName: "CEF User",                 priceRuleID: 23).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleUserRolesAsync(mockContext, RawPriceRuleUserRoles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Price Rule Vendors
            if (DoAll || DoPricing || DoPriceRuleVendorTable)
            {
                var index = 0;
                RawPriceRuleVendors = new()
                {
                    await CreateADummyPriceRuleVendorAsync(id: ++index, key: "Vendor-Base", masterID: 08).ConfigureAwait(false),
                    await CreateADummyPriceRuleVendorAsync(id: ++index, key: "Vendor-Sale", masterID: 20).ConfigureAwait(false),
                };
                await InitializeMockSetPriceRuleVendorsAsync(mockContext, RawPriceRuleVendors).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
