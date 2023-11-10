// <copyright file="PayPalCheckoutProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal checkout provider config class</summary>
#if PAYPAL && NET5_0_OR_GREATER // PayPal package used doesn't have net5+ versions
namespace Clarity.Ecommerce.Providers.Checkouts.PayPalInt
{
    using Interfaces.Providers;

    /// <summary>A PayPal checkout provider configuration.</summary>
    internal static class PayPalCheckoutProviderConfig
    {
        /// <summary>The signature credential.</summary>
        private static PayPal.Authentication.SignatureCredential signatureCredential = null!;

        /// <summary>Gets PayPal signature credential.</summary>
        /// <returns>The PayPal signature credential.</returns>
        internal static PayPal.Authentication.SignatureCredential GetPayPalSignatureCredential()
        {
            if (signatureCredential != null)
            {
                return signatureCredential;
            }
            return signatureCredential = new PayPal.Authentication.SignatureCredential(
                ProviderConfig.GetStringSetting("Clarity.Payment.PayPal.APIUsername"),
                ProviderConfig.GetStringSetting("Clarity.Payment.PayPal.APIPassword"),
                ProviderConfig.GetStringSetting("Clarity.Payment.PayPal.Signature"));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<PayPalCheckoutProvider>() || isDefaultAndActivated;
    }
}
#endif
