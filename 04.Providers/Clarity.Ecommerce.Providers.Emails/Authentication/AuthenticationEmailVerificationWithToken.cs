// <copyright file="AuthenticationEmailVerificationWithToken.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email settings for email verification emails.</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>An email for... email... verification...</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class AuthenticationEmailVerificationWithToken : EmailSettingsBase
    {
        /// <inheritdoc/>
        [AppSettingsKey(".Enabled"),
            DefaultValue(true)]
        public override bool Enabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, GetType()) || asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        [AppSettingsKey(".Token"),
            DefaultValue(null)]
        public string? Token
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
            DefaultValue("EmailVerification.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullReturnPath"),
            DefaultValue("/Verify-Email")]
        public override string? FullReturnPath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
            DefaultValue("Please verify your email with {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".From"),
            DefaultValue("noreply@claritymis.com")]
        public override string? From
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Authentication.EmailVerification";

        /// <inheritdoc/>
        public override Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("token") == true && parameters["token"] != null,
                "This email requires a parameter in the dictionary of { [\"token\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("firstName") == true && parameters["firstName"] != null,
                "This email requires a parameter in the dictionary of { [\"firstName\"] = <string> }");
            // Generate
            var token = (string)parameters!["token"]!;
            var firstName = (string)parameters["firstName"]!;
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{Token}}"] = token;
            replacementDictionary["{{FirstName}}"] = firstName;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
