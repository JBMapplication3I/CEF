// <copyright file="HeartlandPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the heartland payments provider configuration class</summary>
// ReSharper disable CommentTypo, StyleCop.SA1630, StyleCop.SA1631
namespace Clarity.Ecommerce.Providers.Payments.Heartland
{
    using Interfaces.Providers;
    using Utilities;

    /// <summary>A heartland payments provider configuration.</summary>
    internal static class HeartlandPaymentsProviderConfig
    {
        /// <summary>Gets identifier for the merchant.</summary>
        /// <value>The identifier of the merchant.</value>
        /// <example>"skapi_prod_MTyMAQBiHVEAewvIzXVFcmUd2UcyBge_eCpaASUp0A"</example>
        internal static string SecretApiKey { get; }
            = ProviderConfig.GetStringSetting("Clarity.Payments.Heartland.SecretApiKey");

        /// <summary>Gets identifier for the merchant (TestMode).</summary>
        /// <value>The identifier of the merchant (TestMode).</value>
        /// <example>"skapi_cert_MTyMAQBiHVEAewvIzXVFcmUd2UcyBge_eCpaASUp0A"</example>
        internal static string TestSecretApiKey { get; }
            = ProviderConfig.GetStringSetting("Clarity.Payments.Heartland.TestSecretApiKey");

        /// <summary>Gets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        /// <example>"https://prod.api2.heartlandportico.com"</example>
        internal static string ServiceUrl { get; }
            = ProviderConfig.GetStringSetting("Clarity.Payments.Heartland.ServiceUrl");

        /// <summary>Gets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        /// <example>"https://cert.api2.heartlandportico.com"</example>
        internal static string TestServiceUrl { get; }
            = ProviderConfig.GetStringSetting("Clarity.Payments.Heartland.TestServiceUrl");

        /// <summary>Gets the identifier of the developer.</summary>
        /// <value>The identifier of the developer.</value>
        /// <example>"123456"</example>
        internal static string DeveloperID { get; }
            = ProviderConfig.GetStringSetting("Clarity.Payments.Heartland.DeveloperID");

        /// <summary>Gets the version number.</summary>
        /// <value>The version number.</value>
        /// <example>"333"</example>
        internal static string VersionNumber { get; }
            = ProviderConfig.GetStringSetting("Clarity.Payments.Heartland.VersionNumber");

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<HeartlandPaymentsProvider>() || isDefaultAndActivated)
            && (PaymentsProviderBase.ProviderMode == Enums.PaymentProviderMode.Production
                ? Contract.CheckAllValidKeys(SecretApiKey, ServiceUrl)
                : Contract.CheckAllValidKeys(TestSecretApiKey, TestServiceUrl))
            && Contract.CheckAllValidKeys(DeveloperID, VersionNumber);
    }
}
