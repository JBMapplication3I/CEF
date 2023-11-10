// <copyright file="CalendarEventProduct.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event product search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A calendar event product search extensions.</summary>
    public static class CalendarEventProductSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter calendar event products by product
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCalendarEventProductsByProductID<TEntity>(
                this IQueryable<TEntity> query, int? id)
            where TEntity : class, ICalendarEventProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query).Where(x => x.SlaveID == id);
        }
    }
}
