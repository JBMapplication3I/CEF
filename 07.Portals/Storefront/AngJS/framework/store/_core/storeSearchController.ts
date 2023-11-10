/**
 * @file framework/store/_core/storeSearchController.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Store search controller class
 */
module cef.store.core {
    export class StoreSearchController {
        constructor(
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvSecurityService: services.ISecurityService) { // Used by UI
            this.searchTypes = new SearchTypeController(cvApi);
            this.todaysDate = new Date();
        }

        todaysDate: Date;
        // ===== Types & Statuses ===== Note: These were auto-generated in the types folder and then copied here
        searchTypes: SearchTypeController;

        // Users
        getCurrentAccountUsers = this.cvApi.accounts.GetUsersForCurrentAccount;
        // Products
        getCurrentStoreProducts = this.cvApi.products.GetProductsForCurrentStore;
        // Sales Groups
        getCurrentAccountSalesGroups = this.cvApi.sales.GetCurrentAccountSalesGroups;
        // Sales Orders
        getCurrentAccountSalesOrders = this.cvApi.ordering.GetCurrentAccountSalesOrders;
        getCurrentUserSalesOrders = this.cvApi.ordering.GetCurrentUserSalesOrders;
        getSalesOrderTypes = this.cvApi.ordering.GetSalesOrderTypes;
        getSalesOrderStatuses = this.cvApi.ordering.GetSalesOrderStatuses;
        // Sales Invoices
        getCurrentAccountSalesInvoices = this.cvApi.providers.GetCurrentAccountSalesInvoices;
        getCurrentUserSalesInvoices = this.cvApi.providers.GetCurrentUserSalesInvoices;
        getSalesInvoiceTypes = this.cvApi.invoicing.GetSalesInvoiceTypes;
        getSalesInvoiceStatuses = this.cvApi.invoicing.GetSalesInvoiceStatuses;
        // Sales Quotes
        getCurrentAccountSalesQuotes = this.cvApi.providers.GetCurrentAccountSalesQuotes;
        getCurrentUserSalesQuotes = this.cvApi.providers.GetCurrentUserSalesQuotes;
        getSalesQuotes = this.cvApi.quoting.GetSalesQuotes;
        getSalesQuoteTypes = this.cvApi.quoting.GetSalesQuoteTypes;
        getSalesQuoteStatuses = this.cvApi.quoting.GetSalesQuoteStatuses;
        // Sample Requests
        getCurrentAccountSampleRequests = this.cvApi.providers.GetCurrentAccountSampleRequests;
        getCurrentUserSampleRequests = this.cvApi.providers.GetCurrentUserSampleRequests;
        getSampleRequestTypes = this.cvApi.sampling.GetSampleRequestTypes;
        getSampleRequestStatuses = this.cvApi.sampling.GetSampleRequestStatuses;
        // Sales Returns
        getCurrentAccountSalesReturns = this.cvApi.providers.GetCurrentAccountSalesReturns;
        getCurrentUserSalesReturns = this.cvApi.providers.GetCurrentUserSalesReturns;
        getSalesReturnTypes = this.cvApi.returning.GetSalesReturnTypes;
        getSalesReturnStatuses = this.cvApi.returning.GetSalesReturnStatuses;
        // Address Book
        getCurrentAccountAddressBook = this.cvApi.geography.GetCurrentAccountAddressBook;
        getCurrentUserAddressBook = this.cvApi.geography.GetCurrentUserAddressBook;
        // Shopping Lists
        getCurrentUserCartTypes = this.cvApi.shopping.GetCurrentUserCartTypes;
        // Vendors
        getVendors = this.cvApi.stores.GetStores;
        // getVendors = this.cvApi.vendors.GetVendors;
        // Ticket Rules
        getPriceRules = this.cvApi.pricing.GetPriceRules;
        // Subscriptions
        getCurrentUserSubscriptions = this.cvApi.payments.GetCurrentUserSubscriptions;
        getSubscriptionStatuses = this.cvApi.payments.GetSubscriptionStatuses;
    }
}
