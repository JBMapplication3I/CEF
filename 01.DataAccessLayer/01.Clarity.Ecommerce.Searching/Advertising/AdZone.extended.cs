// <copyright file="AdZone.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad zone search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An ad zone search extensions.</summary>
    public static class AdZoneSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter ad zones by ad identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAdZonesByAdID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAdZone
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.AdZones!.Any(y => y.MasterID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter ad zones by zone identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAdZonesByZoneID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAdZone
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.AdZones!.Any(y => y.SlaveID == id!.Value));
        }
    }
}
