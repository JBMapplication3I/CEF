// <copyright file="CEFConfig.Properties.SalesInvoices.cs" company="clarity-ventures.com">
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
        /// <summary>Gets a value indicating whether the sales invoices is enabled.</summary>
        /// <value>True if sales invoices enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Enabled"),
            DefaultValue(true)]
        public static bool SalesInvoicesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices due date is allowed.</summary>
        /// <value>True if sales invoices due date is allowed, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.AllowDueDate"),
            DefaultValue(false)]
        public static bool SalesInvoicesAllowDueDate
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the default Invoice Due Date.</summary>
        /// <value>The default Invoice Due Date.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.DueDateDefault"),
            DefaultValue(30),
            DependsOn(nameof(SalesInvoicesAllowDueDate))]
        public static int SalesInvoicesDueDateDefault
        {
            get => SalesInvoicesAllowDueDate ? TryGet(out int asValue) ? asValue : 30 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices has integrated keys.</summary>
        /// <value>True if sales invoices has integrated keys, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.HasIntegratedKeys"),
            DefaultValue(false),
            DependsOn(nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesHasIntegratedKeys
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the sales invoices emails payment reminders occur at days.</summary>
        /// <value>The sales invoices emails payment reminders occur at days.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.PaymentReminders.OccurAt"),
            DefaultValue("-30,-14,-7,-3,-1,1,3,7,14,30"),
            SplitOn(new[] { ',' }),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static int[] SalesInvoicesEmailsPaymentRemindersOccurAt
        {
            get => SalesInvoicesEnabled
                ? TryGet(out int[] asValue)
                    ? asValue
                    : new[] { -30, -14, -7, -3, -1, 1, 3, 7, 14, 30 }
                : Array.Empty<int>();
            private set => TrySet(value);
        }

        /// <summary>Gets the sales invoices late fees.</summary>
        /// <value>The sales invoices late fees.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.LateFees"),
            DefaultValue("[{ Day: 1, Amount: 3, Kind: 'p' }, { Day: 30, Amount: 3, Kind: 'p' }, { Day: 60, Amount: 3, Kind: 'p' }]"),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static SalesInvoiceLateFee[]? SalesInvoicesLateFees
        {
            get => SalesInvoicesEnabled
                ? TryGet(out SalesInvoiceLateFee[]? asValue)
                    ? asValue
                    : new[] { new SalesInvoiceLateFee(1, 3, 'p'), new SalesInvoiceLateFee(30, 3, 'p'), new SalesInvoiceLateFee(60, 3, 'p') }
                : Array.Empty<SalesInvoiceLateFee>();
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices emails invoice created from connect is
        /// enabled.</summary>
        /// <value>True if sales invoices emails invoice created from connect enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromConnect.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesEmailsInvoiceCreatedFromConnectEnabled
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices emails invoice created from bill me later in
        /// checkout is enabled.</summary>
        /// <value>True if sales invoices emails invoice created from bill me later in checkout enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromBillMeLaterInCheckout.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesEmailsInvoiceCreatedFromBillMeLaterInCheckoutEnabled
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices emails invoice created from netx is enabled.</summary>
        /// <value>True if sales invoices emails invoice created from netx enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromNETX.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesEmailsInvoiceCreatedFromNETXEnabled
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices emails invoice created from order is enabled.</summary>
        /// <value>True if sales invoices emails invoice created from order enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromOrder.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesEmailsInvoiceCreatedFromOrderEnabled
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices emails invoice created from new invoice wizard is
        /// enabled.</summary>
        /// <value>True if sales invoices emails invoice created from new invoice wizard enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromNewInvoiceWizard.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesEmailsInvoiceCreatedFromNewInvoiceWizardEnabled
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices emails payment reminders is enabled.</summary>
        /// <value>True if sales invoices emails payment reminders enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.PaymentReminders.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesEmailsPaymentRemindersEnabled
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices emails payment recieved is enabled.</summary>
        /// <value>True if sales invoices emails payment recieved enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.Emails.PaymentReceived.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesEmailsPaymentReceivedEnabled
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices customer can pay via user dashboard single.</summary>
        /// <value>True if sales invoices customer can pay via user dashboard single, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Single"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCustomerCanPayViaUserDashboardSingle
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices customer can pay via user dashboard single
        /// partially.</summary>
        /// <value>True if sales invoices customer can pay via user dashboard single partially, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Single.Partially"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCustomerCanPayViaUserDashboardSinglePartially
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices customer can pay via user dashboard multi.</summary>
        /// <value>True if sales invoices customer can pay via user dashboard multi, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Multi"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCustomerCanPayViaUserDashboardMulti
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices customer can pay via user dashboard multi
        /// partially.</summary>
        /// <value>True if sales invoices customer can pay via user dashboard multi partially, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Multi.Partially"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCustomerCanPayViaUserDashboardMultiPartially
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices cursor can pay via admin with data.</summary>
        /// <value>True if sales invoices cursor can pay via admin with data, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithData"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCSRCanPayViaAdminWithData
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices cursor can pay via admin with data partially.</summary>
        /// <value>True if sales invoices cursor can pay via admin with data partially, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithData.Partially"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCSRCanPayViaAdminWithDataPartially
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices cursor can pay via admin without data.</summary>
        /// <value>True if sales invoices cursor can pay via admin without data, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithoutData"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCSRCanPayViaAdminWithoutData
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales invoices cursor can pay via admin without data
        /// partially.</summary>
        /// <value>True if sales invoices cursor can pay via admin without data partially, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithoutData.Partially"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool SalesInvoicesCSRCanPayViaAdminWithoutDataPartially
        {
            get => SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by invoice is enabled.</summary>
        /// <value>True if payments by invoice enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByInvoice.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static bool PaymentsByInvoiceEnabled
        {
            get => LoginEnabled && SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by invoice title key.</summary>
        /// <value>The payments by invoice title key.</value>
        [AppSettingsKey("Clarity.Payments.ByInvoice.TitleKey"),
            DefaultValue("ui.storefront.common.Invoice"),
            DependsOn(nameof(PaymentsByInvoiceEnabled))]
        public static string? PaymentsByInvoiceTitleKey
        {
            get => PaymentsByInvoiceEnabled ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.Invoice" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by invoice position.</summary>
        /// <value>The payments by invoice position.</value>
        [AppSettingsKey("Clarity.Payments.ByInvoice.Position"),
            DefaultValue(30),
            DependsOn(nameof(PaymentsByInvoiceEnabled))]
        public static int PaymentsByInvoicePosition
        {
            get => PaymentsByInvoiceEnabled ? TryGet(out int asValue) ? asValue : 30 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by Invoice uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by Invoice uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByInvoice.Uplifts.UseGreater"),
            DefaultValue(false),
            DependsOn(nameof(PaymentsByInvoiceUpliftPercent), nameof(PaymentsByInvoiceUpliftAmount))]
        public static bool PaymentsByInvoiceUpliftUseWhicheverIsGreater
        {
            get => PaymentsByInvoiceUpliftPercent > 0 && PaymentsByInvoiceUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByInvoice.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByInvoice.RestrictedAccountTypes"),
            DefaultValue(null),
            DependsOn(nameof(PaymentsByInvoiceEnabled))]
        public static string? PaymentsByInvoiceRestrictedAccountTypes
        {
            get => PaymentsByInvoiceEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by invoice uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByInvoice.Uplifts.Percent"),
            DefaultValue(0),
            DependsOn(nameof(PaymentsByInvoiceEnabled))]
        public static decimal PaymentsByInvoiceUpliftPercent
        {
            get => PaymentsByInvoiceEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by invoice uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByInvoice.Uplifts.Amount"),
            DefaultValue(0),
            DependsOn(nameof(PaymentsByInvoiceEnabled))]
        public static decimal PaymentsByInvoiceUpliftAmount
        {
            get => PaymentsByInvoiceEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route sales invoices is enabled.</summary>
        /// <value>True if dashboard route sales invoices enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.SalesInvoices.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(SalesInvoicesEnabled))]
        public static bool DashboardRouteSalesInvoicesEnabled
        {
            get => MyDashboardEnabled && SalesInvoicesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path sales invoices.</summary>
        /// <value>The stored files path sales invoices.</value>
        [AppSettingsKey("Clarity.Uploads.Files.SalesInvoice"),
            DefaultValue("/SalesInvoice"),
            DependsOn(nameof(LoginEnabled), nameof(SalesInvoicesEnabled))]
        public static string StoredFilesPathSalesInvoices
        {
            get => TryGet(out string asValue) ? asValue : "/SalesInvoice";
            private set => TrySet(value);
        }

        /// <summary>Gets the check by mail key.</summary>
        /// <value>The check by mail key.</value>'
        [AppSettingsKey("Clarity.Payments.PaymentMethodCheckByMailKey"),
            DefaultValue(null)]
        public static string? CheckByMailKey
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the wire transfer key.</summary>
        /// <value>The wire transfer key.</value>
        [AppSettingsKey("Clarity.Payments.PaymentMethodWireTransferKey"),
            DefaultValue(null)]
        public static string? WireTransferKey
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the online payment record key.</summary>
        /// <value>The online payment record key.</value>
        [AppSettingsKey("Clarity.Payments.PaymentMethodOnlinePaymentRecordKey"),
            DefaultValue(null)]
        public static string? OnlinePaymentRecordKey
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>An credit card.</summary>
        /// <value>The payments by invoice credit card limit.</value>
        [AppSettingsKey("Clarity.Payments.ByInvoice.CreditCardLimit"),
            DefaultValue(1_000_000)]
        public static decimal PaymentsByInvoiceCreditCardLimit
        {
            get => TryGet(out decimal asValue) ? asValue : 1_000_000;
            private set => TrySet(value);
        }
    }
}
