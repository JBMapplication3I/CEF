// <copyright file="AuthenticationAccountValidationWithResetToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication account validation with reset to customer email class</summary>
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

    /// <summary>An authentication account validation with reset to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class AuthenticationAccountValidationWithResetToCustomerEmail : EmailSettingsBase
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
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
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
         DefaultValue("/Membership-Registration")]
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
                parameters?.ContainsKey("resetToken") == true && parameters["resetToken"] != null,
                "This email requires a parameter in the dictionary of { [\"resetToken\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters!.ContainsKey("sellerType") && parameters["sellerType"] != null,
                "This email requires a parameter in the dictionary of { [\"sellerType\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("membershipLevel") && parameters["membershipLevel"] != null,
                "This email requires a parameter in the dictionary of { [\"membershipLevel\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("membershipType") && parameters["membershipType"] != null,
                "This email requires a parameter in the dictionary of { [\"membershipType\"] = <string> }");
            var resetToken = parameters["resetToken"] as string;
            var sellerType = parameters["sellerType"] as string;
            var membershipLevel = parameters["membershipLevel"] as string;
            var membershipType = parameters["membershipType"] as string;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{Token}}"] = Token;
            replacementDictionary["{{ResetToken}}"] = resetToken;
            replacementDictionary["{{SellerType}}"] = sellerType;
            replacementDictionary["{{MembershipLevel}}"] = membershipLevel;
            replacementDictionary["{{MembershipType}}"] = membershipType;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
