// <copyright file="AvalaraTaxesProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the avalara taxes provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Workflow;
    using JSConfigs;
    using Utilities;

    /// <summary>An avalara taxes provider configuration.</summary>
    internal static class AvalaraTaxesProviderConfig
    {
        /// <summary>The Address Origin Code.</summary>
        internal const string OriginCode = "ORIG-01";

        /// <summary>The Address Destination Code.</summary>
        internal const string DestinationCode = "DEST-01";

        /// <summary>Name of the ecommerce.</summary>
        internal const string EcommerceName = "a0o330000048hNX";

        /// <summary>Gets the origin address.</summary>
        /// <value>The origin address.</value>
        internal static IAddressModel? OriginAddress { get; private set; }

        /// <summary>Gets the tax service.</summary>
        /// <value>The tax service.</value>
        internal static AvalaraTaxService TaxService { get; private set; } = null!;

        /// <summary>Gets the company code.</summary>
        /// <value>The company code.</value>
        internal static string? CompanyCode { get; private set; }

        /// <summary>Gets a value indicating whether the tax service is enabled.</summary>
        /// <value>True if tax service enabled, false if not.</value>
        internal static bool TaxServiceEnabled { get; private set; }

        /// <summary>Gets a value indicating whether the document committing is enabled.</summary>
        /// <value>True if document committing enabled, false if not.</value>
        internal static bool DocumentCommittingEnabled { get; private set; }

        /// <summary>Gets the company code.</summary>
        /// <value>The company code.</value>
        internal static string CompanyCodeSetting { get; }
            = CEFConfigDictionary.TaxesAvalaraCompanyCode!;

        /// <summary>Gets the business identification no.</summary>
        /// <value>The business identification no.</value>
        internal static string BusinessIdentificationNo { get; }
            = CEFConfigDictionary.TaxesAvalaraBusinessIdentificationNo!;

        /// <summary>Gets the currency code.</summary>
        /// <value>The currency code.</value>
        internal static string CurrencyCode { get; }
            = CEFConfigDictionary.TaxesAvalaraCurrencyCode!;

        /// <summary>Gets or sets a value indicating whether the logging is enabled.</summary>
        /// <value>True if logging enabled, false if not.</value>
        private static bool LoggingEnabled { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        private static string? AccountNumber { get; set; }

        /// <summary>Gets or sets the license key.</summary>
        /// <value>The license key.</value>
        private static string? LicenseKey { get; set; }

        /// <summary>Gets or sets URL of the service.</summary>
        /// <value>The service URL.</value>
        private static string? ServiceUrl { get; set; }

        /// <summary>Config constructor.</summary>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>A Task.</returns>
        internal static async Task InitializeAsync(string? contextProfileName)
        {
            var settings = (await RegistryLoaderWrapper.GetInstance<ISettingWorkflow>(contextProfileName)
                    .GetSettingsByGroupNameAsync("Avalara", contextProfileName)
                    .ConfigureAwait(false))
                .Where(x => x!.Active)
                .ToList();
            AccountNumber = settings.Find(x => x!.TypeName!.Equals(nameof(AccountNumber), StringComparison.OrdinalIgnoreCase))?.Value;
            LicenseKey = settings.Find(x => x!.TypeName!.Equals(nameof(LicenseKey), StringComparison.OrdinalIgnoreCase))?.Value;
            ServiceUrl = settings.Find(x => x!.TypeName!.Equals(nameof(ServiceUrl), StringComparison.OrdinalIgnoreCase))?.Value;
            CompanyCode = settings.Find(x => x!.TypeName!.Equals(nameof(CompanyCode), StringComparison.OrdinalIgnoreCase))?.Value;
            TaxServiceEnabled = bool.TryParse(
                    settings.Find(x => x!.TypeName!.Equals(nameof(TaxServiceEnabled), StringComparison.OrdinalIgnoreCase))?.Value,
                    out var taxServiceEnabled)
                && taxServiceEnabled;
            LoggingEnabled = bool.TryParse(
                    settings.Find(x => x!.TypeName!.Equals(nameof(LoggingEnabled), StringComparison.OrdinalIgnoreCase))?.Value,
                    out var loggingEnabled)
                && loggingEnabled;
            DocumentCommittingEnabled = bool.TryParse(
                    settings.Find(x => x!.TypeName!.Equals(nameof(DocumentCommittingEnabled), StringComparison.OrdinalIgnoreCase))?.Value,
                    out var documentCommittingEnabled)
                && documentCommittingEnabled;
            OriginAddress = RegistryLoaderWrapper.GetInstance<IAddressModel>(contextProfileName);
            OriginAddress.Street1 = CEFConfigDictionary.ShippingOriginAddressStreet1;
            OriginAddress.Street2 = CEFConfigDictionary.ShippingOriginAddressStreet2;
            OriginAddress.Street3 = CEFConfigDictionary.ShippingOriginAddressStreet3;
            OriginAddress.City = CEFConfigDictionary.ShippingOriginAddressCity;
            OriginAddress.PostalCode = CEFConfigDictionary.ShippingOriginAddressPostalCode;
            OriginAddress.RegionCode = CEFConfigDictionary.ShippingOriginAddressRegionCode;
            OriginAddress.CountryCode = CEFConfigDictionary.ShippingOriginAddressCountryCode;
            TaxService = new(AccountNumber!, LicenseKey!, ServiceUrl!);
        }

        /// <summary>Query if this AvalaraTaxesProviderConfig is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<AvalaraTaxesProvider>() || isDefaultAndActivated)
                && Contract.CheckAllValidKeys(CompanyCode, AccountNumber, LicenseKey, ServiceUrl);
    }
}
