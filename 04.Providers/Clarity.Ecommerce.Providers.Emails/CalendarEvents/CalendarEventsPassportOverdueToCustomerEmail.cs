// <copyright file="CalendarEventsPassportOverdueToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar events passport overdue to customer email class</summary>
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

    /// <summary>A calendar events passport overdue to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class CalendarEventsPassportOverdueToCustomerEmail : EmailSettingsBase
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
         DefaultValue("CalendarEvents.PassportOverdue.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("A Passport Is Overdue for Calendar Event {{CalendarEvent}}")]
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
        protected override string SettingsRoot => "Clarity.Notifications.CalendarEvents.PassportOverdue";

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
                parameters.ContainsKey("date") && parameters["date"] != null,
                "This email requires a parameter in the dictionary of { [\"date\"] = <DateTime> }");
            var firstName = parameters["firstName"] as string;
            var middleName = parameters["middleName"] as string;
            var lastName = parameters["lastName"] as string;
            var date = parameters["date"] as DateTime?;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{FirstName}}"] = firstName;
            replacementDictionary["{{MiddleName}}"] = middleName;
            replacementDictionary["{{LastName}}"] = lastName;
            replacementDictionary["{{Date}}"] = date!.Value.ToString("d");
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
