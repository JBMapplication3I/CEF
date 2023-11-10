// <copyright file="FeatureSet.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
#pragma warning disable IDE1006 // Inconsistent Naming
// ReSharper disable InconsistentNaming, MissingXmlDoc
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class FeatureSet
    {
        public SimpleEnablableFeature? ads { get; set; }

        public AddressBookFeatureSet? addressBook { get; set; }

        public SimpleEnablableFeature? badges { get; set; }

        public SimpleEnablableFeature? brands { get; set; }

        public SimpleEnablableFeature? franchises { get; set; }

        public SimpleEnablableFeature? calendarEvents { get; set; }

        public CartsFeatureSet? carts { get; set; }

        public CategoriesFeatureSet? categories { get; set; }

        public SimpleEnablableFeature? chat { get; set; }

        public SimpleEnablableFeature? contacts_phonePrefixLookups { get; set; }

        public SimpleEnablableFeature? discounts { get; set; }

        public bool emails_enabled { get; set; }

        public SimpleEnablableFeature? emails_splitTemplates { get; set; }

        public InventoryConfig? inventory { get; set; }

        public bool languages_enabled { get; set; }

        public string? languages_default { get; set; }

        public SimpleEnablableFeature? login { get; set; }

        public SimpleEnablableFeature? manufacturers { get; set; }

        public SimpleEnablableFeature? multiCurrency { get; set; }

        public SimpleEnablableFeature? nonProductFavorites { get; set; }

        public bool payments_enabled { get; set; }

        public SimpleEnablableFeature? payments_memberships { get; set; }

        public PaymentConfig? payments_methods_creditCard { get; set; }

        public PaymentConfig? payments_methods_eCheck { get; set; }

        public PaymentConfig? payments_methods_credit { get; set; }

        public SimpleEnablableFeature? payments_subscriptions { get; set; }

        public bool payments_wallet_enabled { get; set; }

        public SimpleEnablableFeature? payments_wallet_creditCard { get; set; }

        public SimpleEnablableFeature? payments_wallet_eCheck { get; set; }

        public PricingConfig? pricing { get; set; }

        public SimpleEnablableFeature? products_categoryAttributes { get; set; }

        public SimpleEnablableFeature? products_futureImports { get; set; }

        public SimpleEnablableFeature? products_notifications { get; set; }

        public SimpleEnablableFeature? products_restrictions { get; set; }

        public SimpleEnablableFeature? products_storedFiles { get; set; }

        public SimpleEnablableFeature? profanityFilter { get; set; }

        public bool profile_enabled { get; set; }

        public SimpleEnablableFeature? profile_images { get; set; }

        public SimpleEnablableFeature? profile_storedFiles { get; set; }

        public bool purchaseOrders_enabled { get; set; }

        public bool purchaseOrders_hasIntegratedKeys { get; set; }

        public PurchasingConfig? purchasing { get; set; }

        public SimpleEnablableFeature? referralRegistrations { get; set; }

        public bool registration_usernameIsEmail { get; set; }

        public bool registration_addressBookIsRequired { get; set; }

        public bool registration_walletIsRequired { get; set; }

        public SimpleEnablableFeature? reporting { get; set; }

        public SimpleEnablableFeature? reviews { get; set; }

        public bool salesGroups_enabled { get; set; }

        public bool salesGroups_hasIntegratedKeys { get; set; }

        public bool salesInvoices_enabled { get; set; }

        public bool salesInvoices_hasIntegratedKeys { get; set; }

        public bool salesOrders_enabled { get; set; }

        public bool salesOrders_hasIntegratedKeys { get; set; }

        public bool salesQuotes_enabled { get; set; }

        public bool salesQuotes_hasIntegratedKeys { get; set; }

        public bool salesQuotes_includeQuantity { get; set; }

        public string? salesQuotes_quoteCartUrlFragment { get; set; }

        public bool salesReturns_enabled { get; set; }

        public bool salesReturns_hasIntegratedKeys { get; set; }

        public bool sampleRequests_enabled { get; set; }

        public bool sampleRequests_hasIntegratedKeys { get; set; }

        public ShippingConfig? shipping { get; set; }

        public bool stores_enabled { get; set; }

        public SimpleEnablableFeature? stores_myStoreAdmin { get; set; }

        public SimpleEnablableFeature? stores_myBrandAdmin { get; set; }

        public SimpleEnablableFeature? stores_siteDomains { get; set; }

        public SimpleEnablableFeature? stores_socialProviders { get; set; }

        public string? stores_storeDetailUrlFragment { get; set; }

        public string? stores_storeLocatorUrlFragment { get; set; }

        public TaxesConfig? taxes { get; set; }

        public bool tracking_enabled { get; set; }

        public int tracking_expires { get; set; }

        public SimpleEnablableFeature? vendors { get; set; }
    }
}
