// <copyright file="VincarioVinLookupProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>
// https://vindecoder.eu/api/
// https://vindecoder.eu/my/api/3.1/docs/actions#toc-decode-request
// Implements the Vincario vin lookup provider configuration class
// </summary>
namespace Clarity.Ecommerce.Providers.VinLookup.Vincario
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <content>Dictionary of Vincario CEF configurations.</content>
    [PublicAPI, GeneratesAppSettings]
    internal static partial class VincarioVinLookupProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="VincarioVinLookupProviderConfig" /> class.</summary>
        static VincarioVinLookupProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(VincarioVinLookupProviderConfig));
        }

        /// <summary>Gets the base address.</summary>
        /// <value>The base address.</value>
        [AppSettingsKey("Clarity.VinLookup.Vincario.BaseAddress"),
         DefaultValue("https://api.vindecoder.eu/3.1")]
        internal static string? BaseAddress
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(VincarioVinLookupProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(VincarioVinLookupProviderConfig));
        }

        /// <summary>Gets the APIKey.</summary>
        /// <value>The APIKey.</value>
        [AppSettingsKey("Clarity.VinLookup.Vincario.APIKey"),
         DefaultValue("f730c919cf72")]
        internal static string APIKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(VincarioVinLookupProviderConfig)) ? asValue : "a4NyFokkcUdkDJep";
            private set => CEFConfigDictionary.TrySet(value, typeof(VincarioVinLookupProviderConfig));
        }

        /// <summary>Gets the SecretKey.</summary>
        /// <value>The SecretKey.</value>
        [AppSettingsKey("Clarity.VinLookup.Vincario.SecretKey"),
         DefaultValue("5831dc1204")]
        internal static string SecretKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(VincarioVinLookupProviderConfig)) ? asValue : "pjqbKIUGlTwwExGqiLgmetdyw";
            private set => CEFConfigDictionary.TrySet(value, typeof(VincarioVinLookupProviderConfig));
        }

        /// <summary>Gets the option id.</summary>
        /// <value>The option id.</value>
        [AppSettingsKey("Clarity.VinLookup.Vincario.ID"),
         DefaultValue("decode")]
        internal static string ID
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(VincarioVinLookupProviderConfig)) ? asValue : "a4NyFokkcUdkDJep";
            private set => CEFConfigDictionary.TrySet(value, typeof(VincarioVinLookupProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<VincarioVinLookupProvider>() || isDefaultAndActivated;

        /// <summary>Loads this VincarioProviderConfig.</summary>
        internal static void Load()
        {
            CEFConfigDictionary.Load(typeof(VincarioVinLookupProviderConfig));
        }
    }
}
