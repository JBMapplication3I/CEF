// <copyright file="CEFConfig.Shipping.cs" company="clarity-ventures.com">
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
        /// <summary>Gets a value indicating whether the shipping is enabled.</summary>
        /// <value>True if shipping enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Enabled"),
            DefaultValue(true)]
        public static bool ShippingEnabled
        {
            get => !TryGet(out bool asValue) || asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping packages is enabled.</summary>
        /// <value>True if shipping packages enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Packages.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(ShippingEnabled))]
        public static bool ShippingPackagesEnabled
        {
            get => ShippingEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping master packs is enabled.</summary>
        /// <value>True if shipping master packs enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.MasterPacks.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(ShippingPackagesEnabled))]
        public static bool ShippingMasterPacksEnabled
        {
            get => ShippingPackagesEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping pallets is enabled.</summary>
        /// <value>True if shipping pallets enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Pallets.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(ShippingPackagesEnabled))]
        public static bool ShippingPalletsEnabled
        {
            get => ShippingPackagesEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping events is enabled.</summary>
        /// <value>True if shipping events enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Events.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(ShippingEnabled))]
        public static bool ShippingEventsEnabled
        {
            get => ShippingEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping handling fees is enabled.</summary>
        /// <value>True if shipping handling fees enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.HandlingFees.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(ShippingEnabled))]
        public static bool ShippingHandlingFeesEnabled
        {
            get => ShippingEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping rates products can be individually free.</summary>
        /// <value>True if shipping rates products can be free, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.ProductsCanBeFree"),
            DefaultValue(false),
            DependsOn(nameof(ShippingEnabled))]
        public static bool ShippingRatesProductsCanBeFree
        {
            get => ShippingEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        #region Lead Times
        /// <summary>Gets a value indicating whether the shipping lead times is enabled.</summary>
        /// <value>True if shipping lead times enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.LeadTimes.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(ShippingEnabled))]
        public static bool ShippingLeadTimesEnabled
        {
            get => ShippingEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time is enabled.</summary>
        /// <value>True if shipping company lead time enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(ShippingEnabled))]
        public static bool ShippingCompanyLeadTimeEnabled
        {
            get => ShippingEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours normal.</summary>
        /// <value>The shipping company lead time in business hours normal.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.Normal"),
            DefaultValue("16:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursNormal
        {
            get => ShippingCompanyLeadTimeEnabled ? TryGet(out TimeSpan asValue) ? asValue : new(16, 0, 0) : TimeSpan.Zero;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours expedited.</summary>
        /// <value>The shipping company lead time in business hours expedited.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.Expedited"),
            DefaultValue("4:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursExpedited
        {
            get => ShippingCompanyLeadTimeEnabled ? TryGet(out TimeSpan asValue) ? asValue : new(4, 0, 0) : TimeSpan.Zero;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time enabled monday.</summary>
        /// <value>True if shipping company lead time enabled monday, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Monday"),
            DefaultValue(true),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static bool ShippingCompanyLeadTimeEnabledMonday
        {
            get => ShippingCompanyLeadTimeEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours start hour monday.</summary>
        /// <value>The shipping company lead time in business hours start hour monday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Monday"),
            DefaultValue("08:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledMonday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursStartHourMonday
        {
            get => ShippingCompanyLeadTimeEnabledMonday ? TryGet(out TimeSpan asValue) ? asValue : new(8, 0, 0) : new(8, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours end hour monday.</summary>
        /// <value>The shipping company lead time in business hours end hour monday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Monday"),
            DefaultValue("17:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledMonday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursEndHourMonday
        {
            get => ShippingCompanyLeadTimeEnabledMonday ? TryGet(out TimeSpan asValue) ? asValue : new(17, 0, 0) : new(17, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time enabled tuesday.</summary>
        /// <value>True if shipping company lead time enabled tuesday, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Tuesday"),
            DefaultValue(true),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static bool ShippingCompanyLeadTimeEnabledTuesday
        {
            get => ShippingCompanyLeadTimeEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours start hour tuesday.</summary>
        /// <value>The shipping company lead time in business hours start hour tuesday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Tuesday"),
            DefaultValue("08:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledTuesday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursStartHourTuesday
        {
            get => ShippingCompanyLeadTimeEnabledTuesday ? TryGet(out TimeSpan asValue) ? asValue : new(8, 0, 0) : new(8, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours end hour tuesday.</summary>
        /// <value>The shipping company lead time in business hours end hour tuesday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Tuesday"),
            DefaultValue("17:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledTuesday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursEndHourTuesday
        {
            get => ShippingCompanyLeadTimeEnabledTuesday ? TryGet(out TimeSpan asValue) ? asValue : new(17, 0, 0) : new(17, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time enabled wednesday.</summary>
        /// <value>True if shipping company lead time enabled wednesday, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Wednesday"),
            DefaultValue(true),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static bool ShippingCompanyLeadTimeEnabledWednesday
        {
            get => ShippingCompanyLeadTimeEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours start hour wednesday.</summary>
        /// <value>The shipping company lead time in business hours start hour wednesday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Wednesday"),
            DefaultValue("08:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledWednesday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursStartHourWednesday
        {
            get => ShippingCompanyLeadTimeEnabledWednesday ? TryGet(out TimeSpan asValue) ? asValue : new(8, 0, 0) : new(8, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours end hour wednesday.</summary>
        /// <value>The shipping company lead time in business hours end hour wednesday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Wednesday"),
            DefaultValue("17:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledWednesday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursEndHourWednesday
        {
            get => ShippingCompanyLeadTimeEnabledWednesday ? TryGet(out TimeSpan asValue) ? asValue : new(17, 0, 0) : new(17, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time enabled thursday.</summary>
        /// <value>True if shipping company lead time enabled thursday, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Thursday"),
            DefaultValue(true),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static bool ShippingCompanyLeadTimeEnabledThursday
        {
            get => ShippingCompanyLeadTimeEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours start hour thursday.</summary>
        /// <value>The shipping company lead time in business hours start hour thursday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Thursday"),
            DefaultValue("08:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledThursday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursStartHourThursday
        {
            get => ShippingCompanyLeadTimeEnabledThursday ? TryGet(out TimeSpan asValue) ? asValue : new(8, 0, 0) : new(8, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours end hour thursday.</summary>
        /// <value>The shipping company lead time in business hours end hour thursday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Thursday"),
            DefaultValue("17:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledThursday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursEndHourThursday
        {
            get => ShippingCompanyLeadTimeEnabledThursday ? TryGet(out TimeSpan asValue) ? asValue : new(17, 0, 0) : new(17, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time enabled friday.</summary>
        /// <value>True if shipping company lead time enabled friday, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Friday"),
            DefaultValue(true),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static bool ShippingCompanyLeadTimeEnabledFriday
        {
            get => ShippingCompanyLeadTimeEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours start hour friday.</summary>
        /// <value>The shipping company lead time in business hours start hour friday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Friday"),
            DefaultValue("08:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledFriday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursStartHourFriday
        {
            get => ShippingCompanyLeadTimeEnabledFriday ? TryGet(out TimeSpan asValue) ? asValue : new(8, 0, 0) : new(8, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours end hour friday.</summary>
        /// <value>The shipping company lead time in business hours end hour friday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Friday"),
            DefaultValue("17:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledFriday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursEndHourFriday
        {
            get => ShippingCompanyLeadTimeEnabledFriday ? TryGet(out TimeSpan asValue) ? asValue : new(17, 0, 0) : new(17, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time enabled saturday.</summary>
        /// <value>True if shipping company lead time enabled saturday, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Saturday"),
            DefaultValue(false),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static bool ShippingCompanyLeadTimeEnabledSaturday
        {
            get => ShippingCompanyLeadTimeEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours start hour saturday.</summary>
        /// <value>The shipping company lead time in business hours start hour saturday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Saturday"),
            DefaultValue("08:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledSaturday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursStartHourSaturday
        {
            get => ShippingCompanyLeadTimeEnabledSaturday ? TryGet(out TimeSpan asValue) ? asValue : new(8, 0, 0) : new(8, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours end hour saturday.</summary>
        /// <value>The shipping company lead time in business hours end hour saturday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Saturday"),
            DefaultValue("17:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledSaturday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursEndHourSaturday
        {
            get => ShippingCompanyLeadTimeEnabledSaturday ? TryGet(out TimeSpan asValue) ? asValue : new(17, 0, 0) : new(17, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping company lead time enabled sunday.</summary>
        /// <value>True if shipping company lead time enabled sunday, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Sunday"),
            DefaultValue(false),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabled))]
        public static bool ShippingCompanyLeadTimeEnabledSunday
        {
            get => ShippingCompanyLeadTimeEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours start hour sunday.</summary>
        /// <value>The shipping company lead time in business hours start hour sunday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Sunday"),
            DefaultValue("08:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledSunday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursStartHourSunday
        {
            get => ShippingCompanyLeadTimeEnabledSunday ? TryGet(out TimeSpan asValue) ? asValue : new(8, 0, 0) : new(8, 0, 0);
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping company lead time in business hours end hour sunday.</summary>
        /// <value>The shipping company lead time in business hours end hour sunday.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Sunday"),
            DefaultValue("17:00:00"),
            DependsOn(nameof(ShippingCompanyLeadTimeEnabledSunday))]
        public static TimeSpan ShippingCompanyLeadTimeInBusinessHoursEndHourSunday
        {
            get => ShippingCompanyLeadTimeEnabledSunday ? TryGet(out TimeSpan asValue) ? asValue : new(17, 0, 0) : new(17, 0, 0);
            private set => TrySet(value);
        }
        #endregion

        /// <summary>Gets a value indicating whether the shipping rates is enabled.</summary>
        /// <value>True if shipping rates enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Rates.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(ShippingEnabled))]
        public static bool ShippingRatesEnabled
        {
            get => ShippingEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping estimates is enabled.</summary>
        /// <value>True if shipping estimates enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Rates.Estimator.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(ShippingRatesEnabled))]
        public static bool ShippingEstimatesEnabled
        {
            get => ShippingRatesEnabled && (!TryGet(out bool asValue) || asValue);
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the shipping rates flat is enabled.</summary>
        /// <value>True if shipping rates flat enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Rates.Flat.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(ShippingRatesEnabled))]
        public static bool ShippingRatesFlatEnabled
        {
            get => ShippingRatesEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping tracking day rolling.</summary>
        /// <value>The shipping tracking day rolling.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Tracking.DayRolling"),
            DefaultValue(null)]
        public static string? ShippingTrackingDayRolling
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        #region Global Free Shipping Threshold (Hard/Soft Stop)
        /// <summary>Gets a value indicating whether the shipping rates free threshold global is enabled.</summary>
        /// <value>True if shipping rates free threshold global enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(ShippingRatesEnabled))]
        public static bool ShippingRatesFreeThresholdGlobalEnabled
        {
            get => ShippingRatesEnabled && TryGet(out bool asValue) && asValue;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount.</summary>
        /// <value>The shipping rates free threshold global amount.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.Amount"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static decimal? ShippingRatesFreeThresholdGlobalAmount
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out decimal? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount after.</summary>
        /// <value>The shipping rates free threshold global amount after.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.AmountAfter"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static decimal? ShippingRatesFreeThresholdGlobalAmountAfter
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out decimal? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount warning message.</summary>
        /// <value>The shipping rates free threshold global amount warning message.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.WarningMessage"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static string? ShippingRatesFreeThresholdGlobalAmountWarningMessage
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount ignored accepted message.</summary>
        /// <value>The shipping rates free threshold global amount ignored accepted message.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.IgnoredAcceptedMessage"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static string? ShippingRatesFreeThresholdGlobalAmountIgnoredAcceptedMessage
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount buffer category display name.</summary>
        /// <value>The shipping rates free threshold global amount buffer category display name.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.BufferCategory.DisplayName"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static string? ShippingRatesFreeThresholdGlobalAmountBufferCategoryDisplayName
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount buffer category name.</summary>
        /// <value>The shipping rates free threshold global amount buffer category name.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.BufferCategory.Name"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static string? ShippingRatesFreeThresholdGlobalAmountBufferCategoryName
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount buffer category SEO URL.</summary>
        /// <value>The shipping rates free threshold global amount buffer category SEO URL.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.BufferCategory.SEOURL"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static string? ShippingRatesFreeThresholdGlobalAmountBufferCategorySeoUrl
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount buffer product name.</summary>
        /// <value>The shipping rates free threshold global amount buffer product name.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.BufferProduct.Name"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static string? ShippingRatesFreeThresholdGlobalAmountBufferProductName
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping rates free threshold global amount buffer product SEO URL.</summary>
        /// <value>The shipping rates free threshold global amount buffer product SEO URL.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.FreeShippingThreshold.Global.BufferProduct.SEOURL"),
            DefaultValue(null),
            DependsOn(nameof(ShippingRatesFreeThresholdGlobalEnabled))]
        public static string? ShippingRatesFreeThresholdGlobalAmountBufferProductSeoUrl
        {
            get => ShippingRatesFreeThresholdGlobalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }
        #endregion

        #region Shipping Origin Address
        /// <summary>Gets the shipping origin address street 1.</summary>
        /// <value>The shipping origin address street 1.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Origin.Street1"),
            DefaultValue("6805 N Capital of Texas Hwy")]
        public static string ShippingOriginAddressStreet1
        {
            get => TryGet(out string asValue) ? asValue : "6805 N Capital of Texas Hwy";
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping origin address street 2.</summary>
        /// <value>The shipping origin address street 2.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Origin.Street2"),
            DefaultValue("Suite 312")]
        public static string ShippingOriginAddressStreet2
        {
            get => TryGet(out string asValue) ? asValue : "Suite 312";
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping origin address street 3.</summary>
        /// <value>The shipping origin address street 3.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Origin.Street3"),
            DefaultValue(null)]
        public static string? ShippingOriginAddressStreet3
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping origin address city.</summary>
        /// <value>The shipping origin address city.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Origin.City"),
            DefaultValue("Austin")]
        public static string ShippingOriginAddressCity
        {
            get => TryGet(out string asValue) ? asValue : "Austin";
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping origin address postal code.</summary>
        /// <value>The shipping origin address postal code.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Origin.PostalCode"),
            DefaultValue("78731")]
        public static string ShippingOriginAddressPostalCode
        {
            get => TryGet(out string asValue) ? asValue : "78731";
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping origin address region code.</summary>
        /// <value>The shipping origin address region code.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Origin.RegionCode"),
            DefaultValue("TX")]
        public static string ShippingOriginAddressRegionCode
        {
            get => TryGet(out string asValue) ? asValue : "TX";
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping origin address country code.</summary>
        /// <value>The shipping origin address country code.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.Origin.CountryCode"),
            DefaultValue("USA")] // Confirmed it's USA, not US 2020-09-24
        public static string ShippingOriginAddressCountryCode
        {
            get => TryGet(out string asValue) ? asValue : "USA";
            private set => TrySet(value);
        }

        /// <summary>Gets the shipping fedex tracking link.</summary>
        /// <value>The shipping fedex tracking link.</value>
        [AppSettingsKey("Clarity.FeatureSet.Shipping.TrackingNumbers.Fedex"),
            DefaultValue("https://www.fedex.com/fedextrack/?trknbr=")]
        public static string FedExTrackingLink
        {
            get => TryGet(out string asValue) ? asValue : "https://www.fedex.com/fedextrack/?trknbr=";
            private set => TrySet(value);
        }
        #endregion
    }
}
