// <copyright file="CalendarEventsNewRegistrationNotificationToGroupLeaderEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar events new registration notification to group leader email class</summary>
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

    /// <summary>A calendar events new registration notification to group leader email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class CalendarEventsNewRegistrationNotificationToGroupLeaderEmail : EmailSettingsBase
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
         DefaultValue("CalendarEvents.NewRegistration.GroupOrganizer.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("A New Registration for Calendar Event {{CalendarEvent}}")]
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
        protected override string SettingsRoot => "Clarity.Notifications.CalendarEvents.NewRegistration.GroupOrganizer";

        /// <inheritdoc/>
        public override Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("firstName") == true && parameters["firstName"] != null,
                "This email requires a parameter in the dictionary of { [\"firstName\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters!.ContainsKey("middleName"), // && parameters["middleName"] != null,
                "This email requires a parameter in the dictionary of { [\"middleName\"] = <string?> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("lastName") && parameters["lastName"] != null,
                "This email requires a parameter in the dictionary of { [\"lastName\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("tourNumber") && parameters["tourNumber"] != null,
                "This email requires a parameter in the dictionary of { [\"tourNumber\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("tourName") && parameters["tourName"] != null,
                "This email requires a parameter in the dictionary of { [\"tourName\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("registeredFirstName") && parameters["registeredFirstName"] != null,
                "This email requires a parameter in the dictionary of { [\"registeredFirstName\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("registeredLastName") && parameters["registeredLastName"] != null,
                "This email requires a parameter in the dictionary of { [\"registeredLastName\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("registeredCity") && parameters["registeredCity"] != null,
                "This email requires a parameter in the dictionary of { [\"registeredCity\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("registeredState") && parameters["registeredState"] != null,
                "This email requires a parameter in the dictionary of { [\"registeredState\"] = <string> }");
            var firstName = parameters["firstName"] as string;
            var middleName = parameters["middleName"] as string;
            var lastName = parameters["lastName"] as string;
            var tourNumber = parameters["tourNumber"] as string;
            var tourName = parameters["tourName"] as string;
            var registeredFirstName = parameters["registeredFirstName"] as string;
            var registeredLastName = parameters["registeredLastName"] as string;
            var registeredCity = parameters["registeredCity"] as string;
            var registeredState = parameters["registeredState"] as string;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{FirstName}}"] = firstName;
            replacementDictionary["{{MiddleName}}"] = middleName;
            replacementDictionary["{{LastName}}"] = lastName;
            replacementDictionary["{{RegisterFirstName}}"] = registeredFirstName;
            replacementDictionary["{{RegisterLastName}}"] = registeredLastName;
            replacementDictionary["{{RegisterCity}}"] = registeredCity;
            replacementDictionary["{{RegisterState}}"] = registeredState;
            replacementDictionary["{{TourNumber}}"] = tourNumber;
            replacementDictionary["{{TourName}}"] = tourName;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
