// <copyright file="OracleTaxesProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle taxes provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Taxes.Oracle
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>An oracle taxes provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    public class OracleTaxesProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="OracleTaxesProviderConfig" /> class.</summary>
        static OracleTaxesProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(OracleTaxesProviderConfig));
        }

        /// <summary>Gets URL of the document.</summary>
        /// <value>The URL.</value>
        [AppSettingsKey("Clarity.Taxes.Oracle.Url"),
         DefaultValue(null)]
        internal static string? Url
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OracleTaxesProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OracleTaxesProviderConfig));
        }

        /// <summary>Query if 'isDefaultAndActivated' is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this  is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<OracleTaxesProvider>() || isDefaultAndActivated)
                && Contract.CheckAllValidKeys(Url);
    }
}
