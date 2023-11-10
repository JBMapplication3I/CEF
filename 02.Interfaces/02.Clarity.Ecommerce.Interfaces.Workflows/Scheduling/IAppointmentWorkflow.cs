// <copyright file="IAppointmentWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAppointmentWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface IAppointmentWorkflow
    {
        /// <summary>
        /// Gets appointments for the given account that occur between the start and end date.
        /// </summary>
        /// <param name="accountID">The account ID to get appointments for.</param>
        /// <param name="start">The start of the date range filter.</param>
        /// <param name="end">The end of the date range filter.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A list of appointment models, wrapped in a CEFAR indicating the success of the operation.</returns>
        Task<CEFActionResponse<List<IAppointmentModel>>> GetAppointmentsForAccountAsync(
            int accountID,
            DateTime start,
            DateTime end,
            string? contextProfileName);

        /// <summary>
        /// Securely gets an appointment, or returns an error if it's unable to do so.
        /// </summary>
        /// <param name="id">The ID of the appointment to get.</param>
        /// <param name="accountID">The ID of the account requesting the details.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A full-mapped model of the requested appointment, or an error indicating why it failed.</returns>
        Task<IAppointmentModel> SecureAppointmentAsync(
            int id,
            int accountID,
            string? contextProfileName);

        /// <summary>
        /// Reserves an appointment, and ensures that the invitees include the given account.
        /// </summary>
        /// <param name="accountID">The account reserving the appointment.</param>
        /// <param name="model">The appointment to reserve.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>The ID of the created appointment, wrapped in a CEFAR.</returns>
        Task<CEFActionResponse<int>> ReserveAppointmentAsync(
            int accountID,
            IAppointmentModel model,
            string? contextProfileName);

        /// <summary>
        /// Confirms the given appointment, if the specified account is allowed to do so.
        /// </summary>
        /// <param name="accountID">The account attempting to confirm the appointment.</param>
        /// <param name="appointmentID">The ID of the appointment to confirm.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A CEFAR indicating the result of the operation.</returns>
        Task<CEFActionResponse> ConfirmAppointmentAsync(
            int accountID,
            int appointmentID,
            string? contextProfileName);

        /// <summary>
        /// Cancels the given appointment, if the specified account is allowed to do so.
        /// </summary>
        /// <param name="accountID">The account attempting to cancel the appointment.</param>
        /// <param name="appointmentID">The ID of the appointment to confirm.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A CEFAR indicating the result of the operation.</returns>
        Task<CEFActionResponse> CancelAppointmentAsync(
            int accountID,
            int appointmentID,
            string? contextProfileName);
    }
}
