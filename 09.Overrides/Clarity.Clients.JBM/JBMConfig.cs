// <copyright file="JBMConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System.ComponentModel;
    using Ecommerce.JSConfigs;
    using JetBrains.Annotations;

    [PublicAPI, GeneratesAppSettings]
    public static partial class JBMConfig
    {
        static JBMConfig()
        {
            Load();
        }

        /// <summary>Gets the custom shipping methods.</summary>
        /// <value>The custom shipping methods.</value>
        /// <remarks>Sets the custom shipping methods.</remarks>
        [AppSettingsKey("JBM.CustomShippingMethods"),
         DefaultValue("")]
        public static string JBMCustomShippingMethods
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the name of JBM's supervisor role name.</summary>
        /// <value>The name of the supervisor role.</value>
        /// <remarks>Sets the name of the supervisor role.</remarks>
        [AppSettingsKey("JBM.SupervisorRoleName"),
         DefaultValue("Supervisor")]
        public static string SupervisorRoleName
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the set id.</summary>
        /// <value>The set id.</value>
        /// <remarks>Sets the set id.</remarks>
        [AppSettingsKey("JBM.SetId"),
         DefaultValue(null)]
        public static long? SetId
        {
            get => CEFConfigDictionary.TryGet(out long asValue, typeof(JBMConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the name of JBM's business unit in fusion.</summary>
        /// <value>The name of the business unit.</value>
        /// <remarks>Sets the name of the business unit.</remarks>
        [AppSettingsKey("JBM.JBMBusinessUnit"),
         DefaultValue("")]
        public static string JBMBusinessUnit
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the Id of JBM's business unit in fusion.</summary>
        /// <value>The Id of the business unit.</value>
        /// <remarks>Sets the Id of the business unit.</remarks>
        [AppSettingsKey("JBM.JBMBusinessUnitId"),
         DefaultValue("300000005751050")]
        public static string JBMBusinessUnitId
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the name of the currency name in fusion.</summary>
        /// <value>The name of the currency in fusion.</value>
        /// <remarks>Sets the name of the currency.</remarks>
        [AppSettingsKey("JBM.JBMTransactionCurrencyName"),
         DefaultValue("")]
        public static string JBMTransactionCurrencyName
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the name of the EMS order type.</summary>
        /// <value>The name of the EMS order type.</value>
        /// <remarks>Sets the name of the EMS order type.</remarks>
        [AppSettingsKey("JBM.JBMEmsOrderType"),
         DefaultValue("")]
        public static string JBMEmsOrderType
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the name of the Medsurge order type.</summary>
        /// <value>The name of the Medsurge order type.</value>
        /// <remarks>Sets the name of the Medsurge order type.</remarks>
        [AppSettingsKey("JBM.JBMMedsurgeOrderType"),
         DefaultValue("")]
        public static string JBMMedsurgeOrderType
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the base url for the fusion API.</summary>
        /// <value>The base url.</value>
        /// <remarks>Sets the base url for the fusion API.</remarks>
        [AppSettingsKey("JBM.JBMFusionBaseURL"),
         DefaultValue("")]
        public static string JBMFusionBaseURL
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the password for the fusion API.</summary>
        /// <value>The password for the fusion API.</value>
        /// <remarks>Sets the password to the fusion API.</remarks>
        [AppSettingsKey("JBM.JBMFusionPassword"),
         DefaultValue("")]
        public static string JBMFusionPassword
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the username for the fusion API.</summary>
        /// <value>The username for the fusion API.</value>
        /// <remarks>Sets the username for the fusion API.</remarks>
        [AppSettingsKey("JBM.JBMFusionUsername"),
         DefaultValue("")]
        public static string JBMFusionUsername
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the URL extension resource for sales orders in the fusion API.</summary>
        /// <value>The URL extension resource for sales orders.</value>
        /// <remarks>Sets the URL extension resource for sales orders.</remarks>
        [AppSettingsKey("JBM.JBMFusionSalesOrderURLExtension"),
         DefaultValue("")]
        public static string JBMFusionSalesOrderURLExtension
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the URL extension resource for shipment lines in the fusion API.</summary>
        /// <value>The URL extension resource for shipment lines.</value>
        /// <remarks>Sets the URL extension resource for shipment lines.</remarks>
        [AppSettingsKey("JBM.JBMFusionShipmentURLExtension"),
         DefaultValue("")]
        public static string JBMFusionShipmentURLExtension
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the value of the sales API to use for Fusion.</summary>
        /// <value>The URL extension resource for the sales API.</value>
        /// <remarks>Sets the URL extension resource for sales API.</remarks>
        [AppSettingsKey("JBM.JBMSalesAPI"),
         DefaultValue("")]
        public static string JBMSalesAPI
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the value of the customer API to use for Fusion.</summary>
        /// <value>The URL extension resource for the customer API.</value>
        /// <remarks>Sets the URL extension resource for customer API.</remarks>
        [AppSettingsKey("JBM.JBMCustomerAPI"),
         DefaultValue("")]
        public static string JBMCustomerAPI
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(JBMConfig)) ? asValue : string.Empty;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the value of the customer API to use for Fusion.</summary>
        /// <value>The URL extension resource for the customer API.</value>
        /// <remarks>Sets the URL extension resource for customer API.</remarks>
        [AppSettingsKey("JBM.TestAccountID"),
         DefaultValue(null)]
        public static string? TestAccountID
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(JBMConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the value of the order line status mappings.</summary>
        /// <value>The order line status mappings.</value>
        /// <remarks>Sets the order line status mappings.</remarks>
        [AppSettingsKey("JBM.OrderLineStatusMappings"),
         DefaultValue(null)]
        public static string? OrderLineStatusMappings
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(JBMConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the value of the compare date reach back in days.</summary>
        /// <value>The compare date reach back in days.</value>
        /// <remarks>Sets the compare date reach back in days.</remarks>
        [AppSettingsKey("JBM.CompareDateReachBack"),
         DefaultValue(-30)]
        public static int? CompareDateReachBack
        {
            get => CEFConfigDictionary.TryGet(out int? asValue, typeof(JBMConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Gets the value of the origin contact identifier.</summary>
        /// <value>The origin contact identifier.</value>
        /// <remarks>Sets the origin contact identifier.</remarks>
        [AppSettingsKey("JBM.OriginContactID"),
         DefaultValue(null)]
        public static int? OriginContactID
        {
            get => CEFConfigDictionary.TryGet(out int? asValue, typeof(JBMConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(JBMConfig));
        }

        /// <summary>Loads this JBMConfig.</summary>
        internal static void Load()
        {
            CEFConfigDictionary.Load(typeof(JBMConfig));
        }
    }
}
