// <copyright file="DoMockingSetupForContextRunnerVendors.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner vendors class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerVendorsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Vendors
            if (DoAll || DoVendors || DoVendorTable)
            {
                var index = 0;
                RawVendors = new()
                {
                    await CreateADummyVendorAsync(
                            id: ++index,
                            key: "Laars",
                            name: "Laars",
                            desc: "desc",
                            accountNumber: "acct#",
                            allowDropShip: true,
                            emailSubject: "SUBJECT",
                            notes1: "notes",
                            sendMethod: "method",
                            shipTo: "Somewhere",
                            shipViaNotes: "ship via notes",
                            signBy: "Bob Jones",
                            termNotes: "term notes",
                            terms: "terms",
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
                    await CreateADummyVendorAsync(
                            id: ++index,
                            key: "Lochinvar",
                            name: "Lochinvar",
                            desc: "desc",
                            accountNumber: "acct#",
                            allowDropShip: true,
                            emailSubject: "SUBJECT",
                            notes1: "notes",
                            sendMethod: "method",
                            shipTo: "Somewhere",
                            shipViaNotes: "ship via notes",
                            signBy: "Bob Jones",
                            termNotes: "term notes",
                            terms: "terms",
                            contactID: 1)
                        .ConfigureAwait(false),
                    await CreateADummyVendorAsync(
                            id: ++index,
                            key: "Rheem",
                            name: "Rheem",
                            desc: "desc",
                            accountNumber: "acct#",
                            allowDropShip: true,
                            emailSubject: "SUBJECT",
                            notes1: "notes",
                            sendMethod: "method",
                            shipTo: "Somewhere",
                            shipViaNotes: "ship via notes",
                            signBy: "Bob Jones",
                            termNotes: "term notes",
                            terms: "terms",
                            contactID: 1)
                        .ConfigureAwait(false),
                    await CreateADummyVendorAsync(
                            id: ++index,
                            key: "Utica",
                            name: "Utica",
                            desc: "desc",
                            accountNumber: "acct#",
                            allowDropShip: true,
                            emailSubject: "SUBJECT",
                            notes1: "notes",
                            sendMethod: "method",
                            shipTo: "Somewhere",
                            shipViaNotes: "ship via notes",
                            signBy: "Bob Jones",
                            termNotes: "term notes",
                            terms: "terms",
                            contactID: 1)
                        .ConfigureAwait(false),
                };
                await InitializeMockSetVendorsAsync(mockContext, RawVendors).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Vendor Accounts
            if (DoAll || DoVendors || DoVendorAccountTable)
            {
                var index = 0;
                RawVendorAccounts = new()
                {
                    await CreateADummyVendorAccountAsync(id: ++index, key: "1|1").ConfigureAwait(false),
                };
                await InitializeMockSetVendorAccountsAsync(mockContext, RawVendorAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Vendor Images
            if (DoAll || DoVendors || DoVendorImageTable)
            {
                RawVendorImages = new()
                {
                    await CreateADummyVendorImageAsync(id: 1, key: "VENDOR-IMAGE-1", name: "Vendor Image 1", desc: "desc", originalFileName: "vendor-image-1.jpg", originalBytes: Array.Empty<byte>(), thumbnailFileName: "vendor-image-1-thumb.jpg", thumbnailBytes: Array.Empty<byte>()).ConfigureAwait(false),
                };
                await InitializeMockSetVendorImagesAsync(mockContext, RawVendorImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Vendor Image Types
            if (DoAll || DoVendors || DoVendorImageTypeTable)
            {
                var index = 0;
                RawVendorImageTypes = new()
                {
                    await CreateADummyVendorImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetVendorImageTypesAsync(mockContext, RawVendorImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Vendor Manufacturers
            if (DoAll || DoVendors || DoVendorManufacturerTable)
            {
                var index = 0;
                RawVendorManufacturers = new()
                {
                    await CreateADummyVendorManufacturerAsync(id: ++index, key: "Laars").ConfigureAwait(false),
                };
                await InitializeMockSetVendorManufacturersAsync(mockContext, RawVendorManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Vendor Products
            if (DoAll || DoVendors || DoVendorProductTable)
            {
                var index = 0;
                RawVendorProducts = new()
                {
                    await CreateADummyVendorProductAsync(id: ++index, key: "Laars-1151", slaveID: 1151).ConfigureAwait(false), // 1
                    await CreateADummyVendorProductAsync(id: ++index, key: "Lochinvar-1151", masterID: 2, slaveID: 1151).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Rheem-1151", masterID: 3, slaveID: 1151).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Utica-1151", masterID: 4, slaveID: 1151).ConfigureAwait(false),

                    await CreateADummyVendorProductAsync(id: ++index, key: "Laars-1152", slaveID: 1152).ConfigureAwait(false), // 5
                    await CreateADummyVendorProductAsync(id: ++index, key: "Lochinvar-1152", masterID: 2, slaveID: 1152).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Rheem-1152", masterID: 3, slaveID: 1152).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Utica-1152", masterID: 4, slaveID: 1152).ConfigureAwait(false),

                    await CreateADummyVendorProductAsync(id: ++index, key: "Laars-1153", slaveID: 1153).ConfigureAwait(false), // 9
                    await CreateADummyVendorProductAsync(id: ++index, key: "Lochinvar-1153", masterID: 2, slaveID: 1153).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Rheem-1153", masterID: 3, slaveID: 1153).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Utica-1153", masterID: 4, slaveID: 1153).ConfigureAwait(false),

                    await CreateADummyVendorProductAsync(id: ++index, key: "Laars-1154", slaveID: 1154).ConfigureAwait(false), // 13
                    await CreateADummyVendorProductAsync(id: ++index, key: "Lochinvar-1154", masterID: 2, slaveID: 1154).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Rheem-1154", masterID: 3, slaveID: 1154).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Utica-1154", masterID: 4, slaveID: 1154).ConfigureAwait(false),

                    await CreateADummyVendorProductAsync(id: ++index, key: "Laars-1155", slaveID: 1155).ConfigureAwait(false), // 17
                    await CreateADummyVendorProductAsync(id: ++index, key: "Lochinvar-1155", masterID: 2, slaveID: 1155).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Rheem-1155", masterID: 3, slaveID: 1155).ConfigureAwait(false),
                    await CreateADummyVendorProductAsync(id: ++index, key: "Utica-1155", masterID: 4, slaveID: 1155).ConfigureAwait(false),
                };
                await InitializeMockSetVendorProductsAsync(mockContext, RawVendorProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Vendor Types
            if (DoAll || DoVendors || DoVendorTypeTable)
            {
                var index = 0;
                RawVendorTypes = new()
                {
                    await CreateADummyVendorTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetVendorTypesAsync(mockContext, RawVendorTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
