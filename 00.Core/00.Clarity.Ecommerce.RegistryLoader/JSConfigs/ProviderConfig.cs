// <copyright file="ProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the provider configuration class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using JSConfigs;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A provider configuration.</summary>
    public static class ProviderConfig
    {
        private static ConcurrentDictionary<string, bool> IsEnabled { get; } = new();

        /// <summary>Check is enabled by settings.</summary>
        /// <param name="provider">The provider.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CheckIsEnabledBySettings(string provider)
        {
            if (!IsEnabled.ContainsKey(provider))
            {
                IsEnabled[provider] = ConfigurationManager.AppSettings["Clarity.Providers.EnabledProviders"]
                        ?.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Any(x => x.Trim() == provider)
                    ?? ConfigurationManager.AppSettings["Clarity.Providers.EnabledProviders"]
                        ?.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Any(x => x.Trim() == provider)
                    ?? false;
            }
            return IsEnabled[provider];
        }

        /// <summary>Determines if we can check is enabled by settings.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CheckIsEnabledBySettings<T>()
            where T : class, IProviderBase
        {
            if (!IsEnabled.ContainsKey(typeof(T).Name))
            {
                IsEnabled[typeof(T).Name] = ConfigurationManager.AppSettings["Clarity.Providers.EnabledProviders"]
                        ?.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Any(x => x.Trim() == typeof(T).Name)
                    ?? ConfigurationManager.AppSettings["Clarity.Providers.EnabledProviders"]
                        ?.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Any(x => x.Trim() == typeof(T).Name)
                    ?? false;
            }
            return IsEnabled[typeof(T).Name];
        }

        /// <summary>Gets decimal setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The decimal setting.</returns>
        public static decimal GetDecimalSetting(string name, decimal @default = 0m)
        {
            return decimal.Parse(ConfigurationManager.AppSettings[name]
                ?? @default.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>Gets decimal array setting.</summary>
        /// <param name="name">    The name.</param>
        /// <param name="default"> The default.</param>
        /// <param name="splitsOn">The splits on.</param>
        /// <returns>An array of decimal.</returns>
        public static decimal[]? GetDecimalArraySetting(string name, decimal[]? @default, char[] splitsOn)
        {
            var value = ConfigurationManager.AppSettings[name];
            if (!Contract.CheckValidKey(value))
            {
                return @default;
            }
            var split = value!.Split(splitsOn, StringSplitOptions.RemoveEmptyEntries);
            var retVal = new decimal[split.Length];
            for (var i = 0; i < split.Length; i++)
            {
                retVal[i] = decimal.Parse(split[i]);
            }
            return retVal;
        }

        /// <summary>Gets double setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The double setting.</returns>
        public static double? GetDoubleSetting(string name, double? @default = 0d)
        {
            return double.TryParse(ConfigurationManager.AppSettings[name], out var value)
                ? value
                : @default;
        }

        /// <summary>Gets double array setting.</summary>
        /// <param name="name">    The name.</param>
        /// <param name="default"> The default.</param>
        /// <param name="splitsOn">The splits on.</param>
        /// <returns>An array of double.</returns>
        public static double[]? GetDoubleArraySetting(string name, double[]? @default, char[] splitsOn)
        {
            var value = ConfigurationManager.AppSettings[name];
            if (!Contract.CheckValidKey(value))
            {
                return @default;
            }
            var split = value!.Split(splitsOn, StringSplitOptions.RemoveEmptyEntries);
            var retVal = new double[split.Length];
            for (var i = 0; i < split.Length; i++)
            {
                retVal[i] = double.Parse(split[i]);
            }
            return retVal;
        }

        /// <summary>Gets integer setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The integer setting.</returns>
        public static int GetIntegerSetting(string name, int @default = 0)
        {
            return int.TryParse(ConfigurationManager.AppSettings[name], out var value)
                ? value
                : @default;
        }

        /// <summary>Gets integer array setting.</summary>
        /// <param name="name">    The name.</param>
        /// <param name="default"> The default.</param>
        /// <param name="splitsOn">The splits on.</param>
        /// <returns>An array of int.</returns>
        public static int[]? GetIntegerArraySetting(string name, int[]? @default, char[] splitsOn)
        {
            var value = ConfigurationManager.AppSettings[name];
            if (!Contract.CheckValidKey(value))
            {
                return @default;
            }
            var split = value!.Split(splitsOn, StringSplitOptions.RemoveEmptyEntries);
            var retVal = new int[split.Length];
            for (var i = 0; i < split.Length; i++)
            {
                retVal[i] = int.Parse(split[i]);
            }
            return retVal;
        }

        /// <summary>Gets boolean setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">True to default.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool GetBooleanSetting(string name, bool @default = false)
        {
            return bool.Parse(ConfigurationManager.AppSettings[name] ?? @default.ToString());
        }

        /// <summary>Gets boolean array setting.</summary>
        /// <param name="name">    The name.</param>
        /// <param name="default"> The default.</param>
        /// <param name="splitsOn">The splits on.</param>
        /// <returns>An array of bool.</returns>
        public static bool[]? GetBooleanArraySetting(string name, bool[]? @default, char[] splitsOn)
        {
            var value = ConfigurationManager.AppSettings[name];
            if (!Contract.CheckValidKey(value))
            {
                return @default;
            }
            var split = value!.Split(splitsOn, StringSplitOptions.RemoveEmptyEntries);
            var retVal = new bool[split.Length];
            for (var i = 0; i < split.Length; i++)
            {
                retVal[i] = bool.Parse(split[i]);
            }
            return retVal;
        }

        /// <summary>Gets time span setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The time span setting.</returns>
        public static TimeSpan GetTimeSpanSetting(string name, TimeSpan @default)
        {
            return TimeSpan.Parse(ConfigurationManager.AppSettings[name] ?? @default.ToString());
        }

        /// <summary>Gets time span setting.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The time span setting.</returns>
        public static TimeSpan GetTimeSpanSetting(string name)
        {
            return TimeSpan.Parse(ConfigurationManager.AppSettings[name] ?? TimeSpan.Zero.ToString());
        }

        /// <summary>Gets string setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The string setting.</returns>
        public static string? GetStringSetting(string name, string? @default = null)
        {
            return ConfigurationManager.AppSettings[name] ?? @default;
        }

        /// <summary>Gets string array setting.</summary>
        /// <param name="name">    The name.</param>
        /// <param name="default"> The default.</param>
        /// <param name="splitsOn">The splits on.</param>
        /// <returns>An array of string.</returns>
        public static string[]? GetStringArraySetting(string name, string[]? @default, char[] splitsOn)
        {
            var value = ConfigurationManager.AppSettings[name];
            if (!Contract.CheckValidKey(value))
            {
                return @default;
            }
            var split = value!.Split(splitsOn, StringSplitOptions.RemoveEmptyEntries);
            var retVal = new string[split.Length];
            for (var i = 0; i < split.Length; i++)
            {
                retVal[i] = split[i].Trim();
            }
            return retVal;
        }

        /// <summary>Gets checkout modes setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The checkout modes setting.</returns>
        public static Enums.CheckoutModes GetCheckoutModesSetting(
            string name,
            Enums.CheckoutModes @default = Enums.CheckoutModes.Single)
        {
            return Enum.TryParse(
                    ConfigurationManager.AppSettings[name] ?? @default.ToString(),
                    out Enums.CheckoutModes retVal)
                ? retVal
                : @default;
        }

        /// <summary>Gets host lookup method setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The host lookup method setting.</returns>
        public static Enums.HostLookupMethod GetHostLookupMethodSetting(
            string name,
            Enums.HostLookupMethod @default = Enums.HostLookupMethod.NotByLookup)
        {
            return Enum.TryParse(
                    ConfigurationManager.AppSettings[name] ?? @default.ToString(),
                    out Enums.HostLookupMethod retVal)
                ? retVal
                : @default;
        }

        /// <summary>Gets host lookup which URL setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The host lookup which URL setting.</returns>
        public static Enums.HostLookupWhichUrl GetHostLookupWhichUrlSetting(
            string name,
            Enums.HostLookupWhichUrl @default = Enums.HostLookupWhichUrl.Primary)
        {
            return Enum.TryParse(
                    ConfigurationManager.AppSettings[name] ?? @default.ToString(),
                    out Enums.HostLookupWhichUrl retVal)
                ? retVal
                : @default;
        }

        /// <summary>Gets payment provider mode setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The payment provider mode setting.</returns>
        public static Enums.PaymentProviderMode GetPaymentProviderModeSetting(
            string name,
            Enums.PaymentProviderMode @default = Enums.PaymentProviderMode.Testing)
        {
            return Enum.TryParse(
                    ConfigurationManager.AppSettings[name] ?? @default.ToString(),
                    out Enums.PaymentProviderMode retVal)
                ? retVal
                : @default;
        }

        /// <summary>Gets payment process mode setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>The payment process mode setting.</returns>
        public static Enums.PaymentProcessMode GetPaymentProcessModeSetting(
            string name,
            Enums.PaymentProcessMode @default = Enums.PaymentProcessMode.AuthorizeAndCapture)
        {
            return Enum.TryParse(
                    ConfigurationManager.AppSettings[name] ?? @default.ToString(),
                    out Enums.PaymentProcessMode retVal)
                ? retVal
                : @default;
        }

        /// <summary>Gets sales invoice late fee setting.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="default">The default.</param>
        /// <returns>An array of sales invoice late fee.</returns>
        public static SalesInvoiceLateFee[]? GetSalesInvoiceLateFeeSetting(
            string name,
            SalesInvoiceLateFee[]? @default = default)
        {
            var stringValue = ConfigurationManager.AppSettings[name];
            if (!Contract.CheckValidKey(stringValue))
            {
                return @default;
            }
            return JsonConvert.DeserializeObject<SalesInvoiceLateFee[]>(stringValue!)
                ?? @default;
        }

        public static object? GetDeserializedTypeFromStringSetting(
            string name,
            Type type,
            object? @default = null)
        {
            var stringValue = ConfigurationManager.AppSettings[name];
            if (!Contract.CheckValidKey(stringValue))
            {
                return @default;
            }
            return JsonConvert.DeserializeObject(
                    stringValue!,
                    type,
                    SerializableAttributesDictionaryExtensions.JsonSettings)
                ?? @default;
        }
    }
}
