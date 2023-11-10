// <copyright file="DoMockingSetupForContextRunnerStores.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner stores class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerStoresAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: SiteDomains
            if (DoAll || DoStores || DoSiteDomainTable)
            {
                var index = 0;
                RawSiteDomains = new()
                {
                    await CreateADummySiteDomainAsync(id: ++index, key: "sitedomain-" + index, name: "SiteDomain One",   desc: "description").ConfigureAwait(false),
                    await CreateADummySiteDomainAsync(id: ++index, key: "sitedomain-" + index, name: "SiteDomain Two",   desc: "description").ConfigureAwait(false),
                    await CreateADummySiteDomainAsync(id: ++index, key: "sitedomain-" + index, name: "SiteDomain Three", desc: "description").ConfigureAwait(false),
                    await CreateADummySiteDomainAsync(id: ++index, key: "sitedomain-" + index, name: "SiteDomain Four",  desc: "description").ConfigureAwait(false),
                    await CreateADummySiteDomainAsync(id: ++index, key: "sitedomain-" + index, name: "SiteDomain Five",  desc: "description").ConfigureAwait(false),
                };
                await InitializeMockSetSiteDomainsAsync(mockContext, RawSiteDomains).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: SiteDomain SocialProviders
            if (DoAll || DoStores || DoSiteDomainSocialProviderTable)
            {
                var index = 0;
                RawSiteDomainSocialProviders = new()
                {
                    await CreateADummySiteDomainSocialProviderAsync(id: ++index, key: "sitedomain-socialprovider-" + index, script: "", urlValues: "").ConfigureAwait(false),
                    await CreateADummySiteDomainSocialProviderAsync(id: ++index, key: "sitedomain-socialprovider-" + index, script: "", urlValues: "").ConfigureAwait(false),
                };
                await InitializeMockSetSiteDomainSocialProvidersAsync(mockContext, RawSiteDomainSocialProviders).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: SocialProviders
            if (DoAll || DoStores || DoSocialProviderTable)
            {
                var index = 0;
                RawSocialProviders = new()
                {
                    await CreateADummySocialProviderAsync(id: ++index, key: "socialprovider-" + index, name: "SocialProvider One", desc: "description", url: "", urlFormat: "").ConfigureAwait(false),
                    await CreateADummySocialProviderAsync(id: ++index, key: "socialprovider-" + index, name: "SocialProvider Two", desc: "description", url: "", urlFormat: "").ConfigureAwait(false),
                    await CreateADummySocialProviderAsync(id: ++index, key: "socialprovider-" + index, name: "SocialProvider Three", desc: "description", url: "", urlFormat: "").ConfigureAwait(false),
                    await CreateADummySocialProviderAsync(id: ++index, key: "socialprovider-" + index, name: "SocialProvider Four", desc: "description", url: "", urlFormat: "").ConfigureAwait(false),
                    await CreateADummySocialProviderAsync(id: ++index, key: "socialprovider-" + index, name: "SocialProvider Five", desc: "description", url: "", urlFormat: "").ConfigureAwait(false),
                };
                await InitializeMockSetSocialProvidersAsync(mockContext, RawSocialProviders).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Stores
            if (DoAll || DoStores || DoStoreTable)
            {
                var index = 0;
                RawStores = new()
                {
                    await CreateADummyStoreAsync(
                            id: ++index,
                            key: "store-" + index,
                            name: "Store " + index,
                            desc: "description",
                            contactID: 0001,
                            typeID: 1,
                            minimumOrderDollarAmount: 2000m,
                            minimumOrderDollarAmountAfter: 0m,
                            // minimumOrderDollarAmountBufferCategoryID: 1,
                            // minimumOrderDollarAmountBufferProductID: 1151,
                            minimumOrderDollarAmountWarningMessage: /* language=HTML */ "<p>Custom Warning Message for <b>{{ownerName}}</b>, missing <b>{{missingAmount}}</b> worth of items. They suggest using more items from the <a href=\"/{{bufferCategorySeoUrl}}\">{{bufferCategoryName}}</a> category or this specific item: \"<a href=\"/Product/{{bufferItemSeoUrl}}>{{bufferItemName}}</a>\".</p>\r\n{{overrideFeeWarningMessage}}",
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
                    await CreateADummyStoreAsync(
                            id: ++index,
                            key: "store-" + index,
                            name: "Store " + index,
                            desc: "description",
                            typeID: 2)
                        .ConfigureAwait(false),
                    await CreateADummyStoreAsync(
                            id: ++index,
                            key: "store-" + index,
                            name: "Store " + index,
                            desc: "description",
                            typeID: 3)
                        .ConfigureAwait(false),
                    await CreateADummyStoreAsync(
                            id: ++index,
                            key: "store-" + index,
                            name: "Store " + index,
                            desc: "description",
                            typeID: 1)
                        .ConfigureAwait(false),
                    await CreateADummyStoreAsync(
                            id: ++index,
                            key: "store-" + index,
                            name: "Store " + index,
                            desc: "description",
                            typeID: 1)
                        .ConfigureAwait(false),
                };
                await InitializeMockSetStoresAsync(mockContext, RawStores).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Accounts
            if (DoAll || DoStores || DoStoreAccountTable)
            {
                var index = 0;
                RawStoreAccounts = new()
                {
                    await CreateADummyStoreAccountAsync(id: ++index, key: "store-account-" + index, hasAccessToStore: true, pricePointID: 0).ConfigureAwait(false),
                    await CreateADummyStoreAccountAsync(id: ++index, key: "store-account-" + index, masterID: 2, hasAccessToStore: true, pricePointID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetStoreAccountsAsync(mockContext, RawStoreAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Badges
            if (DoAll || DoStores || DoStoreBadgeTable)
            {
                var index = 0;
                RawStoreBadges = new()
                {
                    await CreateADummyStoreBadgeAsync(id: ++index, key: "store-badge-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetStoreBadgesAsync(mockContext, RawStoreBadges).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Categories
            if (DoAll || DoStores || DoStoreCategoryTable)
            {
                var index = 0;
                RawStoreCategories = new()
                {
                    await CreateADummyStoreCategoryAsync(id: ++index, key: "KEY-1", masterID: 1, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyStoreCategoryAsync(id: ++index, key: "KEY-2", masterID: 2, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyStoreCategoryAsync(id: ++index, key: "KEY-3", masterID: 3, slaveID: 6, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyStoreCategoryAsync(id: ++index, key: "KEY-4", masterID: 4, slaveID: 3, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyStoreCategoryAsync(id: ++index, key: "KEY-4", masterID: 5, slaveID: 3, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetStoreCategoriesAsync(mockContext, RawStoreCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Contacts
            if (DoAll || DoStores || DoStoreContactTable)
            {
                var index = 0;
                RawStoreContacts = new()
                {
                    await CreateADummyStoreContactAsync(id: ++index, key: "STORE-1|CONTACT|STORECONTACT-1|TEST", name: "BILL TO", desc: "desc").ConfigureAwait(false),
                    await CreateADummyStoreContactAsync(id: ++index, key: "STORE-2|CONTACT|STORECONTACT-2|TEST", name: "SHIP TO", desc: "desc", slaveID: 2, masterID: 2).ConfigureAwait(false),
                    await CreateADummyStoreContactAsync(id: ++index, key: "STORE-3|CONTACT|STORECONTACT-3|TEST", name: "OTHER",   desc: "desc", slaveID: 3, masterID: 3).ConfigureAwait(false),
                };
                await InitializeMockSetStoreContactsAsync(mockContext, RawStoreContacts).ConfigureAwait(false);
            }
            #region Apply Data and set up IQueryable: Store Contacts
            if (DoAll || DoStores || DoStoreContactTable)
            {
                var index = 0;
                RawStoreContacts = new()
                {
                    await CreateADummyStoreContactAsync(id: ++index, key: "STORE-1|CONTACT|STORECONTACT-1|TEST", name: "BILL TO", desc: "desc").ConfigureAwait(false),
                    await CreateADummyStoreContactAsync(id: ++index, key: "STORE-2|CONTACT|STORECONTACT-2|TEST", name: "SHIP TO", desc: "desc", slaveID: 2, masterID: 2).ConfigureAwait(false),
                    await CreateADummyStoreContactAsync(id: ++index, key: "STORE-3|CONTACT|STORECONTACT-3|TEST", name: "OTHER",   desc: "desc", slaveID: 3, masterID: 3).ConfigureAwait(false),
                };
                await InitializeMockSetStoreContactsAsync(mockContext, RawStoreContacts).ConfigureAwait(false);
            }
            #endregion
            #endregion
            #region Apply Data and set up IQueryable: Store Countries
            if (DoAll || DoStores || DoStoreCountryTable)
            {
                var index = 0;
                RawStoreCountries = new()
                {
                    await CreateADummyStoreCountryAsync(id: ++index, key: "store-country-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetStoreCountriesAsync(mockContext, RawStoreCountries).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Districts
            if (DoAll || DoStores || DoStoreDistrictTable)
            {
                var index = 0;
                RawStoreDistricts = new()
                {
                    await CreateADummyStoreDistrictAsync(id: ++index, key: "store-district-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetStoreDistrictsAsync(mockContext, RawStoreDistricts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Images
            if (DoAll || DoStores || DoStoreImageTable)
            {
                var index = 0;
                RawStoreImages = new()
                {
                    await CreateADummyStoreImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetStoreImagesAsync(mockContext, RawStoreImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Image Types
            if (DoAll || DoStores || DoStoreImageTypeTable)
            {
                var index = 0;
                RawStoreImageTypes = new()
                {
                    await CreateADummyStoreImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetStoreImageTypesAsync(mockContext, RawStoreImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Inventory Locations
            if (DoAll || DoStores || DoStoreInventoryLocationTable)
            {
                var index = 0;
                RawStoreInventoryLocations = new()
                {
                    await CreateADummyStoreInventoryLocationAsync(id: ++index, key: "store-inventory-location-" + index).ConfigureAwait(false),
                    await CreateADummyStoreInventoryLocationAsync(id: ++index, key: "store-inventory-location-" + index, slaveID: 2, typeID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetStoreInventoryLocationsAsync(mockContext, RawStoreInventoryLocations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Inventory Location Types
            if (DoAll || DoStores || DoStoreInventoryLocationTypeTable)
            {
                var index = 0;
                RawStoreInventoryLocationTypes = new()
                {
                    await CreateADummyStoreInventoryLocationTypeAsync(id: ++index, key: "Internal-Warehouse", name: "Internal Warehouse", desc: "desc", sortOrder: 1, displayName: "Internal Warehouse").ConfigureAwait(false),
                    await CreateADummyStoreInventoryLocationTypeAsync(id: ++index, key: "Distibution-Center", name: "Distribution Center", desc: "desc", sortOrder: 2, displayName: "Distribution Center").ConfigureAwait(false),
                };
                await InitializeMockSetStoreInventoryLocationTypesAsync(mockContext, RawStoreInventoryLocationTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Manufacturers
            if (DoAll || DoStores || DoStoreManufacturerTable)
            {
                var index = 0;
                RawStoreManufacturers = new()
                {
                    await CreateADummyStoreManufacturerAsync(id: ++index, key: "store-manufacturer-" + index, masterID: 1, slaveID: 1).ConfigureAwait(false),
                    await CreateADummyStoreManufacturerAsync(id: ++index, key: "store-manufacturer-" + index, masterID: 2, slaveID: 1).ConfigureAwait(false),
                    await CreateADummyStoreManufacturerAsync(id: ++index, key: "store-manufacturer-" + index, masterID: 1, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyStoreManufacturerAsync(id: ++index, key: "store-manufacturer-" + index, masterID: 2, slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetStoreManufacturersAsync(mockContext, RawStoreManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Products
            if (DoAll || DoStores || DoStoreProductTable)
            {
                var index = 0;
                RawStoreProducts = new()
                {
                    await CreateADummyStoreProductAsync(id: ++index, key: "store-product-" + index, isVisibleIn: true, slaveID: 1151).ConfigureAwait(false),
                    await CreateADummyStoreProductAsync(id: ++index, key: "store-product-" + index, isVisibleIn: true, slaveID: 1152).ConfigureAwait(false),
                };
                await InitializeMockSetStoreProductsAsync(mockContext, RawStoreProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Regions
            if (DoAll || DoStores || DoStoreRegionTable)
            {
                var index = 0;
                RawStoreRegions = new()
                {
                    await CreateADummyStoreRegionAsync(id: ++index, key: "store-region-" + index).ConfigureAwait(false),
                    await CreateADummyStoreRegionAsync(id: ++index, key: "store-region-" + index).ConfigureAwait(false),
                    await CreateADummyStoreRegionAsync(id: ++index, key: "store-region-" + index).ConfigureAwait(false),
                    await CreateADummyStoreRegionAsync(id: ++index, key: "store-region-" + index).ConfigureAwait(false)
                };
                await InitializeMockSetStoreRegionsAsync(mockContext, RawStoreRegions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Subscriptions
            if (DoAll || DoStores || DoStoreSubscriptionTable)
            {
                var index = 0;
                RawStoreSubscriptions = new()
                {
                    await CreateADummyStoreSubscriptionAsync(id: ++index, key: "store-subscription-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetStoreSubscriptionsAsync(mockContext, RawStoreSubscriptions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Types
            if (DoAll || DoStores || DoStoreTypeTable)
            {
                var index = 0;
                RawStoreTypes = new()
                {
                    await CreateADummyStoreTypeAsync(id: ++index, key: "Catalog", name: "Catalog", desc: "desc", sortOrder: 1, displayName: "Catalog").ConfigureAwait(false),
                    await CreateADummyStoreTypeAsync(id: ++index, key: "Directory", name: "Directory", desc: "desc", sortOrder: 2, displayName: "Directory").ConfigureAwait(false),
                    await CreateADummyStoreTypeAsync(id: ++index, key: "Manufacturer", name: "Manufacturer", desc: "desc", sortOrder: 3, displayName: "Manufacturer").ConfigureAwait(false),
                };
                await InitializeMockSetStoreTypesAsync(mockContext, RawStoreTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Users
            if (DoAll || DoStores || DoStoreUserTable)
            {
                var index = 0;
                RawStoreUsers = new()
                {
                    await CreateADummyStoreUserAsync(id: ++index, key: "store-user-" + index).ConfigureAwait(false),
                    await CreateADummyStoreUserAsync(id: ++index, key: "store-user-" + index, slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetStoreUsersAsync(mockContext, RawStoreUsers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Vendors
            if (DoAll || DoStores || DoStoreVendorTable)
            {
                var index = 0;
                RawStoreVendors = new()
                {
                    await CreateADummyStoreVendorAsync(id: ++index, key: "store-vendor-" + index).ConfigureAwait(false),
                    await CreateADummyStoreVendorAsync(id: ++index, key: "store-vendor-" + index, slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetStoreVendorsAsync(mockContext, RawStoreVendors).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
