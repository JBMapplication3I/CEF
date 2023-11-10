// <copyright file="CEFConfig.Properties.Surcharges.cs" company="clarity-ventures.com">
// Copyright (c) clarity-ventures.com. All rights reserved.
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
        /// <summary>If true, then SalesInvoiceHandlers/StandardActionsProvider.cs will automatically create one new
        /// invoice for each invoice being paid when a surcharge is present and > 0. When paying many invoices, the
        /// surcharge will be prorated across all of them (surcharge * curInvoice.BalanceDue /
        /// AllInvoices.Sum(BalanceDue)).</summary>
        /// <value>True if create surcharge invoice enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.Enabled"),
            DefaultValue(true)]
        public static bool CreateSurchargeInvoiceEnabled
        {
            get => TryGet(out bool val) ? val : true;
            set => TrySet(value);
        }

        /// <summary>If true, then SalesInvoiceHandlers/StandardActionsProvider.cs will automatically create one new
        /// invoice for each invoice being paid when a surcharge is present and > 0. When paying many invoices, the
        /// surcharge will be prorated across all of them (surcharge * curInvoice.BalanceDue /
        /// AllInvoices.Sum(BalanceDue)).</summary>
        /// <value>True if capture bin enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CaptureBIN.Enabled"),
            DefaultValue(false)]
        public static bool CaptureBINEnabled
        {
            get => TryGet(out bool val) ? val : true;
            set => TrySet(value);
        }

        /// <summary>A template that will be used to form the title of each generated invoice.</summary>
        /// <remarks>Available variables: ("this invoice" refers to each invoice in the list of those being paid).
        /// <list>
        ///     <item>
        ///         <term>tgtInvoiceID</term>
        ///         <description>The ID of this particular invoice.</description>
        ///     </item>
        ///     <item>
        ///         <term>tgtInvoiceKey</term>
        ///         <description>The custom key of this particular invoice.</description>
        ///     </item>
        ///     <item>
        ///         <term>tgtSurchargeAmount</term>
        ///         <description>The prorated amount assigned to this particular invoice.</description>
        ///     </item>
        /// </list></remarks>
        /// <value>The create surcharge invoice title template.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.TitleTemplate"),
            DefaultValue("Surcharge for paying {tgtSurchargeAmount} on invoice {tgtInvoiceKey} (#{tgtInvoiceID})")]
        public static string CreateSurchargeInvoiceTitleTemplate
        {
            get => TryGet(out string val) ? val : "Surcharge for paying {tgtSurchargeAmount} on invoice {tgtInvoiceKey} (#{tgtInvoiceID})";
            set => TrySet(value);
        }

        /// <summary>A template that will be used to form the SKU of each generated surcharge invoice's line item.</summary>
        /// <inheritdoc cref="CreateSurchargeInvoiceTitleTemplate"/>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.SKUTemplate"),
            DefaultValue("Surcharge for paying {tgtSurchargeAmount} on invoice {tgtInvoiceKey} (#{tgtInvoiceID})")]
        public static string CreateSurchargeInvoiceSKUTemplate
        {
            get => TryGet(out string val) ? val : "Surcharge for paying {tgtSurchargeAmount} on invoice {tgtInvoiceKey} (#{tgtInvoiceID})";
            set => TrySet(value);
        }

        /// <summary>A template that will be used to form the line item name of each generated surcharge invoice's line
        /// item.</summary>
        /// <inheritdoc cref="CreateSurchargeInvoiceTitleTemplate"/>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.ItemNameTemplate"),
            DefaultValue("Surcharge for paying {tgtSurchargeAmount} on invoice {tgtInvoiceKey} (#{tgtInvoiceID})")]
        public static string CreateSurchargeInvoiceItemNameTemplate
        {
            get => TryGet(out string val) ? val : "Surcharge for paying {tgtSurchargeAmount} on invoice {tgtInvoiceKey} (#{tgtInvoiceID})";
            set => TrySet(value);
        }

        /// <summary>The status key that will be assigned to the newly generated surcharge invoice.</summary>
        /// <value>The create surcharge invoice status key.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.StatusKey"),
            DefaultValue("Paid")]
        public static string CreateSurchargeInvoiceStatusKey
        {
            get => TryGet(out string val) ? val : "Paid";
            set => TrySet(value);
        }

        /// <summary>The type key that will be assigned to the newly generated surcharge invoice.</summary>
        /// <value>The create surcharge invoice type key.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.TypeKey"),
            DefaultValue("General")]
        public static string CreateSurchargeInvoiceTypeKey
        {
            get => TryGet(out string val) ? val : "General";
            set => TrySet(value);
        }

        /// <summary>The state key that will be assigned to the newly generated surcharge invoice.</summary>
        /// <value>The create surcharge invoice state key.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.StateKey"),
            DefaultValue("HISTORY")]
        public static string CreateSurchargeInvoiceStateKey
        {
            get => TryGet(out string val) ? val : "HISTORY";
            set => TrySet(value);
        }

        /// <summary>The key of the attribute that will be used to link the newly generated invoice to the ID of the paid
        /// invoice it was created for.</summary>
        /// <value>The create surcharge invoice generated by attribute key.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.CreateSurchargeInvoice.GeneratedByAttributeKey"),
            DefaultValue("GeneratedByInvoice")]
        public static string CreateSurchargeInvoiceGeneratedByAttributeKey
        {
            get => TryGet(out string val) ? val : "GeneratedByInvoice";
            set => TrySet(value);
        }
    }
}
