// <copyright file="CEFConfig.Properties.Checkout.cs" company="clarity-ventures.com">
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
        /// <summary>Gets a value indicating whether the checkout add addresses to book.</summary>
        /// <value>True if checkout add addresses to book, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.AddAddressesToBook"),
         DefaultValue(true),
         DependsOn(nameof(AddressBookEnabled))]
        public static bool PurchaseAddAddressesToBook
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the phone prefix lookups is enabled.</summary>
        /// <value>True if phone prefix lookups enabled, false if not.</value>
        [AppSettingsKey("Clarity.PhonePrefixLookups.Enabled"),
         DefaultValue(false)]
        public static bool PhonePrefixLookupsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the mini cart is enabled.</summary>
        /// <value>True if mini cart enabled, false if not.</value>
        [AppSettingsKey("Clarity.Checkout.MiniCart.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(CartsEnabled))]
        public static bool MiniCartEnabled
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the split shipping is enabled.</summary>
        /// <value>True if split shipping enabled, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.Split.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(ShippingEnabled))]
        public static bool SplitShippingEnabled
        {
            get => ShippingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the split shipping only allow one destination.</summary>
        /// <value>True if split shipping only allow one destination, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.Split.OnlyAllowOneDestination"),
         DefaultValue(false),
         DependsOn(nameof(SplitShippingEnabled))]
        public static bool SplitShippingOnlyAllowOneDestination
        {
            get => SplitShippingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the restricted shipping is enabled.</summary>
        /// <value>True if restricted shipping enabled, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.Restricted.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(ShippingEnabled))]
        public static bool RestrictedShippingEnabled
        {
            get => ShippingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Ship to Store: Buy products online from another store and have them moved to your local store.</summary>
        /// <value>True if shipping ship to store enabled, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.ShipToStore.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(ShippingEstimatesEnabled), nameof(StoresEnabled))]
        public static bool ShippingShipToStoreEnabled
        {
            get => ShippingEstimatesEnabled && StoresEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>In Store Pickup: Buy products online and get them from the store's service desk.</summary>
        /// <value>True if shipping in store pickup enabled, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.InStorePickup.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(ShippingEstimatesEnabled), nameof(StoresEnabled))]
        public static bool ShippingInStorePickupEnabled
        {
            get => ShippingEstimatesEnabled && StoresEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the billing is enabled.</summary>
        /// <value>True if billing enabled, false if not.</value>
        [AppSettingsKey("Clarity.Billing.Enabled"),
         DefaultValue(true)]
        public static bool BillingEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the checkout sales order default type key.</summary>
        /// <value>The checkout sales order default type key.</value>
        [AppSettingsKey("Clarity.Ordering.CheckoutDefaultOrderTypeKey"),
         DefaultValue("Web"),
         DependsOn(nameof(SalesOrdersEnabled))]
        public static string? CheckoutSalesOrderDefaultTypeKey
        {
            get => SalesOrdersEnabled ? TryGet(out string? asValue) ? asValue : "Web" : null;
            private set => TrySet(value);
        }

        #region ACH
        /// <summary>Gets a value indicating whether the payments by ACH is enabled.</summary>
        /// <value>True if payments by ACH enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByACH.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsEnabled))]
        public static bool PaymentsByACHEnabled
        {
            get => PaymentsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by ACH title key.</summary>
        /// <value>The payments by ACH title key.</value>
        [AppSettingsKey("Clarity.Payments.ByACH.TitleKey"),
         DefaultValue("ui.storefront.common.ACH"),
         DependsOn(nameof(PaymentsByACHEnabled))]
        public static string? PaymentsByACHTitleKey
        {
            get => PaymentsByACHEnabled ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.ACH" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by ACH position.</summary>
        /// <value>The payments by ACH position.</value>
        [AppSettingsKey("Clarity.Payments.ByACH.Position"),
         DefaultValue(50),
         DependsOn(nameof(PaymentsByACHEnabled))]
        public static int PaymentsByACHPosition
        {
            get => PaymentsByACHEnabled ? TryGet(out int asValue) ? asValue : 50 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by ACH uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by ACH uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByACH.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByACHUpliftPercent), nameof(PaymentsByACHUpliftAmount))]
        public static bool PaymentsByACHUpliftUseWhicheverIsGreater
        {
            get => PaymentsByACHUpliftPercent > 0 && PaymentsByACHUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByACH.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByACH.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByACHEnabled))]
        public static string? PaymentsByACHRestrictedAccountTypes
        {
            get => PaymentsByACHEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by ACH uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByACH.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByACHEnabled))]
        public static decimal PaymentsByACHUpliftPercent
        {
            get => PaymentsByACHEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by ACH uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByACH.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByACHEnabled))]
        public static decimal PaymentsByACHUpliftAmount
        {
            get => PaymentsByACHEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }
        #endregion

        #region Credit Card
        /// <summary>Gets a value indicating whether the payments by credit card is enabled.</summary>
        /// <value>True if payments by credit card enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByCreditCard.Enabled"),
         DefaultValue(true)]
        public static bool PaymentsByCreditCardEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by credit card title key.</summary>
        /// <value>The payments by credit card title key.</value>
        [AppSettingsKey("Clarity.Payments.ByCreditCard.TitleKey"),
         DefaultValue("ui.storefront.common.CreditCard"),
         DependsOn(nameof(PaymentsByCreditCardEnabled))]
        public static string? PaymentsByCreditCardTitleKey
        {
            get => PaymentsByCreditCardEnabled
                ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.CreditCard"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by credit card position.</summary>
        /// <value>The payments by credit card position.</value>
        [AppSettingsKey("Clarity.Payments.ByCreditCard.Position"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByCreditCardEnabled))]
        public static int PaymentsByCreditCardPosition
        {
            get => PaymentsByCreditCardEnabled ? TryGet(out int asValue) ? asValue : 0 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by credit card uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by credit card uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByCreditCard.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByCreditCardUpliftPercent), nameof(PaymentsByCreditCardUpliftAmount))]
        public static bool PaymentsByCreditCardUpliftUseWhicheverIsGreater
        {
            get => PaymentsByCreditCardUpliftPercent > 0 && PaymentsByCreditCardUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByCreditCard.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION .</example>
        [AppSettingsKey("Clarity.Payments.ByCreditCard.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByCreditCardEnabled))]
        public static string? PaymentsByCreditCardRestrictedAccountTypes
        {
            get => PaymentsByCreditCardEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by credit card uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByCreditCard.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByCreditCardEnabled))]
        public static decimal PaymentsByCreditCardUpliftPercent
        {
            get => PaymentsByCreditCardEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by credit card uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByCreditCard.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByCreditCardEnabled))]
        public static decimal PaymentsByCreditCardUpliftAmount
        {
            get => PaymentsByCreditCardEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }
        #endregion

        #region eCheck
        /// <summary>Gets a value indicating whether the payments by echeck is enabled.</summary>
        /// <value>True if payments by echeck enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByEcheck.Enabled"),
         DefaultValue(false)]
        public static bool PaymentsByEcheckEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by echeck title key.</summary>
        /// <value>The payments by echeck title key.</value>
        [AppSettingsKey("Clarity.Payments.ByEcheck.TitleKey"),
         DefaultValue("ui.storefront.common.Echeck"),
         DependsOn(nameof(PaymentsByEcheckEnabled))]
        public static string? PaymentsByEcheckTitleKey
        {
            get => PaymentsByEcheckEnabled ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.Echeck" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by echeck position.</summary>
        /// <value>The payments by echeck position.</value>
        [AppSettingsKey("Clarity.Payments.ByEcheck.Position"),
         DefaultValue(20),
         DependsOn(nameof(PaymentsByEcheckEnabled))]
        public static int PaymentsByEcheckPosition
        {
            get => PaymentsByEcheckEnabled ? TryGet(out int asValue) ? asValue : 20 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by eCheck uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by eCheck uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByEcheck.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByECheckUpliftPercent), nameof(PaymentsByECheckUpliftAmount))]
        public static bool PaymentsByECheckUpliftUseWhicheverIsGreater
        {
            get => PaymentsByECheckUpliftPercent > 0 && PaymentsByECheckUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByECheck.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByECheck.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByEcheckEnabled))]
        public static string? PaymentsByECheckRestrictedAccountTypes
        {
            get => PaymentsByEcheckEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by e check uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByEcheck.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByEcheckEnabled))]
        public static decimal PaymentsByECheckUpliftPercent
        {
            get => PaymentsByEcheckEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by e check uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByEcheck.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByEcheckEnabled))]
        public static decimal PaymentsByECheckUpliftAmount
        {
            get => PaymentsByEcheckEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }
        #endregion

        #region Free
        /// <summary>Gets a value indicating whether the payments by free is enabled.</summary>
        /// <value>True if payments by free enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByFree.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(PaymentsByFreeEnabled))]
        public static bool PaymentsByFreeEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by free title key.</summary>
        /// <value>The payments by free title key.</value>
        [AppSettingsKey("Clarity.Payments.ByFree.TitleKey"),
         DefaultValue("ui.storefront.common.Free"),
         DependsOn(nameof(PaymentsByFreeEnabled))]
        public static string? PaymentsByFreeTitleKey
        {
            get => PaymentsByFreeEnabled
                ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.Free"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by free position.</summary>
        /// <value>The payments by free position.</value>
        [AppSettingsKey("Clarity.Payments.ByFree.Position"),
         DefaultValue(60),
         DependsOn(nameof(PaymentsByFreeEnabled))]
        public static int PaymentsByFreePosition
        {
            get => PaymentsByFreeEnabled ? TryGet(out int asValue) ? asValue : 60 : int.MaxValue;
            private set => TrySet(value);
        }
        #endregion

        #region Invoice
        // See CEFConfig.Properties.SalesInvoices.cs
        #endregion

        #region PayPal
        /// <summary>Gets a value indicating whether the payments by pay palette is enabled.</summary>
        /// <value>True if payments by pay palette enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByPayPal.Enabled"),
         DefaultValue(false)]
        public static bool PaymentsByPayPalEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by pay palette title key.</summary>
        /// <value>The payments by pay palette title key.</value>
        [AppSettingsKey("Clarity.Payments.ByPayPal.TitleKey"),
         DefaultValue("ui.storefront.common.PayPal"),
         DependsOn(nameof(PaymentsByPayPalEnabled))]
        public static string? PaymentsByPayPalTitleKey
        {
            get => PaymentsByPayPalEnabled ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.PayPal" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by pay palette position.</summary>
        /// <value>The payments by pay palette position.</value>
        [AppSettingsKey("Clarity.Payments.ByPayPal.Position"),
         DefaultValue(100),
         DependsOn(nameof(PaymentsByPayPalEnabled))]
        public static int PaymentsByPayPalPosition
        {
            get => PaymentsByPayPalEnabled ? TryGet(out int asValue) ? asValue : 100 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by PayPal uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by PayPal uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByPayPal.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByPayPalUpliftPercent), nameof(PaymentsByPayPalUpliftAmount))]
        public static bool PaymentsByPayPalUpliftUseWhicheverIsGreater
        {
            get => PaymentsByPayPalUpliftPercent > 0 && PaymentsByPayPalUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByPayPal.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByPayPal.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByPayPalEnabled))]
        public static string? PaymentsByPayPalRestrictedAccountTypes
        {
            get => PaymentsByPayPalEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by pay palette uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByPayPal.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByPayPalEnabled))]
        public static decimal PaymentsByPayPalUpliftPercent
        {
            get => PaymentsByPayPalEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by pay palette uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByPayPal.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByPayPalEnabled))]
        public static decimal PaymentsByPayPalUpliftAmount
        {
            get => PaymentsByPayPalEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }
        #endregion

        #region Payoneer
        /// <summary>Gets a value indicating whether the payments by payoneer is enabled.</summary>
        /// <value>True if payments by payoneer enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByPayoneer.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(LoginEnabled))]
        public static bool PaymentsByPayoneerEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by payoneer title key.</summary>
        /// <value>The payments by payoneer title key.</value>
        [AppSettingsKey("Clarity.Payments.ByPayoneer.TitleKey"),
         DefaultValue("ui.storefront.common.Payoneer"),
         DependsOn(nameof(PaymentsByPayoneerEnabled))]
        public static string? PaymentsByPayoneerTitleKey
        {
            get => PaymentsByPayoneerEnabled
                ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.Payoneer"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by payoneer position.</summary>
        /// <value>The payments by payoneer position.</value>
        [AppSettingsKey("Clarity.Payments.ByPayoneer.Position"),
         DefaultValue(100),
         DependsOn(nameof(PaymentsByPayoneerEnabled))]
        public static int PaymentsByPayoneerPosition
        {
            get => PaymentsByPayoneerEnabled ? TryGet(out int asValue) ? asValue : 100 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by Payoneer uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by Payoneer uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByPayoneer.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByPayoneerUpliftPercent), nameof(PaymentsByPayoneerUpliftAmount))]
        public static bool PaymentsByPayoneerUpliftUseWhicheverIsGreater
        {
            get => PaymentsByPayoneerUpliftPercent > 0 && PaymentsByPayoneerUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByPayoneer.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByPayoneer.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByPayoneerEnabled))]
        public static string? PaymentsByPayoneerRestrictedAccountTypes
        {
            get => PaymentsByPayoneerEnabled ? TryGet(out string asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by payoneer uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByPayoneer.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByPayoneerEnabled))]
        public static decimal PaymentsByPayoneerUpliftPercent
        {
            get => PaymentsByPayoneerEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by payoneer uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByPayoneer.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByPayoneerEnabled))]
        public static decimal PaymentsByPayoneerUpliftAmount
        {
            get => PaymentsByPayoneerEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }
        #endregion

        #region Quote Me
        // See CEFConfig.Properties.SalesQuotes.cs
        #endregion

        #region Store Credit
        /// <summary>Gets a value indicating whether the payments by store credit is enabled.</summary>
        /// <value>True if payments by store credit enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByStoreCredit.Enabled"),
         DefaultValue(false)]
        public static bool PaymentsByStoreCreditEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by store credit title key.</summary>
        /// <value>The payments by store credit title key.</value>
        [AppSettingsKey("Clarity.Payments.ByStoreCredit.TitleKey"),
         DefaultValue("ui.storefront.common.StoreCredit"),
         DependsOn(nameof(PaymentsByStoreCreditEnabled))]
        public static string? PaymentsByStoreCreditTitleKey
        {
            get => PaymentsByStoreCreditEnabled
                ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.StoreCredit"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by store credit position.</summary>
        /// <value>The payments by store credit position.</value>
        [AppSettingsKey("Clarity.Payments.ByStoreCredit.Position"),
         DefaultValue(70),
         DependsOn(nameof(PaymentsByStoreCreditEnabled))]
        public static int PaymentsByStoreCreditPosition
        {
            get => PaymentsByStoreCreditEnabled ? TryGet(out int asValue) ? asValue : 70 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by StoreCredit uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by StoreCredit uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByStoreCredit.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByStoreCreditUpliftPercent), nameof(PaymentsByStoreCreditUpliftAmount))]
        public static bool PaymentsByStoreCreditUpliftUseWhicheverIsGreater
        {
            get => PaymentsByStoreCreditUpliftPercent > 0 && PaymentsByStoreCreditUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByStoreCredit.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByStoreCredit.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByStoreCreditEnabled))]
        public static string? PaymentsByStoreCreditRestrictedAccountTypes
        {
            get => PaymentsByStoreCreditEnabled ? TryGet(out string asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by store credit uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByStoreCredit.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByStoreCreditEnabled))]
        public static decimal PaymentsByStoreCreditUpliftPercent
        {
            get => PaymentsByStoreCreditEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by store credit uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByStoreCredit.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByStoreCreditEnabled))]
        public static decimal PaymentsByStoreCreditUpliftAmount
        {
            get => PaymentsByStoreCreditEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }
        #endregion

        #region Wire Transfer
        /// <summary>Gets a value indicating whether the payments by wire transfer is enabled.</summary>
        /// <value>True if payments by wire transfer enabled, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByWireTransfer.Enabled"),
         DefaultValue(false)]
        public static bool PaymentsByWireTransferEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by wire transfer title key.</summary>
        /// <value>The payments by wire transfer title key.</value>
        [AppSettingsKey("Clarity.Payments.ByWireTransfer.TitleKey"),
         DefaultValue("ui.storefront.common.WireTransfer"),
         DependsOn(nameof(PaymentsByWireTransferEnabled))]
        public static string? PaymentsByWireTransferTitleKey
        {
            get => PaymentsByWireTransferEnabled
                ? TryGet(out string? asValue) ? asValue : "ui.storefront.common.WireTransfer"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments by wire transfer position.</summary>
        /// <value>The payments by wire transfer position.</value>
        [AppSettingsKey("Clarity.Payments.ByWireTransfer.Position"),
         DefaultValue(40),
         DependsOn(nameof(PaymentsByWireTransferEnabled))]
        public static int PaymentsByWireTransferPosition
        {
            get => PaymentsByWireTransferEnabled ? TryGet(out int asValue) ? asValue : 40 : int.MaxValue;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments by WireTransfer uplift use whichever is greater (if not, will combine both).</summary>
        /// <value>True if payments by WireTransfer uplift use whichever is greater, false if not.</value>
        [AppSettingsKey("Clarity.Payments.ByWireTransfer.Uplifts.UseGreater"),
         DefaultValue(false),
         DependsOn(nameof(PaymentsByWireTransferUpliftPercent), nameof(PaymentsByWireTransferUpliftAmount))]
        public static bool PaymentsByWireTransferUpliftUseWhicheverIsGreater
        {
            get => PaymentsByWireTransferUpliftPercent > 0 && PaymentsByWireTransferUpliftAmount > 0
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>A list of account types restricted from using PaymentsByWireTransfer.</summary>
        /// <value>Comma delimited list of account types.</value>
        /// <example>CUSTOMER,ORGANIZATION.</example>
        [AppSettingsKey("Clarity.Payments.ByWireTransfer.RestrictedAccountTypes"),
         DefaultValue(null),
         DependsOn(nameof(PaymentsByWireTransferEnabled))]
        public static string? PaymentsByWireTransferRestrictedAccountTypes
        {
            get => PaymentsByWireTransferEnabled ? TryGet(out string asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>A percentage uplift.</summary>
        /// <value>The payments by wire transfer uplift percent.</value>
        /// <example>0.03 = 3% increase, -0.03 = 3% decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByWireTransfer.Uplifts.Percent"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByWireTransferEnabled))]
        public static decimal PaymentsByWireTransferUpliftPercent
        {
            get => PaymentsByWireTransferEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>An amount uplift.</summary>
        /// <value>The payments by wire transfer uplift amount.</value>
        /// <example>5.00 = $5.00 increase, -5.00 = $5.00 decrease.</example>
        [AppSettingsKey("Clarity.Payments.ByWireTransfer.Uplifts.Amount"),
         DefaultValue(0),
         DependsOn(nameof(PaymentsByWireTransferEnabled))]
        public static decimal PaymentsByWireTransferUpliftAmount
        {
            get => PaymentsByWireTransferEnabled ? TryGet(out decimal asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }
        #endregion

        #region Custom
        /// <summary>Gets the payments by custom.</summary>
        /// <value>The payments by custom.</value>
        [AppSettingsKey("Clarity.Payments.ByCustom"),
         DefaultValue(""),
         SplitOn(new[] { ',' })]
        public static string[] PaymentsByCustom
        {
            get => TryGet(out string[] asValue) ? asValue : Array.Empty<string>();
            private set => TrySet(value);
        }
        #endregion

        #region Purchase Pane: Method
        /// <summary>Gets a value indicating whether the purchase panes method is enabled.</summary>
        /// <value>True if purchase panes method enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Method.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool PurchasePanesMethodEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes method show.</summary>
        /// <value>True if purchase panes method show, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Method.Show"),
         DefaultValue(false),
         DependsOn(nameof(PurchasePanesMethodEnabled))]
        public static bool PurchasePanesMethodShow
        {
            get => PurchasePanesMethodEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes method position.</summary>
        /// <value>The purchase panes method position.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Method.Position"),
         DefaultValue(0),
         DependsOn(nameof(PurchasePanesMethodEnabled))]
        public static int PurchasePanesMethodPosition
        {
            get => PurchasePanesMethodEnabled ? TryGet(out int asValue) ? asValue : 0 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes method title.</summary>
        /// <value>The purchase panes method title.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Method.Title"),
         DefaultValue("ui.storefront.checkout.checkoutPanels.CheckoutMethod"),
         DependsOn(nameof(PurchasePanesMethodEnabled))]
        public static string PurchasePanesMethodTitle
        {
            get => PurchasePanesMethodEnabled
                ? TryGet(out string asValue)
                    ? asValue!
                    : "ui.storefront.checkout.checkoutPanels.CheckoutMethod"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes method continue text.</summary>
        /// <value>The purchase panes method continue text.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Method.ContinueText"),
         DefaultValue(""),
         DependsOn(nameof(PurchasePanesMethodEnabled))]
        public static string PurchasePanesMethodContinueText
        {
            get => PurchasePanesMethodEnabled
                ? TryGet(out string asValue) ? asValue : string.Empty
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes method show button.</summary>
        /// <value>True if purchase panes method show button, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Method.ShowButton"),
         DefaultValue(false),
         DependsOn(nameof(PurchasePanesMethodEnabled))]
        public static bool PurchasePanesMethodShowButton
        {
            get => PurchasePanesMethodEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Purchase Pane: Billing
        /// <summary>Gets a value indicating whether the purchase panes billing is enabled.</summary>
        /// <value>True if purchase panes billing enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Billing.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(BillingEnabled))]
        public static bool PurchasePanesBillingEnabled
        {
            get => BillingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes billing show.</summary>
        /// <value>True if purchase panes billing show, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Billing.Show"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesBillingEnabled))]
        public static bool PurchasePanesBillingShow
        {
            get => PurchasePanesBillingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes billing position.</summary>
        /// <value>The purchase panes billing position.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Billing.Position"),
         DefaultValue(1),
         DependsOn(nameof(PurchasePanesBillingEnabled))]
        public static int PurchasePanesBillingPosition
        {
            get => PurchasePanesBillingEnabled ? TryGet(out int asValue) ? asValue : 1 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes billing title.</summary>
        /// <value>The purchase panes billing title.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Billing.Title"),
         DefaultValue("ui.storefront.checkout.checkoutPanels.billingAndContactInformation"),
         DependsOn(nameof(PurchasePanesBillingEnabled))]
        public static string PurchasePanesBillingTitle
        {
            get => PurchasePanesBillingEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.checkoutPanels.billingAndContactInformation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes billing continue text.</summary>
        /// <value>The purchase panes billing continue text.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Billing.ContinueText"),
         DefaultValue("ui.storefront.cart.continueToBilling"),
         DependsOn(nameof(PurchasePanesBillingEnabled))]
        public static string PurchasePanesBillingContinueText
        {
            get => PurchasePanesBillingEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.cart.continueToBilling"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes billing show button.</summary>
        /// <value>True if purchase panes billing show button, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Billing.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesBillingEnabled))]
        public static bool PurchasePanesBillingShowButton
        {
            get => PurchasePanesBillingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Purchase Pane: Shipping
        /// <summary>Gets a value indicating whether the purchase panes shipping is enabled.</summary>
        /// <value>True if purchase panes shipping enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Shipping.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(ShippingEnabled))]
        public static bool PurchasePanesShippingEnabled
        {
            get => ShippingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes shipping show.</summary>
        /// <value>True if purchase panes shipping show, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Shipping.Show"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesShippingEnabled))]
        public static bool PurchasePanesShippingShow
        {
            get => PurchasePanesShippingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes shipping position.</summary>
        /// <value>The purchase panes shipping position.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Shipping.Position"),
         DefaultValue(2),
         DependsOn(nameof(PurchasePanesShippingEnabled))]
        public static int PurchasePanesShippingPosition
        {
            get => PurchasePanesShippingEnabled ? TryGet(out int asValue) ? asValue : 2 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes shipping title.</summary>
        /// <value>The purchase panes shipping title.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Shipping.Title"),
         DefaultValue("ui.storefront.common.ShippingInformation"),
         DependsOn(nameof(PurchasePanesShippingEnabled))]
        public static string PurchasePanesShippingTitle
        {
            get => PurchasePanesShippingEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.common.ShippingInformation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes shipping continue text.</summary>
        /// <value>The purchase panes shipping continue text.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Shipping.ContinueText"),
         DefaultValue("ui.storefront.checkout.views.accountInformation.continueToShipping"),
         DependsOn(nameof(PurchasePanesShippingEnabled))]
        public static string PurchasePanesShippingContinueText
        {
            get => PurchasePanesShippingEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.views.accountInformation.continueToShipping"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes shipping show button.</summary>
        /// <value>True if purchase panes shipping show button, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Shipping.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesShippingEnabled))]
        public static bool PurchasePanesShippingShowButton
        {
            get => PurchasePanesShippingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Purchase Pane: Payments
        /// <summary>Gets a value indicating whether the purchase panes payments is enabled.</summary>
        /// <value>True if purchase panes payments enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Payments.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(PaymentsEnabled))]
        public static bool PurchasePanesPaymentsEnabled
        {
            get => PaymentsEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes payments show.</summary>
        /// <value>True if purchase panes payments show, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Payments.Show"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesPaymentsEnabled))]
        public static bool PurchasePanesPaymentsShow
        {
            get => PurchasePanesPaymentsEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes payments position.</summary>
        /// <value>The purchase panes payments position.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Payments.Position"),
         DefaultValue(3),
         DependsOn(nameof(PaymentsEnabled), nameof(PurchasePanesShippingEnabled))]
        public static int PurchasePanesPaymentsPosition
        {
            get => PaymentsEnabled && PurchasePanesPaymentsEnabled ? TryGet(out int asValue) ? asValue : 3 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes payments title.</summary>
        /// <value>The purchase panes payments title.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Payments.Title"),
         DefaultValue("ui.storefront.checkout.views.paymentInformation.selectAPaymentMethod"),
         DependsOn(nameof(PurchasePanesPaymentsEnabled))]
        public static string PurchasePanesPaymentsTitle
        {
            get => PurchasePanesPaymentsEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.views.paymentInformation.selectAPaymentMethod"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes payments continue text.</summary>
        /// <value>The purchase panes payments continue text.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Payments.ContinueText"),
         DefaultValue("ui.storefront.checkout.views.shippingInformation.continueToPayment"),
         DependsOn(nameof(PurchasePanesPaymentsEnabled))]
        public static string PurchasePanesPaymentsContinueText
        {
            get => PurchasePanesPaymentsEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.views.shippingInformation.continueToPayment"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes payments show button.</summary>
        /// <value>True if purchase panes payments show button, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Payments.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesPaymentsEnabled))]
        public static bool PurchasePanesPaymentsShowButton
        {
            get => PurchasePanesPaymentsEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Purchase Pane: Confirmation
        /// <summary>Gets a value indicating whether the purchase panes confirmation is enabled.</summary>
        /// <value>True if purchase panes confirmation enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Confirmation.Enabled"),
         DefaultValue(false)]
        public static bool PurchasePanesConfirmationEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes confirmation show.</summary>
        /// <value>True if purchase panes confirmation show, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Confirmation.Show"),
         DefaultValue(false),
         DependsOn(nameof(PurchasePanesConfirmationEnabled))]
        public static bool PurchasePanesConfirmationShow
        {
            get => PurchasePanesConfirmationEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes confirmation position.</summary>
        /// <value>The purchase panes confirmation position.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Confirmation.Position"),
         DefaultValue(4),
         DependsOn(nameof(PurchasePanesConfirmationEnabled))]
        public static int PurchasePanesConfirmationPosition
        {
            get => PurchasePanesConfirmationEnabled ? TryGet(out int asValue) ? asValue : 4 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes confirmation title.</summary>
        /// <value>The purchase panes confirmation title.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Confirmation.Title"),
         DefaultValue("ui.storefront.checkout.checkoutPanels.orderConfirmation"),
         DependsOn(nameof(PurchasePanesConfirmationEnabled))]
        public static string PurchasePanesConfirmationTitle
        {
            get => PurchasePanesConfirmationEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.checkoutPanels.orderConfirmation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes confirmation continue text.</summary>
        /// <value>The purchase panes confirmation continue text.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Confirmation.ContinueText"),
         DefaultValue("ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase"),
         DependsOn(nameof(PurchasePanesConfirmationEnabled))]
        public static string PurchasePanesConfirmationContinueText
        {
            get => PurchasePanesConfirmationEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes confirmation show button.</summary>
        /// <value>True if purchase panes confirmation show button, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Confirmation.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesConfirmationEnabled))]
        public static bool PurchasePanesConfirmationShowButton
        {
            get => PurchasePanesConfirmationEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Purchase Pane: Completed
        /// <summary>Gets a value indicating whether the purchase panes completed is enabled.</summary>
        /// <value>True if purchase panes completed enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Completed.Enabled"),
         DefaultValue(true)]
        public static bool PurchasePanesCompletedEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes completed show.</summary>
        /// <value>True if purchase panes completed show, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Completed.Show"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesCompletedEnabled))]
        public static bool PurchasePanesCompletedShow
        {
            get => PurchasePanesCompletedEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes completed position.</summary>
        /// <value>The purchase panes completed position.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Completed.Position"),
         DefaultValue(5),
         DependsOn(nameof(PurchasePanesCompletedEnabled))]
        public static int PurchasePanesCompletedPosition
        {
            get => PurchasePanesCompletedEnabled ? TryGet(out int asValue) ? asValue : 4 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes completed title.</summary>
        /// <value>The purchase panes completed title.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Completed.Title"),
         DefaultValue("ui.storefront.checkout.checkoutPanels.Complete"),
         DependsOn(nameof(PurchasePanesCompletedEnabled))]
        public static string PurchasePanesCompletedTitle
        {
            get => PurchasePanesCompletedEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.checkoutPanels.Complete"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase panes completed continue text.</summary>
        /// <value>The purchase panes completed continue text.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Completed.ContinueText"),
         DefaultValue("ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase"),
         DependsOn(nameof(PurchasePanesCompletedEnabled))]
        public static string PurchasePanesCompletedContinueText
        {
            get => PurchasePanesCompletedEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase panes completed show button.</summary>
        /// <value>True if purchase panes completed show button, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Panes.Completed.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(PurchasePanesCompletedEnabled))]
        public static bool PurchasePanesCompletedShowButton
        {
            get => PurchasePanesCompletedEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        /// <summary>Gets a value indicating whether the purchase use preferred payment method for later payments.</summary>
        /// <value>True if purchase use preferred payment method for later payments, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.UsePreferredPaymentMethodForLaterPayments"),
         DefaultValue(false)]
        public static bool PurchaseUsePreferredPaymentMethodForLaterPayments
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase allow account on hold orders.</summary>
        /// <value>True if purchase allow account on hold orders, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.AllowAccountOnHoldOrders"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool PurchaseAllowAccountOnHoldOrders
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase require ship option.</summary>
        /// <value>True if purchase require ship option, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.RequireShipOption"),
         DefaultValue(false)]
        public static bool PurchaseRequireShipOption
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase override and force no ship to option if when ship
        /// option selected.</summary>
        /// <value>True if purchase override and force no ship to option if when ship option selected, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.OverrideAndForceNoShipToOptionIfWhenShipOptionSelected"),
         DefaultValue(true),
         MutuallyExclusiveWith(nameof(PurchaseRequireShipOption))]
        public static bool PurchaseOverrideAndForceNoShipToOptionIfWhenShipOptionSelected
        {
            get => !PurchaseRequireShipOption ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase override and force ship to home option if no ship
        /// option selected.</summary>
        /// <value>True if purchase override and force ship to home option if no ship option selected, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.OverrideAndForceShipToHomeOptionIfNoShipOptionSelected"),
         DefaultValue(false),
         MutuallyExclusiveWith(nameof(PurchaseOverrideAndForceNoShipToOptionIfWhenShipOptionSelected))]
        public static bool PurchaseOverrideAndForceShipToHomeOptionIfNoShipOptionSelected
        {
            get => !PurchaseOverrideAndForceNoShipToOptionIfWhenShipOptionSelected
                ? TryGet(out bool asValue) ? asValue : false
            : false;
            private set => TrySet(value);
        }

        /// <summary>When populating the UI with Billing and Shipping details, re-use the last order's information when
        /// possible Note: Only possible when using Single and Split (not Targets) checkouts.</summary>
        /// <value>True if purchase use recently used addresses, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.UseRecentlyUsedAddresses"),
         DefaultValue(false),
         DependsOn(nameof(LoginEnabled), nameof(PurchasePanesBillingEnabled), nameof(PurchasePanesShippingEnabled))]
        public static bool PurchaseUseRecentlyUsedAddresses
        {
            get => LoginEnabled && (PurchasePanesBillingEnabled || PurchasePanesShippingEnabled)
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase create account is enabled.</summary>
        /// <value>True if purchase create account enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.CreateAccount.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool PurchaseCreateAccountEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Set the state of create account checkbox default.</summary>
        /// <value>True if guest create account starting value, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.CreateAccount.StartingValue"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled), nameof(PurchaseCreateAccountEnabled))]
        public static bool PurchaseGuestCreateAccountStartingValue
        {
            get => LoginEnabled
                ? PurchaseCreateAccountEnabled
                    ? TryGet(out bool asValue) ? asValue : true
                    : true
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase enter step by click is enabled.</summary>
        /// <value>True if purchase enter step by click enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.EnterStepByClick.Enabled"),
         DefaultValue(false)]
        public static bool PurchaseEnterStepByClickEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase special instructions is enabled.</summary>
        /// <value>True if purchase special instructions enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.SpecialInstructions.Enabled"),
         DefaultValue(false)]
        public static bool PurchaseSpecialInstructionsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the purchase final action button text.</summary>
        /// <value>The purchase final action button text.</value>
        [AppSettingsKey("Clarity.Purchase.FinalActionButtonText"),
         DefaultValue("ui.storefront.cart.continueShopping")]
        public static string PurchaseFinalActionButtonText
        {
            get => TryGet(out string asValue) ? asValue : "ui.storefront.cart.continueShopping";
            private set => TrySet(value);
        }

        /// <summary>Gets the type of the purchase default cart.</summary>
        /// <value>The type of the purchase default cart.</value>
        [AppSettingsKey("Clarity.Purchase.DefaultCartType"),
         DefaultValue("Cart")]
        public static string PurchaseDefaultCartType
        {
            get => TryGet(out string asValue) ? asValue : "Cart";
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the guest purchase is enabled.</summary>
        /// <value>True if guest purchase enabled, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Guest.Enabled"),
         DefaultValue(false),
         MutuallyExclusiveWith(nameof(SplitShippingEnabled))]
        public static bool GuestPurchaseEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Which endpoint to call when completing checkout.</summary>
        /// <value>The purchase mode.</value>
        /// <example>Single = 0, Targets = 2.</example>
        [AppSettingsKey("Clarity.Purchase.Mode"),
         DefaultValue(Enums.CheckoutModes.Single)]
        public static Enums.CheckoutModes PurchaseMode
        {
            get => TryGet<Enums.CheckoutModes>(out var asValue) ? asValue : Enums.CheckoutModes.Single;
            private set => TrySet(value);
        }

        /// <summary>Hide billing first name.</summary>
        /// <value>True if purchase hide billing first name, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Billing.HideFirstName"),
         DefaultValue(false)]
        public static bool PurchaseHideBillingFirstName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing first name.</summary>
        /// <value>True if purchase hide billing last name, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Billing.HideLastName"),
         DefaultValue(false)]
        public static bool PurchaseHideBillingLastName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing last name.</summary>
        /// <value>True if purchase hide billing email, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Billing.HideEmail"),
         DefaultValue(false)]
        public static bool PurchaseHideBillingEmail
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing phone.</summary>
        /// <value>True if purchase hide billing phone, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Billing.HidePhone"),
         DefaultValue(false)]
        public static bool PurchaseHideBillingPhone
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide billing fax.</summary>
        /// <value>True if purchase hide billing fax, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Billing.HideFax"),
         DefaultValue(false)]
        public static bool PurchaseHideBillingFax
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase billing show make this my default.</summary>
        /// <value>True if purchase billing show make this my default, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Billing.ShowMakeThisMyDefault"),
         DefaultValue(false),
         DependsOn(nameof(AddressBookAllowMakeThisMyNewDefaultBillingInCheckout))]
        public static bool PurchaseBillingShowMakeThisMyDefault
        {
            get => AddressBookAllowMakeThisMyNewDefaultBillingInCheckout
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping first name.</summary>
        /// <value>True if purchase hide shipping first name, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Shipping.HideFirstName"),
         DefaultValue(false)]
        public static bool PurchaseHideShippingFirstName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping last name.</summary>
        /// <value>True if purchase hide shipping last name, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Shipping.HideLastName"),
         DefaultValue(false),
         Unused]
        public static bool PurchaseHideShippingLastName
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping email.</summary>
        /// <value>True if purchase hide shipping email, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Shipping.HideEmail"),
         DefaultValue(false),
         Unused]
        public static bool PurchaseHideShippingEmail
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping phone.</summary>
        /// <value>True if purchase hide shipping phone, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Shipping.HidePhone"),
         DefaultValue(false)]
        public static bool PurchaseHideShippingPhone
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Hide shipping fax.</summary>
        /// <value>True if purchase hide shipping fax, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Shipping.HideFax"),
         DefaultValue(false),
         Unused]
        public static bool PurchaseHideShippingFax
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase shipping show make this my default.</summary>
        /// <value>True if purchase shipping show make this my default, false if not.</value>
        [AppSettingsKey("Clarity.Purchase.Inputs.Shipping.ShowMakeThisMyDefault"),
         DefaultValue(false),
         DependsOn(nameof(AddressBookAllowMakeThisMyNewDefaultShippingInCheckout))]
        public static bool PurchaseShippingShowMakeThisMyDefault
        {
            get => AddressBookAllowMakeThisMyNewDefaultShippingInCheckout
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the checkout sections.</summary>
        /// <value>The checkout sections.</value>
        public static TemplateSection[] CheckoutSections
        {
            get => TryGet<TemplateSection[]>(out var asValue)
                ? asValue
                : Array.Empty<TemplateSection>();
            private set => TrySet(value);
        }
    }
}
