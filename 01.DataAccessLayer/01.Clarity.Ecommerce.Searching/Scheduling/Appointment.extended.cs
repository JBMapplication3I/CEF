// <copyright file="Appointment.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the appointment search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>
    /// Provides extensions for filtering scheduled events in search queries.
    /// </summary>
    public static class AppointmentSearchExtensions
    {
        /// <summary>Filters appointments by a date/time range. Note that if the appointment overlaps the range at all, it
        /// will be returned. It is also non-inclusive for appointments that end at the start time, or start at the end time,
        /// such that if an appointment is from 3-4PM and the query window is 4PM-5PM, the appointment will be filtered out.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to filter in.</param>
        /// <param name="start">The start of the range to filter.</param>
        /// <param name="end">  The end of the range to filter.</param>
        /// <returns>A query that filters appointments by a date/time range.</returns>
        public static IQueryable<TEntity> FilterAppointmentsByTimeRange<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? start,
                DateTime? end)
            where TEntity : class, IAppointment
        {
            if (!Contract.CheckNotNull(start, end))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                // Starts during search range
                .Where(x => x.AppointmentStart >= start && x.AppointmentStart < end
                    // Ends during search range
                    || x.AppointmentEnd > start && x.AppointmentEnd <= end
                    // Starts before and ends after search range
                    || x.AppointmentStart <= start && x.AppointmentEnd >= end);
        }

        /// <summary>
        /// Filters appointments by those that occur on the given calendar.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to filter in.</param>
        /// <param name="calendarID">The ID of the calendar to filter by.</param>
        /// <returns>A query that filters appointments by calendar.</returns>
        public static IQueryable<TEntity> FilterAppointmentsByCalendarID<TEntity>(
                this IQueryable<TEntity> query,
                int calendarID)
            where TEntity : class, IAppointment
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Calendars!
                    .Any(y => y.Active && y.MasterID == calendarID && y.Master!.Active));
        }

        /// <summary>
        /// Filters appointments by accounts whose calendar is on the appointment.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to filter in.</param>
        /// <param name="accountID">The ID of the account to find appointmetns for.</param>
        /// <returns>A query that filters appointments by account.</returns>
        public static IQueryable<TEntity> FilterAppointmentsByAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int accountID)
            where TEntity : class, IAppointment
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Calendars!
                    .Any(y => y.Active
                        && y.Master!.Active
                        && y.Master.AccountID == accountID));
        }
    }
}
