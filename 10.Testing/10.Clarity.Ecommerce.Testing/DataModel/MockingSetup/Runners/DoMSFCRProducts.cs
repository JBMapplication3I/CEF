// <copyright file="DoMockingSetupForContextRunnerProductsAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner products class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerProductsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Future Imports
            if (DoAll || DoProducts || DoFutureImportTable)
            {
                RawFutureImports = new()
                {
                    await CreateADummyFutureImportAsync(1, key: "Pending", name: "Pending", desc: "desc", attempts: 0, fileName: "somefile.xlsx", runImportAt: CreatedDate.AddDays(3)).ConfigureAwait(false),
                };
                await InitializeMockSetFutureImportsAsync(mockContext, RawFutureImports).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Future Import Statuses
            if (DoAll || DoProducts || DoFutureImportStatusTable)
            {
                RawFutureImportStatuses = new()
                {
                    await CreateADummyFutureImportStatusAsync(1, key: "Pending", name: "Pending", desc: "desc", sortOrder: 0, displayName: "Pending").ConfigureAwait(false),
                    await CreateADummyFutureImportStatusAsync(2, key: "Retrying", name: "Import Failed - Retrying", desc: "desc", sortOrder: 1, displayName: "Import Failed - Retrying").ConfigureAwait(false),
                    await CreateADummyFutureImportStatusAsync(3, key: "Retries Failed", name: "Import Failed - Retries Failed", desc: "desc", sortOrder: 2, displayName: "Import Failed - Retries Failed").ConfigureAwait(false),
                    await CreateADummyFutureImportStatusAsync(4, key: "Completed", name: "Completed", desc: "desc", sortOrder: 3, displayName: "Completed").ConfigureAwait(false),
                    await CreateADummyFutureImportStatusAsync(5, key: "Cancelled", name: "Cancelled", desc: "desc", sortOrder: 4, displayName: "Cancelled").ConfigureAwait(false),
                };
                await InitializeMockSetFutureImportStatusesAsync(mockContext, RawFutureImportStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Products
            if (DoAll || DoProducts || DoProductTable)
            {
                RawProducts = new()
                {
                    await CreateADummyProductAsync(id: 001151, key: "0200-ANGBT-BFUT-IA",       name: "200ml BF ANG BT",                desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", packageID: 0001, priceBase: 12.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 011m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "200ml-BF-ANG-BT",          shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "CS",   weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m,
                        jsonAttributes: new SerializableAttributesDictionary { ["Color"] = new SerializableAttributeObject { ID = 2, Key = "Color", Value = "Blue" } }.SerializeAttributesDictionary()).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 001152, key: "0200-ANGRF-OPERA-VSI-IA",  name: "200ml OPERA ANG",                desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", packageID: 0001, priceBase: 30.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "200ml-OPERA-ANG",          shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "CS",   weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 001153, key: "0700-ANGBT-HAMART-IA",     name: "700ml BF ANG BT Testing",        desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 12.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "300ml-BF-ANG-BT",          shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 001154, key: "0300-ANGRF-LUNA-VSI-IA",   name: "300ml LUNA ANG",                 desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 40.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "300ml-OPERA-ANG",          shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 001155, key: "0400-ANGBT-VISTA-IA",      name: "400ml BF VISTA BT",              desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 52.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "400ml-BF-ANG-BT",          shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 001156, key: "0500-ANGRF-TERRA-VSI-IA",  name: "500ml TERRA ANG",                desc: "This is random", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 60.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "500ml-OPERA-ANG",          shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 000969, key: "ZORK-BLACK",               name: "BLACK ZORK",                     desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 1000m, manufacturerPartNumber: "mnf", packageID: 0001, priceBase: 00.19000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "BLACK-ZORK",               shortDescription: "short desc", stockQuantity: 1000, unitOfMeasure: "CS",   weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    // Variant
                    await CreateADummyProductAsync(id: 400000, key: "VariantMaster",            name: "A Variant Master",               desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Variant-Master",         shortDescription: "short desc", stockQuantity: 0001, typeID: 3, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 400001, key: "VariantOfMaster1",         name: "A Variant Of Master 1",          desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Variant-Master-1",       shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 400002, key: "VariantOfMaster2",         name: "A Variant Of Master 2",          desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Variant-Master-2",       shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    // Kit
                    await CreateADummyProductAsync(id: 400003, key: "KitMaster",                name: "A Kit Master",                   desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Master",             shortDescription: "short desc", stockQuantity: 0001, typeID: 2, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 400004, key: "KitComponent1",            name: "A Kit Component 1",              desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Component-1",        shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 400005, key: "KitComponent2",            name: "A Kit Component 2",              desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Component-2",        shortDescription: "short desc", stockQuantity: 0001, typeID: 6, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    // Membership Kit
                    await CreateADummyProductAsync(id: 500000, key: "KitMasterMem",             name: "A Kit Master Membership",        desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Master-Mem",         shortDescription: "short desc", stockQuantity: 0001, typeID: 2, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 500001, key: "KitComponent1Mem",         name: "A Kit Component 1 Membership",   desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Component-1-Mem",    shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 500002, key: "KitComponent2Mem",         name: "A Kit Component 2 Membership",   desc: "desc", isTaxable: true, isUnlimitedStock: true,  isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 05.99000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Component-2-Mem",    shortDescription: "short desc", typeID: 6, unitOfMeasure: "EACH", weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    // Membership
                    await CreateADummyProductAsync(id: 600000, key: "BronzeMembership",         name: "A Bronze Membership",            desc: "desc", isTaxable: true, isUnlimitedStock: true,  isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 05.99000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Bronze-Membership",      shortDescription: "short desc", typeID: 6, unitOfMeasure: "EACH", weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 600001, key: "GoldMembership",           name: "A Gold Membership",              desc: "desc", isTaxable: true, isUnlimitedStock: true,  isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 09.99000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Gold-Membership",        shortDescription: "short desc", typeID: 6, unitOfMeasure: "EACH", weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 600002, key: "GoldMembershipNoAds",      name: "A Gold Membership without Ads",  desc: "desc", isTaxable: true, isUnlimitedStock: true,  isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 09.99000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Gold-Membership-No-Ads", shortDescription: "short desc", typeID: 6, unitOfMeasure: "EACH", weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    // Membership Kit 2 (No unique ad thresholds)
                    await CreateADummyProductAsync(id: 700000, key: "KitMasterMem2",            name: "A Kit Master Membership 2",      desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Master-Mem-2",       shortDescription: "short desc", stockQuantity: 0001, typeID: 2, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 700001, key: "KitComponent1Mem2",        name: "A Kit Component 1 Membership 2", desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 00.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Component-1-Mem-2",  shortDescription: "short desc", stockQuantity: 0001, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 700002, key: "KitComponent2Mem2",        name: "A Kit Component 2 Membership 2", desc: "desc", isTaxable: true, isUnlimitedStock: true,  isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 05.99000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "A-Kit-Component-2-Mem-2",  shortDescription: "short desc", typeID: 6, unitOfMeasure: "EACH", weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    // Minimum and Maximum Purchase Amounts
                    await CreateADummyProductAsync(id: 000970, key: "ZORK-BLUE-A",              name: "BLUE ZORK A",                    desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 1000m, manufacturerPartNumber: "mnf", maximumPurchaseQuantity: 0002, packageID: 0001, priceBase: 00.19000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "BLUE-ZORK-A",              shortDescription: "short desc", stockQuantity: 1000, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 000971, key: "ZORK-BLUE-B",              name: "BLUE ZORK B",                    desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 1000m, manufacturerPartNumber: "mnf", maximumPurchaseQuantityIfPastPurchased: 0005, packageID: 0001, priceBase: 00.19000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "BLUE-ZORK-B",              shortDescription: "short desc", stockQuantity: 1000, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 000972, key: "ZORK-BLUE-C",              name: "BLUE ZORK C",                    desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 1000m, manufacturerPartNumber: "mnf", maximumPurchaseQuantity: 0002, maximumPurchaseQuantityIfPastPurchased: 0000, packageID: 0001, priceBase: 00.19000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "BLUE-ZORK-C",              shortDescription: "short desc", stockQuantity: 1000, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 000973, key: "ZORK-BLUE-D",              name: "BLUE ZORK D",                    desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 1000m, manufacturerPartNumber: "mnf", minimumPurchaseQuantity: 0002, packageID: 0001, priceBase: 00.19000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "BLUE-ZORK-D",              shortDescription: "short desc", stockQuantity: 1000, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m,
                        jsonAttributes: new SerializableAttributesDictionary { ["Purchasable By Individuals"] = new SerializableAttributeObject { Key = "Purchasable By Individuals", Value = "false" } }.SerializeAttributesDictionary()).ConfigureAwait(false),
                    await CreateADummyProductAsync(
                            id: 000974,
                            key: "ZORK-BLUE-E",
                            name: "BLUE ZORK E",
                            desc: "desc",
                            isTaxable: true,
                            isVisible: true,
                            kitBaseQuantityPriceMultiplier: 1000m,
                            manufacturerPartNumber: "mnf",
                            packageID: 0001,
                            priceBase: 00.19000m,
                            priceMsrp: 000m,
                            priceReduction: 000m,
                            priceSale: 000m,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "meta data",
                            seoPageTitle: "page title",
                            seoUrl: "BLUE-ZORK-E",
                            shortDescription: "short desc",
                            stockQuantity: 1000,
                            typeID: 1,
                            statusID: 1,
                            unitOfMeasure: "EACH",
                            weight: 001m,
                            weightUnitOfMeasure: "lbs",
                            depthUnitOfMeasure: "in",
                            widthUnitOfMeasure: "in",
                            heightUnitOfMeasure: "in",
                            flatShippingCharge: 0,
                            restockingFeeAmount: 0.00m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0.00m,
                            mustPurchaseInMultiplesOfAmount: 24m,
                            mustPurchaseInMultiplesOfAmountWarningMessage: /* language=HTML */ "<p>Your cart does not meet the purchasing requirement for the Product <b>{{ownerName}}</b> which must be purchased in multiples of <b>{{requiredAmount}}</b>. You need an additional <b>{{missingAmount}} units</b>. Please add more items to your shopping cart to ensure your total quantity meets the requirement.</p>\r\n{{overrideFeeWarningMessage}}",
                            mustPurchaseInMultiplesOfAmountOverrideFee: 5m,
                            mustPurchaseInMultiplesOfAmountOverrideFeeIsPercent: true,
                            mustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage: /* language=HTML */ "<p>An Override Fee of <b>{{overrideFee}}</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['MustPurchaseProductInMultiplesOfAmountIgnoredAcceptedFor-Product-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            mustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage: /* language=HTML */ "<p>The Override Fee of <b>{{overrideFee}}</b> will be added by {{ownerName}} at the time of Invoicing. Please note that the fee is subject to change without notice.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['MustPurchaseProductInMultiplesOfAmountIgnoredAcceptedFor-Product-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>")
                        .ConfigureAwait(false),
                    await CreateADummyProductAsync(id: 000975, key: "ZORK-BLUE-F",              name: "BLUE ZORK F",                    desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 1000m, manufacturerPartNumber: "mnf", minimumPurchaseQuantity: 0002, minimumPurchaseQuantityIfPastPurchased: 0005, packageID: 0001, priceBase: 00.19000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "BLUE-ZORK-F",              shortDescription: "short desc", stockQuantity: 1000, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                    // Other
                    await CreateADummyProductAsync(id: 000100, key: "PRODUCT-A",                name: "Product A",                      desc: "desc", isTaxable: true, isVisible: true, kitBaseQuantityPriceMultiplier: 0001m, manufacturerPartNumber: "mnf", priceBase: 12.00000m, priceMsrp: 000m, priceReduction: 000m, priceSale: 000m, seoDescription: "meta desc", seoKeywords: "meta kw", seoMetaData: "meta data", seoPageTitle: "page title", seoUrl: "Product-A",                shortDescription: "short desc", stockQuantity: 0000, unitOfMeasure: "EACH", weight: 001m, weightUnitOfMeasure: "lbs", depthUnitOfMeasure: "in", widthUnitOfMeasure: "in", heightUnitOfMeasure: "in", flatShippingCharge: 0, restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m, packageID: 1).ConfigureAwait(false),
                };
                RawProducts.First(p => p.Object.ID == 0969).Object.Active = false;
                await InitializeMockSetProductsAsync(mockContext, RawProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Associations
            if (DoAll || DoProducts || DoProductAssociationTable)
            {
                var index = 0;
                RawProductAssociations = new()
                {
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Variant 1",   masterID: 400000, slaveID: 400001, quantity: 1, sortOrder: 0003, typeID: 3, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Variant 2",   masterID: 400000, slaveID: 400002, quantity: 5, sortOrder: 0003, typeID: 3, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Kit 1",       masterID: 400003, slaveID: 400004, quantity: 1, sortOrder: 0002, typeID: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Kit 2",       masterID: 400003, slaveID: 400005, quantity: 5, sortOrder: 0002, typeID: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Variant 3",   masterID: 001151, slaveID: 001152, quantity: 1, typeID: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Kit 3",       masterID: 001151, slaveID: 001153, quantity: 1, typeID: 3, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Kit 1 Mem",   masterID: 500000, slaveID: 500001, quantity: 1, sortOrder: 0003, typeID: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Kit 2 Mem",   masterID: 500000, slaveID: 500002, quantity: 1, sortOrder: 0003, typeID: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Kit 1 Mem 2", masterID: 700000, slaveID: 700001, quantity: 1, sortOrder: 0003, typeID: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                    await CreateADummyProductAssociationAsync(id: ++index, key: "Kit 2 Mem 2", masterID: 700000, slaveID: 700002, quantity: 1, sortOrder: 0003, typeID: 2, unitOfMeasure: "EA").ConfigureAwait(false),
                };
                await InitializeMockSetProductAssociationsAsync(mockContext, RawProductAssociations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Association Types
            if (DoAll || DoProducts || DoProductAssociationTypeTable)
            {
                var index = 0;
                RawProductAssociationTypes = new()
                {
                    await CreateADummyProductAssociationTypeAsync(id: ++index, key: "RELATED-PRODUCT", name: "Related Product", desc: "desc", sortOrder: 0, displayName: "Related Product").ConfigureAwait(false),
                    await CreateADummyProductAssociationTypeAsync(id: ++index, key: "KIT-COMPONENT", name: "Kit Component", desc: "desc", sortOrder: 1, displayName: "Kit Component").ConfigureAwait(false),
                    await CreateADummyProductAssociationTypeAsync(id: ++index, key: "VARIANT-OF-MASTER", name: "Variant of Master", desc: "desc", sortOrder: 2, displayName: "Variant of Master").ConfigureAwait(false),
                };
                await InitializeMockSetProductAssociationTypesAsync(mockContext, RawProductAssociationTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Categories
            if (DoAll || DoProducts || DoProductCategoryTable)
            {
                var index = 0;
                RawProductCategories = new()
                {
                    await CreateADummyProductCategoryAsync(id: ++index, key: "PRODUCT-CATEGORY-1", masterID: 1151, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyProductCategoryAsync(id: ++index, key: "PRODUCT-CATEGORY-2", masterID: 1152, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyProductCategoryAsync(id: ++index, key: "PRODUCT-CATEGORY-3", masterID: 1152, slaveID: 6).ConfigureAwait(false),
                    await CreateADummyProductCategoryAsync(id: ++index, key: "PRODUCT-CATEGORY-4", masterID: 0969, slaveID: 3).ConfigureAwait(false),
                };
                await InitializeMockSetProductCategoriesAsync(mockContext, RawProductCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Downloads
            if (DoAll || DoProducts || DoProductDownloadTable)
            {
                RawProductDownloads = new()
                {
                    await CreateADummyProductDownloadAsync(id: 1, key: "PRODUCT-DOWNLOAD-1", name: "Product Download 1").ConfigureAwait(false),
                };
                await InitializeMockSetProductDownloadsAsync(mockContext, RawProductDownloads).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Download Types
            if (DoAll || DoProducts || DoProductDownloadTypeTable)
            {
                var index = 0;
                RawProductDownloadTypes = new()
                {
                    await CreateADummyProductDownloadTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetProductDownloadTypesAsync(mockContext, RawProductDownloadTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Files
            if (DoAll || DoProducts || DoProductFileTable)
            {
                var index = 0;
                RawProductFiles = new()
                {
                    await CreateADummyProductFileAsync(id: ++index, key: "File-" + index, name: "File " + index, desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetProductFilesAsync(mockContext, RawProductFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Images
            if (DoAll || DoProducts || DoProductImageTable)
            {
                RawProductImages = new()
                {
                    await CreateADummyProductImageAsync(id: 1, key: "PRODUCT-IMAGE-NEW-1", name: "Product Image New 1", desc: "desc", masterID: 1151, isPrimary: true, originalFileName: "product-image-1.jpg", thumbnailFileName: "product-image-1-thumb.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetProductImagesAsync(mockContext, RawProductImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Image Types
            if (DoAll || DoProducts || DoProductImageTypeTable)
            {
                var index = 0;
                RawProductImageTypes = new()
                {
                    await CreateADummyProductImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetProductImageTypesAsync(mockContext, RawProductImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Inventory Location Sections
            if (DoAll || DoProducts || DoProductInventoryLocationSectionTable)
            {
                var index = 0;
                RawProductInventoryLocationSections = new()
                {
                    await CreateADummyProductInventoryLocationSectionAsync(id: ++index, key: "Shelf 1-1B|0200-ANGRF-OPERA-VSI-IA",  masterID: 1152, quantity: 0001).ConfigureAwait(false),
                    await CreateADummyProductInventoryLocationSectionAsync(id: ++index, key: "Overhead 1|0200-ANGRF-OPERA-VSI-IA",  masterID: 1152, slaveID: 2, quantity: 0100).ConfigureAwait(false),
                    await CreateADummyProductInventoryLocationSectionAsync(id: ++index, key: "Back Dock 1|0200-ANGRF-OPERA-VSI-IA", masterID: 1152, slaveID: 3, quantity: 6134).ConfigureAwait(false),
                    await CreateADummyProductInventoryLocationSectionAsync(id: ++index, key: "Shelf 1-1B|BLUE ZORK E",              masterID: 0974, quantity: 1000).ConfigureAwait(false),
                };
                await InitializeMockSetProductInventoryLocationSectionsAsync(mockContext, RawProductInventoryLocationSections).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Membership Levels
            if (DoAll || DoProducts || DoProductMembershipLevelTable)
            {
                RawProductMembershipLevels = new()
                {
                    await CreateADummyProductMembershipLevelAsync(id: 1, key: "PRODUCT-MEMBERSHIP-LEVEL-1", masterID: 000100).ConfigureAwait(false),
                    await CreateADummyProductMembershipLevelAsync(id: 2, key: "PRODUCT-MEMBERSHIP-LEVEL-2", masterID: 500002, membershipRepeatTypeID: 4).ConfigureAwait(false),
                    await CreateADummyProductMembershipLevelAsync(id: 3, key: "PRODUCT-MEMBERSHIP-LEVEL-3", masterID: 600000, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyProductMembershipLevelAsync(id: 4, key: "PRODUCT-MEMBERSHIP-LEVEL-4", masterID: 600001, slaveID: 3).ConfigureAwait(false),
                    await CreateADummyProductMembershipLevelAsync(id: 5, key: "PRODUCT-MEMBERSHIP-LEVEL-5", masterID: 600002, slaveID: 4).ConfigureAwait(false),
                    await CreateADummyProductMembershipLevelAsync(id: 6, key: "PRODUCT-MEMBERSHIP-LEVEL-6", masterID: 700002, slaveID: 5, membershipRepeatTypeID: 4).ConfigureAwait(false),
                };
                await InitializeMockSetProductMembershipLevelsAsync(mockContext, RawProductMembershipLevels).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Price Points
            if (DoAll || DoProducts || DoProductPricePointTable)
            {
                RawProductPricePoints = new()
                {
                    await CreateADummyProductPricePointAsync(id: 00001, key: "PRODUCT-A|WEB|USD|cs|1|2147483647|||store-1",                  minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.1078m, price:   null, masterID: 0000100, slaveID: 001, priceRoundingID:   null, unitOfMeasure: "cs", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    // 0200-ANGBT-BFUT-IA
                    await CreateADummyProductPricePointAsync(id: 11186, key: "0200-ANGBT-BFUT-IA|FT|USD|CS|1|2147483647|||store-1",          minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.1078m, price:   null, masterID: 0001151, slaveID: 002, priceRoundingID: 007204, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11187, key: "0200-ANGBT-BFUT-IA|HT|USD|CS|1|2147483647|||store-1",          minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.1380m, price:   null, masterID: 0001151, slaveID: 003, priceRoundingID: 007205, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11188, key: "0200-ANGBT-BFUT-IA|LTL|USD|CS|1|119|||store-1",                minQuantity: 0001, maxQuantity: 0000000119, percentDiscount: 0.2850m, price:   null, masterID: 0001151, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11189, key: "0200-ANGBT-BFUT-IA|LTL|USD|CS|120|240|||store-1",              minQuantity: 0120, maxQuantity: 0000000239, percentDiscount: 0.2597m, price:   null, masterID: 0001151, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11190, key: "0200-ANGBT-BFUT-IA|LTL|USD|CS|240|1439|||store-1",             minQuantity: 0240, maxQuantity: 0000001439, percentDiscount: 0.1775m, price:   null, masterID: 0001151, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11191, key: "0200-ANGBT-BFUT-IA|LTL|USD|CS|1440|2879|||store-1",            minQuantity: 1440, maxQuantity: 0000002879, percentDiscount: 0.1380m, price:   null, masterID: 0001151, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11192, key: "0200-ANGBT-BFUT-IA|LTL|USD|CS|2880|2147483647|||store-1",      minQuantity: 2880, maxQuantity: 2147483647, percentDiscount: 0.1078m, price:   null, masterID: 0001151, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11193, key: "0200-ANGBT-BFUT-IA|RETAIL|USD|CS|1|2147483647|||store-1",      minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.3272m, price:   null, masterID: 0001151, slaveID: 005, priceRoundingID: 007207, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11194, key: "0200-ANGBT-BFUT-IA|WEB|USD|CS|1|2147483647|||store-1",         minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.3272m, price:   null, masterID: 0001151, slaveID: 001, priceRoundingID: 007208, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    // 0200-ANGRF-OPERA-VSI-IA
                    await CreateADummyProductPricePointAsync(id: 11195, key: "0200-ANGRF-OPERA-VSI-IA|FT|USD|CS|1|2147483647|||store-1",     minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.1078m, price:   null, masterID: 0001152, slaveID: 002, priceRoundingID: 007204, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11196, key: "0200-ANGRF-OPERA-VSI-IA|HT|USD|CS|1|2147483647|||store-1",     minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.1380m, price:   null, masterID: 0001152, slaveID: 003, priceRoundingID: 007205, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11197, key: "0200-ANGRF-OPERA-VSI-IA|LTL|USD|CS|1|59|||store-1",            minQuantity: 0001, maxQuantity: 0000000059, percentDiscount: 0.3272m, price:   null, masterID: 0001152, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11198, key: "0200-ANGRF-OPERA-VSI-IA|LTL|USD|CS|60|1191|||store-1",         minQuantity: 0060, maxQuantity: 0000000119, percentDiscount: 0.2597m, price:   null, masterID: 0001152, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11199, key: "0200-ANGRF-OPERA-VSI-IA|LTL|USD|CS|120|719|||store-1",         minQuantity: 0120, maxQuantity: 0000000719, percentDiscount: 0.1775m, price:   null, masterID: 0001152, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11200, key: "0200-ANGRF-OPERA-VSI-IA|LTL|USD|CS|720|1439|||store-1",        minQuantity: 0720, maxQuantity: 0000001439, percentDiscount: 0.1380m, price:   null, masterID: 0001152, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11201, key: "0200-ANGRF-OPERA-VSI-IA|LTL|USD|CS|1440|2147483647|||store-1", minQuantity: 1440, maxQuantity: 2147483647, percentDiscount: 0.1078m, price:   null, masterID: 0001152, slaveID: 004, priceRoundingID: 007206, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 11202, key: "0200-ANGRF-OPERA-VSI-IA|WEB|USD|CS|1|2147483647|||store-1",    minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.3500m, price:   null, masterID: 0001152, slaveID: 001, priceRoundingID: 007208, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    // ZORK-BLACK
                    await CreateADummyProductPricePointAsync(id: 10003, key: "ZORK-BLACK|WEB|USD|CS|1|2|||store-1",                          minQuantity: 0001, maxQuantity: 0000000002, percentDiscount: 0.4062m, price:   null, masterID: 0000969, slaveID: 001, priceRoundingID: 006442, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // WEB
                    await CreateADummyProductPricePointAsync(id: 10004, key: "ZORK-BLACK|WEB|USD|CS|3|5|||store-1",                          minQuantity: 0003, maxQuantity: 0000000005, percentDiscount: 0.3333m, price:   null, masterID: 0000969, slaveID: 001, priceRoundingID: 006442, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // WEB
                    await CreateADummyProductPricePointAsync(id: 10005, key: "ZORK-BLACK|WEB|USD|CS|6|19|||store-1",                         minQuantity: 0006, maxQuantity: 0000000019, percentDiscount: 0.2664m, price:   null, masterID: 0000969, slaveID: 001, priceRoundingID: 006442, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // WEB
                    await CreateADummyProductPricePointAsync(id: 10006, key: "ZORK-BLACK|WEB|USD|CS|20|49|||store-1",                        minQuantity: 0020, maxQuantity: 0000000049, percentDiscount: 0.2400m, price:   null, masterID: 0000969, slaveID: 001, priceRoundingID: 006442, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // WEB
                    await CreateADummyProductPricePointAsync(id: 10007, key: "ZORK-BLACK|WEB|USD|CS|50|2147483647|||store-1",                minQuantity: 0050, maxQuantity: 2147483647, percentDiscount: 0.2240m, price:   null, masterID: 0000969, slaveID: 001, priceRoundingID: 006442, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // WEB
                    await CreateADummyProductPricePointAsync(id: 11419, key: "ZORK-BLACK|LTL|USD|CS|1|2|||store-1",                          minQuantity: 0001, maxQuantity: 0000000002, percentDiscount: 0.4062m, price:   null, masterID: 0000969, slaveID: 004, priceRoundingID: 006443, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // LTL
                    await CreateADummyProductPricePointAsync(id: 11420, key: "ZORK-BLACK|LTL|USD|CS|3|5|||store-1",                          minQuantity: 0003, maxQuantity: 0000000005, percentDiscount: 0.3333m, price:   null, masterID: 0000969, slaveID: 004, priceRoundingID: 006443, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // LTL
                    await CreateADummyProductPricePointAsync(id: 11421, key: "ZORK-BLACK|LTL|USD|CS|6|19|||store-1",                         minQuantity: 0006, maxQuantity: 0000000019, percentDiscount: 0.2664m, price:   null, masterID: 0000969, slaveID: 004, priceRoundingID: 006443, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // LTL
                    await CreateADummyProductPricePointAsync(id: 11422, key: "ZORK-BLACK|LTL|USD|CS|20|49|||store-1",                        minQuantity: 0020, maxQuantity: 0000000049, percentDiscount: 0.2400m, price:   null, masterID: 0000969, slaveID: 004, priceRoundingID: 006443, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // LTL
                    await CreateADummyProductPricePointAsync(id: 11423, key: "ZORK-BLACK|LTL|USD|CS|50|2147483647|||store-1",                minQuantity: 0050, maxQuantity: 2147483647, percentDiscount: 0.2240m, price:   null, masterID: 0000969, slaveID: 004, priceRoundingID: 006443, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // LTL
                    await CreateADummyProductPricePointAsync(id: 10009, key: "ZORK-BLACK|ZORK1-2M|USD|CS|1|2147483647|||store-1",            minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.4062m, price:   null, masterID: 0000969, slaveID: 054, priceRoundingID: 006444, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // ZORK1-2M
                    await CreateADummyProductPricePointAsync(id: 10010, key: "ZORK-BLACK|ZORK20-49M|USD|CS|1|2147483647|||store-1",          minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.2400m, price:   null, masterID: 0000969, slaveID: 055, priceRoundingID: 006445, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // ZORK3-5M
                    await CreateADummyProductPricePointAsync(id: 10011, key: "ZORK-BLACK|ZORK3-5M|USD|CS|1|2147483647|||store-1",            minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.3333m, price:   null, masterID: 0000969, slaveID: 056, priceRoundingID: 006446, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // ZORK6-19M
                    await CreateADummyProductPricePointAsync(id: 10012, key: "ZORK-BLACK|ZORK50M+|USD|CS|1|2147483647|||store-1",            minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.2240m, price:   null, masterID: 0000969, slaveID: 057, priceRoundingID: 006447, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // ZORK20-49M
                    await CreateADummyProductPricePointAsync(id: 10013, key: "ZORK-BLACK|ZORK6-19M|USD|CS|1|2147483647|||store-1",           minQuantity: 0001, maxQuantity: 2147483647, percentDiscount: 0.2664m, price:   null, masterID: 0000969, slaveID: 058, priceRoundingID: 006448, unitOfMeasure: "CS", currencyID: 0001, storeID: 1).ConfigureAwait(false), // ZORK50M+
                    // Knee Pro-Tec™ Patellar Tendon Strap
                    await CreateADummyProductPricePointAsync(id: 30001, key: "PRODUCT-A|WEB|USD|EA|1|2147483647|||store-1",                  minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 19.95m, masterID: 1000001, slaveID: 001, priceRoundingID: 500001, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 30002, key: "PRODUCT-A|WHOLESALE|USD|EA|1|2147483647|||store-1",            minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 11.70m, masterID: 1000001, slaveID: 500, priceRoundingID: 500002, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    // Back of Knee Compression Wrap
                    await CreateADummyProductPricePointAsync(id: 30003, key: "PRODUCT-A|WEB|USD|EA|1|2147483647|||store-1",                  minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 19.95m, masterID: 1000002, slaveID: 001, priceRoundingID: 500003, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 30004, key: "PRODUCT-A|WHOLESALE|USD|EA|1|2147483647|||store-1",            minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 11.05m, masterID: 1000002, slaveID: 500, priceRoundingID: 500004, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    // Short Sleeve™ Knee Support
                    await CreateADummyProductPricePointAsync(id: 30005, key: "PRODUCT-A|WEB|USD|EA|1|2147483647|||store-1",                  minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 19.95m, masterID: 1000003, slaveID: 001, priceRoundingID: 500005, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 30006, key: "PRODUCT-A|WHOLESALE|USD|EA|1|2147483647|||store-1",            minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 12.35m, masterID: 1000003, slaveID: 500, priceRoundingID: 500006, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    // 3D Flat Premium Knee Support
                    await CreateADummyProductPricePointAsync(id: 30007, key: "PRODUCT-A|WEB|USD|EA|1|2147483647|||store-1",                  minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 19.95m, masterID: 1000004, slaveID: 001, priceRoundingID: 500007, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                    await CreateADummyProductPricePointAsync(id: 30008, key: "PRODUCT-A|WHOLESALE|USD|EA|1|2147483647|||store-1",            minQuantity: 0001, maxQuantity: 2147483647, percentDiscount:    null, price: 13.00m, masterID: 1000004, slaveID: 500, priceRoundingID: 500008, unitOfMeasure: "EA", currencyID: 0001, storeID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetProductPricePointsAsync(mockContext, RawProductPricePoints).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Restrictions
            if (DoAll || DoProducts || DoProductRestrictionTable)
            {
                var index = 0;
                RawProductRestrictions = new()
                {
                    await CreateADummyProductRestrictionAsync(id: ++index, key: "PRODUCT-RESTRICTION-1", productID: 1151, canPurchaseDomestically: true, canPurchaseInternationally: true, canPurchaseIntraRegion: true, canShipDomestically: true, canShipInternationally: true, canShipIntraRegion: true).ConfigureAwait(false),
                };
                await InitializeMockSetProductRestrictionsAsync(mockContext, RawProductRestrictions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Ship Carrier Methods
            if (DoAll || DoProducts || DoProductShipCarrierMethodTable)
            {
                RawProductShipCarrierMethods = new()
                {
                    await CreateADummyProductShipCarrierMethodAsync(id: 1, key: "PRODUCT-SHIP-CARRIER-METHOD-1").ConfigureAwait(false),
                };
                await InitializeMockSetProductShipCarrierMethodsAsync(mockContext, RawProductShipCarrierMethods).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Statuses
            if (DoAll || DoProducts || DoProductStatusTable)
            {
                RawProductStatuses = new()
                {
                    await CreateADummyProductStatusAsync(01, sortOrder: 0, key: "NORMAL", name: "Normal", displayName: "Normal", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetProductStatusesAsync(mockContext, RawProductStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Subscription Types
            if (DoAll || DoProducts || DoProductSubscriptionTypeTable)
            {
                RawProductSubscriptionTypes = new()
                {
                    await CreateADummyProductSubscriptionTypeAsync(id: 1, key: "PRODUCT-SUBSCRIPTION-TYPE-1", masterID: 000100).ConfigureAwait(false),
                    await CreateADummyProductSubscriptionTypeAsync(id: 2, key: "PRODUCT-SUBSCRIPTION-TYPE-2", masterID: 500002).ConfigureAwait(false),
                    await CreateADummyProductSubscriptionTypeAsync(id: 3, key: "PRODUCT-SUBSCRIPTION-TYPE-3", masterID: 600000, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyProductSubscriptionTypeAsync(id: 4, key: "PRODUCT-SUBSCRIPTION-TYPE-4", masterID: 600001, slaveID: 4).ConfigureAwait(false),
                };
                await InitializeMockSetProductSubscriptionTypesAsync(mockContext, RawProductSubscriptionTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Product Types
            if (DoAll || DoProducts || DoProductTypeTable)
            {
                RawProductTypes = new()
                {
                    await CreateADummyProductTypeAsync(id: 01, sortOrder: 0, key: "GENERAL",        name: "General",        displayName: "General",        desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 02, sortOrder: 1, key: "KIT",            name: "Kit",            displayName: "Kit",            desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 03, sortOrder: 2, key: "VARIANT-MASTER", name: "Variant Master", displayName: "Variant Master", desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 04, sortOrder: 2, key: "VARIANT",        name: "Variant",        displayName: "Variant",        desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 05, sortOrder: 2, key: "VARIANT-KIT",    name: "Variant Kit",    displayName: "Variant Kit",    desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 06, sortOrder: 3, key: "MEMBERSHIP",     name: "Membership",     displayName: "Membership",     desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 07, sortOrder: 3, key: "SUBSCRIPTION",   name: "Subscription",   displayName: "Subscription",   desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 08, sortOrder: 4, key: "AD-ZONE-ACCESS", name: "Ad Zone Access", displayName: "Ad Zone Access", desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 09, sortOrder: 5, key: "FREE-SAMPLE",    name: "Free Sample",    displayName: "Free Sample",    desc: "desc").ConfigureAwait(false),
                    await CreateADummyProductTypeAsync(id: 10, sortOrder: 6, key: "PAID-SAMPLE",    name: "Paid Sample",    displayName: "Paid Sample",    desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetProductTypesAsync(mockContext, RawProductTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
