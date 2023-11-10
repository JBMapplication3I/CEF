// <copyright file="DoMockingSetupForContextRunnerManufacturers.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner manufacturers class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerManufacturersAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Manufacturers
            if (DoAll || DoManufacturers || DoManufacturerTable)
            {
                var index = 0;
                RawManufacturers = new()
                {
                    await CreateADummyManufacturerAsync(
                            id: ++index,
                            key: "MNF-" + index,
                            name: "Manufacturer 1",
                            desc: "desc",
                            contactID: 1,
                            minimumOrderDollarAmount: 2000m,
                            minimumOrderDollarAmountAfter: 0m,
                            // minimumOrderDollarAmountBufferCategoryID: 1,
                            // minimumOrderDollarAmountBufferProductID: 1151,
                            minimumOrderDollarAmountWarningMessage: /* language=HTML */ "<p>Custom Warning Message for <b>{{ownerName}}</b>, missing <b>{{missingAmount}}</b> worth of items. They suggest using more items from the <a href=\"/{{bufferCategorySeoUrl}}\">{{bufferCategoryName}}</a> category or this specific item: \"<a href=\"/Product/{{bufferItemSeoUrl}}\">{{bufferItemName}}</a>\".</p>\r\n{{overrideFeeWarningMessage}}",
                            minimumOrderDollarAmountOverrideFee: 10m,
                            minimumOrderDollarAmountOverrideFeeWarningMessage: /* language=HTML */ "<p>An Override Fee of <b>{{overrideFee}}</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumOrderDollarAmountOverrideFeeAcceptedMessage: /* language=HTML */ "<p>The Override Fee of <b>{{overrideFee}}</b> will be added by {{ownerName}} at the time of Invoicing. Please note that the fee is subject to change without notice.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumOrderQuantityAmount: 20m,
                            minimumOrderQuantityAmountAfter: 0m,
                            // minimumOrderQuantityAmountBufferCategoryID: 1,
                            // minimumOrderQuantityAmountBufferProductID: 1151,
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
                            minimumForFreeShippingQuantityAmountIgnoredAcceptedMessage: /* language=HTML */ "<p>The Notice for Free Shipping Minimums for {{ownerName}} has been ignored.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>",
                            jsonAttributes: new SerializableAttributesDictionary
                            {
                                ["Color"] = new() { Key = "Color", Value = "Red" },
                            }.SerializeAttributesDictionary())
                        .ConfigureAwait(false),
                    await CreateADummyManufacturerAsync(
                            id: ++index,
                            key: "MNF-" + index,
                            name: "Manufacturer 2",
                            desc: "desc",
                            contactID: 1,
                            minimumOrderDollarAmount: 2000m,
                            minimumOrderDollarAmountAfter: 0m,
                            // minimumOrderDollarAmountBufferCategoryID: 1,
                            // minimumOrderDollarAmountBufferProductID: 1151,
                            minimumOrderDollarAmountWarningMessage: /* language=HTML */ "<p>Custom Warning Message for <b>{{ownerName}}</b>, missing <b>{{missingAmount}}</b> worth of items. They suggest using more items from the <a href=\"/{{bufferCategorySeoUrl}}\">{{bufferCategoryName}}</a> category or this specific item: \"<a href=\"/Product/{{bufferItemSeoUrl}}\">{{bufferItemName}}</a>\".</p>\r\n{{overrideFeeWarningMessage}}",
                            minimumOrderDollarAmountOverrideFee: 10m,
                            minimumOrderDollarAmountOverrideFeeWarningMessage: /* language=HTML */ "<p>An Override Fee of <b>{{overrideFee}}</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumOrderDollarAmountOverrideFeeAcceptedMessage: /* language=HTML */ "<p>The Override Fee of <b>{{overrideFee}}</b> will be added by {{ownerName}} at the time of Invoicing. Please note that the fee is subject to change without notice.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of {{overrideFee}}</label></div>",
                            minimumOrderQuantityAmount: 20m,
                            minimumOrderQuantityAmountAfter: 0m,
                            // minimumOrderQuantityAmountBufferCategoryID: 1,
                            // minimumOrderQuantityAmountBufferProductID: 1151,
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
                            minimumForFreeShippingQuantityAmountIgnoredAcceptedMessage: /* language=HTML */ "<p>The Notice for Free Shipping Minimums for {{ownerName}} has been ignored.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-{{ownerID}}'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>",
                            jsonAttributes: new SerializableAttributesDictionary
                            {
                                ["Color"] = new() { Key = "Color", Value = "Green" },
                            }.SerializeAttributesDictionary())
                        .ConfigureAwait(false),
                };
                await InitializeMockSetManufacturersAsync(mockContext, RawManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Manufacturer Images
            if (DoAll || DoManufacturers || DoManufacturerImageTable)
            {
                RawManufacturerImages = new()
                {
                    await CreateADummyManufacturerImageAsync(id: 1, key: "MANUFACTURER-IMAGE-1", name: "Manufacturer Image 1", desc: "desc", isPrimary: true, originalFileName: "manufacturer-image.jpg", originalBytes: Array.Empty<byte>(), thumbnailFileName: "manufacturer-image-thumb.jpg", thumbnailBytes: Array.Empty<byte>()).ConfigureAwait(false),
                };
                await InitializeMockSetManufacturerImagesAsync(mockContext, RawManufacturerImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Manufacturer Image Types
            if (DoAll || DoManufacturers || DoManufacturerImageTypeTable)
            {
                RawManufacturerImageTypes = new()
                {
                    await CreateADummyManufacturerImageTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetManufacturerImageTypesAsync(mockContext, RawManufacturerImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Manufacturer Products
            if (DoAll || DoManufacturers || DoManufacturerProductTable)
            {
                var index = 0;
                RawManufacturerProducts = new()
                {
                    await CreateADummyManufacturerProductAsync(id: ++index, key: "MNF-1|PROD-1151", slaveID: 1151).ConfigureAwait(false),
                    await CreateADummyManufacturerProductAsync(id: ++index, key: "MNF-1|PROD-1152", slaveID: 1152).ConfigureAwait(false),
                    await CreateADummyManufacturerProductAsync(id: ++index, key: "MNF-2|PROD-1151", slaveID: 1151).ConfigureAwait(false),
                    await CreateADummyManufacturerProductAsync(id: ++index, key: "MNF-2|PROD-1152", slaveID: 1152).ConfigureAwait(false),
                };
                await InitializeMockSetManufacturerProductsAsync(mockContext, RawManufacturerProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Manufacturer Types
            if (DoAll || DoManufacturers || DoManufacturerTypeTable)
            {
                RawManufacturerTypes = new()
                {
                    await CreateADummyManufacturerTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetManufacturerTypesAsync(mockContext, RawManufacturerTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
