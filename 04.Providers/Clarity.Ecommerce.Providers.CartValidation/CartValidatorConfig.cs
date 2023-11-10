// <copyright file="CartValidatorConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator configuration class</summary>
#if NET5_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Clarity.Ecommerce.Testing.Net5")]
#endif

namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System.Collections.Generic;
    using System.Configuration;
    using Interfaces.Providers.CartValidation;
    using JSConfigs;

    /// <summary>A cart validator configuration.</summary>
    public class CartValidatorConfig : ICartValidatorConfig
    {
        /// <summary>The prefix.</summary>
        private const string Prefix = "Clarity.Carts.Validation.";

        /// <summary>The second prefix.</summary>
        private const string Prefix2 = "Clarity.Checkout.";

        /// <inheritdoc/>
        public bool DoBrandRestrictionsByMinMax { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoBrandRestrictionsByMinMax"] ?? "false");

        /// <inheritdoc/>
        public bool DoCategoryRestrictionsByMinMax { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoCategoryRestrictionsByMinMax"] ?? "false");

        /// <inheritdoc/>
        public bool DoFranchiseRestrictionsByMinMax { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoFranchiseRestrictionsByMinMax"] ?? "false");

        /// <inheritdoc/>
        public bool DoManufacturerRestrictionsByMinMax { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoManufacturerRestrictionsByMinMax"] ?? "false");

        /// <inheritdoc/>
        public bool DoProductRestrictionsByAccountType { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoProductRestrictionsByAccountType"] ?? "false");

        /// <inheritdoc/>
        public bool DoProductRestrictionsByMinMax { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoProductRestrictionsByMinMax"] ?? "true");

        /// <inheritdoc/>
        public bool DoStoreRestrictionsByMinMax { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoStoreRestrictionsByMinMax"] ?? "false");

        /// <inheritdoc/>
        public bool DoVendorRestrictionsByMinMax { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoVendorRestrictionsByMinMax"] ?? "false");

        /// <inheritdoc/>
        public bool DoProductRestrictionsByMustPurchaseMultiplesOfAmount { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoProductRestrictionsByMustPurchaseMultiplesOfAmount"] ?? "false");

        /// <inheritdoc/>
        public bool DoProductRestrictionsByDocumentRequiredForPurchase { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "DoProductRestrictionsByDocumentRequiredForPurchase"] ?? "false");

        /// <inheritdoc/>
        public bool UseShipToHomeFromAnyDCStockCheck { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "UseShipToHomeFromAnyDCStockCheck"] ?? "false");

        /// <inheritdoc/>
        public bool UsePickupInStoreStockCheck { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "UsePickupInStoreStockCheck"] ?? "false");

        /// <inheritdoc/>
        public bool UseShipToStoreFromStoreDCStockCheck { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix + "UseShipToStoreFromStoreDCStockCheck"] ?? "false");

        /// <inheritdoc/>
        public bool OverrideAndForceShipToHomeOptionIfNoShipOptionSelected { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix2 + "OverrideAndForceShipToHomeOptionIfNoShipOptionSelected"] ?? "false");

        /// <inheritdoc/>
        public bool OverrideAndForceNoShipToOptionIfWhenShipOptionSelected { get; set; }
            = bool.Parse(ConfigurationManager.AppSettings[Prefix2 + "OverrideAndForceNoShipToOptionIfWhenShipOptionSelected"] ?? "true");

        /// <inheritdoc/>
        public virtual IReadOnlyDictionary<string, string> ProductRestrictionsKeys
        {
            get
            {
                // ReSharper disable once InvertIf
                if (ProductRestrictionsKeysValue == null)
                {
                    if (!CEFConfigDictionary.CartValidationDoProductRestrictions
                        || string.IsNullOrWhiteSpace(CEFConfigDictionary.CartValidationProductRestrictionsKeys))
                    {
                        ProductRestrictionsKeysValue = new();
                    }
                    else
                    {
                        ProductRestrictionsKeysValue = new();
                        foreach (var line in CEFConfigDictionary.CartValidationProductRestrictionsKeys!.Split(';'))
                        {
                            var lineParts = line.Split('|');
                            ProductRestrictionsKeysValue.Add(lineParts[0], lineParts[1]);
                        }
                    }
                }
                return ProductRestrictionsKeysValue;
            }
        }

        /// <summary>Gets or sets the product restrictions keys.</summary>
        /// <value>The product restrictions keys value.</value>
        protected internal Dictionary<string, string>? ProductRestrictionsKeysValue { get; set; }
    }
}
