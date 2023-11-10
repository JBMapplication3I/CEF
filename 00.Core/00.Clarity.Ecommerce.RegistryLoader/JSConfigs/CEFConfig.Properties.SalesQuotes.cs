// <copyright file="CEFConfig.Properties.SalesQuotes.cs" company="clarity-ventures.com">
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
        /// <summary>Gets a value indicating whether the sales quotes is enabled.</summary>
        /// <value>True if sales quotes enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(SplitShippingEnabled))]
        public static bool SalesQuotesEnabled
        {
            get => SplitShippingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quotes has integrated keys.</summary>
        /// <value>True if sales quotes has integrated keys, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.HasIntegratedKeys"),
         DefaultValue(false),
         DependsOn(nameof(SalesQuotesEnabled))]
        public static bool SalesQuotesHasIntegratedKeys
        {
            get => SalesQuotesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires store identifier.</summary>
        /// <value>True if sales quote requires store identifier, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresStoreID"),
         DefaultValue(false)]
        public static bool SalesQuoteRequiresStoreID
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires store identifier or key.</summary>
        /// <value>True if sales quote requires store identifier or key, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresStoreIDOrKey"),
         DefaultValue(false)]
        public static bool SalesQuoteRequiresStoreIDOrKey
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires store product identifier.</summary>
        /// <value>True if sales quote requires store product identifier, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresStoreProductID"),
         DefaultValue(false),
         Unused]
        public static bool SalesQuoteRequiresStoreProductID
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires brand identifier.</summary>
        /// <value>True if sales quote requires brand identifier, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresBrandID"),
         DefaultValue(false),
         Unused]
        public static bool SalesQuoteRequiresBrandID
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires brand identifier or key.</summary>
        /// <value>True if sales quote requires brand identifier or key, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresBrandIDOrKey"),
         DefaultValue(false),
         Unused]
        public static bool SalesQuoteRequiresBrandIDOrKey
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires brand product identifier.</summary>
        /// <value>True if sales quote requires brand product identifier, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresBrandProductID"),
         DefaultValue(false),
         Unused]
        public static bool SalesQuoteRequiresBrandProductID
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires franchise identifier.</summary>
        /// <value>True if sales quote requires franchise identifier, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresFranchiseID"),
         DefaultValue(false),
         Unused]
        public static bool SalesQuoteRequiresFranchiseID
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires franchise identifier or key.</summary>
        /// <value>True if sales quote requires franchise identifier or key, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresFranchiseIDOrKey"),
         DefaultValue(false),
         Unused]
        public static bool SalesQuoteRequiresFranchiseIDOrKey
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quote requires franchise product identifier.</summary>
        /// <value>True if sales quote requires franchise product identifier, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.RequiresFranchiseProductID"),
         DefaultValue(false),
         Unused]
        public static bool SalesQuoteRequiresFranchiseProductID
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Include the Quantities in valuation of quotes (adds extra columns to the UI and import/exports).</summary>
        /// <value>True if sales quotes include quantity column, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.IncludeQuantityColumn"),
         DefaultValue(true)]
        public static bool SalesQuotesIncludeQuantityColumn
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales quotes use the quote cart.</summary>
        /// <value>True if sales quotes use the quote cart, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesQuotes.UseQuoteCart"),
         DefaultValue(true)]
        public static bool SalesQuotesUseQuoteCart
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the quote cart route host.</summary>
        /// <value>The quote cart route host URL.</value>
        [AppSettingsKey("Clarity.Routes.QuoteCart.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? QuoteCartRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the quote cart route relative file.</summary>
        /// <value>The full pathname of the quote cart route relative file.</value>
        [AppSettingsKey("Clarity.Routes.QuoteCart.RelativePath"),
         DefaultValue("/Quote")]
        public static string QuoteCartRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Quote";
            private set => TrySet(value);
        }

        /// <summary>Gets the quote cart route host lookup method.</summary>
        /// <value>The quote cart route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.QuoteCart.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod QuoteCartRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the quote cart route host lookup which.</summary>
        /// <value>The quote cart route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.QuoteCart.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? QuoteCartRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the submit quote route host.</summary>
        /// <value>The submit quote route host URL.</value>
        [AppSettingsKey("Clarity.Routes.SubmitQuote.RootUrl"),
         DefaultValue(null),
         Unused]
        public static string? SubmitQuoteRouteHostUrl
        {
            get => TryGet(out string asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the submit quote route relative file.</summary>
        /// <value>The full pathname of the submit quote route relative file.</value>
        [AppSettingsKey("Clarity.Routes.SubmitQuote.RelativePath"),
         DefaultValue("/Submit-Quote")]
        public static string SubmitQuoteRouteRelativePath
        {
            get => TryGet(out string asValue) ? asValue : "/Quote";
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote route host lookup method.</summary>
        /// <value>The submit quote route host lookup method.</value>
        [AppSettingsKey("Clarity.Routes.SubmitQuote.HostLookup.Method"),
         DefaultValue(Enums.HostLookupMethod.NotByLookup),
         Unused]
        public static Enums.HostLookupMethod SubmitQuoteRouteHostLookupMethod
        {
            get => TryGet<Enums.HostLookupMethod>(out var asValue) ? asValue : Enums.HostLookupMethod.NotByLookup;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the submit quote route host lookup which.</summary>
        /// <value>The submit quote route host lookup which URL.</value>
        [AppSettingsKey("Clarity.Routes.SubmitQuote.HostLookup.WhichUrl"),
         DefaultValue(Enums.HostLookupWhichUrl.Primary),
         Unused]
        public static Enums.HostLookupWhichUrl? SubmitQuoteRouteHostLookupWhichUrl
        {
            get => TryGet<Enums.HostLookupWhichUrl>(out var asValue) ? asValue : Enums.HostLookupWhichUrl.Primary;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by quote me is enabled.</summary>
        /// <value>True if payments by quote me enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByQuoteMe.Enabled"),
         DefaultValue(false)]
        public static bool PaymentsByQuoteMeEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by quote me title key.</summary>
        /// <value>The payments by quote me title key.</value>
        [AppSettingsKey("Clarity.Payments.ByQuoteMe.TitleKey"),
         DefaultValue("ui.storefront.common.QuoteMe"),
         DependsOn(nameof(PaymentsByQuoteMeEnabled))]
        public static string? PaymentsByQuoteMeTitleKey
        {
            get => PaymentsByQuoteMeEnabled
                ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.QuoteMe"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by quote me position.</summary>
        /// <value>The payments by quote me position.</value>
        [AppSettingsKey("Clarity.Payments.ByQuoteMe.Position"),
         DefaultValue(90),
         DependsOn(nameof(PaymentsByQuoteMeEnabled))]
        public static int PaymentsByQuoteMePosition
        {
            get => PaymentsByQuoteMeEnabled ? TryGet(out int asValue) ? asValue : 90 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by QuoteMe uplift use whichever is greater (if not,
        /// will combine both).</summary>
        /// <value>True if payments by QuoteMe uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByQuoteMe.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByQuoteMeUpliftPercent), nameof(PaymentsByQuoteMeUpliftAmount))]
        public static bool PaymentsByQuoteMeUpliftUseWhicheverIsGreater
        {
            get => PaymentsByQuoteMeUpliftPercent > 0 && PaymentsByQuoteMeUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByQuoteMe.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByQuoteMe.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByQuoteMeEnabled))]
        public static string? PaymentsByQuoteMeRestrictedAccountTypes
        {
            get => PaymentsByQuoteMeEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by quote me uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByQuoteMe.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByQuoteMeEnabled))]
        public static decimal PaymentsByQuoteMeUpliftPercent
        {
            get => PaymentsByQuoteMeEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by quote me uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByQuoteMe.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByQuoteMeEnabled))]
        public static decimal PaymentsByQuoteMeUpliftAmount
        {
            get => PaymentsByQuoteMeEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route sales quotes is enabled.</summary>
        /// <value>True if dashboard route sales quotes enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.SalesQuotes.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(MyDashboardEnabled), nameof(SalesQuotesEnabled))]
        public static bool DashboardRouteSalesQuotesEnabled
        {
            get => MyDashboardEnabled && SalesQuotesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the stored files path sales quotes.</summary>
        /// <value>The stored files path sales quotes.</value>
        [AppSettingsKey("Clarity.Uploads.Files.SalesQuote"),
         DefaultValue("/SalesQuote")]
        public static string StoredFilesPathSalesQuotes
        {
            get => TryGet(out string asValue) ? asValue : "/SalesQuote";
            private set => TrySet(value);
        }

        /// <summary>Gets the imports path sales quotes.</summary>
        /// <value>The imports path sales quotes.</value>
        [AppSettingsKey("Clarity.Uploads.Images.SalesQuote"),
         DefaultValue("/SalesQuote"),
         DependsOn(nameof(SalesQuotesEnabled))]
        public static string? ImportsPathSalesQuotes
        {
            get => SalesQuotesEnabled ? TryGet(out string? asValue) ? asValue : "/SalesQuote" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the checkout add addresses to book.</summary>
        /// <value>True if checkout add addresses to book, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.AddAddressesToBook"),
         DefaultValue(true),
         DependsOn(nameof(AddressBookEnabled))]
        public static bool SubmitQuoteAddAddressesToBook
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the mini cart is enabled.</summary>
        /// <value>True if mini cart enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.MiniCart.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(CartsEnabled))]
        public static bool SubmitQuoteMiniCartEnabled
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the guest submit quote is enabled.</summary>
        /// <value>True if guest submit quote enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Guest.Enabled"),
         DefaultValue(false),
         MutuallyExclusiveWith(nameof(SplitShippingEnabled))]
        public static bool GuestSubmitQuoteEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit sales quote default type key.</summary>
        /// <value>The submit sales quote default type key.</value>
        [AppSettingsKey("Clarity.Quoting.SubmitDefaultQuoteTypeKey"),
         DefaultValue("Web"),
         DependsOn(nameof(SalesQuotesEnabled))]
        public static string? SubmitSalesQuoteDefaultTypeKey
        {
            get => SalesQuotesEnabled ? TryGet(out string? asValue) ? asValue : "Web" : null;
            private set => TrySet(value);
        }

        #region Submit Quote Pane: Method
        /// <summary>Gets a value indicating whether the submit quote panes method is enabled.</summary>
        /// <value>True if submit quote panes method enabled, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Method.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool SubmitQuotePanesMethodEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes method position.</summary>
        /// <value>The submit quote panes method position.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Method.Position"),
         DefaultValue(0),
         DependsOn(nameof(SubmitQuotePanesMethodEnabled))]
        public static int SubmitQuotePanesMethodPosition
        {
            get => SubmitQuotePanesMethodEnabled ? TryGet(out int asValue) ? asValue : 0 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes method title.</summary>
        /// <value>The submit quote panes method title.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Method.Title"),
         DefaultValue("ui.storefront.checkout.checkoutPanels.CheckoutMethod"),
         DependsOn(nameof(SubmitQuotePanesMethodEnabled))]
        public static string SubmitQuotePanesMethodTitle
        {
            get => SubmitQuotePanesMethodEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.checkoutPanels.CheckoutMethod"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes method continue text.</summary>
        /// <value>The submit quote panes method continue text.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Method.ContinueText"),
         DefaultValue(""),
         DependsOn(nameof(SubmitQuotePanesMethodEnabled))]
        public static string SubmitQuotePanesMethodContinueText
        {
            get => SubmitQuotePanesMethodEnabled
                ? TryGet(out string asValue) ? asValue : string.Empty
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the submit quote panes method show button.</summary>
        /// <value>True if submit quote panes method show button, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Method.ShowButton"),
         DefaultValue(false),
         DependsOn(nameof(SubmitQuotePanesMethodEnabled))]
        public static bool SubmitQuotePanesMethodShowButton
        {
            get => SubmitQuotePanesMethodEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Submit Quote Pane: Shipping
        /// <summary>Gets a value indicating whether the submit quote panes shipping is enabled.</summary>
        /// <value>True if submit quote panes shipping enabled, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Shipping.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(ShippingEnabled))]
        public static bool SubmitQuotePanesShippingEnabled
        {
            get => ShippingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes shipping position.</summary>
        /// <value>The submit quote panes shipping position.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Shipping.Position"),
         DefaultValue(2),
         DependsOn(nameof(SubmitQuotePanesShippingEnabled))]
        public static int SubmitQuotePanesShippingPosition
        {
            get => SubmitQuotePanesShippingEnabled ? TryGet(out int asValue) ? asValue : 2 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes shipping title.</summary>
        /// <value>The submit quote panes shipping title.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Shipping.Title"),
         DefaultValue("ui.storefront.common.ShippingInformation"),
         DependsOn(nameof(SubmitQuotePanesShippingEnabled))]
        public static string SubmitQuotePanesShippingTitle
        {
            get => SubmitQuotePanesShippingEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.common.ShippingInformation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes shipping continue text.</summary>
        /// <value>The submit quote panes shipping continue text.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Shipping.ContinueText"),
         DefaultValue("ui.storefront.checkout.views.accountInformation.continueToShipping"),
         DependsOn(nameof(SubmitQuotePanesShippingEnabled))]
        public static string SubmitQuotePanesShippingContinueText
        {
            get => SubmitQuotePanesShippingEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.views.accountInformation.continueToShipping"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the submit quote panes shipping show button.</summary>
        /// <value>True if submit quote panes shipping show button, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Shipping.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(SubmitQuotePanesShippingEnabled))]
        public static bool SubmitQuotePanesShippingShowButton
        {
            get => SubmitQuotePanesShippingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Submit Quote Pane: Confirmation
        /// <summary>Gets a value indicating whether the submit quote panes confirmation is enabled.</summary>
        /// <value>True if submit quote panes confirmation enabled, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Confirmation.Enabled"),
         DefaultValue(false)]
        public static bool SubmitQuotePanesConfirmationEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the submit quote panes confirmation is conditionally enabled.</summary>
        /// <value>True if submit quote panes confirmation conditionally enabled, false if not.</value>
        /// <remarks>Only show if there's no other mid-pane to show (free items that don't ship).</remarks>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Confirmation.ConditionallyEnabled"),
         DefaultValue(true)]
        public static bool SubmitQuotePanesConfirmationConditionallyEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes confirmation position.</summary>
        /// <value>The submit quote panes confirmation position.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Confirmation.Position"),
         DefaultValue(3),
         DependsOn(nameof(SubmitQuotePanesConfirmationEnabled))]
        public static int SubmitQuotePanesConfirmationPosition
        {
            get => SubmitQuotePanesConfirmationEnabled || SubmitQuotePanesConfirmationConditionallyEnabled
                ? TryGet(out int asValue)
                    ? asValue
                    : 3
                : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes confirmation title.</summary>
        /// <value>The submit quote panes confirmation title.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Confirmation.Title"),
         DefaultValue("ui.storefront.submitQuote.QuoteConfirmation"),
         DependsOn(nameof(SubmitQuotePanesConfirmationEnabled))]
        public static string SubmitQuotePanesConfirmationTitle
        {
            get => SubmitQuotePanesConfirmationEnabled || SubmitQuotePanesConfirmationConditionallyEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.submitQuote.QuoteConfirmation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes confirmation continue text.</summary>
        /// <value>The submit quote panes confirmation continue text.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Confirmation.ContinueText"),
         DefaultValue("ui.storefront.common.SubmitQuote"),
         DependsOn(nameof(SubmitQuotePanesConfirmationEnabled))]
        public static string SubmitQuotePanesConfirmationContinueText
        {
            get => SubmitQuotePanesConfirmationEnabled || SubmitQuotePanesConfirmationConditionallyEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.common.SubmitQuote"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the submit quote panes confirmation show button.</summary>
        /// <value>True if submit quote panes confirmation show button, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Confirmation.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(SubmitQuotePanesConfirmationEnabled))]
        public static bool SubmitQuotePanesConfirmationShowButton
        {
            get => SubmitQuotePanesConfirmationEnabled || SubmitQuotePanesConfirmationConditionallyEnabled
                ? TryGet(out bool asValue)
                    ? asValue
                    : true
                : false;
            private set => TrySet(value);
        }
        #endregion

        #region Submit Quote Pane: Completed
        /// <summary>Gets a value indicating whether the submit quote panes completed is enabled.</summary>
        /// <value>True if submit quote panes completed enabled, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Completed.Enabled"),
         DefaultValue(true)]
        public static bool SubmitQuotePanesCompletedEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes completed position.</summary>
        /// <value>The submit quote panes completed position.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Completed.Position"),
         DefaultValue(4),
         DependsOn(nameof(SubmitQuotePanesCompletedEnabled))]
        public static int SubmitQuotePanesCompletedPosition
        {
            get => SubmitQuotePanesCompletedEnabled
                ? TryGet(out int asValue)
                    ? asValue
                    : 4
                : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes completed title.</summary>
        /// <value>The submit quote panes completed title.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Completed.Title"),
         DefaultValue("ui.storefront.checkout.checkoutPanels.Complete"),
         DependsOn(nameof(SubmitQuotePanesCompletedEnabled))]
        public static string SubmitQuotePanesCompletedTitle
        {
            get => SubmitQuotePanesCompletedEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.checkoutPanels.Complete"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the submit quote panes completed continue text.</summary>
        /// <value>The submit quote panes completed continue text.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Completed.ContinueText"),
         DefaultValue("ui.storefront.common.SubmitQuote"),
         DependsOn(nameof(SubmitQuotePanesCompletedEnabled))]
        public static string SubmitQuotePanesCompletedContinueText
        {
            get => SubmitQuotePanesCompletedEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.common.SubmitQuote"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the submit quote panes completed show button.</summary>
        /// <value>True if submit quote panes completed show button, false if not.</value>
        [AppSettingsKey("Clarity.SubmitQuote.Panes.Completed.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(SubmitQuotePanesCompletedEnabled))]
        public static bool SubmitQuotePanesCompletedShowButton
        {
            get => SubmitQuotePanesCompletedEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Personal Details Display
        /// <summary>Hide billing first name.</summary>
        /// <value>True if submit quote hide billing first name, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideFirstName"),
         DefaultValue(false)]
        public static bool SubmitQuoteHideBillingFirstName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing last name.</summary>
        /// <value>True if submit quote hide billing last name, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideLastName"),
         DefaultValue(false),
         Unused]
        public static bool SubmitQuoteHideBillingLastName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing email.</summary>
        /// <value>True if submit quote hide billing email, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideEmail"),
         DefaultValue(false),
         Unused]
        public static bool SubmitQuoteHideBillingEmail
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing phone.</summary>
        /// <value>True if submit quote hide billing phone, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.ShippBillinging.HidePhone"),
         DefaultValue(false)]
        public static bool SubmitQuoteHideBillingPhone
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing fax.</summary>
        /// <value>True if submit quote hide billing fax, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideFax"),
         DefaultValue(false),
         Unused]
        public static bool SubmitQuoteHideBillingFax
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping first name.</summary>
        /// <value>True if submit quote hide shipping first name, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideFirstName"),
         DefaultValue(false)]
        public static bool SubmitQuoteHideShippingFirstName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping last name.</summary>
        /// <value>True if submit quote hide shipping last name, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideLastName"),
         DefaultValue(false),
         Unused]
        public static bool SubmitQuoteHideShippingLastName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping email.</summary>
        /// <value>True if submit quote hide shipping email, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideEmail"),
         DefaultValue(false),
         Unused]
        public static bool SubmitQuoteHideShippingEmail
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping phone.</summary>
        /// <value>True if submit quote hide shipping phone, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HidePhone"),
         DefaultValue(false)]
        public static bool SubmitQuoteHideShippingPhone
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping fax.</summary>
        /// <value>True if submit quote hide shipping fax, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideFax"),
         DefaultValue(false),
         Unused]
        public static bool SubmitQuoteHideShippingFax
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion
    }
}
