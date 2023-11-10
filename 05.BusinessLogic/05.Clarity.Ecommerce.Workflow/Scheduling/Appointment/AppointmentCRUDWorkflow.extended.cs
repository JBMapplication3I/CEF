// <copyright file="AppointmentCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Appointment Workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using ServiceStack;
    using Utilities;

    public partial class AppointmentWorkflow
    {
        /// <summary>Gets or sets the ID of the "Reserved" appointment status.</summary>
        /// <value>The ID of the "Reserved" appointment status.</value>
        public static int? AppointmentStatusIDForReserved { get; set; }

        /// <summary>Gets or sets the ID of the "Confirmed" appointment status.</summary>
        /// <value>The ID of the "Confirmed" appointment status.</value>
        public static int? AppointmentStatusIDForConfirmed { get; set; }

        /// <summary>Gets or sets the ID of the "Canceled" appointment status.</summary>
        /// <value>The ID of the "Canceled" appointment status.</value>
        public static int? AppointmentStatusIDForCanceled { get; set; }

        /// <summary>Gets or sets the ID of the "General" appointment type.</summary>
        /// <value>The ID of the "General" appointment type.</value>
        public static int? AppointmentTypeIDForGeneral { get; set; }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<IAppointmentModel>>> GetAppointmentsForAccountAsync(
            int accountID,
            DateTime start,
            DateTime end,
            string? contextProfileName)
        {
            Contract.RequiresValidID(accountID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var calendarIDResult = await Workflows.Calendars.GetCalendarIDForAccountAsync(
                    accountID,
                    context)
                .ConfigureAwait(false);
            if (!calendarIDResult.ActionSucceeded)
            {
                return calendarIDResult.ChangeFailingCEFARType<List<IAppointmentModel>>();
            }
            return context.Appointments
                    .FilterByActive(true)
                    .FilterAppointmentsByTimeRange(start, end)
                    .FilterAppointmentsByCalendarID(calendarIDResult.Result)
                    .SelectListAppointmentAndMapToAppointmentModel(contextProfileName)
                    .ToList()
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public async Task<IAppointmentModel> SecureAppointmentAsync(
            int id,
            int accountID,
            string? contextProfileName)
        {
            Contract.RequiresAllValidIDs(id, accountID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var result = context.Appointments
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(id)
                .FilterAppointmentsByAccountID(accountID)
                .SelectFirstFullAppointmentAndMapToAppointmentModel(contextProfileName);
            if (result != null)
            {
                return result;
            }
            throw HttpError.Unauthorized("Unauthorized to view this Appointment");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> ReserveAppointmentAsync(
            int accountID,
            IAppointmentModel model,
            string? contextProfileName)
        {
            var calendarIDResult = await Workflows.Calendars.GetCalendarIDForAccountAsync(
                    accountID,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!calendarIDResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<int>(calendarIDResult.Messages.ToArray());
            }
            var calendarID = calendarIDResult.Result;
            var timestamp = DateExtensions.GenDateTime;
            model.Active = true;
            model.CreatedDate = timestamp;
            model.StatusID = await GetReservedStatusIDAsync(contextProfileName).ConfigureAwait(false);
            model.TypeID = await GetGeneralTypeIDAsync(contextProfileName).ConfigureAwait(false);
            model.Calendars ??= new();
            if (model.Calendars.All(x => x.MasterID != calendarID))
            {
                var calendarAppointment = RegistryLoaderWrapper.GetInstance<ICalendarAppointmentModel>(contextProfileName);
                calendarAppointment.MasterID = calendarID;
                calendarAppointment.CreatedDate = timestamp;
                model.Calendars.Add(calendarAppointment);
            }
            return await CreateAsync(model, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ConfirmAppointmentAsync(
            int accountID,
            int appointmentID,
            string? contextProfileName)
        {
            return await UpdateStatusAsync(
                    appointmentID,
                    accountID,
                    await GetConfirmedStatusIDAsync(contextProfileName).ConfigureAwait(false),
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CancelAppointmentAsync(
            int accountID,
            int appointmentID,
            string? contextProfileName)
        {
            return await UpdateStatusAsync(
                    appointmentID,
                    accountID,
                    await GetCanceledStatusIDAsync(contextProfileName).ConfigureAwait(false),
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Updates the status.</summary>
        /// <param name="appointmentID">     Identifier for the appointment.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="statusID">          Identifier for the status.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        protected async Task<CEFActionResponse> UpdateStatusAsync(
            int appointmentID,
            int accountID,
            int statusID,
            string? contextProfileName)
        {
            if (!Contract.CheckAllValidIDs(accountID, appointmentID))
            {
                return CEFAR.FailingCEFAR("ERROR! Valid account and appointment IDs are required");
            }
            var appointmentModel = await SecureAppointmentAsync(
                    appointmentID,
                    accountID,
                    contextProfileName)
                .ConfigureAwait(false);
            appointmentModel.StatusID = statusID;
            var result = await UpdateAsync(
                    appointmentModel,
                    contextProfileName)
                .ConfigureAwait(false);
            return (result != null).BoolToCEFAR();
        }

        /// <summary>Gets canceled status identifier.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The canceled status identifier.</returns>
        protected async Task<int> GetCanceledStatusIDAsync(string? contextProfileName)
        {
            if (!Contract.CheckValidID(AppointmentStatusIDForCanceled))
            {
                AppointmentStatusIDForCanceled = await Workflows.AppointmentStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Canceled",
                        byName: "Canceled",
                        byDisplayName: "Canceled",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return AppointmentStatusIDForCanceled!.Value;
        }

        /// <summary>Gets reserved status identifier.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The reserved status identifier.</returns>
        private async Task<int> GetReservedStatusIDAsync(string? contextProfileName)
        {
            if (!Contract.CheckValidID(AppointmentStatusIDForReserved))
            {
                AppointmentStatusIDForReserved = await Workflows.AppointmentStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Reserved",
                        byName: "Reserved",
                        byDisplayName: "Reserved",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return AppointmentStatusIDForReserved!.Value;
        }

        /// <summary>Gets confirmed status identifier.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The confirmed status identifier.</returns>
        private async Task<int> GetConfirmedStatusIDAsync(string? contextProfileName)
        {
            if (!Contract.CheckValidID(AppointmentStatusIDForConfirmed))
            {
                AppointmentStatusIDForConfirmed = await Workflows.AppointmentStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Confirmed",
                        byName: "Confirmed",
                        byDisplayName: "Confirmed",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return AppointmentStatusIDForConfirmed!.Value;
        }

        /// <summary>Gets general type identifier.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The general type identifier.</returns>
        private async Task<int> GetGeneralTypeIDAsync(string? contextProfileName)
        {
            if (!Contract.CheckValidID(AppointmentTypeIDForGeneral))
            {
                AppointmentTypeIDForGeneral = await Workflows.AppointmentTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "General",
                        byName: "General",
                        byDisplayName: "General",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return AppointmentTypeIDForGeneral!.Value;
        }
    }
}
