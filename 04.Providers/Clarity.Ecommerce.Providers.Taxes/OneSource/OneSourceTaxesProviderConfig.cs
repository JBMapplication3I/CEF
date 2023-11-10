// <copyright file="OneSourceTaxesProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the OneSource taxes provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.OneSource
{
    using Clarity.Ecommerce.Enums;
    using Interfaces.Providers;
    using JSConfigs;
    using Utilities;

    /// <summary>An OneSource taxes provider configuration.</summary>
    internal static class OneSourceTaxesProviderConfig
    {
        internal static readonly string ServiceUrlUat = "https://onesource-idt-det-amer-ws.iv.determination.thomsonreuters.com/sabrix/services/taxcalculationservice/2011-09-01/taxcalculationservice";
        internal static readonly string ServiceUrlProduction = "https://onesource-idt-det-amer-ws.iv.determination.thomsonreuters.com/sabrix/services/taxcalculationservice/2011-09-01/taxcalculationservice";

        internal static PaymentProviderMode ProviderMode { get; set; } = PaymentProviderMode.Development;

        /// <summary>Gets the external company id.</summary>
        /// <value>The external company id.</value>
        internal static string ExternalCompanyID { get; }
            = CEFConfigDictionary.TaxesOneSourceExternalCompanyID!;

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        internal static string Username { get; }
            = CEFConfigDictionary.TaxesOneSourceUsername!;

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        internal static string Password { get; }
            = CEFConfigDictionary.TaxesOneSourcePassword!;

        /// <summary>Gets the currency code.</summary>
        /// <value>The currency code.</value>
        internal static string CurrencyCode { get; }
            = CEFConfigDictionary.TaxesOneSourceCurrencyCode!;

        /// <summary>Query if this OneSourceTaxesProviderConfig is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<OneSourceTaxesProvider>() || isDefaultAndActivated)
                && Contract.CheckAllValidKeys(ExternalCompanyID, Username, Password);
    }
}
