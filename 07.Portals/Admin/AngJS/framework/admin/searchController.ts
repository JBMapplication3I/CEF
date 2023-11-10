/**
 * @file framework/admin/searchController.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Search controller class
 */
module cef.admin {
    export class SearchController {
        constructor(
            readonly $scope: ng.IScope,
            private readonly $q: ng.IQService,
            private readonly $filter: ng.IFilterService,
            private readonly $translate: ng.translate.ITranslateService,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
            private readonly $http: ng.IHttpService) {
            this.searchTypes = new controls.types.SearchTypeController($http, $scope, cvApi);
        }

        searchApi = () => { return this.$http.get(this.$filter("corsLink")("/operations/metadata?format=json", "api")); };

        // ===== Types & Statuses ===== Note: These were auto-generated in the types folder and then copied here
        searchTypes: controls.types.SearchTypeController;

        // ===== Sales =====
        // Sales Groups
        getSalesGroups = this.cvApi.sales.GetSalesGroups;
        deactivateSalesGroup = (id: number) => { return this.cvApi.sales.DeactivateSalesGroupByID(id); }
        reactivateSalesGroup = (id: number) => { return this.cvApi.sales.ReactivateSalesGroupByID(id); }
        deleteSalesGroup = (id: number) => { return this.cvApi.sales.DeleteSalesGroupByID(id); }
        // Sales Orders
        getSalesOrders = this.cvApi.ordering.GetSalesOrders;
        deactivateSalesOrder = (id: number) => { return this.cvApi.ordering.DeactivateSalesOrderByID(id); }
        reactivateSalesOrder = (id: number) => { return this.cvApi.ordering.ReactivateSalesOrderByID(id); }
        deleteSalesOrder = (id: number) => { return this.cvApi.ordering.DeleteSalesOrderByID(id); }
        // Sales Invoices
        getSalesInvoices = this.cvApi.invoicing.GetSalesInvoices;
        deactivateSalesInvoice = (id: number) => { return this.cvApi.invoicing.DeactivateSalesInvoiceByID(id); }
        reactivateSalesInvoice = (id: number) => { return this.cvApi.invoicing.ReactivateSalesInvoiceByID(id); }
        deleteSalesInvoice = (id: number) => { return this.cvApi.invoicing.DeleteSalesInvoiceByID(id); }
        // Sales Quotes
        getSalesQuotes = this.cvApi.quoting.GetSalesQuotes;
        deactivateSalesQuote = (id: number) => { return this.cvApi.quoting.DeactivateSalesQuoteByID(id); }
        reactivateSalesQuote = (id: number) => { return this.cvApi.quoting.ReactivateSalesQuoteByID(id); }
        deleteSalesQuote = (id: number) => { return this.cvApi.quoting.DeleteSalesQuoteByID(id); }
        // Sample Requests
        getSampleRequests = this.cvApi.sampling.GetSampleRequests;
        deactivateSampleRequest = (id: number) => { return this.cvApi.sampling.DeactivateSampleRequestByID(id); }
        reactivateSampleRequest = (id: number) => { return this.cvApi.sampling.ReactivateSampleRequestByID(id); }
        deleteSampleRequest = (id: number) => { return this.cvApi.sampling.DeleteSampleRequestByID(id); }
        // Sales Returns
        getSalesReturns = this.cvApi.returning.GetSalesReturns;
        deactivateSalesReturn = (id: number) => { return this.cvApi.returning.DeactivateSalesReturnByID(id); }
        reactivateSalesReturn = (id: number) => { return this.cvApi.returning.ReactivateSalesReturnByID(id); }
        deleteSalesReturn = (id: number) => { return this.cvApi.returning.DeleteSalesReturnByID(id); }
        // Purchase Orders
        getPurchaseOrders = this.cvApi.purchasing.GetPurchaseOrders;
        deactivatePurchaseOrder = (id: number) => { return this.cvApi.purchasing.DeactivatePurchaseOrderByID(id); };
        reactivatePurchaseOrder = (id: number) => { return this.cvApi.purchasing.ReactivatePurchaseOrderByID(id); };
        deletePurchaseOrder = (id: number) => { return this.cvApi.purchasing.DeletePurchaseOrderByID(id); };
        // ===== Accounts =====
        // Accounts
        getAccounts = this.cvApi.accounts.GetAccounts;
        deactivateAccount = (id: number) => { return this.cvApi.accounts.DeactivateAccountByID(id); }
        reactivateAccount = (id: number) => { return this.cvApi.accounts.ReactivateAccountByID(id); }
        deleteAccount = (id: number) => { return this.cvApi.accounts.DeleteAccountByID(id); }
        // Users
        getUsers = this.cvApi.contacts.GetUsers;
        deactivateUser = (id: number) => { return this.cvApi.contacts.DeactivateUserByID(id); }
        reactivateUser = (id: number) => { return this.cvApi.contacts.ReactivateUserByID(id); }
        deleteUser = (id: number) => { return this.cvApi.contacts.DeleteUserByID(id); }
        // Roles
        getRoles = this.cvApi.authentication.GetRolesAsListing;
        deleteRole = (name: string) => { return this.cvApi.authentication.DeleteRole({ Name: name }); }
        // Discounts
        getDiscounts = this.cvApi.discounts.GetDiscounts;
        deactivateDiscount = (id: number) => {
            return this.$q((resolve, reject) => {
                this.cvApi.discounts.DeactivateDiscountByID(id)
                    .then(resolve) // succeeded normally
                    .catch(reason => {
                        // Failed, set end date to today
                        console.error(reason);
                        this.cvConfirmModalFactory(
                            this.$translate("ui.admin.grid.ConfirmDeactivate.Discount.Message")
                        ).then(success => {
                            if (!success) {
                                reject("User cancelled");
                                return;
                            }
                            this.cvApi.discounts.EndDiscountByID(id)
                                .then(resolve)
                                .catch(reject);
                        }).catch(reject);
                    });
            })
        };
        reactivateDiscount = (id: number) => { return this.cvApi.discounts.ReactivateDiscountByID(id); };
        deleteDiscount = (id: number) => { return this.cvApi.discounts.DeleteDiscountByID(id); };
        // Price Rules
        getPriceRules = this.cvApi.pricing.GetPriceRules;
        deactivatePriceRule = (id: number) => { return this.cvApi.pricing.DeactivatePriceRuleByID(id); }
        reactivatePriceRule = (id: number) => { return this.cvApi.pricing.ReactivatePriceRuleByID(id); }
        deletePriceRule = (id: number) => { return this.cvApi.pricing.DeletePriceRuleByID(id); }
        // Stores
        getStores = this.cvApi.stores.GetStores;
        deactivateStore = (id: number) => { return this.cvApi.stores.DeactivateStoreByID(id); }
        reactivateStore = (id: number) => { return this.cvApi.stores.ReactivateStoreByID(id); }
        deleteStore = (id: number) => { return this.cvApi.stores.DeleteStoreByID(id); }
        // Brands
        getBrands = this.cvApi.brands.GetBrands;
        deactivateBrand = (id: number) => { return this.cvApi.brands.DeactivateBrandByID(id); }
        reactivateBrand = (id: number) => { return this.cvApi.brands.ReactivateBrandByID(id); }
        deleteBrand = (id: number) => { return this.cvApi.brands.DeleteBrandByID(id); }
        // Badges
        getBadges = this.cvApi.badges.GetBadges;
        deactivateBadge = (id: number) => { return this.cvApi.badges.DeactivateBadgeByID(id); }
        reactivateBadge = (id: number) => { return this.cvApi.badges.ReactivateBadgeByID(id); }
        deleteBadge = (id: number) => { return this.cvApi.badges.DeleteBadgeByID(id); }
        // Site Domains
        getSiteDomains = this.cvApi.stores.GetSiteDomains;
        deactivateSiteDomain = (id: number) => { return this.cvApi.stores.DeactivateSiteDomainByID(id); }
        reactivateSiteDomain = (id: number) => { return this.cvApi.stores.ReactivateSiteDomainByID(id); }
        deleteSiteDomain = (id: number) => { return this.cvApi.stores.DeleteSiteDomainByID(id); }
        // Social Providers
        getSocialProviders = this.cvApi.stores.GetSocialProviders;
        deactivateSocialProvider = (id: number) => { return this.cvApi.stores.DeactivateSocialProviderByID(id); }
        reactivateSocialProvider = (id: number) => { return this.cvApi.stores.ReactivateSocialProviderByID(id); }
        deleteSocialProvider = (id: number) => { return this.cvApi.stores.DeleteSocialProviderByID(id); }
        // Reviews
        getReviews = this.cvApi.reviews.GetReviews;
        deactivateReview = (id: number) => { return this.cvApi.reviews.DeactivateReviewByID(id); }
        reactivateReview = (id: number) => { return this.cvApi.reviews.ReactivateReviewByID(id); }
        deleteReview = (id: number) => { return this.cvApi.reviews.DeleteReviewByID(id); }
        // ===== Inventory =====
        // Products
        getProducts = this.cvApi.products.GetProducts;
        deactivateProduct = (id: number) => { return this.cvApi.products.DeactivateProductByID(id); };
        reactivateProduct = (id: number) => { return this.cvApi.products.ReactivateProductByID(id); };
        deleteProduct = (id: number) => { return this.cvApi.products.DeleteProductByID(id); };
        exportProductsToExcel = this.cvApi.products.GetProductsAsExcelDoc;
        // Warehouses
        getInventoryLocations = this.cvApi.inventory.GetInventoryLocations;
        deactivateInventoryLocation = (id: number) => { return this.cvApi.inventory.DeactivateInventoryLocationByID(id); };
        reactivateInventoryLocation = (id: number) => { return this.cvApi.inventory.ReactivateInventoryLocationByID(id); };
        deleteInventoryLocation = (id: number) => { return this.cvApi.inventory.DeleteInventoryLocationByID(id); };
        // Vendors
        getVendors = this.cvApi.vendors.GetVendors;
        deactivateVendor = id => { return this.cvApi.vendors.DeactivateVendorByID(id); };
        reactivateVendor = id => { return this.cvApi.vendors.ReactivateVendorByID(id); };
        deleteVendor = id => { return this.cvApi.vendors.DeleteVendorByID(id); };
        // Manufacturers
        getManufacturers = this.cvApi.manufacturers.GetManufacturers;
        deactivateManufacturer = id => { return this.cvApi.manufacturers.DeactivateManufacturerByID(id); };
        reactivateManufacturer = id => { return this.cvApi.manufacturers.ReactivateManufacturerByID(id); };
        deleteManufacturer = id => { return this.cvApi.manufacturers.DeleteManufacturerByID(id); };
        // Categories
        getCategories = this.cvApi.categories.GetCategories;
        deactivateCategory = (id: number) => { return this.cvApi.categories.DeactivateCategoryByID(id); };
        reactivateCategory = (id: number) => { return this.cvApi.categories.ReactivateCategoryByID(id); };
        deleteCategory = (id: number) => { return this.cvApi.categories.DeleteCategoryByID(id); };
        // Attributes
        getAttributes = this.cvApi.attributes.GetGeneralAttributes;
        deactivateAttribute = (id: number) => { return this.cvApi.attributes.DeactivateGeneralAttributeByID(id); };
        reactivateAttribute = (id: number) => { return this.cvApi.attributes.ReactivateGeneralAttributeByID(id); };
        deleteAttribute = (id: number) => { return this.cvApi.attributes.DeleteGeneralAttributeByID(id); };
        // ===== Shipments =====
        // Carrier Accounts
        getCarriers = this.cvApi.shipping.GetShipCarriers;
        deactivateCarrier = (id: number) => { return this.cvApi.shipping.DeactivateShipCarrierByID(id); }
        reactivateCarrier = (id: number) => { return this.cvApi.shipping.ReactivateShipCarrierByID(id); }
        deleteCarrier = (id: number) => { return this.cvApi.shipping.DeleteShipCarrierByID(id); }
        // Discounts
        getShippingDiscounts = () => this.$q((resolve, reject) => {
            reject();
            /*
            this.cvApi.shipping.GetShippingDiscounts().then(r => {
                if (r.data) {
                    let result = Object.keys(r.data).map((key, index) => {
                        return { ID: index, OptionName: key, Value: r.data[key], Active: true };
                    });
                    resolve(result);
                } else {
                    reject("no response data");
                }
            }).catch(reject);
            */
        });
        deactivateShippingDiscount = (id: any) => { return this.$q.reject(); /* return this.cvApi.shipping.DeactivateShippingDiscountsByKey(id); */ }
        // Shipping Packages
        getShippingPackages = this.cvApi.shipping.GetPackages;
        deactivateShippingPackage = (id: number) => { return this.cvApi.shipping.DeactivatePackageByID(id); };
        reactivateShippingPackage = (id: number) => { return this.cvApi.shipping.ReactivatePackageByID(id); };
        deleteShippingPackage = (id: number) => { return this.cvApi.shipping.DeletePackageByID(id); };
        // ===== System =====
        // Setting
        getSettings = this.cvApi.structure.GetSettings;
        deactivateSetting = (id: number) => { return this.cvApi.structure.DeactivateSettingByID(id); };
        reactivateSetting = (id: number) => { return this.cvApi.structure.ReactivateSettingByID(id); };
        deleteSetting = (id: number) => { return this.cvApi.structure.DeleteSettingByID(id); };
        // UI Translations
        getUiTranslations = this.cvApi.globalization.GetUiTranslations;
        deactivateUiTranslation = (id: number) => { return this.cvApi.globalization.DeactivateUiTranslationByID(id); };
        reactivateUiTranslation = (id: number) => { return this.cvApi.globalization.ReactivateUiTranslationByID(id); };
        deleteUiTranslation = (id: number) => { return this.cvApi.globalization.DeleteUiTranslationByID(id); };
        // UI Keys
        getUiKeys = this.cvApi.globalization.GetUiKeys;
        deactivateUiKeys = (id: number) => { return this.cvApi.globalization.DeactivateUiKeyByID(id); };
        reactivateUiKeys = (id: number) => { return this.cvApi.globalization.ReactivateUiKeyByID(id); };
        deleteUiKey = (id: number) => { return this.cvApi.globalization.DeleteUiKeyByID(id); };
        // Languages
        getLanguages = this.cvApi.globalization.GetLanguages;
        deactivateLanguage = (id: number) => { return this.cvApi.globalization.DeactivateLanguageByID(id); };
        reactivateLanguage = (id: number) => { return this.cvApi.globalization.ReactivateLanguageByID(id); };
        deleteLanguage = (id: number) => { return this.cvApi.globalization.DeleteLanguageByID(id); };
        // Currencies
        getCurrencies = this.cvApi.currencies.GetCurrencies;
        deactivateCurrency = (id: number) => { return this.cvApi.currencies.DeactivateCurrencyByID(id); };
        reactivateCurrency = (id: number) => { return this.cvApi.currencies.ReactivateCurrencyByID(id); };
        deleteCurrency = (id: number) => { return this.cvApi.currencies.DeleteCurrencyByID(id); };
        // Email Templates
        getEmailTemplates = this.cvApi.messaging.GetEmailTemplates;
        deactivateEmailTemplate = (id: number) => { return this.cvApi.messaging.DeactivateEmailTemplateByID(id); };
        reactivateEmailTemplate = (id: number) => { return this.cvApi.messaging.ReactivateEmailTemplateByID(id); };
        deleteEmailTemplate = (id: number) => { return this.cvApi.messaging.DeleteEmailTemplateByID(id); };
        // Email Queues
        getEmailQueues = this.cvApi.messaging.GetEmailQueues;
        deactivateEmailQueue = (id: number) => { return this.cvApi.messaging.DeactivateEmailQueueByID(id); };
        reactivateEmailQueue = (id: number) => { return this.cvApi.messaging.ReactivateEmailQueueByID(id); };
        deleteEmailQueue = (id: number) => { return this.cvApi.messaging.DeleteEmailQueueByID(id); };
        // Import/Export Mappings
        getImportExportMappings = this.cvApi.structure.GetImportExportMappings;
        deactivateImportExportMapping = (id: number) => { return this.cvApi.structure.DeactivateImportExportMappingByID(id); };
        reactivateImportExportMapping = (id: number) => { return this.cvApi.structure.ReactivateImportExportMappingByID(id); };
        deleteImportExportMapping = (id: number) => { return this.cvApi.structure.DeleteImportExportMappingByID(id); };
        // Logs
        getLogs = this.cvApi.structure.GetEventLogs;
        deactivateLog = (id: number) => { return this.cvApi.structure.DeactivateEventLogByID(id); };
        reactivateLog = (id: number) => { return this.cvApi.structure.ReactivateEventLogByID(id); };
        deleteLog = (id: number) => { return this.cvApi.structure.DeleteEventLogByID(id); };
    }

    adminApp.controller("SearchController", SearchController);
}
