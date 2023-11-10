// <copyright file="DoMockingSetupForContextRunnerCategories.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner categories class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerCategoriesAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            if (DoAll || DoCategories || DoCategoryTable)
            {
                RawCategories = new()
                {
                    await CreateADummyCategoryAsync(
                            id: 01,
                            key: "GLASS",
                            name: "Glass",
                            desc: "desc",
                            displayName: "Glass",
                            footerContent: "Footer Content",
                            headerContent: "Header Content",
                            includeInMenu: true,
                            isVisible: true,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "meta data",
                            seoPageTitle: "page title",
                            seoUrl: "Glass",
                            sidebarContent: "Sidebar Content",
                            sortOrder: 1,
                            typeID: 1,
                            restockingFeeAmount: 0.00m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0.00m,
                            jsonAttributes: new SerializableAttributesDictionary
                            {
                                ["Color"] = new() { Key = "Color", Value = "Red" },
                            }.SerializeAttributesDictionary())
                        .ConfigureAwait(false),
                    await CreateADummyCategoryAsync(
                            id: 02,
                            key: "WINE",
                            name: "Wine",
                            desc: "desc",
                            displayName: "Wine",
                            footerContent: "Footer Content",
                            headerContent: "Header Content",
                            includeInMenu: true,
                            isVisible: true,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "meta data",
                            seoPageTitle: "page title",
                            seoUrl: "Wine",
                            sidebarContent: "Sidebar Content",
                            sortOrder: 2,
                            typeID: 1,
                            restockingFeeAmount: 0m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0m,
                            minimumOrderDollarAmount: 2000m,
                            minimumOrderDollarAmountAfter: 0m,
                            minimumOrderDollarAmountBufferCategoryID: 1,
                            minimumOrderDollarAmountBufferProductID: 1151,
                            minimumOrderDollarAmountWarningMessage: /* language=HTML */ "<p>Custom Warning Message for <b>{{ownerName}}</b>, missing <b>{{missingAmount}}</b> worth of items. They suggest using more items from the <a href=\"/{{bufferCategorySeoUrl}}\">{{bufferCategoryName}}</a> category or this specific item: \"<a href=\"/Product/{{bufferItemSeoUrl}}>{{bufferItemName}}</a>\".</p>\r\n{{overrideFeeWarningMessage}}",
                            minimumOrderDollarAmountOverrideFee: 10m,
                            minimumOrderDollarAmountOverrideFeeWarningMessage: /* language=HTML */ "<p>An Override Fee of <b>{{overrideFee}}</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumOrderDollarAmountOverrideFeeAcceptedMessage: /* language=HTML */ "<p>The Override Fee of <b>{{overrideFee}}</b> will be added by {{ownerName}} at the time of Invoicing. Please note that the fee is subject to change without notice.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumOrderQuantityAmount: 20m,
                            minimumOrderQuantityAmountAfter: 0m,
                            minimumOrderQuantityAmountBufferCategoryID: 1,
                            minimumOrderQuantityAmountBufferProductID: 1151,
                            minimumOrderQuantityAmountWarningMessage: /* language=HTML */ "<p>Custom Warning Message for <b>{{ownerName}}</b> missing <b>{{missingAmount}}</b> units of items. They suggest using more items from the <a ui-sref-plus uisrp-is-catalog=\"true\" uisrp-params=\"{ 'category': '{{bufferCategorySeoUrl}}' }\">{{bufferCategoryName}}</a> category or this specific item: <a ui-sref-plus uisrp-is-product=\"true\" uisrp-seo=\"{{bufferItemSeoUrl}}\">{{bufferItemName}}</a>.</p>",
                            minimumOrderQuantityAmountOverrideFee: 9m,
                            minimumOrderQuantityAmountOverrideFeeWarningMessage: /* language=HTML */ "<p>An Override Fee of <b>{{overrideFee}}</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['QuantityAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumOrderQuantityAmountOverrideFeeAcceptedMessage: /* language=HTML */ "<p>The Override Fee of <b>{{overrideFee}}</b> will be added by {{ownerName}} at the time of Invoicing. Please note that the fee is subject to change without notice.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['QuantityAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumForFreeShippingDollarAmount: 2500m,
                            minimumForFreeShippingDollarAmountAfter: 2500m,
                            minimumForFreeShippingDollarAmountWarningMessage: /* language=HTML */ "<p>Your cart does not meet the minimum free shipping requirement of <b>{{requiredAmount}}</b> for <b>{{ownerName}}</b>, you need an additional <b>{{missingAmount}}</b>. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingDollarAmountIgnoredAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>",
                            minimumForFreeShippingDollarAmountIgnoredAcceptedMessage: /* language=HTML */ "<p>The Notice for Free Shipping Minimums for {{ownerName}} has been ignored.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingDollarAmountIgnoredAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>",
                            minimumForFreeShippingQuantityAmount: 25m,
                            minimumForFreeShippingQuantityAmountAfter: 25m,
                            minimumForFreeShippingQuantityAmountWarningMessage: /* language=HTML */ "<p>Your cart does not meet the minimum free shipping requirement of <b>{{requiredAmount}}</b> units for <b>{{ownerName}}</b>, you need an additional <b>{{missingAmount}}</b> units. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>",
                            minimumForFreeShippingQuantityAmountIgnoredAcceptedMessage: /* language=HTML */ "<p>The Notice for Free Shipping Minimums for {{ownerName}} has been ignored.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>")
                        .ConfigureAwait(false),
                    await CreateADummyCategoryAsync(
                            id: 03,
                            key: "ATHLETICS",
                            name: "Athletics",
                            desc: "desc",
                            displayName: "Athletics",
                            footerContent: "Footer Content",
                            headerContent: "Header Content",
                            includeInMenu: true,
                            isVisible: true,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "meta data",
                            seoPageTitle: "page title",
                            seoUrl: "Athletics",
                            sidebarContent: "Sidebar Content",
                            sortOrder: 3,
                            typeID: 1,
                            restockingFeeAmount: 0.00m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0.00m)
                        .ConfigureAwait(false),
                    await CreateADummyCategoryAsync(
                            id: 04,
                            key: "NO MENU",
                            name: "No Menu",
                            desc: "desc",
                            displayName: "No Menu",
                            footerContent: "Footer Content",
                            headerContent: "Header Content",
                            isVisible: true,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "meta data",
                            seoPageTitle: "page title",
                            seoUrl: "No-Menu",
                            sidebarContent: "Sidebar Content",
                            sortOrder: 4,
                            typeID: 1,
                            restockingFeeAmount: 0.00m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0.00m)
                        .ConfigureAwait(false),
                    await CreateADummyCategoryAsync(
                            id: 05,
                            key: "AGENCY",
                            name: "Agency",
                            desc: "desc",
                            displayName: "Agency",
                            footerContent: "Footer Content",
                            headerContent: "Header Content",
                            includeInMenu: true,
                            isVisible: true,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "meta data",
                            seoPageTitle: "page title",
                            seoUrl: "Agency",
                            sidebarContent: "Sidebar Content",
                            sortOrder: 5,
                            typeID: 1,
                            restockingFeeAmount: 0.00m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0.00m)
                        .ConfigureAwait(false),
                    await CreateADummyCategoryAsync(
                            id: 06,
                            key: "DISCCH",
                            name: "Discounted Computer Hardware",
                            desc: "desc",
                            parentID: 0005,
                            displayName: "Discounted Computer Hardware",
                            footerContent: "Footer Content",
                            headerContent: "Header Content",
                            isVisible: true,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "meta data",
                            seoPageTitle: "page title",
                            seoUrl: "Technology-Hardware",
                            sidebarContent: "Sidebar Content",
                            sortOrder: 6,
                            typeID: 1,
                            restockingFeeAmount: 0.00m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0.00m)
                        .ConfigureAwait(false),
                    await CreateADummyCategoryAsync(
                            id: 07,
                            key: "GTKG",
                            name: "Gifts That Keep Giving",
                            desc: "desc",
                            parentID: 0005,
                            displayName: "Gifts that Keep Giving",
                            footerContent: "Footer Content",
                            headerContent: "Header Content",
                            isVisible: true,
                            seoDescription: "meta desc",
                            seoKeywords: "meta kw",
                            seoMetaData: "page title",
                            seoPageTitle: "meta data",
                            seoUrl: "Giving",
                            sidebarContent: "Sidebar Content",
                            sortOrder: 7,
                            typeID: 1,
                            restockingFeeAmount: 0.00m,
                            restockingFeeAmountCurrencyID: 1,
                            restockingFeePercent: 0.00m)
                        .ConfigureAwait(false),
                };
                await InitializeMockSetCategoriesAsync(mockContext, RawCategories).ConfigureAwait(false);
            }
            if (DoAll || DoCategories || DoCategoryFileTable)
            {
                var index = 0;
                RawCategoryFiles = new()
                {
                    await CreateADummyCategoryFileAsync(id: ++index, key: "File-" + index, name: "File " + index, desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetCategoryFilesAsync(mockContext, RawCategoryFiles).ConfigureAwait(false);
            }
            if (DoAll || DoCategories || DoCategoryImageTable)
            {
                var index = 0;
                RawCategoryImages = new()
                {
                    await CreateADummyCategoryImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetCategoryImagesAsync(mockContext, RawCategoryImages).ConfigureAwait(false);
            }
            if (DoAll || DoCategories || DoCategoryImageTypeTable)
            {
                var index = 0;
                RawCategoryImageTypes = new()
                {
                    await CreateADummyCategoryImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCategoryImageTypesAsync(mockContext, RawCategoryImageTypes).ConfigureAwait(false);
            }
            if (DoAll || DoCategories || DoCategoryTypeTable)
            {
                var index = 0;
                RawCategoryTypes = new()
                {
                    await CreateADummyCategoryTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCategoryTypesAsync(mockContext, RawCategoryTypes).ConfigureAwait(false);
            }
        }
    }
}
