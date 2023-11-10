// <copyright file="AppointmentService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Appointment Service class</summary>
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Scheduling/SecureAppointment/{ID}", "GET",
            Summary = "Securely retrieves an appointment, or returns an error if not possible.")]
    public partial class GetSecureAppointment : ImplementsIDBase, IReturn<CEFActionResponse<AppointmentModel>>
    {
    }

    [PublicAPI,
        Route("/Scheduling/CurrentAppointments/TimeRange", "GET",
            Summary = "Gets appointments for the current account in the given time range.")]
    public partial class GetCurrentAccountAppointments : IReturn<CEFActionResponse<List<AppointmentModel>>>
    {
        [ApiMember(Name = nameof(Start), DataType = "DateTime", ParameterType = "query")]
        public DateTime Start { get; set; }

        [ApiMember(Name = nameof(End), DataType = "DateTime", ParameterType = "query")]
        public DateTime End { get; set; }
    }

    [PublicAPI,
        Route("/Scheduling/ReserveAppointment", "POST",
            Summary = "Reserves an appointment with the given details.")]
    public partial class ReserveAppointment : AppointmentModel, IReturn<CEFActionResponse<int>>
    {
    }

    [PublicAPI,
        Route("/Scheduling/ConfirmAppointment/{ID}", "PATCH",
            Summary = "Confirms the given appointment.")]
    public partial class ConfirmAppointment : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI,
        Route("/Scheduling/CancelAppointment/{ID}", "PATCH",
            Summary = "Confirms the given appointment.")]
    public partial class CancelAppointment : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object> Get(GetSecureAppointment request)
        {
            return await Workflows.Appointments.SecureAppointmentAsync(
                    request.ID,
                    CurrentAccountIDOrThrow401,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object> Get(GetCurrentAccountAppointments request)
        {
            return await Workflows.Appointments.GetAppointmentsForAccountAsync(
                    CurrentAccountIDOrThrow401,
                    request.Start,
                    request.End,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object> Post(ReserveAppointment request)
        {
            return await Workflows.Appointments.ReserveAppointmentAsync(
                    CurrentAccountIDOrThrow401,
                    request,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(ConfirmAppointment request)
        {
            return await Workflows.Appointments.ConfirmAppointmentAsync(
                    CurrentAccountIDOrThrow401,
                    request.ID,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(CancelAppointment request)
        {
            return await Workflows.Appointments.CancelAppointmentAsync(
                    CurrentUserIDOrThrow401,
                    request.ID,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }
    }
}
