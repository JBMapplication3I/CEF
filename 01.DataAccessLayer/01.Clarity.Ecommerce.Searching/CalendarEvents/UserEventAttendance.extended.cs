// <copyright file="UserEventAttendance.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user event attendance search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A user event attendance search extensions.</summary>
    public static class UserEventAttendanceSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter user event attendances by calendar event
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUserEventAttendancesByCalendarEventID<TEntity>(
            this IQueryable<TEntity> query, int? id)
            where TEntity : class, IUserEventAttendance
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user event attendances by calendar event key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUserEventAttendancesByCalendarEventKey<TEntity>(
            this IQueryable<TEntity> query, string? key)
            where TEntity : class, IUserEventAttendance
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return query.Where(x => x.Master != null && x.Master.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user event attendances by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUserEventAttendancesByUserID<TEntity>(
            this IQueryable<TEntity> query, int? id)
            where TEntity : class, IUserEventAttendance
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.SlaveID == id);
        }
    }
}
