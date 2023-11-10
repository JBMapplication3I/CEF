// <copyright file="CalendarEvent.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A calendar event search extensions.</summary>
    public static class CalendarEventSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter calendar events by end date before date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventsByEndDateBeforeDate<TEntity>(
            this IQueryable<TEntity> query, DateTime? endDate)
            where TEntity : class, ICalendarEvent
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidDate(endDate))
            {
                return query;
            }
            return query.Where(x => x.EndDate < endDate);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter calendar event by last statement state.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="days"> The days.</param>
        /// <param name="today">The today.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventByLastStatementState<TEntity>(
            this IQueryable<TEntity> query, int days, DateTime? today)
            where TEntity : class, ICalendarEvent
        {
            // StartDate - days < Today < Start Date
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidDate(today))
            {
                return query;
            }
            return query.Where(x => DbFunctions.AddDays(x.StartDate, -1 * days) < today && today < x.StartDate);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter calendar events by strict days until
        /// departure.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">                   The query to act on.</param>
        /// <param name="strictDaysUntilDeparture">The strict days until departure.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventsByStrictDaysUntilDeparture<TEntity>(
            this IQueryable<TEntity> query, int? strictDaysUntilDeparture)
            where TEntity : class, ICalendarEvent
        {
            Contract.RequiresNotNull(query);
            if (!strictDaysUntilDeparture.HasValue)
            {
                return query;
            }
            var departureDate = DateExtensions.GenDateTime.AddDays(strictDaysUntilDeparture.Value);
            var min = departureDate.AddDays(-1);
            var max = departureDate.AddDays(1);
            return query
                .Where(x => x.StartDate != DateTime.MinValue
                         && x.StartDate > min
                         && x.StartDate < max);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter calendar events by days until departure.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">             The query to act on.</param>
        /// <param name="daysUntilDeparture">The days until departure.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventsByDaysUntilDeparture<TEntity>(
            this IQueryable<TEntity> query, int? daysUntilDeparture)
            where TEntity : class, ICalendarEvent
        {
            Contract.RequiresNotNull(query);
            if (!daysUntilDeparture.HasValue)
            {
                return query;
            }
            var currentDate = DateTime.Today;
            var departureDate = DateExtensions.GenDateTime.AddDays(daysUntilDeparture.Value);
            return query
                .Where(x => x.StartDate != DateTime.MinValue
                         && x.StartDate > currentDate
                         && x.StartDate <= departureDate);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter calendar events by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventsByUserID<TEntity>(
            this IQueryable<TEntity> query, int? id)
            where TEntity : class, ICalendarEvent
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.UserEventAttendances!.Any(y => y.Active && y.SlaveID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter calendar events by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventsByProductID<TEntity>(
            this IQueryable<TEntity> query, int? id)
            where TEntity : class, ICalendarEvent
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.Products!.Any(y => y.Active && y.SlaveID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter calendar events by current events.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">            The query to act on.</param>
        /// <param name="currentEventsOnly">The current events only.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventsByCurrentEvents<TEntity>(
            this IQueryable<TEntity> query, bool? currentEventsOnly)
            where TEntity : class, ICalendarEvent
        {
            Contract.RequiresNotNull(query);
            if (!currentEventsOnly.GetValueOrDefault(false))
            {
                return query;
            }
            var currentDate = DateTime.Today;
            return query.Where(x => x.EndDate >= currentDate);
        }
    }
}
