// <copyright file="ICalendarEventWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICalendarEventWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface ICalendarEventWorkflow
    {
        /// <summary>Gets calendar events with admin users.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calendar events with admin users.</returns>
        Task<CEFActionResponse<(List<ICalendarEventModel> results, int totalPages, int totalCount)>> GetCalendarEventsWithAdminUsersAsync(
            ICalendarEventSearchModel request,
            string? contextProfileName);

        /// <summary>Can user change package.</summary>
        /// <param name="eventID">           Identifier for the event.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CanUserChangePackageAsync(int eventID, string? contextProfileName);

        /// <summary>Change package.</summary>
        /// <param name="eventID">           Identifier for the event.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="currentUserID">     Identifier for the current user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ChangePackageAsync(
            int eventID,
            int productID,
            int currentUserID,
            string? contextProfileName);

        /// <summary>Gets done events with no survey.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The done events with no survey.</returns>
        Task<List<ICalendarEventModel>> GetDoneEventsWithNoSurveyAsync(
            ICalendarEventSearchModel search,
            string? contextProfileName);

        /// <summary>Gets event keys in last statement state.</summary>
        /// <param name="days">              The days.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The event keys in last statement state.</returns>
        Task<List<string>> GetEventKeysInLastStatementStateAsync(int days, string? contextProfileName);
    }
}
