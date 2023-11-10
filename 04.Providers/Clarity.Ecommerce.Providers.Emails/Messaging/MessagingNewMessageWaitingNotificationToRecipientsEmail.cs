// <copyright file="MessagingNewMessageWaitingNotificationToRecipientsEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the messaging new message waiting notification to recipients email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A messaging new message waiting notification to recipients email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public partial class MessagingNewMessageWaitingNotificationToRecipientsEmail : EmailSettingsBase
    {
        /// <inheritdoc/>
        [AppSettingsKey(".Enabled"),
         DefaultValue(true)]
        public override bool Enabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, GetType()) || asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("Messages.NewWaiting.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("You have a new message waiting at {{CompanyName}}")]
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

        /// <summary>Gets a value indicating whether the email message contents is enabled.</summary>
        /// <value>True if enable email message contents, false if not.</value>
        [AppSettingsKey(".EnableEmailMessageContents"),
         DefaultValue(false)]
        public virtual bool EnableEmailMessageContents
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, GetType()) && asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <summary>Gets the body template path content.</summary>
        /// <value>The body template path content.</value>
        [AppSettingsKey(".BodyTemplatePath.Content"),
         DefaultValue(null)]
        public virtual string BodyTemplatePathContent
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : ".BodyTemplatePath.Content";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <summary>Gets the body template path no content.</summary>
        /// <value>The body template path no content.</value>
        [AppSettingsKey(".BodyTemplatePath.NoContent"),
         DefaultValue(null)]
        public virtual string BodyTemplatePathNoContent
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : ".BodyTemplatePath.NoContent";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.Messages.NewWaiting";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("senderUsername") == true && parameters["senderUsername"] != null,
                "This email requires a parameter in the dictionary of { [\"senderUsername\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters!.ContainsKey("subject") && parameters["subject"] != null,
                "This email requires a parameter in the dictionary of { [\"subject\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("messageID") && parameters["messageID"] != null,
                "This email requires a parameter in the dictionary of { [\"messageID\"] = <int> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("recipients") && parameters["recipients"] != null,
                "This email requires a parameter in the dictionary of { [\"recipients\"] = <string[]> }");
            var senderUsername = parameters["senderUsername"] as string;
            var subject = parameters["subject"] as string;
            var body = parameters["body"] as string;
            var messageID = parameters["messageID"] as int?;
            var recipients = parameters["recipients"] as string[];
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{MessageID}}"] = messageID.ToString();
            replacementDictionary["{{SenderUsername}}"] = senderUsername;
            replacementDictionary["{{Subject}}"] = subject;
            replacementDictionary["{{Body}}"] = body;
            FullTemplatePath = EnableEmailMessageContents ? BodyTemplatePathContent : BodyTemplatePathNoContent;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var aggregate = await CEFAR.AggregateAsync(
                    recipients!,
                    async recipient =>
                    {
                        var result = await FormatAndQueueEmailAsync(
                                to: recipient,
                                replacementDictionary: replacementDictionary,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        return result.ActionSucceeded
                            ? result.Result.WrapInPassingCEFARIfNotNull()
                            : CEFAR.FailingCEFAR<IEmailQueueModel>("Unable to queue email");
                    })
                .ConfigureAwait(false);
            return new(aggregate.ActionSucceeded);
        }
    }
}
