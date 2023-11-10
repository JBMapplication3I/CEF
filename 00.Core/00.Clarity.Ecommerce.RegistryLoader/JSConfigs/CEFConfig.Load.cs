// <copyright file="CEFConfig.Load.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef config. load class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Utilities;

    public static partial class CEFConfigDictionary
    {
        /// <summary>Gets a value indicating whether this CEFConfigDictionary is loading.</summary>
        /// <value>True if this CEFConfigDictionary is loading, false if not.</value>
        public static Dictionary<Type, bool> IsLoading { get; private set; } = new();

        /// <summary>Gets a value indicating whether this CEFConfigDictionary has loaded.</summary>
        /// <value>True if this CEFConfigDictionary has loaded, false if not.</value>
        public static Dictionary<Type, bool> HasLoaded { get; private set; } = new();

        /// <summary>Gets the load attempts.</summary>
        /// <value>The load attempts.</value>
        public static ConcurrentDictionary<Type, int> LoadAttempts { get; private set; } = new();

        /// <summary>Loads this CEFConfigDictionary.</summary>
        /// <param name="toLoad">      The type to load against. Will default to <see cref="CEFConfigDictionary"/> if
        ///                            not set.</param>
        /// <param name="instance">    The instance.</param>
        /// <param name="settingsRoot">The settings root. Will default to null/string.Empty if not set.</param>
        public static void Load(Type? toLoad = null, object? instance = null, string? settingsRoot = null)
        {
            var external = true;
            if (toLoad == null)
            {
                external = false;
                toLoad = typeof(CEFConfigDictionary);
            }
            if (HasLoaded.ContainsKey(toLoad) && HasLoaded[toLoad]
                || IsLoading.ContainsKey(toLoad) && IsLoading[toLoad])
            {
                return;
            }
            if (!LoadAttempts.ContainsKey(toLoad))
            {
                LoadAttempts.TryAdd(toLoad, 0);
            }
            if (++LoadAttempts[toLoad] > 3)
            {
                // Prevent just infinitely trying to load
                throw new ConfigurationErrorsException("The CEFConfigDictionary has tried to load too many times");
            }
            IsLoading[toLoad] = true;
            var propertyInfos = /*external
                ? toLoad.GetProperties(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.SetProperty)
                : */toLoad.GetProperties(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
            foreach (var propertyInfo in propertyInfos)
            {
                LoadProperty(toLoad, propertyInfo, external ? instance : null, settingsRoot);
            }
            IsLoading[toLoad] = false;
            HasLoaded[toLoad] = true;
        }

        /// <summary>Loads a property.</summary>
        /// <param name="type">        The type.</param>
        /// <param name="propertyInfo">Information describing the property.</param>
        /// <param name="instance">    The instance.</param>
        /// <param name="settingsRoot">The settings root.</param>
        private static void LoadProperty(Type type, PropertyInfo propertyInfo, object? instance = null, string? settingsRoot = null)
        {
            if (propertyInfo.Name is nameof(CheckoutSections) or nameof(DashboardPageConfig) or nameof(LoadAttempts) or nameof(IsLoading) or nameof(HasLoaded))
            {
                // Skip
                return;
            }
            var keyAttr = propertyInfo.GetCustomAttribute<AppSettingsKeyAttribute>();
            if (keyAttr == null)
            {
                // Skip
                return;
            }
            var key = keyAttr.Key;
            if (Contract.CheckValidKey(settingsRoot))
            {
                key = settingsRoot + key;
            }
            var defaultValueAttr = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
            object? defaultValue;
            if (defaultValueAttr == null)
            {
                var defaultValueOfAttr = propertyInfo.GetCustomAttribute<DefaultValueOfAttribute>();
                if (defaultValueOfAttr == null)
                {
                    throw new ConfigurationErrorsException(
                        "A setting property must either have a DefaultValue or DefaultValueOf attribute.");
                }
                var property = type.GetProperty(defaultValueOfAttr.PropertyName);
                if (property == null)
                {
                    throw new ConfigurationErrorsException(
                        $"The DefaultValueOf attribute must contain the name of a valid property on type {type.Name}.");
                }
                if (!TryGet(out object? value, defaultValueOfAttr.OtherType ?? type, property.Name))
                {
                    throw new ConfigurationErrorsException(
                        $"Other property hasn't loaded yet {type.Name}.");
                }
                defaultValue = value;
            }
            else
            {
                defaultValue = defaultValueAttr.Value;
            }
            var splitOn = propertyInfo.GetCustomAttribute<SplitOnAttribute>()!;
            var setMethod = propertyInfo.GetSetMethod(true)!;
            var getMethod = propertyInfo.GetGetMethod(true)!;
            if (getMethod.ReturnType == typeof(decimal) || getMethod.ReturnType == typeof(decimal?))
            {
                if (defaultValue is decimal asDecimal)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetDecimalSetting(key, asDecimal) });
                }
                else if (defaultValue is double asDouble)
                {
                    setMethod.Invoke(null, new object[] { ProviderConfig.GetDecimalSetting(key, (decimal)asDouble) });
                }
                else if (defaultValue is int asInt)
                {
                    setMethod.Invoke(null, new object[] { ProviderConfig.GetDecimalSetting(key, asInt) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetDecimalSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(decimal[]))
            {
                if (defaultValue is decimal[] asDecimalArr)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetDecimalArraySetting(key, asDecimalArr, splitOn.SplitOn) });
                }
                else if (defaultValue is double[] asDoubleArr)
                {
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    setMethod.Invoke(null, new object?[] { ProviderConfig.GetDecimalArraySetting(key, asDoubleArr.Cast<decimal>().ToArray(), splitOn.SplitOn) });
                }
                else if (defaultValue is int[] asIntArr)
                {
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    setMethod.Invoke(null, new object?[] { ProviderConfig.GetDecimalArraySetting(key, asIntArr.Cast<decimal>().ToArray(), splitOn.SplitOn) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetDecimalArraySetting(key, null, splitOn.SplitOn) });
                }
            }
            else if (getMethod.ReturnType == typeof(double) || getMethod.ReturnType == typeof(double?))
            {
                if (defaultValue is double asDouble)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetDoubleSetting(key, asDouble) });
                }
                else if (defaultValue is int asInt)
                {
                    setMethod.Invoke(null, new object?[] { ProviderConfig.GetDoubleSetting(key, asInt) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetDoubleSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(double[]))
            {
                if (defaultValue is double[] asDoubleArr)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetDoubleArraySetting(key, asDoubleArr, splitOn.SplitOn) });
                }
                else if (defaultValue is int[] asIntArr)
                {
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    setMethod.Invoke(null, new object?[] { ProviderConfig.GetDoubleArraySetting(key, asIntArr.Cast<double>().ToArray(), splitOn.SplitOn) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetDoubleArraySetting(key, null, splitOn.SplitOn) });
                }
            }
            else if (getMethod.ReturnType == typeof(int) || getMethod.ReturnType == typeof(int?))
            {
                if (defaultValue is int asInt)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetIntegerSetting(key, asInt) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetIntegerSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(int[]))
            {
                if (defaultValue is int[] asIntArr)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetIntegerArraySetting(key, asIntArr, splitOn.SplitOn) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetIntegerArraySetting(key, null, splitOn.SplitOn) });
                }
            }
            else if (getMethod.ReturnType == typeof(bool) || getMethod.ReturnType == typeof(bool?))
            {
                if (defaultValue is bool asBool)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetBooleanSetting(key, asBool) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetBooleanSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(bool[]))
            {
                if (defaultValue is bool[] asBoolArr)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetBooleanArraySetting(key, asBoolArr, splitOn.SplitOn) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetBooleanArraySetting(key, null, splitOn.SplitOn) });
                }
            }
            else if (getMethod.ReturnType == typeof(string))
            {
                if (defaultValue is string asString)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetStringSetting(key, asString) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetStringSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(string[]))
            {
                if (defaultValue is string asString)
                {
                    setMethod.Invoke(
                        null,
                        // ReSharper disable once StyleCop.SA1118
                        new object?[]
                        {
                            ProviderConfig.GetStringArraySetting(
                                key,
                                asString.Split(splitOn.SplitOn, StringSplitOptions.RemoveEmptyEntries),
                                splitOn.SplitOn),
                        });
                }
                else if (defaultValue is string[] asStringArr)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetStringArraySetting(key, asStringArr, splitOn.SplitOn) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetStringArraySetting(key, null, splitOn.SplitOn) });
                }
            }
            else if (getMethod.ReturnType == typeof(TimeSpan))
            {
                if (defaultValue is TimeSpan asTimeSpan)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetTimeSpanSetting(key, asTimeSpan) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetTimeSpanSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(Enums.CheckoutModes))
            {
                if (defaultValue is Enums.CheckoutModes asEnum)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetCheckoutModesSetting(key, asEnum) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetCheckoutModesSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(Enums.HostLookupMethod))
            {
                if (defaultValue is Enums.HostLookupMethod asEnum)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetHostLookupMethodSetting(key, asEnum) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetHostLookupMethodSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(Enums.HostLookupWhichUrl)
                || getMethod.ReturnType == typeof(Enums.HostLookupWhichUrl?))
            {
                if (defaultValue is Enums.HostLookupWhichUrl asEnum)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetHostLookupWhichUrlSetting(key, asEnum) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetHostLookupWhichUrlSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(Enums.PaymentProviderMode))
            {
                if (defaultValue is Enums.PaymentProviderMode asEnum)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetPaymentProviderModeSetting(key, asEnum) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetPaymentProviderModeSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(Enums.PaymentProcessMode))
            {
                if (defaultValue is Enums.PaymentProcessMode asEnum)
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetPaymentProcessModeSetting(key, asEnum) });
                }
                else
                {
                    setMethod.Invoke(instance, new object[] { ProviderConfig.GetPaymentProcessModeSetting(key) });
                }
            }
            else if (getMethod.ReturnType == typeof(SalesInvoiceLateFee[]))
            {
                if (defaultValue is SalesInvoiceLateFee[] asValue)
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetSalesInvoiceLateFeeSetting(key, asValue) });
                }
                else
                {
                    setMethod.Invoke(instance, new object?[] { ProviderConfig.GetSalesInvoiceLateFeeSetting(key) });
                }
            }
            else
            {
                if (defaultValue is string asString)
                {
                    var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject(
                        asString,
                        getMethod.ReturnType,
                        SerializableAttributesDictionaryExtensions.JsonSettings);
                    setMethod.Invoke(instance, new[] { ProviderConfig.GetDeserializedTypeFromStringSetting(key, getMethod.ReturnType, deserialized) });
                }
                else
                {
                    setMethod.Invoke(instance, new[] { ProviderConfig.GetDeserializedTypeFromStringSetting(key, getMethod.ReturnType) });
                }
            }
            /*
            else
            {
                throw new ArgumentException(
                    $"Unknown Type for setting: '{propertyInfo.Name}' '{key}' '{defaultValue}'");
            }
            */
        }
    }
}
