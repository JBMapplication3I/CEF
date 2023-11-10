// <copyright file="ICartValidatorConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartValidatorConfig interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.CartValidation
{
    using System.Collections.Generic;

    /// <summary>Interface for cart validator configuration.</summary>
    public interface ICartValidatorConfig
    {
        /// <summary>Gets or sets a value indicating whether the do brand restrictions by minimum maximum.</summary>
        /// <value>True if do brand restrictions by minimum maximum, false if not.</value>
        bool DoBrandRestrictionsByMinMax { get; set; }

        /// <summary>Gets or sets a value indicating whether the do category restrictions by minimum maximum.</summary>
        /// <value>True if do category restrictions by minimum maximum, false if not.</value>
        bool DoCategoryRestrictionsByMinMax { get; set; }

        /// <summary>Gets or sets a value indicating whether the do franchise restrictions by minimum maximum.</summary>
        /// <value>True if do franchise restrictions by minimum maximum, false if not.</value>
        bool DoFranchiseRestrictionsByMinMax { get; set; }

        /// <summary>Gets or sets a value indicating whether the do manufacturer restrictions by minimum maximum.</summary>
        /// <value>True if do manufacturer restrictions by minimum maximum, false if not.</value>
        bool DoManufacturerRestrictionsByMinMax { get; set; }

        /// <summary>Gets or sets a value indicating whether the do product restrictions by account type.</summary>
        /// <value>True if do product restrictions by account type, false if not.</value>
        bool DoProductRestrictionsByAccountType { get; set; }

        /// <summary>Gets or sets a value indicating whether the do product restrictions by document required for
        /// purchase.</summary>
        /// <value>True if do product restrictions by document required for purchase, false if not.</value>
        bool DoProductRestrictionsByDocumentRequiredForPurchase { get; set; }

        /// <summary>Gets or sets a value indicating whether the do product restrictions by minimum maximum.</summary>
        /// <value>True if do product restrictions by minimum maximum, false if not.</value>
        bool DoProductRestrictionsByMinMax { get; set; }

        /// <summary>Gets or sets a value indicating whether the do product restrictions by must purchase multiples of
        /// amount.</summary>
        /// <value>True if do product restrictions by must purchase multiples of amount, false if not.</value>
        bool DoProductRestrictionsByMustPurchaseMultiplesOfAmount { get; set; }

        /// <summary>Gets or sets a value indicating whether the do store restrictions by minimum maximum.</summary>
        /// <value>True if do store restrictions by minimum maximum, false if not.</value>
        bool DoStoreRestrictionsByMinMax { get; set; }

        /// <summary>Gets or sets a value indicating whether the do vendor restrictions by minimum maximum.</summary>
        /// <value>True if do vendor restrictions by minimum maximum, false if not.</value>
        bool DoVendorRestrictionsByMinMax { get; set; }

        /// <summary>Gets or sets a value indicating whether the override and force no ship to option if when ship
        /// option selected.</summary>
        /// <value>True if override and force no ship to option if when ship option selected, false if not.</value>
        bool OverrideAndForceNoShipToOptionIfWhenShipOptionSelected { get; set; }

        /// <summary>Gets or sets a value indicating whether the override and force ship to home option if no ship
        /// option selected.</summary>
        /// <value>True if override and force ship to home option if no ship option selected, false if not.</value>
        bool OverrideAndForceShipToHomeOptionIfNoShipOptionSelected { get; set; }

        /// <summary>Gets the product restrictions keys.</summary>
        /// <value>The product restrictions keys.</value>
        IReadOnlyDictionary<string, string> ProductRestrictionsKeys { get; }

        /// <summary>Gets or sets a value indicating whether this CartWorkflow use pickup in store stock check.</summary>
        /// <value>True if use pickup in store stock check, false if not.</value>
        bool UsePickupInStoreStockCheck { get; set; }

        /// <summary>Gets or sets a value indicating whether this CartWorkflow use ship to home from any device
        /// context stock check.</summary>
        /// <value>True if use ship to home from any device-context stock check, false if not.</value>
        bool UseShipToHomeFromAnyDCStockCheck { get; set; }

        /// <summary>Gets or sets a value indicating whether this CartWorkflow use shop to store from store device
        /// context stock check.</summary>
        /// <value>True if use shop to store from store device-context stock check, false if not.</value>
        bool UseShipToStoreFromStoreDCStockCheck { get; set; }
    }
}
