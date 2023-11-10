// <copyright file="IUserEventAttendanceWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUserEventAttendanceWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface IUserEventAttendanceWorkflow
    {
        /// <summary>Gets event attendees by event identifier.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The event attendees by event identifier.</returns>
        Task<CEFActionResponse<List<IUserEventAttendanceModel>>> GetEventAttendeesByEventIDAsync(
            IUserEventAttendanceSearchModel request,
            string? contextProfileName);

        /// <summary>Cancel attendance.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CancelAttendanceAsync(int id, string? contextProfileName);

        /// <summary>Export to excel.</summary>
        /// <param name="eventID">           Identifier for the event.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A DataSet.</returns>
        Task<DataSet?> ExportToExcelAsync(int eventID, string? contextProfileName);

        /// <summary>Gets group leaders IDs.</summary>
        /// <param name="eventID">           Identifier for the event.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The group leaders IDs.</returns>
        Task<List<int>> GetGroupLeadersIDsAsync(int eventID, string? contextProfileName);
    }
}
