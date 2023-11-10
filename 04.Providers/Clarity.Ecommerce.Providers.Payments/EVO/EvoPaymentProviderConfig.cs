// <copyright file="EvoPaymentProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the evo payment provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /* appSettings
     * <add key="Clarity.Payments.Evo.LoginUserName" value="" />
     * <add key="Clarity.Payments.Evo.LoginPassword" value="" />
     * <add key="Clarity.Payments.Evo.LoginVendor" value="" />
     * <add key="Clarity.Payments.Evo.LoginPartner" value="" />
     * <add key="Clarity.Payments.Evo.Mode" value="true" />
     * <add key="Clarity.Payments.Evo.DeviceID" value="2:413b000f-a98d-b4b4-69a5-c939e264485f" />
     * <add key="Clarity.Payments.Evo.DevicePassword" value="QAZXSWwsxzaq!@#$4321" />
     * <add key="Clarity.Payments.Evo.SetupID" value="ClarityGateway" />
     * */

    /* Sandbox account
     * https://sandbox.payfabric.com/Portal/Account/?wforce=1
     * un: Sterling.Runion@ClarityMIS.com
     * psw: QAZXSWwsxzaq!@#$4321
     * Gateway Account Profile: ClarityGateway
     *
     * Authorize.net
     * https://developer.authorize.net/hello_world/sandbox.html
     * Sterling.Runion@ClarityMIS.com
     * un -> ClarityVentures2020
     * pw -> QAZXSWwsxzaq!@#$4321
     *    API creds
     * API Login ID -> 8erh83DCpAp
     * Trans key -> 884CCM2749cjyLN6
     * Key -> Simon
     * */
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>An evo payment provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class EvoPaymentProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="EvoPaymentProviderConfig" /> class.</summary>
        static EvoPaymentProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets the identifier of the setup.</summary>
        /// <value>The identifier of the setup.</value>
        [AppSettingsKey("Clarity.Payments.Evo.SetupId"),
         DefaultValue(null)]
        internal static string? SetupID
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(EvoPaymentProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets the identifier of the device.</summary>
        /// <value>The identifier of the device.</value>
        [AppSettingsKey("Clarity.Payments.Evo.DeviceId"),
         DefaultValue(null)]
        internal static string? DeviceID
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(EvoPaymentProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets the device password.</summary>
        /// <value>The device password.</value>
        [AppSettingsKey("Clarity.Payments.Evo.DevicePassword"),
         DefaultValue(null)]
        internal static string? DevicePassword
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(EvoPaymentProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets the name of the login user.</summary>
        /// <value>The name of the login user.</value>
        [AppSettingsKey("Clarity.Payments.Evo.LoginUserName"),
         DefaultValue(null)]
        internal static string? LoginUserName
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(EvoPaymentProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets the login password.</summary>
        /// <value>The login password.</value>
        [AppSettingsKey("Clarity.Payments.Evo.LoginPassword"),
         DefaultValue(null)]
        internal static string? LoginPassword
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(EvoPaymentProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets the login vendor.</summary>
        /// <value>The login vendor.</value>
        [AppSettingsKey("Clarity.Payments.Evo.LoginVendor"),
         DefaultValue(null)]
        internal static string? LoginVendor
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(EvoPaymentProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets the login partner.</summary>
        /// <value>The login partner.</value>
        [AppSettingsKey("Clarity.Payments.Evo.LoginPartner"),
         DefaultValue(null)]
        internal static string? LoginPartner
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(EvoPaymentProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>Gets a value indicating whether the test mode is on.</summary>
        /// <value>True if test mode, false if not.</value>
        [AppSettingsKey("Clarity.Payments.Evo.Mode"),
         DefaultValue(true)]
        internal static bool TestMode
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(EvoPaymentProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(EvoPaymentProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<EvoPaymentProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(DeviceID, DevicePassword, SetupID, LoginUserName, LoginPassword);
    }
}
