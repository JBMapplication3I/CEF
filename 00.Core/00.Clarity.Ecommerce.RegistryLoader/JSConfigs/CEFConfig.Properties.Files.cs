// <copyright file="CEFConfig.Properties.Files.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class.</summary>
// ReSharper disable StyleCop.SA1202, StyleCop.SA1300, StyleCop.SA1303, StyleCop.SA1516, StyleCop.SA1602
#nullable enable
namespace Clarity.Ecommerce.JSConfigs
{
    using System.ComponentModel;

    /// <summary>Dictionary of CEF configurations.</summary>
    public static partial class CEFConfigDictionary
    {
        #region Stored Files
        /// <summary>Gets the full pathname of the stored files internal local file.</summary>
        /// <value>The full pathname of the stored files internal local file.</value>
        [AppSettingsKey("Clarity.Uploads.Files"),
         DefaultValue(null)]
        public static string StoredFilesInternalLocalPath
        {
            get => TryGet(out string asValue)
                ? asValue
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                : throw new System.Configuration.ConfigurationErrorsException(
                    "Clarity.Uploads.Files is required in appSettings.config");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the seo site maps relative file.</summary>
        /// <value>The full pathname of the seo site maps relative file.</value>
        [AppSettingsKey("Clarity.Uploads.SEO.SiteMaps"),
         DefaultValue(null)]
        public static string SEOSiteMapsRelativePath
        {
            get => TryGet(out string asValue)
                ? asValue
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                : throw new System.Configuration.ConfigurationErrorsException(
                    "Clarity.Uploads.SEO.SiteMaps is required in appSettings.config");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path suffix.</summary>
        /// <value>The stored files path suffix.</value>
        [AppSettingsKey("Clarity.Uploads.Files.Suffix"),
         DefaultValue("/Files")]
        public static string StoredFilesPathSuffix
        {
            get => TryGet(out string asValue) ? asValue : "/Files";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path accounts.</summary>
        /// <value>The stored files path accounts.</value>
        [AppSettingsKey("Clarity.Uploads.Files.Account"),
         DefaultValue("/Account")]
        public static string StoredFilesPathAccounts
        {
            get => TryGet(out string asValue) ? asValue : "/Account";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path calendar events.</summary>
        /// <value>The stored files path calendar events.</value>
        [AppSettingsKey("Clarity.Uploads.Files.CalendarEvent"),
         DefaultValue("/CalendarEvent")]
        public static string StoredFilesPathCalendarEvents
        {
            get => TryGet(out string asValue) ? asValue : "/CalendarEvent";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path carts.</summary>
        /// <value>The stored files path carts.</value>
        [AppSettingsKey("Clarity.Uploads.Files.Cart"),
         DefaultValue("/Cart")]
        public static string StoredFilesPathCarts
        {
            get => TryGet(out string asValue) ? asValue : "/Cart";
            private set => TrySet(value);
        }

        /// <summary>Gets the categories the stored files path belongs to.</summary>
        /// <value>The stored files path categories.</value>
        [AppSettingsKey("Clarity.Uploads.Files.Category"),
         DefaultValue("/Category")]
        public static string StoredFilesPathCategories
        {
            get => TryGet(out string asValue) ? asValue : "/Category";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path email queue attachments.</summary>
        /// <value>The stored files path email queue attachments.</value>
        [AppSettingsKey("Clarity.Uploads.Files.EmailQueueAttachment"),
         DefaultValue("/EmailQueueAttachment")]
        public static string StoredFilesPathEmailQueueAttachments
        {
            get => TryGet(out string asValue) ? asValue : "/EmailQueueAttachment";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path message attachments.</summary>
        /// <value>The stored files path message attachments.</value>
        [AppSettingsKey("Clarity.Uploads.Files.MessageAttachment"),
         DefaultValue("/MessageAttachment")]
        public static string StoredFilesPathMessageAttachments
        {
            get => TryGet(out string asValue) ? asValue : "/MessageAttachment";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path products.</summary>
        /// <value>The stored files path products.</value>
        [AppSettingsKey("Clarity.Uploads.Files.Product"),
         DefaultValue("/Product")]
        public static string StoredFilesPathProducts
        {
            get => TryGet(out string asValue) ? asValue : "/Product";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path purchase orders.</summary>
        /// <value>The stored files path purchase orders.</value>
        [AppSettingsKey("Clarity.Uploads.Files.PurchaseOrder"),
         DefaultValue("/PurchaseOrder")]
        public static string StoredFilesPathPurchaseOrders
        {
            get => TryGet(out string asValue) ? asValue : "/PurchaseOrder";
            private set => TrySet(value);
        }

        // See CEFConfig.Properties.SalesInvoices.cs

        // See CEFConfig.Properties.SalesQuotes.cs

        // See CEFConfig.Properties.SalesReturns.cs

        /// <summary>Gets the stored files path sales orders.</summary>
        /// <value>The stored files path sales orders.</value>
        [AppSettingsKey("Clarity.Uploads.Files.SalesOrder"),
         DefaultValue("/SalesOrder")]
        public static string StoredFilesPathSalesOrders
        {
            get => TryGet(out string asValue) ? asValue : "/SalesOrder";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path sample requests.</summary>
        /// <value>The stored files path sample requests.</value>
        [AppSettingsKey("Clarity.Uploads.Files.SampleRequest"),
         DefaultValue("/SampleRequest")]
        public static string StoredFilesPathSampleRequests
        {
            get => TryGet(out string asValue) ? asValue : "/SampleRequest";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path users.</summary>
        /// <value>The stored files path users.</value>
        [AppSettingsKey("Clarity.Uploads.Files.User"),
         DefaultValue("/User")]
        public static string StoredFilesPathUsers
        {
            get => TryGet(out string asValue) ? asValue : "/User";
            private set => TrySet(value);
        }
        #endregion

        #region Imports
        /// <summary>Gets the imports path suffix.</summary>
        /// <value>The imports path suffix.</value>
        [AppSettingsKey("Clarity.Uploads.Imports.Suffix"),
         DefaultValue("/Imports/")]
        public static string ImportsPathSuffix
        {
            get => TryGet(out string asValue) ? asValue : "/Imports/";
            private set => TrySet(value);
        }

        /// <summary>Gets the imports path excels.</summary>
        /// <value>The imports path excels.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Excel"),
         DefaultValue("/Excel")]
        public static string ImportsPathExcels
        {
            get => TryGet(out string asValue) ? asValue : "/Excel";
            private set => TrySet(value);
        }

        /// <summary>Gets the imports path products.</summary>
        /// <value>The imports path products.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Product"),
         DefaultValue("/Product")]
        public static string ImportsPathProducts
        {
            get => TryGet(out string asValue) ? asValue : "/Product";
            private set => TrySet(value);
        }

        /// <summary>Gets the imports path product price points.</summary>
        /// <value>The imports path product price points.</value>
        [AppSettingsKey("Clarity.Uploads.Images.ProductPricePoint"),
         DefaultValue("/ProductPricePoint"),
         DependsOn(nameof(PricingTieredPricingEnabled))]
        public static string? ImportsPathProductPricePoints
        {
            get => PricingTieredPricingEnabled ? TryGet(out string? asValue) ? asValue : "/ProductPricePoint" : null;
            private set => TrySet(value);
        }

        // Sales Quotes Import Path: See CEFConfig.Properties.SalesQuotes.cs

        /// <summary>Gets the imports path users.</summary>
        /// <value>The imports path users.</value>
        [AppSettingsKey("Clarity.Uploads.Images.User"),
         DefaultValue("/User")]
        public static string ImportsPathUsers
        {
            get => TryGet(out string asValue) ? asValue : "/User";
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the products product categories allow resolve by name should be
        /// imported.</summary>
        /// <value>True if import products product categories allow resolve by name, false if not.</value>
        [AppSettingsKey("Clarity.Importing.ProductCategories.AllowResolveByName"),
         DefaultValue(true),
         DependsOn(nameof(CategoriesEnabled))]
        public static bool ImportProductsProductCategoriesAllowResolveByName
        {
            get => CategoriesEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the products product categories allow resolve by seo URL should be
        /// imported.</summary>
        /// <value>True if import products product categories allow resolve by seo url, false if not.</value>
        [AppSettingsKey("Clarity.Importing.ProductCategories.AllowResolveBySeoUrl"),
         DefaultValue(true),
         DependsOn(nameof(CategoriesEnabled))]
        public static bool ImportProductsProductCategoriesAllowResolveBySeoUrl
        {
            get => CategoriesEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the products allow save brand products with brand should be
        /// imported.</summary>
        /// <value>True if import products allow save brand products with brand, false if not.</value>
        [AppSettingsKey("Clarity.Importing.BrandProducts.AllowSaveWithBrand"),
         DefaultValue(false),
         DependsOn(nameof(BrandsEnabled))]
        public static bool ImportProductsAllowSaveBrandProductsWithBrand
        {
            get => BrandsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the products allow save franchise products with franchise should be
        /// imported.</summary>
        /// <value>True if import products allow save franchise products with franchise, false if not.</value>
        [AppSettingsKey("Clarity.Importing.FranchiseProducts.AllowSaveWithFranchise"),
         DefaultValue(false),
         DependsOn(nameof(FranchisesEnabled))]
        public static bool ImportProductsAllowSaveFranchiseProductsWithFranchise
        {
            get => FranchisesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the products allow save store products with store should be
        /// imported.</summary>
        /// <value>True if import products allow save store products with store, false if not.</value>
        [AppSettingsKey("Clarity.Importing.StoreProducts.AllowSaveWithStore"),
         DefaultValue(false),
         DependsOn(nameof(StoresEnabled))]
        public static bool ImportProductsAllowSaveStoreProductsWithStore
        {
            get => StoresEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the name of the import users alternate custom key column.</summary>
        /// <value>The name of the import users alternate custom key column.</value>
        [AppSettingsKey("Clarity.Importing.Users.AlternateCustomKeyColumnName"),
         DefaultValue(null)]
        public static string? ImportUsersAlternateCustomKeyColumnName
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion

        #region Images
        /// <summary>Gets the images path suffix.</summary>
        /// <value>The images path suffix.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Suffix"),
         DefaultValue("/Images/")]
        public static string ImagesPathSuffix
        {
            get => TryGet(out string asValue) ? asValue : "/Images/";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path accounts.</summary>
        /// <value>The images path accounts.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Account"),
         DefaultValue("/Account")]
        public static string ImagesPathAccounts
        {
            get => TryGet(out string asValue) ? asValue : "/Account";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path ads.</summary>
        /// <value>The images path ads.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Ad"),
         DefaultValue("/Ad"),
         DependsOn(nameof(AdsEnabled))]
        public static string? ImagesPathAds
        {
            get => AdsEnabled ? TryGet(out string? asValue) ? asValue : "/Ad" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the images path brands.</summary>
        /// <value>The images path brands.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Brand"),
         DefaultValue("/Brand"),
         DependsOn(nameof(BrandsEnabled))]
        public static string? ImagesPathBrands
        {
            get => BrandsEnabled ? TryGet(out string? asValue) ? asValue : "/Brand" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the images path brands.</summary>
        /// <value>The images path brands.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Franchise"),
         DefaultValue("/Franchise"),
         DependsOn(nameof(FranchisesEnabled))]
        public static string? ImagesPathFranchises
        {
            get => FranchisesEnabled ? TryGet(out string? asValue) ? asValue : "/Franchise" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the images path calendar events.</summary>
        /// <value>The images path calendar events.</value>
        [AppSettingsKey("Clarity.Uploads.Images.CalendarEvent"),
         DefaultValue("/CalendarEvent"),
         DependsOn(nameof(CalendarEventsEnabled))]
        public static string? ImagesPathCalendarEvents
        {
            get => CalendarEventsEnabled ? TryGet(out string? asValue) ? asValue : "/CalendarEvent" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the categories the images path belongs to.</summary>
        /// <value>The images path categories.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Category"),
         DefaultValue("/Category"),
         DependsOn(nameof(CategoriesEnabled))]
        public static string? ImagesPathCategories
        {
            get => CategoriesEnabled ? TryGet(out string? asValue) ? asValue : "/Category" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the images path countries.</summary>
        /// <value>The images path countries.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Country"),
         DefaultValue("/Country")]
        public static string ImagesPathCountries
        {
            get => TryGet(out string asValue) ? asValue : "/Country";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path currencies.</summary>
        /// <value>The images path currencies.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Currency"),
         DefaultValue("/Currency")]
        public static string ImagesPathCurrencies
        {
            get => TryGet(out string asValue) ? asValue : "/Currency";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path languages.</summary>
        /// <value>The images path languages.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Language"),
         DefaultValue("/Language")]
        public static string ImagesPathLanguages
        {
            get => TryGet(out string asValue) ? asValue : "/Language";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path manufacturers.</summary>
        /// <value>The images path manufacturers.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Manufacturer"),
         DefaultValue("/Manufacturer"),
         DependsOn(nameof(ManufacturersEnabled))]
        public static string? ImagesPathManufacturers
        {
            get => ManufacturersEnabled ? TryGet(out string? asValue) ? asValue : "/Manufacturer" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the images path products.</summary>
        /// <value>The images path products.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Product"),
         DefaultValue("/Product")]
        public static string ImagesPathProducts
        {
            get => TryGet(out string asValue) ? asValue : "/Product";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path regions.</summary>
        /// <value>The images path regions.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Region"),
         DefaultValue("/Region")]
        public static string ImagesPathRegions
        {
            get => TryGet(out string asValue) ? asValue : "/Region";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path stores.</summary>
        /// <value>The images path stores.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Store"),
         DefaultValue("/Store"),
         DependsOn(nameof(StoresEnabled))]
        public static string? ImagesPathStores
        {
            get => StoresEnabled ? TryGet(out string? asValue) ? asValue : "/Store" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the images path users.</summary>
        /// <value>The images path users.</value>
        [AppSettingsKey("Clarity.Uploads.Images.User"),
         DefaultValue("/User")]
        public static string ImagesPathUsers
        {
            get => TryGet(out string asValue) ? asValue : "/User";
            private set => TrySet(value);
        }

        /// <summary>Gets the images path vendors.</summary>
        /// <value>The images path vendors.</value>
        [AppSettingsKey("Clarity.Uploads.Images.Vendor"),
         DefaultValue("/Vendor"),
         DependsOn(nameof(VendorsEnabled))]
        public static string? ImagesPathVendors
        {
            get => VendorsEnabled ? TryGet(out string? asValue) ? asValue : "/Vendor" : null;
            private set => TrySet(value);
        }
        #endregion
    }
}
