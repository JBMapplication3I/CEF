// <copyright file="CEFConfig.Properties.Routes.cs" company="clarity-ventures.com">
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
        /// <summary>Gets URL of the API storefront route host.</summary>
        /// <value>The API storefront route host URL.</value>
        [AppSettingsKey("Clarity.API-Storefront.Requests.RootUrl"),
         DefaultValue(null)]
        public static string? APIStorefrontRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the API storefront route relative file.</summary>
        /// <value>The full pathname of the API storefront route relative file.</value>
        [AppSettingsKey("Clarity.API-Storefront.Requests.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/API-Storefront")]
        public static string? APIStorefrontRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : "/DesktopModules/ClarityEcommerce/API-Storefront";
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the API store admin route host.</summary>
        /// <value>The API store admin route host URL.</value>
        [AppSettingsKey("Clarity.API-StoreAdmin.Requests.RootUrl"),
         DefaultValue(null)]
        public static string? APIStoreAdminRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the API store admin route relative file.</summary>
        /// <value>The full pathname of the API store admin route relative file.</value>
        [AppSettingsKey("Clarity.API-StoreAdmin.Requests.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/API-StoreAdmin")]
        public static string? APIStoreAdminRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : "/DesktopModules/ClarityEcommerce/API-StoreAdmin";
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the API brand admin route host.</summary>
        /// <value>The API brand admin route host URL.</value>
        [AppSettingsKey("Clarity.API-BrandAdmin.Requests.RootUrl"),
         DefaultValue(null)]
        public static string? APIBrandAdminRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the API brand admin route relative file.</summary>
        /// <value>The full pathname of the API brand admin route relative file.</value>
        [AppSettingsKey("Clarity.API-BrandAdmin.Requests.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/API-BrandAdmin")]
        public static string? APIBrandAdminRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : "/DesktopModules/ClarityEcommerce/API-BrandAdmin";
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the API franchise admin route host.</summary>
        /// <value>The API franchise admin route host URL.</value>
        [AppSettingsKey("Clarity.API-FranchiseAdmin.Requests.RootUrl"),
         DefaultValue(null)]
        public static string? APIFranchiseAdminRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the API franchise admin route relative file.</summary>
        /// <value>The full pathname of the API franchise admin route relative file.</value>
        [AppSettingsKey("Clarity.API-FranchiseAdmin.Requests.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/API-FranchiseAdmin")]
        public static string? APIFranchiseAdminRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : "/DesktopModules/ClarityEcommerce/API-FranchiseAdmin";
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the API manufacturer admin route host.</summary>
        /// <value>The API manufacturer admin route host URL.</value>
        [AppSettingsKey("Clarity.API-ManufacturerAdmin.Requests.RootUrl"),
         DefaultValue(null)]
        public static string? APIManufacturerAdminRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the API manufacturer admin route relative file.</summary>
        /// <value>The full pathname of the API manufacturer admin route relative file.</value>
        [AppSettingsKey("Clarity.API-ManufacturerAdmin.Requests.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/API-ManufacturerAdmin")]
        public static string? APIManufacturerAdminRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : "/DesktopModules/ClarityEcommerce/API-ManufacturerAdmin";
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the API vendor admin route host.</summary>
        /// <value>The API vendor admin route host URL.</value>
        [AppSettingsKey("Clarity.API-VendorAdmin.Requests.RootUrl"),
         DefaultValue(null)]
        public static string? APIVendorAdminRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the API vendor admin route relative file.</summary>
        /// <value>The full pathname of the API vendor admin route relative file.</value>
        [AppSettingsKey("Clarity.API-VendorAdmin.Requests.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/API-VendorAdmin")]
        public static string? APIVendorAdminRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : "/DesktopModules/ClarityEcommerce/API-VendorAdmin";
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the API admin route host.</summary>
        /// <value>The API admin route host URL.</value>
        [AppSettingsKey("Clarity.API-Admin.Requests.RootUrl"),
         DefaultValue(null)]
        public static string? APIAdminRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the API admin route relative file.</summary>
        /// <value>The full pathname of the API admin route relative file.</value>
        [AppSettingsKey("Clarity.API-Admin.Requests.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/API-Admin")]
        public static string? APIAdminRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : "/DesktopModules/ClarityEcommerce/API-Admin";
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the site route host.</summary>
        /// <value>The site route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Site.RootUrl"),
         DefaultValue(null)]
        public static string SiteRouteHostUrl
        {
            get => TryGet(out string asValue)
                ? asValue
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                : throw new System.Configuration.ConfigurationErrorsException(
                    "The SiteRouteHostUrl setting is required");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            private set => TrySet(value);
        }

        /// <summary>Gets the site route host URL ssl.</summary>
        /// <value>The site route host URL ssl.</value>
        [AppSettingsKey("Clarity.Routes.Site.RootUrlSSL"),
         DefaultValue(null)]
        public static string? SiteRouteHostUrlSSL
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the site route relative file.</summary>
        /// <value>The full pathname of the site route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Site.RelativePath"),
         DefaultValue(null)]
        public static string? SiteRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the site route host lookup method.</summary>
        /// <value>The site route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Site.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod SiteRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the site route host lookup which.</summary>
        /// <value>The site route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Site.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? SiteRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the route host.</summary>
        /// <value>The user interface route host URL.</value>
        [AppSettingsKey("Clarity.Routes.UI.RootUrl"),
         DefaultValue(null)]
        public static string? UIRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the route relative file.</summary>
        /// <value>The full pathname of the route relative file.</value>
        [AppSettingsKey("Clarity.Routes.UI.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/UI-Storefront")]
        public static string UIRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/DesktopModules/ClarityEcommerce/UI-Storefront";
            private set => TrySet(value);
        }

        /// <summary>Gets the route host lookup method.</summary>
        /// <value>The user interface route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.UI.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod UIRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the route host lookup which.</summary>
        /// <value>The user interface route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.UI.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? UIRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the route host.</summary>
        /// <value>The user interface route host URL.</value>
        [AppSettingsKey("Clarity.Routes.UI-Admin.RootUrl"),
         DefaultValue(null)]
        public static string? UIAdminRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the route relative file.</summary>
        /// <value>The full pathname of the route relative file.</value>
        [AppSettingsKey("Clarity.Routes.UI-Admin.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/UI-Admin")]
        public static string UIAdminRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/DesktopModules/ClarityEcommerce/UI-Admin";
            private set => TrySet(value);
        }

        /// <summary>Gets the route host lookup method.</summary>
        /// <value>The user interface route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.UI-Admin.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod UIAdminRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the route host lookup which.</summary>
        /// <value>The user interface route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.UI-Admin.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? UIAdminRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the template override route host.</summary>
        /// <value>The user interface template override route host URL.</value>
        [AppSettingsKey("Clarity.Routes.UITemplateOverride.RootUrl"),
         DefaultValue(null)]
        public static string? UITemplateOverrideRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the template override route relative file.</summary>
        /// <value>The full pathname of the template override route relative file.</value>
        [AppSettingsKey("Clarity.Routes.UITemplateOverride.RelativePath"),
         DefaultValue("/Portals/_default/Skins/Clarity/Ecommerce/framework")]
        public static string UITemplateOverrideRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Portals/_default/Skins/Clarity/Ecommerce/framework";
            private set => TrySet(value);
        }

        /// <summary>Gets the template override route host lookup method.</summary>
        /// <value>The user interface template override route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.UITemplateOverride.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod UITemplateOverrideRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the template override route host lookup which.</summary>
        /// <value>The user interface template override route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.UITemplateOverride.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? UITemplateOverrideRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the template override route host.</summary>
        /// <value>The user interface template override route host URL.</value>
        [AppSettingsKey("Clarity.Routes.UIAdminTemplateOverride.RootUrl"),
         DefaultValue(null)]
        public static string? UIAdminTemplateOverrideRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the template override route relative file.</summary>
        /// <value>The full pathname of the template override route relative file.</value>
        [AppSettingsKey("Clarity.Routes.UIAdminTemplateOverride.RelativePath"),
         DefaultValue("/Portals/_default/Skins/Clarity/Ecommerce/framework")]
        public static string UIAdminTemplateOverrideRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Portals/_default/Skins/Clarity/Ecommerce/framework";
            private set => TrySet(value);
        }

        /// <summary>Gets the template override route host lookup method.</summary>
        /// <value>The user interface template override route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.UIAdminTemplateOverride.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod UIAdminTemplateOverrideRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the template override route host lookup which.</summary>
        /// <value>The user interface template override route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.UIAdminTemplateOverride.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? UIAdminTemplateOverrideRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether my store admin is enabled.</summary>
        /// <value>True if my store admin enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Stores.MyStoreAdmin.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(StoresEnabled))]
        public static bool MyStoreAdminEnabled
        {
            get => StoresEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my store admin route host.</summary>
        /// <value>my store admin route host URL.</value>
        [AppSettingsKey("Clarity.Routes.MyStoreAdmin.RootUrl"),
         DefaultValue(null),
         DependsOn(nameof(MyStoreAdminEnabled))]
        public static string? MyStoreAdminRouteHostUrl
        {
            get => MyStoreAdminEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of my store admin route relative file.</summary>
        /// <value>The full pathname of my store admin route relative file.</value>
        [AppSettingsKey("Clarity.Routes.MyStoreAdmin.RelativePath"),
         DefaultValue("/My-Store"),
         DependsOn(nameof(MyStoreAdminEnabled))]
        public static string? MyStoreAdminRouteRelativePath
        {
            get => MyStoreAdminEnabled ? TryGet(out string? asValue) ? asValue : "/My-Store" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets my store admin route host lookup method.</summary>
        /// <value>my store admin route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.MyStoreAdmin.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         DependsOn(nameof(MyStoreAdminEnabled))]
        public static Enums.HostLookupMethod MyStoreAdminRouteHostLookupMethod
        {
            get => MyStoreAdminEnabled
                ? TryGet<Enums.HostLookupMethod>(out var asValue)
                    ? asValue
                    : Enums.HostLookupMethod.NotByLookup
                : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my store admin route host lookup which.</summary>
        /// <value>my store admin route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.MyStoreAdmin.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         DependsOn(nameof(MyStoreAdminEnabled))]
        public static Enums.HostLookupWhichUrl? MyStoreAdminRouteHostLookupWhichUrl
        {
            get => MyStoreAdminEnabled
                ? TryGet<Enums.HostLookupWhichUrl>(out var asValue)
                    ? asValue
                    : Enums.HostLookupWhichUrl.Primary
                : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether my brand admin is enabled.</summary>
        /// <value>True if my brand admin enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Brands.MyBrandAdmin.Enabled"),
         DefaultValue(false)]
        public static bool MyBrandAdminEnabled
        {
            get => BrandsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my brand admin route host.</summary>
        /// <value>my brand admin route host URL.</value>
        [AppSettingsKey("Clarity.Routes.MyBrandAdmin.RootUrl"),
         DefaultValue(null),
         DependsOn(nameof(MyBrandAdminEnabled))]
        public static string? MyBrandAdminRouteHostUrl
        {
            get => MyBrandAdminEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of my brand admin route relative file.</summary>
        /// <value>The full pathname of my brand admin route relative file.</value>
        [AppSettingsKey("Clarity.Routes.MyBrandAdmin.RelativePath"),
         DefaultValue("/My-Brand"),
         DependsOn(nameof(MyBrandAdminEnabled))]
        public static string? MyBrandAdminRouteRelativePath
        {
            get => MyBrandAdminEnabled ? TryGet(out string? asValue) ? asValue : "/My-Brand" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets my brand admin route host lookup method.</summary>
        /// <value>my brand admin route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.MyBrandAdmin.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         DependsOn(nameof(MyBrandAdminEnabled))]
        public static Enums.HostLookupMethod MyBrandAdminRouteHostLookupMethod
        {
            get => MyBrandAdminEnabled
                ? TryGet<Enums.HostLookupMethod>(out var asValue)
                    ? asValue
                    : Enums.HostLookupMethod.NotByLookup
                : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my brand admin route host lookup which.</summary>
        /// <value>my brand admin route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.MyBrandAdmin.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         DependsOn(nameof(MyBrandAdminEnabled))]
        public static Enums.HostLookupWhichUrl? MyBrandAdminRouteHostLookupWhichUrl
        {
            get => MyFranchiseAdminEnabled
                ? TryGet<Enums.HostLookupWhichUrl>(out var asValue)
                    ? asValue
                    : Enums.HostLookupWhichUrl.Primary
                : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether my franchise admin is enabled.</summary>
        /// <value>True if my franchise admin enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Franchises.MyFranchiseAdmin.Enabled"),
         DefaultValue(false)]
        public static bool MyFranchiseAdminEnabled
        {
            get => FranchisesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my franchise admin route host.</summary>
        /// <value>my franchise admin route host URL.</value>
        [AppSettingsKey("Clarity.Routes.MyFranchiseAdmin.RootUrl"),
         DefaultValue(null),
         DependsOn(nameof(MyFranchiseAdminEnabled))]
        public static string? MyFranchiseAdminRouteHostUrl
        {
            get => MyFranchiseAdminEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of my franchise admin route relative file.</summary>
        /// <value>The full pathname of my franchise admin route relative file.</value>
        [AppSettingsKey("Clarity.Routes.MyFranchiseAdmin.RelativePath"),
         DefaultValue("/My-Franchise"),
         DependsOn(nameof(MyFranchiseAdminEnabled))]
        public static string? MyFranchiseAdminRouteRelativePath
        {
            get => MyFranchiseAdminEnabled ? TryGet(out string? asValue) ? asValue : "/My-Franchise" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets my franchise admin route host lookup method.</summary>
        /// <value>my franchise admin route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.MyFranchiseAdmin.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         DependsOn(nameof(MyFranchiseAdminEnabled))]
        public static Enums.HostLookupMethod MyFranchiseAdminRouteHostLookupMethod
        {
            get => MyFranchiseAdminEnabled
                ? TryGet<Enums.HostLookupMethod>(out var asValue)
                    ? asValue
                    : Enums.HostLookupMethod.NotByLookup
                : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my franchise admin route host lookup which.</summary>
        /// <value>my franchise admin route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.MyFranchiseAdmin.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         DependsOn(nameof(MyFranchiseAdminEnabled))]
        public static Enums.HostLookupWhichUrl? MyFranchiseAdminRouteHostLookupWhichUrl
        {
            get => MyFranchiseAdminEnabled
                ? TryGet<Enums.HostLookupWhichUrl>(out var asValue)
                    ? asValue
                    : Enums.HostLookupWhichUrl.Primary
                : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether my manufacturer admin is enabled.</summary>
        /// <value>True if my manufacturer admin enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Manufacturers.MyManufacturerAdmin.Enabled"),
         DefaultValue(false)]
        public static bool MyManufacturerAdminEnabled
        {
            get => ManufacturersEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my manufacturer admin route host.</summary>
        /// <value>my manufacturer admin route host URL.</value>
        [AppSettingsKey("Clarity.Routes.MyManufacturerAdmin.RootUrl"),
         DefaultValue(null),
         DependsOn(nameof(MyManufacturerAdminEnabled))]
        public static string? MyManufacturerAdminRouteHostUrl
        {
            get => MyManufacturerAdminEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of my manufacturer admin route relative file.</summary>
        /// <value>The full pathname of my manufacturer admin route relative file.</value>
        [AppSettingsKey("Clarity.Routes.MyManufacturerAdmin.RelativePath"),
         DefaultValue("/My-Manufacturer"),
         DependsOn(nameof(MyManufacturerAdminEnabled))]
        public static string? MyManufacturerAdminRouteRelativePath
        {
            get => MyManufacturerAdminEnabled ? TryGet(out string? asValue) ? asValue : "/My-Manufacturer" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets my manufacturer admin route host lookup method.</summary>
        /// <value>my manufacturer admin route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.MyManufacturerAdmin.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         DependsOn(nameof(MyManufacturerAdminEnabled))]
        public static Enums.HostLookupMethod MyManufacturerAdminRouteHostLookupMethod
        {
            get => MyManufacturerAdminEnabled
                ? TryGet<Enums.HostLookupMethod>(out var asValue)
                    ? asValue
                    : Enums.HostLookupMethod.NotByLookup
                : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my manufacturer admin route host lookup which.</summary>
        /// <value>my manufacturer admin route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.MyManufacturerAdmin.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         DependsOn(nameof(MyManufacturerAdminEnabled))]
        public static Enums.HostLookupWhichUrl? MyManufacturerAdminRouteHostLookupWhichUrl
        {
            get => MyManufacturerAdminEnabled
                ? TryGet<Enums.HostLookupWhichUrl>(out var asValue)
                    ? asValue
                    : Enums.HostLookupWhichUrl.Primary
                : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether my vendor admin is enabled.</summary>
        /// <value>True if my vendor admin enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Vendors.MyVendorAdmin.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(VendorsEnabled))]
        public static bool MyVendorAdminEnabled
        {
            get => VendorsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my vendor admin route host.</summary>
        /// <value>my vendor admin route host URL.</value>
        [AppSettingsKey("Clarity.Routes.MyVendorAdmin.RootUrl"),
         DefaultValue(null),
         DependsOn(nameof(MyVendorAdminEnabled))]
        public static string? MyVendorAdminRouteHostUrl
        {
            get => MyVendorAdminEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of my vendor admin route relative file.</summary>
        /// <value>The full pathname of my vendor admin route relative file.</value>
        [AppSettingsKey("Clarity.Routes.MyVendorAdmin.RelativePath"),
         DefaultValue("/"),
         DependsOn(nameof(MyVendorAdminEnabled))]
        public static string? MyVendorAdminRouteRelativePath
        {
            get => MyVendorAdminEnabled ? TryGet(out string? asValue) ? asValue : "/" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets my vendor admin route host lookup method.</summary>
        /// <value>my vendor admin route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.MyVendorAdmin.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         DependsOn(nameof(MyVendorAdminEnabled))]
        public static Enums.HostLookupMethod MyVendorAdminRouteHostLookupMethod
        {
            get => MyVendorAdminEnabled
                ? TryGet<Enums.HostLookupMethod>(out var asValue)
                    ? asValue
                    : Enums.HostLookupMethod.NotByLookup
                : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of my vendor admin route host lookup which.</summary>
        /// <value>my vendor admin route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.MyVendorAdmin.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         DependsOn(nameof(MyVendorAdminEnabled))]
        public static Enums.HostLookupWhichUrl? MyVendorAdminRouteHostLookupWhichUrl
        {
            get => MyVendorAdminEnabled
                ? TryGet<Enums.HostLookupWhichUrl>(out var asValue)
                    ? asValue
                    : Enums.HostLookupWhichUrl.Primary
                : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the connect live route host.</summary>
        /// <value>The connect live route host URL.</value>
        [AppSettingsKey("Clarity.Routes.ConnectLive.RootUrl"),
         DefaultValue(null)]
        public static string? ConnectLiveRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the connect live route relative file.</summary>
        /// <value>The full pathname of the connect live route relative file.</value>
        [AppSettingsKey("Clarity.Routes.ConnectLive.RelativePath"),
         DefaultValue("/Connect-Live")]
        public static string ConnectLiveRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Connect-Live";
            private set => TrySet(value);
        }

        /// <summary>Gets the connect live route host lookup method.</summary>
        /// <value>The connect live route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.ConnectLive.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ConnectLiveRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the connect live route host lookup which.</summary>
        /// <value>The connect live route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.ConnectLive.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ConnectLiveRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the admin route host.</summary>
        /// <value>The admin route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Admin.RootUrl"),
         DefaultValue(null)]
        public static string? AdminRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the admin route relative file.</summary>
        /// <value>The full pathname of the admin route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Admin.RelativePath"),
         DefaultValue("/Admin/Clarity-Ecommerce-Admin")]
        public static string AdminRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Admin/Clarity-Ecommerce-Admin";
            private set => TrySet(value);
        }

        /// <summary>Gets the admin route host lookup method.</summary>
        /// <value>The admin route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Admin.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod AdminRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the admin route host lookup which.</summary>
        /// <value>The admin route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Admin.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? AdminRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>The Relative URL to the Checkout page Do not leave a trailing slash.</summary>
        /// <value>The checkout route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Checkout.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? CheckoutRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the checkout route relative file.</summary>
        /// <value>The full pathname of the checkout route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Checkout.RelativePath"),
         DefaultValue("/Checkout")]
        public static string CheckoutRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Checkout";
            private set => TrySet(value);
        }

        /// <summary>Gets the checkout route host lookup method.</summary>
        /// <value>The checkout route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Checkout.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod CheckoutRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the checkout route host lookup which.</summary>
        /// <value>The checkout route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Checkout.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? CheckoutRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>The Relative URL to the root of the Catalog.</summary>
        /// <value>The catalog route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Catalog.RootUrl"),
         DefaultValue(null)]
        public static string? CatalogRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the catalog route relative file.</summary>
        /// <value>The full pathname of the catalog route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Catalog.RelativePath"),
         DefaultValue("/Catalog")]
        public static string CatalogRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Catalog";
            private set => TrySet(value);
        }

        /// <summary>Gets the catalog route host lookup method.</summary>
        /// <value>The catalog route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Catalog.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod CatalogRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the catalog route host lookup which.</summary>
        /// <value>The catalog route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Catalog.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? CatalogRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the product detail route host.</summary>
        /// <value>The product detail route host URL.</value>
        [AppSettingsKey("Clarity.Routes.ProductDetail.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? ProductDetailRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the product detail route relative file.</summary>
        /// <value>The full pathname of the product detail route relative file.</value>
        [AppSettingsKey("Clarity.Routes.ProductDetail.RelativePath"),
         DefaultValue("/Product")]
        public static string ProductDetailRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Product";
            private set => TrySet(value);
        }

        /// <summary>Gets the product detail route host lookup method.</summary>
        /// <value>The product detail route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.ProductDetail.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod ProductDetailRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the product detail route host lookup which.</summary>
        /// <value>The product detail route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.ProductDetail.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? ProductDetailRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the store detail route host.</summary>
        /// <value>The store detail route host URL.</value>
        [AppSettingsKey("Clarity.Routes.StoreDetail.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? StoreDetailRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the store detail route relative file.</summary>
        /// <value>The full pathname of the store detail route relative file.</value>
        [AppSettingsKey("Clarity.Routes.StoreDetail.RelativePath"),
         DefaultValue("/Store")]
        public static string StoreDetailRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Store";
            private set => TrySet(value);
        }

        /// <summary>Gets the store detail route host lookup method.</summary>
        /// <value>The store detail route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.StoreDetail.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod StoreDetailRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the store detail route host lookup which.</summary>
        /// <value>The store detail route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.StoreDetail.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? StoreDetailRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the store locator route host.</summary>
        /// <value>The store locator route host URL.</value>
        [AppSettingsKey("Clarity.Routes.StoreLocator.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? StoreLocatorRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the store locator route relative file.</summary>
        /// <value>The full pathname of the store locator route relative file.</value>
        [AppSettingsKey("Clarity.Routes.StoreLocator.RelativePath"),
         DefaultValue("/Store-Locator")]
        public static string StoreLocatorRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Store-Locator";
            private set => TrySet(value);
        }

        /// <summary>Gets the store locator route host lookup method.</summary>
        /// <value>The store locator route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.StoreLocator.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod StoreLocatorRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the store locator route host lookup which.</summary>
        /// <value>The store locator route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.StoreLocator.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? StoreLocatorRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the category route host.</summary>
        /// <value>The category route host URL.</value>
        [AppSettingsKey("Clarity.Routes.CategoryDetail.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? CategoryRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the category route relative file.</summary>
        /// <value>The full pathname of the category route relative file.</value>
        [AppSettingsKey("Clarity.Routes.CategoryDetail.RelativePath"),
         DefaultValue("/Categories")]
        public static string CategoryRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Category";
            private set => TrySet(value);
        }

        /// <summary>Gets the category route host lookup method.</summary>
        /// <value>The category route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.CategoryDetail.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod CategoryRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the category route host lookup which.</summary>
        /// <value>The category route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.CategoryDetail.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? CategoryRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the dashboard route host.</summary>
        /// <value>The dashboard route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Dashboard.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? DashboardRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the dashboard route relative file.</summary>
        /// <value>The full pathname of the dashboard route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Dashboard.RelativePath"),
         DefaultValue("/Dashboard")]
        public static string DashboardRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Dashboard";
            private set => TrySet(value);
        }

        /// <summary>Gets the dashboard route host lookup method.</summary>
        /// <value>The dashboard route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Dashboard.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod DashboardRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the dashboard route host lookup which.</summary>
        /// <value>The dashboard route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Dashboard.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? DashboardRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the cart route host.</summary>
        /// <value>The cart route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Cart.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? CartRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the cart route relative file.</summary>
        /// <value>The full pathname of the cart route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Cart.RelativePath"),
         DefaultValue("/Cart")]
        public static string CartRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Cart";
            private set => TrySet(value);
        }

        /// <summary>Gets the cart route host lookup method.</summary>
        /// <value>The cart route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Cart.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod CartRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the cart route host lookup which.</summary>
        /// <value>The cart route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Cart.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? CartRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the terms route host.</summary>
        /// <value>The terms route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Terms.RootUrl"),
         DefaultValue(null)]
        public static string? TermsRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the terms route relative file.</summary>
        /// <value>The full pathname of the terms route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Terms.RelativePath"),
         DefaultValue("/Terms")]
        public static string TermsRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Terms";
            private set => TrySet(value);
        }

        /// <summary>Gets the terms route host lookup method.</summary>
        /// <value>The terms route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Terms.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod TermsRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the terms route host lookup which.</summary>
        /// <value>The terms route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Terms.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? TermsRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the privacy route host.</summary>
        /// <value>The privacy route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Privacy.RootUrl"),
         DefaultValue(null)]
        public static string? PrivacyRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the privacy route relative file.</summary>
        /// <value>The full pathname of the privacy route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Privacy.RelativePath"),
         DefaultValue("/Privacy")]
        public static string PrivacyRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Privacy";
            private set => TrySet(value);
        }

        /// <summary>Gets the privacy route host lookup method.</summary>
        /// <value>The privacy route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Privacy.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod PrivacyRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the privacy route host lookup which.</summary>
        /// <value>The privacy route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Privacy.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? PrivacyRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the contact us route host.</summary>
        /// <value>The contact us route host URL.</value>
        [AppSettingsKey("Clarity.Routes.ContactUs.RootUrl"),
         DefaultValue(null)]
        public static string? ContactUsRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the contact us route relative file.</summary>
        /// <value>The full pathname of the contact us route relative file.</value>
        [AppSettingsKey("Clarity.Routes.ContactUs.RelativePath"),
         DefaultValue("/Info/Contact-Us")]
        public static string ContactUsRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Info/Contact-Us";
            private set => TrySet(value);
        }

        /// <summary>Gets the contact us route host lookup method.</summary>
        /// <value>The contact us route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.ContactUs.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ContactUsRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the contact us route host lookup which.</summary>
        /// <value>The contact us route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.ContactUs.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ContactUsRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the login route host.</summary>
        /// <value>The login route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Login.RootUrl"),
         DefaultValue(null)]
        public static string? LoginRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the login route relative file.</summary>
        /// <value>The full pathname of the login route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Login.RelativePath"),
         DefaultValue("/Authentication/Sign-In")]
        public static string LoginRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Authentication/Sign-In";
            private set => TrySet(value);
        }

        /// <summary>Gets the login route host lookup method.</summary>
        /// <value>The login route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Login.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod LoginRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the login route host lookup which.</summary>
        /// <value>The login route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Login.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? LoginRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the registration route host.</summary>
        /// <value>The registration route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Registration.RootUrl"),
         DefaultValue(null)]
        public static string? RegistrationRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the registration route relative file.</summary>
        /// <value>The full pathname of the registration route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Registration.RelativePath"),
         DefaultValue("/Authentication/Registration")]
        public static string RegistrationRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Authentication/Registration";
            private set => TrySet(value);
        }

        /// <summary>Gets the registration route host lookup method.</summary>
        /// <value>The registration route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Registration.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod RegistrationRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the registration route host lookup which.</summary>
        /// <value>The registration route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Registration.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? RegistrationRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the forced password reset route host.</summary>
        /// <value>The forced password reset route host URL.</value>
        [AppSettingsKey("Clarity.Routes.ForcedPasswordReset.RootUrl"),
         DefaultValue(null)]
        public static string? ForcedPasswordResetRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the forced password reset route relative file.</summary>
        /// <value>The full pathname of the forced password reset route relative file.</value>
        [AppSettingsKey("Clarity.Routes.ForcedPasswordReset.RelativePath"),
         DefaultValue("/Authentication/Forced-Password-Reset")]
        public static string ForcedPasswordResetRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Authentication/Forced-Password-Reset";
            private set => TrySet(value);
        }

        /// <summary>Gets the forced password reset route host lookup method.</summary>
        /// <value>The forced password reset route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.ForcedPasswordReset.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ForcedPasswordResetRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the forced password reset route host lookup which.</summary>
        /// <value>The forced password reset route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.ForcedPasswordReset.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ForcedPasswordResetRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the forgot password route host.</summary>
        /// <value>The forgot password route host URL.</value>
        [AppSettingsKey("Clarity.Routes.ForgotPassword.RootUrl"),
         DefaultValue(null)]
        public static string? ForgotPasswordRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the forgot password route relative file.</summary>
        /// <value>The full pathname of the forgot password route relative file.</value>
        [AppSettingsKey("Clarity.Routes.ForgotPassword.RelativePath"),
         DefaultValue("/Authentication/Forgot-Password")]
        public static string ForgotPasswordRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Authentication/Forgot-Password";
            private set => TrySet(value);
        }

        /// <summary>Gets the forgot password route host lookup method.</summary>
        /// <value>The forgot password route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.ForgotPassword.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ForgotPasswordRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the forgot password route host lookup which.</summary>
        /// <value>The forgot password route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.ForgotPassword.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ForgotPasswordRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the forgot username route host.</summary>
        /// <value>The forgot username route host URL.</value>
        [AppSettingsKey("Clarity.Routes.ForgotUsername.RootUrl"),
         DefaultValue(null)]
        public static string? ForgotUsernameRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the forgot username route relative file.</summary>
        /// <value>The full pathname of the forgot username route relative file.</value>
        [AppSettingsKey("Clarity.Routes.ForgotUsername.RelativePath"),
         DefaultValue("/Authentication/Forgot-Username")]
        public static string ForgotUsernameRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Authentication/Forgot-Username";
            private set => TrySet(value);
        }

        /// <summary>Gets the forgot username route host lookup method.</summary>
        /// <value>The forgot username route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.ForgotUsername.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ForgotUsernameRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the forgot username route host lookup which.</summary>
        /// <value>The forgot username route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.ForgotUsername.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ForgotUsernameRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the membership registration route host.</summary>
        /// <value>The membership registration route host URL.</value>
        [AppSettingsKey("Clarity.Routes.MembershipRegistration.RootUrl"),
         DefaultValue(null)]
        public static string? MembershipRegistrationRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the membership registration route relative file.</summary>
        /// <value>The full pathname of the membership registration route relative file.</value>
        [AppSettingsKey("Clarity.Routes.MembershipRegistration.RelativePath"),
         DefaultValue("/Membership-Registration")]
        public static string MembershipRegistrationRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Membership-Registration";
            private set => TrySet(value);
        }

        /// <summary>Gets the membership registration route host lookup method.</summary>
        /// <value>The membership registration route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.MembershipRegistration.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod MembershipRegistrationRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the membership registration route host lookup which.</summary>
        /// <value>The membership registration route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.MembershipRegistration.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? MembershipRegistrationRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the reporting route host.</summary>
        /// <value>The reporting route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Reporting.RootUrl"),
         DefaultValue(null)]
        public static string? ReportingRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the reporting route relative file.</summary>
        /// <value>The full pathname of the reporting route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Reporting.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/Reporting")]
        public static string ReportingRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/DesktopModules/ClarityEcommerce/Reporting";
            private set => TrySet(value);
        }

        /// <summary>Gets the reporting route host lookup method.</summary>
        /// <value>The reporting route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Reporting.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ReportingRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the reporting route host lookup which.</summary>
        /// <value>The reporting route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Reporting.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ReportingRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the scheduler route host.</summary>
        /// <value>The scheduler route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Scheduler.RootUrl"),
         DefaultValue(null)]
        public static string? SchedulerRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the scheduler route relative file.</summary>
        /// <value>The full pathname of the scheduler route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Scheduler.RelativePath"),
         DefaultValue("/DesktopModules/ClarityEcommerce/Scheduler")]
        public static string SchedulerRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/DesktopModules/ClarityEcommerce/Scheduler";
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler route host lookup method.</summary>
        /// <value>The scheduler route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Scheduler.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod SchedulerRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the scheduler route host lookup which.</summary>
        /// <value>The scheduler route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Scheduler.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? SchedulerRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the connect route host.</summary>
        /// <value>The connect route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Connect.RootUrl"),
         DefaultValue(null)]
        public static string? ConnectRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the connect route relative file.</summary>
        /// <value>The full pathname of the connect route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Connect.RelativePath"),
         DefaultValue(null)]
        public static string? ConnectRouteRelativePath
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the connect route host lookup method.</summary>
        /// <value>The connect route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Connect.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ConnectRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the connect route host lookup which.</summary>
        /// <value>The connect route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Connect.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ConnectRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the company logo route host.</summary>
        /// <value>The company logo route host URL.</value>
        [AppSettingsKey("Clarity.Routes.CompanyLogo.RootUrl"),
         DefaultValue(null)]
        public static string? CompanyLogoRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the company logo route relative file.</summary>
        /// <value>The full pathname of the company logo route relative file.</value>
        [AppSettingsKey("Clarity.Routes.CompanyLogo.RelativePath"),
         DefaultValue("/Portals/0/clarity-ecommerce-logo.png")]
        public static string CompanyLogoRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Portals/0/clarity-ecommerce-logo.png";
            private set => TrySet(value);
        }

        /// <summary>Gets the company logo route host lookup method.</summary>
        /// <value>The company logo route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.CompanyLogo.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod CompanyLogoRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the company logo route host lookup which.</summary>
        /// <value>The company logo route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.CompanyLogo.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? CompanyLogoRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the stored files root route host.</summary>
        /// <value>The stored files root route host URL.</value>
        [AppSettingsKey("Clarity.Routes.StoredFiles.RootUrl"),
         DefaultValue(null)]
        public static string? StoredFilesRootRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the stored files root route relative file.</summary>
        /// <value>The full pathname of the stored files root route relative file.</value>
        [AppSettingsKey("Clarity.Routes.StoredFiles.RelativePath"),
         DefaultValue("/images/ecommerce")]
        public static string StoredFilesRootRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/images/ecommerce";
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files root route host lookup method.</summary>
        /// <value>The stored files root route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.StoredFiles.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod StoredFilesRootRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the stored files root route host lookup which.</summary>
        /// <value>The stored files root route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.StoredFiles.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? StoredFilesRootRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the images root route host.</summary>
        /// <value>The images root route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Images.RootUrl"),
         DefaultValue(null)]
        public static string? ImagesRootRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the images root route relative file.</summary>
        /// <value>The full pathname of the images root route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Images.RelativePath"),
         DefaultValue("/images/ecommerce")]
        public static string ImagesRootRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/images/ecommerce";
            private set => TrySet(value);
        }

        /// <summary>Gets the images root route host lookup method.</summary>
        /// <value>The images root route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Images.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ImagesRootRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the images root route host lookup which.</summary>
        /// <value>The images root route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Images.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ImagesRootRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the imports root route host.</summary>
        /// <value>The imports root route host URL.</value>
        [AppSettingsKey("Clarity.Routes.Imports.RootUrl"),
         DefaultValue(null)]
        public static string? ImportsRootRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the imports root route relative file.</summary>
        /// <value>The full pathname of the imports root route relative file.</value>
        [AppSettingsKey("Clarity.Routes.Imports.RelativePath"),
         DefaultValue("/images/ecommerce")]
        public static string ImportsRootRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/images/ecommerce";
            private set => TrySet(value);
        }

        /// <summary>Gets the imports root route host lookup method.</summary>
        /// <value>The imports root route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.Imports.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod ImportsRootRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the imports root route host lookup which.</summary>
        /// <value>The imports root route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.Imports.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? ImportsRootRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the template override route host.</summary>
        /// <value>The user interface template override route host URL.</value>
        [AppSettingsKey("Clarity.Routes.EmailTemplate.RootUrl"),
         DefaultValue(null)]
        public static string? EmailTemplateRouteHostUrl
        {
            get => TryGet(out string? asValue) ? asValue : SiteRouteHostUrl;
            set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the template override route relative file.</summary>
        /// <value>The full pathname of the template override route relative file.</value>
        [AppSettingsKey("Clarity.Routes.EmailTemplate.RelativePath"),
         DefaultValue("/Portals/_default/Skins/Clarity/Ecommerce/Email")]
        public static string EmailTemplateRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Portals/_default/Skins/Clarity/Ecommerce/Email";
            private set => TrySet(value);
        }

        /// <summary>Gets the template override route host lookup method.</summary>
        /// <value>The user interface template override route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.EmailTemplate.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup)]
        public static Enums.HostLookupMethod EmailTemplateRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the template override route host lookup which.</summary>
        /// <value>The user interface template override route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.EmailTemplate.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary)]
        public static Enums.HostLookupWhichUrl? EmailTemplateRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }
    }
}
