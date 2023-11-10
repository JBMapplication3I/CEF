// <copyright file="CalendarEventsMaxCapacityNotificationToGroupLeaderEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar events maximum capacity notification to group leader email class</summary>
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

    /// <summary>A calendar events maximum capacity notification to group leader email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class CalendarEventsMaxCapacityNotificationToGroupLeaderEmail : EmailSettingsBase
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
         DefaultValue("CalendarEvents.MaxCapacity.GroupLeader.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Calendar Event {{CalendarEvent}} is at Maximum Capacity {{CompanyName}}")]
        public override string? Subject
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
        [AppSettingsKey(".From"),
         DefaultValue("noreply@claritymis.com")]
        public override string? From
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.CalendarEvents.MaxCapacity.GroupLeader";

        /// <inheritdoc/>
        public override Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("tourNumber") == true && parameters["tourNumber"] != null,
                "This email requires a parameter in the dictionary of { [\"tourNumber\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters!.ContainsKey("tourName") && parameters["tourName"] != null,
                "This email requires a parameter in the dictionary of { [\"tourName\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("tourMaxCapacity") && parameters["tourMaxCapacity"] != null,
                "This email requires a parameter in the dictionary of { [\"tourMaxCapacity\"] = <int> }");
            var tourNumber = parameters["tourNumber"] as string;
            var tourName = parameters["tourName"] as string;
            var tourMaxCapacity = parameters["tourMaxCapacity"] as int?;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{TourNumber}}"] = tourNumber;
            replacementDictionary["{{TourName}}"] = tourName;
            replacementDictionary["{{TourMaxCapacity}}"] = tourMaxCapacity.ToString();
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
