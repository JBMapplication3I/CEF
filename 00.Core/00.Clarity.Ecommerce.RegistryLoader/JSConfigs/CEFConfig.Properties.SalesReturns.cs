// <copyright file="CEFConfig.Properties.SalesReturns.cs" company="clarity-ventures.com">
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
        /// <summary>Gets a value indicating whether the sales returns is enabled.</summary>
        /// <value>True if sales returns enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesReturns.Enabled"),
         DefaultValue(false)]
        public static bool SalesReturnsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales returns has integrated keys.</summary>
        /// <value>True if sales returns has integrated keys, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesReturns.HasIntegratedKeys"),
         DefaultValue(false),
         DependsOn(nameof(SalesReturnsEnabled))]
        public static bool SalesReturnsHasIntegratedKeys
        {
            get => SalesReturnsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        #region Returns Destination Address
        /// <summary>One item per RMA vs multiple.</summary>
        /// <value>True if returns are single creation, false if not.</value>
        [AppSettingsKey("Clarity.SalesReturns.SingleCreation"),
         DefaultValue(true),
         Unused]
        public static bool ReturnsAreSingleCreation
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the returns validity period in days.</summary>
        /// <value>The returns validity period in days.</value>
        [AppSettingsKey("Clarity.SalesReturns.ValidityPeriodInDays"),
         DefaultValue(45),
         Unused]
        public static int? ReturnsValidityPeriodInDays
        {
            get => TryGet(out int? asValue) ? asValue : 45;
            private set => TrySet(value);
        }

        /// <summary>Gets the returns number format.</summary>
        /// <value>The returns number format.</value>
        [AppSettingsKey("Clarity.SalesReturns.NumberFormat"),
         DefaultValue("{{OrderID}}RMA{{ItemSku}}"),
         Unused]
        public static string ReturnsNumberFormat
        {
            get => TryGet(out string asValue) ? asValue : "{{OrderID}}RMA{{ItemSku}}";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address company.</summary>
        /// <value>The returns destination address company.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.Company"),
         DefaultValue("Company Returns"),
         Unused]
        public static string ReturnsDestinationAddressCompany
        {
            get => TryGet(out string asValue) ? asValue : "Company Returns";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address street 1.</summary>
        /// <value>The returns destination address street 1.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.Street1"),
         DefaultValue("6805 N Capital of Texas Hwy"),
         Unused]
        public static string ReturnsDestinationAddressStreet1
        {
            get => TryGet(out string asValue) ? asValue : "6805 N Capital of Texas Hwy";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address street 2.</summary>
        /// <value>The returns destination address street 2.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.Street2"),
         DefaultValue("Suite 312"),
         Unused]
        public static string ReturnsDestinationAddressStreet2
        {
            get => TryGet(out string asValue) ? asValue : "Suite 312";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address street 3.</summary>
        /// <value>The returns destination address street 3.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.Street3"),
         DefaultValue(null),
         Unused]
        public static string? ReturnsDestinationAddressStreet3
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address city.</summary>
        /// <value>The returns destination address city.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.City"),
         DefaultValue("Austin"),
         Unused]
        public static string ReturnsDestinationAddressCity
        {
            get => TryGet(out string asValue) ? asValue : "Austin";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address postal code.</summary>
        /// <value>The returns destination address postal code.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.PostalCode"),
         DefaultValue("78731"),
         Unused]
        public static string ReturnsDestinationAddressPostalCode
        {
            get => TryGet(out string asValue) ? asValue : "78731";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address region code.</summary>
        /// <value>The returns destination address region code.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.RegionCode"),
         DefaultValue("TX"),
         Unused]
        public static string ReturnsDestinationAddressRegionCode
        {
            get => TryGet(out string asValue) ? asValue : "TX";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address country code.</summary>
        /// <value>The returns destination address country code.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.CountryCode"),
         DefaultValue("USA"), // Confirmed it's USA, not US 2020-09-24
         Unused]
        public static string ReturnsDestinationAddressCountryCode
        {
            get => TryGet(out string asValue) ? asValue : "USA";
            private set => TrySet(value);
        }

        /// <summary>Gets the returns destination address phone.</summary>
        /// <value>The returns destination address phone.</value>
        [AppSettingsKey("Clarity.ReturnsDestination.Phone"),
         DefaultValue(null),
         Unused]
        public static string? ReturnsDestinationAddressPhone
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion

        /// <summary>Gets a value indicating whether the dashboard route sales returns is enabled.</summary>
        /// <value>True if dashboard route sales returns enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.SalesReturns.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(MyDashboardEnabled), nameof(SalesReturnsEnabled))]
        public static bool DashboardRouteSalesReturnsEnabled
        {
            get => MyDashboardEnabled && SalesReturnsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path sales returns.</summary>
        /// <value>The stored files path sales returns.</value>
        [AppSettingsKey("Clarity.Uploads.Files.SalesReturn"),
         DefaultValue("/SalesReturn")]
        public static string StoredFilesPathSalesReturns
        {
            get => TryGet(out string asValue) ? asValue : "/SalesReturn";
            private set => TrySet(value);
        }
    }
}
