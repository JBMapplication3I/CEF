// <copyright file="CEFConfig.Properties4.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class.</summary>
// ReSharper disable StyleCop.SA1202, StyleCop.SA1300, StyleCop.SA1303, StyleCop.SA1516, StyleCop.SA1602
#nullable enable
namespace Clarity.Ecommerce.JSConfigs
{
    using System;
    using System.ComponentModel;

    /// <summary>Dictionary of CEF configurations.</summary>
    public static partial class CEFConfigDictionary
    {
        #region Sales Groups
        /// <summary>Gets a value indicating whether the sales groups is enabled.</summary>
        /// <value>True if sales groups enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesGroups.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(SplitShippingEnabled))]
        public static bool SalesGroupsEnabled
        {
            get => SplitShippingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales groups has integrated keys.</summary>
        /// <value>True if sales groups has integrated keys, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesGroups.HasIntegratedKeys"),
         DefaultValue(false),
         DependsOn(nameof(SalesGroupsEnabled))]
        public static bool SalesGroupsHasIntegratedKeys
        {
            get => SalesGroupsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Sample Requests
        /// <summary>Gets a value indicating whether the sample requests is enabled.</summary>
        /// <value>True if sample requests enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SampleRequests.Enabled"),
         DefaultValue(false)]
        public static bool SampleRequestsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sample requests has integrated keys.</summary>
        /// <value>True if sample requests has integrated keys, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SampleRequests.HasIntegratedKeys"),
         DefaultValue(false),
         DependsOn(nameof(SampleRequestsEnabled))]
        public static bool SampleRequestsHasIntegratedKeys
        {
            get => SampleRequestsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sample request enforces free sample type.</summary>
        /// <value>True if sample request enforces free sample type, false if not.</value>
        [AppSettingsKey("Clarity.SampleRequests.EnforcesFreeSampleType"),
         DefaultValue(false)]
        public static bool SampleRequestEnforcesFreeSampleType
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Scheduler
        /// <summary>Gets the name of the scheduler node.</summary>
        /// <value>The name of the scheduler node.</value>
        [AppSettingsKey("Clarity.Scheduler.NodeName"),
         DefaultValue("CEFSchedulerNode")]
        public static string SchedulerNodeName
        {
            get => TryGet(out string asValue) ? asValue : "CEFSchedulerNode";
            private set => TrySet(value);
        }

        /// <summary>Gets the number of scheduler workers.</summary>
        /// <value>The number of scheduler workers.</value>
        [AppSettingsKey("Clarity.Scheduler.WorkerCount"),
         DefaultValue(1)]
        public static int SchedulerWorkerCount
        {
            get => TryGet(out int asValue) ? asValue : 1;
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler server timeout in seconds.</summary>
        /// <value>The scheduler server timeout in seconds.</value>
        [AppSettingsKey("Clarity.Scheduler.ServerTimeoutInSeconds"),
         DefaultValue(60)]
        public static int SchedulerServerTimeoutInSeconds
        {
            get => TryGet(out int asValue) ? asValue : 60;
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler heart beat interval in seconds.</summary>
        /// <value>The scheduler heart beat interval in seconds.</value>
        [AppSettingsKey("Clarity.Scheduler.HeartBeatIntervalInSeconds"),
         DefaultValue(30)]
        public static int SchedulerHeartBeatIntervalInSeconds
        {
            get => TryGet(out int asValue) ? asValue : 30;
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler schedule polling interval in seconds.</summary>
        /// <value>The scheduler schedule polling interval in seconds.</value>
        [AppSettingsKey("Clarity.Scheduler.SchedulePollingIntervalInSeconds"),
         DefaultValue(30)]
        public static int SchedulerSchedulePollingIntervalInSeconds
        {
            get => TryGet(out int asValue) ? asValue : 30;
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler server check interval in seconds.</summary>
        /// <value>The scheduler server check interval in seconds.</value>
        [AppSettingsKey("Clarity.Scheduler.ServerCheckIntervalInSeconds"),
         DefaultValue(90)]
        public static int SchedulerServerCheckIntervalInSeconds
        {
            get => TryGet(out int asValue) ? asValue : 90;
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler shutdown timeout.</summary>
        /// <value>The scheduler shutdown timeout.</value>
        [AppSettingsKey("Clarity.Scheduler.ShutdownTimeout"),
         DefaultValue(0)]
        public static int SchedulerShutdownTimeout
        {
            get => TryGet(out int asValue) ? asValue : 0;
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler time zone.</summary>
        /// <value>The scheduler time zone.</value>
        [AppSettingsKey("Clarity.Scheduler.TimeZone"),
         DefaultValue("Central Standard Time")]
        public static string SchedulerTimeZone
        {
            get => TryGet(out string asValue) ? asValue : "Central Standard Time";
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler queues.</summary>
        /// <value>The scheduler queues.</value>
        [AppSettingsKey("Clarity.Scheduler.Queues"),
         DefaultValue("DEFAULT")]
        public static string SchedulerQueues
        {
            get => TryGet(out string asValue) ? asValue : "DEFAULT";
            private set => TrySet(value);
        }

        /// <summary>Gets the scheduler dashboard statistics polling interval in seconds.</summary>
        /// <value>The scheduler dashboard statistics polling interval in seconds.</value>
        [AppSettingsKey("Clarity.Scheduler.Dashboard.StatsPollingIntervalInSeconds"),
         DefaultValue(5_000)]
        public static int SchedulerDashboardStatsPollingIntervalInSeconds
        {
            get => TryGet(out int asValue) ? asValue : 5_000;
            private set => TrySet(value);
        }
        #endregion

        #region Stores
        /// <summary>Site-wide activation of stores UI and functionality.</summary>
        /// <value>True if stores enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Stores.Enabled"),
         DefaultValue(false)]
        public static bool StoresEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the stores do restrictions by minimum maximum (hard/soft stops).</summary>
        /// <value>True if stores do restrictions by minimum maximum, false if not (hard/soft stops).</value>
        [AppSettingsKey("Clarity.Carts.Validation.DoStoreRestrictionsByMinMax"),
         DefaultValue(false)]
        public static bool StoresDoRestrictionsByMinMax
        {
            get => StoresEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Tasks
        /// <summary>Gets the send final statement with final p random PDF inserts after x coordinate days.</summary>
        /// <value>The send final statement with final p random PDF inserts after x coordinate days.</value>
        [AppSettingsKey("Clarity.Tasks.SendFinalStatementWithFinalPRandPDFInsertsAfterXDays"),
         DefaultValue(90)]
        public static int SendFinalStatementWithFinalPRandPDFInsertsAfterXDays
        {
            get => TryGet(out int asValue) ? asValue : 90;
            private set => TrySet(value);
        }

        /// <summary>Utilizes membership levels for users.</summary>
        /// <value>True if enabled, false if not.</value>
        [AppSettingsKey("Clarity.Tasks.Subscription.ProductMembershipLevels"),
         DefaultValue(true)]
        public static bool MembershipLevelsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }
        #endregion

        #region Taxes
        /// <summary>Gets a value indicating whether the taxes is enabled.</summary>
        /// <value>True if taxes enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Taxes.Enabled"),
         DefaultValue(false)]
        public static bool TaxesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether district level taxes is enabled.</summary>
        /// <value>True if taxes should be calculated by district(county) enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Taxes.DistrictLevelTaxes.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(TaxesEnabled))]
        public static bool DistrictLevelTaxesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        #region Avalara
        /// <summary>Gets a value indicating whether the taxes avalara is enabled.</summary>
        /// <value>True if taxes avalara enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Taxes.Avalara.Enabled"),
         DefaultValue(false)]
        public static bool TaxesAvalaraEnabled
        {
            get => TaxesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes avalara company code.</summary>
        /// <value>The taxes avalara company code.</value>
        [AppSettingsKey("Clarity.Tax.Avalara.CompanyCode"),
         DefaultValue(null)]
        public static string? TaxesAvalaraCompanyCode
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes avalara business identification no.</summary>
        /// <value>The taxes avalara business identification no.</value>
        [AppSettingsKey("Clarity.Tax.Avalara.BusinessIdentificationNo"),
         DefaultValue(null)]
        public static string? TaxesAvalaraBusinessIdentificationNo
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes avalara currency code.</summary>
        /// <value>The taxes avalara currency code.</value>
        [AppSettingsKey("Clarity.Tax.Avalara.CurrencyCode"),
         DefaultValue(null)]
        public static string? TaxesAvalaraCurrencyCode
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes avalara license key.</summary>
        /// <value>The taxes avalara license key.</value>
        [AppSettingsKey("Clarity.Tax.Avalara.LicenseKey"),
         DefaultValue(null)]
        public static string? TaxesAvalaraLicenseKey
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the taxes avalara enable development mode.</summary>
        /// <value>True if taxes avalara enable development mode, false if not.</value>
        [AppSettingsKey("Clarity.Tax.Avalara.EnableDevelopmentMode"),
         DefaultValue(false)]
        public static bool TaxesAvalaraEnableDevelopmentMode
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes avalara account number.</summary>
        /// <value>The taxes avalara account number.</value>
        [AppSettingsKey("Clarity.Tax.Avalara.AccountNumber"),
         DefaultValue(null)]
        public static int? TaxesAvalaraAccountNumber
        {
            get => TryGet(out int? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion Avalara

        #region OneSource
        /// <summary>Gets the taxes onesource currency code.</summary>
        /// <value>The taxes onesource currency code.</value>
        [AppSettingsKey("Clarity.Tax.OneSource.CurrencyCode"),
         DefaultValue("USD")]
        public static string? TaxesOneSourceCurrencyCode
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes onesource username.</summary>
        /// <value>The taxes onesource username.</value>
        [AppSettingsKey("Clarity.Tax.OneSource.Username"),
         DefaultValue(null)]
        public static string? TaxesOneSourceUsername
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes onesource password.</summary>
        /// <value>The taxes onesource username.</value>
        [AppSettingsKey("Clarity.Tax.OneSource.Password"),
         DefaultValue(null)]
        public static string? TaxesOneSourcePassword
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the taxes onesource external company id.</summary>
        /// <value>The taxes onesource external company id.</value>
        [AppSettingsKey("Clarity.Tax.OneSource.ExternalCompanyID"),
         DefaultValue(null)]
        public static string? TaxesOneSourceExternalCompanyID
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion OneSource

        /// <summary>Gets the taxes use origin.</summary>
        /// <value>The taxes use origin.</value>
        [AppSettingsKey("Clarity.Tax.UseOrigin"),
         DefaultValue(null)]
        public static string? TaxesUseOrigin
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion

        #region Tracking
        /// <summary>Gets a value indicating whether the tracking is enabled.</summary>
        /// <value>True if tracking enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Tracking.Enabled"),
            DefaultValue(true)]
        public static bool TrackingEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>How long the visit cookies last before expiration (in minutes).</summary>
        /// <value>The tracking expires after x coordinate minutes.</value>
        [AppSettingsKey("Clarity.Tracking.ExpiresAfterXMinutes"),
            DefaultValue(120),
            DependsOn(nameof(TrackingEnabled))]
        public static int TrackingExpiresAfterXMinutes
        {
            get => TrackingEnabled ? TryGet(out int asValue) ? asValue : 120 : 0;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the facebook pixel service is enabled.</summary>
        /// <value>True if facebook pixel service enabled, false if not.</value>
        [AppSettingsKey("Clarity.Analytics.FacebookPixelService.Enabled"),
            DefaultValue(false)]
        public static bool FacebookPixelServiceEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the google tag manager is enabled.</summary>
        /// <value>True if google tag manager enabled, false if not.</value>
        [AppSettingsKey("Clarity.Analytics.GoogleTagManager.Enabled"),
            DefaultValue(false)]
        public static bool GoogleTagManagerEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Vendors
        /// <summary>Gets a value indicating whether the vendors is enabled.</summary>
        /// <value>True if vendors enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Vendors.Enabled"),
         DefaultValue(false)]
        public static bool VendorsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the vendors do restrictions by minimum maximum (hard/soft stops).</summary>
        /// <value>True if vendors do restrictions by minimum maximum, false if not (hard/soft stops).</value>
        [AppSettingsKey("Clarity.Carts.Validation.DoVendorRestrictionsByMinMax"),
         DefaultValue(false)]
        public static bool VendorsDoRestrictionsByMinMax
        {
            get => VendorsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion
    }
}
