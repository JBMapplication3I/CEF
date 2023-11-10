// <copyright file="CalendarEventsAssistanceRequestNotificationToBackOfficeEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar events assistance request notification to back office email class</summary>
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

    /// <summary>A calendar events assistance request notification to back office email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class CalendarEventsAssistanceRequestNotificationToBackOfficeEmail : EmailSettingsBase
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
         DefaultValue("CalendarEvents.AssistanceRequest.BackOffice.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Notification for an Assistance Request for Calendar Event {{CalendarEvent}}")]
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
        [AppSettingsKey(".To"),
         DefaultValue(null)]
        public override string? To
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.CalendarEvents.AssistanceRequest.BackOffice";

        /// <inheritdoc/>
        public override Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("fromDate") == true,
                "This email requires a parameter in the dictionary of { [\"fromDate\"] = <DateTime?> }");
            Contract.Requires<ArgumentException>(
                parameters!.ContainsKey("toDate"),
                "This email requires a parameter in the dictionary of { [\"toDate\"] = <DateTime?> }");
            var fromDate = (DateTime?)parameters["fromDate"];
            var toDate = (DateTime?)parameters["toDate"];
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{FromDate}}"] = fromDate.HasValue ? fromDate.Value.ToString("g") : string.Empty;
            replacementDictionary["{{ToDate}}"] = toDate.HasValue ? toDate.Value.ToString("g") : string.Empty;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(CEFConfigDictionary.EmailDefaultsBackOfficeEmailAddress, replacementDictionary, contextProfileName);
        }
    }
}
