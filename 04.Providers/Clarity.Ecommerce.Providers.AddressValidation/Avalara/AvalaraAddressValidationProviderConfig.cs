// <copyright file="AvalaraAddressValidationProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the avalara address validation configuration class</summary>
#if NET5_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Clarity.Ecommerce.Testing.Net5")]
#endif

namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Workflow;
    using Utilities;

    /// <summary>An avalara validation address configuration.</summary>
    internal static class AvalaraAddressValidationProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="AvalaraAddressValidationProviderConfig"/> class.</summary>
        static AvalaraAddressValidationProviderConfig()
        {
            // ReSharper disable once AsyncConverter.AsyncWait
            InitializeAsync(null).Wait(10_000);
        }

        /// <summary>Gets or sets a value indicating whether from database should be read.</summary>
        /// <value>True if read from database, false if not.</value>
        internal static bool ReadFromDb { get; set; } = false;

        /// <summary>Gets a value indicating whether the initialized.</summary>
        /// <value>True if initialized, false if not.</value>
        internal static bool Initialized { get; private set; }

        /// <summary>Gets the account number.</summary>
        /// <value>The account number.</value>
        internal static string AccountNumber { get; private set; } = null!;

        /// <summary>Gets the license key.</summary>
        /// <value>The license key.</value>
        internal static string LicenseKey { get; private set; } = null!;

        /// <summary>Gets URL of the service.</summary>
        /// <value>The service URL.</value>
        internal static string ServiceUrl { get; private set; } = null!;

        /////// <summary>Gets the company code.</summary>
        /////// <value>The company code.</value>
        ////internal static string CompanyCode { get; }

        /// <summary>Gets or sets a value indicating whether the address service is enabled.</summary>
        /// <value>True if address service enabled, false if not.</value>
        internal static bool AddressServiceEnabled { get; set; }

        /// <summary>Gets or sets the address service countries.</summary>
        /// <value>The address service countries.</value>
        internal static string? AddressServiceCountries { get; set; }

        /// <summary>Initializes this <seealso cref="AvalaraAddressValidationProviderConfig"/>.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        internal static async Task InitializeAsync(string? contextProfileName)
        {
            if (!ReadFromDb)
            {
                return;
            }
            try
            {
                var settings = (await RegistryLoaderWrapper.GetInstance<ISettingWorkflow>(contextProfileName)
                    .GetSettingsByGroupNameAsync("Avalara", contextProfileName).ConfigureAwait(false))
                    .Where(x => x is { Active: true })
                    .Cast<ISettingModel>()
                    .ToList();
                AccountNumber = settings.Find(x => x!.TypeName!.Equals(nameof(AccountNumber), StringComparison.OrdinalIgnoreCase))?.Value!;
                LicenseKey = settings.Find(x => x!.TypeName!.Equals(nameof(LicenseKey), StringComparison.OrdinalIgnoreCase))?.Value!;
                ServiceUrl = settings.Find(x => x!.TypeName!.Equals(nameof(ServiceUrl), StringComparison.OrdinalIgnoreCase))?.Value!;
                ////CompanyCode = settings.Find(x => x.TypeName.Equals(nameof(CompanyCode), StringComparison.OrdinalIgnoreCase))?.Value;
                AddressServiceEnabled = bool.TryParse(
                        settings.Find(x => x.TypeName!.Equals(nameof(AddressServiceEnabled), StringComparison.OrdinalIgnoreCase))?.Value ?? "false",
                        out var addressServiceEnabled)
                    && addressServiceEnabled;
                AddressServiceCountries = settings.Find(x => x.TypeName!.Equals(nameof(AddressServiceCountries), StringComparison.OrdinalIgnoreCase))?.Value;
                if (!IsValidInner())
                {
                    return;
                }
                AvalaraAddressValidationService.Initialize(AccountNumber!, LicenseKey!, ServiceUrl!);
                Initialized = true;
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
        {
            if (!(ProviderConfig.CheckIsEnabledBySettings<AvalaraAddressValidationProvider>() || isDefaultAndActivated))
            {
                return false;
            }
            if (!Initialized)
            {
                // ReSharper disable once AsyncConverter.AsyncWait
                InitializeAsync(null).Wait(10_000);
            }
            return IsValidInner();
        }

        /// <summary>Query if this AvalaraAddressValidationProviderConfig is valid inner.</summary>
        /// <returns>True if valid inner, false if not.</returns>
        private static bool IsValidInner()
        {
            return Contract.CheckAllValidKeys(AccountNumber, LicenseKey, ServiceUrl/*, CompanyCode*/);
        }
    }
}
