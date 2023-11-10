// <copyright file="AWSS3FilesProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AWS S3 files provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Files.AWSS3
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Interfaces.Providers;
    using JSConfigs;
    using Utilities;

    /// <summary>The awss 3 files provider configuration.</summary>
    internal static class AWSS3FilesProviderConfig
    {
        /// <summary>URL of the amazon aws root.</summary>
        internal const string AmazonAwsRootUrl = "https://s3.amazonaws.com/";

        /// <summary>The cef files provider parameter key profile.</summary>
        internal const string CefFilesProviderParameterKeyProfile = "profile";

        /// <summary>Identifier for the cef files provider parameter key access key.</summary>
        internal const string CefFilesProviderParameterKeyAccessKeyId = "accesskey";

        /// <summary>The cef files provider parameter key secret access key.</summary>
        internal const string CefFilesProviderParameterKeySecretAccessKey = "secret";

        /// <summary>The profiles.</summary>
        private static Dictionary<string, Dictionary<string, string>> profiles;

        /// <summary>Gets the profiles.</summary>
        /// <value>The profiles.</value>
        internal static Dictionary<string, Dictionary<string, string>> Profiles
            => profiles ??= GetProfilesFromConfig();

        /// <summary>Gets the profile.</summary>
        /// <value>The profile.</value>
        internal static string Profile { get; }
            = CEFConfigDictionary.UploadsAWSDefaultProfile;

        /// <summary>Gets the bucket.</summary>
        /// <value>The bucket.</value>
        internal static string Bucket { get; }
            = CEFConfigDictionary.UploadsAWSDefaultBucket;

        /// <summary>Gets the pathname of the folder.</summary>
        /// <value>The pathname of the folder.</value>
        internal static string Folder { get; }
            = CEFConfigDictionary.UploadsAWSDefaultFolder;

        /// <summary>Gets the region.</summary>
        /// <value>The region.</value>
        internal static string Region { get; }
            = CEFConfigDictionary.UploadsAWSDefaultRegionEndpoint;

        /// <summary>Gets the ACL.</summary>
        /// <value>The ACL.</value>
        internal static string Acl { get; }
            = CEFConfigDictionary.UploadsAWSDefaultCannedACL;

        /// <summary>Gets the access key.</summary>
        /// <value>The access key.</value>
        internal static string AccessKey { get; private set; }
            = CEFConfigDictionary.UploadsAWSDefaultAccessKeyId;

        /// <summary>Gets the secret.</summary>
        /// <value>The secret.</value>
        internal static string Secret { get; private set; }
            = CEFConfigDictionary.UploadsAWSDefaultSecretAccessKey;

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
        {
            var profileParams = Profiles.Where(x => x.Key == Profile).Select(x => x.Value).FirstOrDefault();
            if (profileParams?.ContainsKey(CefFilesProviderParameterKeyAccessKeyId) == true)
            {
                AccessKey = profileParams[CefFilesProviderParameterKeyAccessKeyId];
            }
            if (profileParams?.ContainsKey(CefFilesProviderParameterKeySecretAccessKey) == true)
            {
                Secret = profileParams[CefFilesProviderParameterKeySecretAccessKey];
            }
            return (ProviderConfig.CheckIsEnabledBySettings<AWSS3FilesProvider>() || isDefaultAndActivated)
                && Contract.CheckAllValidKeys(Profile, AccessKey, Secret, Region);
        }

        /// <summary>Gets profiles from configuration.</summary>
        /// <returns>The profiles from configuration.</returns>
        internal static Dictionary<string, Dictionary<string, string>> GetProfilesFromConfig()
        {
            profiles = new Dictionary<string, Dictionary<string, string>>();
            var profileConfigValues = ConfigurationManager.AppSettings.GetValues("Clarity.Uploads.AWS.CredentialProfile");
            if (profileConfigValues == null)
            {
                return profiles;
            }
            foreach (var configValue in profileConfigValues)
            {
                var profileParams = configValue.Split(';')
                    .Select(s => s.Split('='))
                    .ToDictionary(s => s.First().Trim().ToLower(), s => s.Last().Trim());
                profileParams.TryGetValue(CefFilesProviderParameterKeyProfile, out var profileName);
                profileParams.TryGetValue(CefFilesProviderParameterKeyAccessKeyId, out var accessKey);
                profileParams.TryGetValue(CefFilesProviderParameterKeySecretAccessKey, out var secret);
                if (Contract.CheckAllValidKeys(profileName?.Trim(), accessKey?.Trim(), secret?.Trim()))
                {
                    // ReSharper disable once PossibleNullReferenceException verified via if statement
                    profiles.Add(profileName.Trim().ToLower(), profileParams);
                }
            }
            return profiles;
        }
    }
}
