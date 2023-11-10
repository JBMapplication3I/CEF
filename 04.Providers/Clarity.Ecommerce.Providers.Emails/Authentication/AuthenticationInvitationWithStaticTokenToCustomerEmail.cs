// <copyright file="AuthenticationInvitationWithStaticTokenToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication invitation with static token to customer email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;

    /// <summary>An authentication invitation with static token to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class AuthenticationInvitationWithStaticTokenToCustomerEmail : EmailSettingsBase
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
         DefaultValue("mD64w2U5hUK55qMbw9s983z9ZcfFN6UQcEHnPX8EZGVxhdxE2RaZ6YGCNavy2f2X")]
        public string Token
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : "mD64w2U5hUK55qMbw9s983z9ZcfFN6UQcEHnPX8EZGVxhdxE2RaZ6YGCNavy2f2X";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("Invitation.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullReturnPath"),
         DefaultValue("/Membership-Seller-Select")]
        public override string? FullReturnPath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Your Invitation to Join {{CompanyName}}")]
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
        protected override string SettingsRoot => "Clarity.Messaging.Authentication.Invitation.ToCustomer";

        /// <inheritdoc/>
        public override Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{Token}}"] = Token;
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
