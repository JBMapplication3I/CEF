/**
 * @file framework/store/_core/types.searchController.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Types search controller class (storefront)
 */
module cef.store.core {
    export class SearchTypeController {
        constructor(private readonly cvApi: api.ICEFAPI) { }

        // ===== Types & Statuses =====
        // Account Association Types
        getAccountAssociationTypes = this.cvApi.accounts.GetAccountAssociationTypes;
        // // Account Image Types
        // getAccountImageTypes = this.cvApi.accounts.GetAccountImageTypes;
        // // Account Product Types
        // getAccountProductTypes = this.cvApi.accounts.GetAccountProductTypes;
        // Account Statuses
        getAccountStatuses = this.cvApi.accounts.GetAccountStatuses;
        // Account Types
        getAccountTypes = this.cvApi.accounts.GetAccountTypes;
        // // Ad Image Types
        // getAdImageTypes = this.cvApi.advertising.GetAdImageTypes;
        // // Ad Statuses
        // getAdStatuses = this.cvApi.advertising.GetAdStatuses;
        // // Ad Types
        // getAdTypes = this.cvApi.advertising.GetAdTypes;
        // // Zone Statuses
        // getZoneStatuses = this.cvApi.advertising.GetZoneStatuses;
        // // Zone Types
        // getZoneTypes = this.cvApi.advertising.GetZoneTypes;
        // Attribute Groups
        getAttributeGroups = this.cvApi.attributes.GetAttributeGroups;
        // Attribute Tabs
        getAttributeTabs = this.cvApi.attributes.GetAttributeTabs;
        // Attribute Types
        getAttributeTypes = this.cvApi.attributes.GetAttributeTypes;
        // // Badge Image Types
        // getBadgeImageTypes = this.cvApi.badges.GetBadgeImageTypes;
        // // Badge Types
        // getBadgeTypes = this.cvApi.badges.GetBadgeTypes;
        // // Brand Image Types
        // getBrandImageTypes = this.cvApi.brands.GetBrandImageTypes;
        // // Calendar Event Image Types
        // getCalendarEventImageTypes = this.cvApi.calendarEvents.GetCalendarEventImageTypes;
        // // Calendar Event Statuses
        // getCalendarEventStatuses = this.cvApi.calendarEvents.GetCalendarEventStatuses;
        // // Calendar Event Types
        // getCalendarEventTypes = this.cvApi.calendarEvents.GetCalendarEventTypes;
        // // User Event Attendance Types
        // getUserEventAttendanceTypes = this.cvApi.calendarEvents.GetUserEventAttendanceTypes;
        // // Category Image Types
        // getCategoryImageTypes = this.cvApi.categories.GetCategoryImageTypes;
        // Category Types
        getCategoryTypes = this.cvApi.categories.GetCategoryTypes;
        // // Contact Image Types
        // getContactImageTypes = this.cvApi.contacts.GetContactImageTypes;
        // Contact Types
        getContactTypes = this.cvApi.contacts.GetContactTypes;
        // // Referral Code Statuses
        // getReferralCodeStatuses = this.cvApi.contacts.GetReferralCodeStatuses;
        // // Referral Code Types
        // getReferralCodeTypes = this.cvApi.contacts.GetReferralCodeTypes;
        // // User Image Types
        // getUserImageTypes = this.cvApi.contacts.GetUserImageTypes;
        // User Online Statuses
        getUserOnlineStatuses = this.cvApi.contacts.GetUserOnlineStatuses;
        // User Statuses
        getUserStatuses = this.cvApi.contacts.GetUserStatuses;
        // User Types
        getUserTypes = this.cvApi.contacts.GetUserTypes;
        // // Counter Log Types
        // getCounterLogTypes = this.cvApi.counters.GetCounterLogTypes;
        // // Counter Types
        // getCounterTypes = this.cvApi.counters.GetCounterTypes;
        // // Currency Image Types
        // getCurrencyImageTypes = this.cvApi.currencies.GetCurrencyImageTypes;
        // // Country Image Types
        // getCountryImageTypes = this.cvApi.geography.GetCountryImageTypes;
        // // Region Image Types
        // getRegionImageTypes = this.cvApi.geography.GetRegionImageTypes;
        // // Language Image Types
        // getLanguageImageTypes = this.cvApi.globalization.GetLanguageImageTypes;
        // // Group Statuses
        // getGroupStatuses = this.cvApi.groups.GetGroupStatuses;
        // // Group Types
        // getGroupTypes = this.cvApi.groups.GetGroupTypes;
        // Sales Invoice States
        getSalesInvoiceStates = this.cvApi.invoicing.GetSalesInvoiceStates;
        // Sales Invoice Statuses
        getSalesInvoiceStatuses = this.cvApi.invoicing.GetSalesInvoiceStatuses;
        // Sales Invoice Types
        getSalesInvoiceTypes = this.cvApi.invoicing.GetSalesInvoiceTypes;
        // // Manufacturer Image Types
        // getManufacturerImageTypes = this.cvApi.manufacturers.GetManufacturerImageTypes;
        // // Manufacturer Types
        // getManufacturerTypes = this.cvApi.manufacturers.GetManufacturerTypes;
        // // Email Statuses
        // getEmailStatuses = this.cvApi.messaging.GetEmailStatuses;
        // // Email Types
        // getEmailTypes = this.cvApi.messaging.GetEmailTypes;
        // Sales Order States
        getSalesOrderStates = this.cvApi.ordering.GetSalesOrderStates;
        // Sales Order Statuses
        getSalesOrderStatuses = this.cvApi.ordering.GetSalesOrderStatuses;
        // Sales Order Types
        getSalesOrderTypes = this.cvApi.ordering.GetSalesOrderTypes;
        // Memberships
        getMemberships = this.cvApi.payments.GetMemberships;
        // Membership Levels
        getMembershipLevels = this.cvApi.payments.GetMembershipLevels;
        // Payment Statuses
        getPaymentStatuses = this.cvApi.payments.GetPaymentStatuses;
        // Payment Types
        getPaymentTypes = this.cvApi.payments.GetPaymentTypes;
        // Repeat Types
        getRepeatTypes = this.cvApi.payments.GetRepeatTypes;
        // Subscription Statuses
        getSubscriptionStatuses = this.cvApi.payments.GetSubscriptionStatuses;
        // Subscription Types
        getSubscriptionTypes = this.cvApi.payments.GetSubscriptionTypes;
        // Price Points
        getPricePoints = this.cvApi.pricing.GetPricePoints;
        // // Future Import Statuses
        // getFutureImportStatuses = this.cvApi.products.GetFutureImportStatuses;
        // Product Association Types
        getProductAssociationTypes = this.cvApi.products.GetProductAssociationTypes;
        // Product Image Types
        getProductImageTypes = this.cvApi.products.GetProductImageTypes;
        // Product Statuses
        getProductStatuses = this.cvApi.products.GetProductStatuses;
        // Product Types
        getProductTypes = this.cvApi.products.GetProductTypes;
        // // Purchase Order States
        // getPurchaseOrderStates = this.cvApi.purchasing.GetPurchaseOrderStates;
        // // Purchase Order Statuses
        // getPurchaseOrderStatuses = this.cvApi.purchasing.GetPurchaseOrderStatuses;
        // // Purchase Order Types
        // getPurchaseOrderTypes = this.cvApi.purchasing.GetPurchaseOrderTypes;
        // Sales Quote States
        getSalesQuoteStates = this.cvApi.quoting.GetSalesQuoteStates;
        // Sales Quote Statuses
        getSalesQuoteStatuses = this.cvApi.quoting.GetSalesQuoteStatuses;
        // Sales Quote Types
        getSalesQuoteTypes = this.cvApi.quoting.GetSalesQuoteTypes;
        // // Report Types
        // getReportTypes = this.cvApi.reporting.GetReportTypes;
        // Sales Return Reasons
        getSalesReturnReasons = this.cvApi.returning.GetSalesReturnReasons;
        // Sales Return States
        getSalesReturnStates = this.cvApi.returning.GetSalesReturnStates;
        // Sales Return Statuses
        getSalesReturnStatuses = this.cvApi.returning.GetSalesReturnStatuses;
        // Sales Return Types
        getSalesReturnTypes = this.cvApi.returning.GetSalesReturnTypes;
        // Review Types
        getReviewTypes = this.cvApi.reviews.GetReviewTypes;
        // Sales Item Target Types
        getSalesItemTargetTypes = this.cvApi.sales.GetSalesItemTargetTypes;
        // Sample Request States
        getSampleRequestStates = this.cvApi.sampling.GetSampleRequestStates;
        // Sample Request Statuses
        getSampleRequestStatuses = this.cvApi.sampling.GetSampleRequestStatuses;
        // Sample Request Types
        getSampleRequestTypes = this.cvApi.sampling.GetSampleRequestTypes;
        // Package Types
        getPackageTypes = this.cvApi.shipping.GetPackageTypes;
        // Shipment Statuses
        getShipmentStatuses = this.cvApi.shipping.GetShipmentStatuses;
        // Shipment Types
        getShipmentTypes = this.cvApi.shipping.GetShipmentTypes;
        // Cart States
        getCartStates = this.cvApi.shopping.GetCartStates;
        // Cart Statuses
        getCartStatuses = this.cvApi.shopping.GetCartStatuses;
        // Cart Types
        getCartTypes = this.cvApi.shopping.GetCartTypes;
        // // Store Image Types
        // getStoreImageTypes = this.cvApi.stores.GetStoreImageTypes;
        // Store Inventory Location Types
        getStoreInventoryLocationTypes = this.cvApi.stores.GetStoreInventoryLocationTypes;
        // Store Types
        getStoreTypes = this.cvApi.stores.GetStoreTypes;
        // Note Types
        getNoteTypes = this.cvApi.structure.GetNoteTypes;
        // // Setting Groups
        // getSettingGroups = this.cvApi.structure.GetSettingGroups;
        // // Setting Types
        // getSettingTypes = this.cvApi.structure.GetSettingTypes;
        // // Campaign Statuses
        // getCampaignStatuses = this.cvApi.tracking.GetCampaignStatuses;
        // // Campaign Types
        // getCampaignTypes = this.cvApi.tracking.GetCampaignTypes;
        // // Event Statuses
        // getEventStatuses = this.cvApi.tracking.GetEventStatuses;
        // // Event Types
        // getEventTypes = this.cvApi.tracking.GetEventTypes;
        // // I P Organization Statuses
        // getIPOrganizationStatuses = this.cvApi.tracking.GetIPOrganizationStatuses;
        // // Page View Statuses
        // getPageViewStatuses = this.cvApi.tracking.GetPageViewStatuses;
        // // Page View Types
        // getPageViewTypes = this.cvApi.tracking.GetPageViewTypes;
        // // Visit Statuses
        // getVisitStatuses = this.cvApi.tracking.GetVisitStatuses;
        // // Vendor Image Types
        // getVendorImageTypes = this.cvApi.vendors.GetVendorImageTypes;
        // // Vendor Types
        // getVendorTypes = this.cvApi.vendors.GetVendorTypes;
    }
}
