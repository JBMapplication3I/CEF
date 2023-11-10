// <copyright file="CalendarEventsWeeklyReportToGroupLeaderEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar events weekly report to group leader email class</summary>
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

    /// <summary>A calendar events weekly report to group leader email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class CalendarEventsWeeklyReportToGroupLeaderEmail : EmailSettingsBase
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
         DefaultValue("CalendarEvents.WeeklyReport.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Weekly Calendar Events Report")]
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
        protected override string SettingsRoot => "Clarity.Notifications.CalendarEvents.WeeklyReport.GroupOrganizer";

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
                parameters.ContainsKey("tourName") && parameters["tourName"] != null,
                "This email requires a parameter in the dictionary of { [\"tourName\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("registeredCount") && parameters["registeredCount"] != null,
                "This email requires a parameter in the dictionary of { [\"registeredCount\"] = <int> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("maxAttendees") && parameters["maxAttendees"] != null,
                "This email requires a parameter in the dictionary of { [\"maxAttendees\"] = <int> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("passportDueDate") && parameters["passportDueDate"] != null,
                "This email requires a parameter in the dictionary of { [\"passportDueDate\"] = <DateTime> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("finalPaymentReminderDate") && parameters["finalPaymentReminderDate"] != null,
                "This email requires a parameter in the dictionary of { [\"finalPaymentReminderDate\"] = <DateTime> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("finalPaymentDueDate") && parameters["finalPaymentDueDate"] != null,
                "This email requires a parameter in the dictionary of { [\"finalPaymentDueDate\"] = <DateTime> }");
            var firstName = parameters["firstName"] as string;
            var middleName = parameters["middleName"] as string;
            var lastName = parameters["lastName"] as string;
            var tourName = parameters["tourName"] as string;
            var registeredCount = parameters["registeredCount"] as int?;
            var maxAttendees = parameters["maxAttendees"] as int?;
            var passportDueDate = parameters["passportDueDate"] as DateTime?;
            var finalPaymentReminderDate = parameters["finalPaymentReminderDate"] as DateTime?;
            var finalPaymentDueDate = parameters["finalPaymentDueDate"] as DateTime?;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{FirstName}}"] = firstName;
            replacementDictionary["{{MiddleName}}"] = middleName;
            replacementDictionary["{{LastName}}"] = lastName;
            replacementDictionary["{{TourName}}"] = tourName;
            replacementDictionary["{{RegisteredCount}}"] = registeredCount.ToString();
            replacementDictionary["{{MaxAttendance}}"] = maxAttendees.ToString();
            replacementDictionary["{{PassportDueDate}}"] = passportDueDate!.Value.ToString("g");
            replacementDictionary["{{FinalPaymentReminderDate}}"] = finalPaymentReminderDate!.Value.ToString("g");
            replacementDictionary["{{FinalPaymentDueDate}}"] = finalPaymentDueDate!.Value.ToString("g");
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
