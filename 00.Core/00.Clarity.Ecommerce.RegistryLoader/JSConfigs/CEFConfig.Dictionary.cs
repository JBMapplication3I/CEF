// <copyright file="CEFConfig.Dictionary.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF config dictionary class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
#if !NET5_0_OR_GREATER
    using System.Configuration;
    using System.IO;
    using System.Web.Configuration;
    using ServiceStack;
#endif
    using Models;
    using Debug = System.Diagnostics.Debug;

    public static partial class CEFConfigDictionary
    {
        /// <summary>Gets the dictionary.</summary>
        /// <value>The dictionary.</value>
        public static Dictionary<Type, Dictionary<string, object?>> Dict { get; } = new();

        /// <summary>Attempts to get from the given data.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="got">   The got.</param>
        /// <param name="toLoad">to load.</param>
        /// <param name="key">   The key.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool TryGet<T>(out T got, Type? toLoad = null, [CallerMemberName] string key = null!)
        {
            toLoad ??= typeof(CEFConfigDictionary);
            try
            {
                if (!Dict.ContainsKey(toLoad))
                {
                    got = default!;
                    return false;
                }
                if (!Dict[toLoad].ContainsKey(key))
                {
                    got = default!;
                    return false;
                }
                if (Dict[toLoad][key] == null)
                {
                    got = default!;
                    return true;
                }
                got = (T)Dict[toLoad][key]!;
                return true;
            }
            catch (InvalidCastException)
            {
                // Attempt to load the settings again, they didn't store correctly
                Debug.WriteLine("InvalidCast > LoadAgain");
                HasLoaded[typeof(CEFConfigDictionary)] = false;
                Load();
                return TryGet(out got, toLoad, key);
            }
        }

        /// <summary>Attempts to set from the given data.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="value"> The value.</param>
        /// <param name="toLoad">to load.</param>
        /// <param name="key">   The key.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        // ReSharper disable once UnusedMethodReturnValue.Local
        public static bool TrySet<T>(T? value, Type? toLoad = null, [CallerMemberName] string key = null!)
        {
            toLoad ??= typeof(CEFConfigDictionary);
            if (!Dict.ContainsKey(toLoad))
            {
                Dict[toLoad] = new();
            }
            if (Dict[toLoad].ContainsKey(key) && Equals(Dict[toLoad][key], value))
            {
                return true;
            }
            Dict[toLoad][key] = value;
            return true;
        }

        /// <summary>Attempts to set from user interface from the given data.</summary>
        /// <param name="keysToUpdate">The keys to update.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static CEFActionResponse TrySetFromUI(Dictionary<Type, Dictionary<string, object>> keysToUpdate)
        {
            var response = CEFAR.PassingCEFAR();
            var toUpdateInFile = new Dictionary<string, string>();
            foreach (var outerKeyPair in keysToUpdate)
            {
                var toLoad = outerKeyPair.Key;
                toLoad ??= typeof(CEFConfigDictionary);
                if (!Dict.ContainsKey(toLoad))
                {
                    Dict[toLoad] = new();
                }
                foreach (var innerKeyPair in outerKeyPair.Value)
                {
                    if (Dict[toLoad].ContainsKey(innerKeyPair.Key) && Equals(Dict[toLoad][innerKeyPair.Key], innerKeyPair.Value))
                    {
                        continue;
                    }
                    try
                    {
                        Dict[toLoad][innerKeyPair.Key] = innerKeyPair.Value;
                        toUpdateInFile[innerKeyPair.Key] = innerKeyPair.Value?.ToString() ?? string.Empty;
                    }
                    catch
                    {
                        response.ActionSucceeded = false;
                        response.Messages.Add($"toLoad: '{outerKeyPair.Key}', key: '{innerKeyPair.Key}' failed to update");
                    }
                }
            }
            if (toUpdateInFile.Any())
            {
                AddOrUpdateAppSettings(toUpdateInFile);
            }
            return response;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void AddOrUpdateAppSettings(Dictionary<string, string> toUpdateInFile)
        {
            try
            {
#if NET5_0_OR_GREATER
                throw new NotImplementedException("NET4 Process not available in NET5, requires refactor");
#else
                var exepath = APIAdminRouteRelativePath ?? "/";
                var config = WebConfigurationManager.OpenWebConfiguration(exepath);
                var currentAppSettingsFilePath = config.AppSettings.File;
                config.AppSettings.File = currentAppSettingsFilePath;
                // To avoid: Error CS0266, Explicitly cast 'System.Configuration.AppSettingsSection'
                var myAppSettings = (AppSettingsSection)config.GetSection(config.AppSettings.SectionInformation.Name);
                foreach (var kvp in toUpdateInFile)
                {
                    if (!myAppSettings.Settings.AllKeys.Contains(kvp.Key))
                    {
                        myAppSettings.Settings.Add(kvp.Key, kvp.Value);
                    }
                    else
                    {
                        myAppSettings.Settings[kvp.Key].Value = kvp.Value;
                    }
                }
                var actualPathToSaveTo = Path.GetFullPath(
                    Path.Combine(
                        Path.GetDirectoryName(config.FilePath)!,
                        currentAppSettingsFilePath));
                // NOTE: We have to write to a file once so that it loads the full data into the get xml call.
                // Using a separate file since it would be the full thing
                config.SaveAs(actualPathToSaveTo + ".other", ConfigurationSaveMode.Modified);
                // Get the raw xml so we can push it to a file
                var rawXml = myAppSettings.SectionInformation.GetRawXml();
                rawXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n"
                    + rawXml
                        .Replace($" file=\"{currentAppSettingsFilePath}\"", string.Empty)
                        .Replace("    ", "  ")
                        .Replace("\r\n    value", " value")
                        .Replace("  </appSettings>", "</appSettings>\r\n");
                var bytes = rawXml.ToUtf8Bytes();
                using (Stream fileStream = File.Open(actualPathToSaveTo, FileMode.Create))
                {
                    fileStream.Write(bytes, 0, bytes.Length);
                }
                // Delete the extra file we had to make and the bin folder it also creates
                File.Delete(actualPathToSaveTo + ".other");
                Directory.Delete(Path.GetDirectoryName(actualPathToSaveTo) + "\\bin", true);
                // Tell the app to reload the settings
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
