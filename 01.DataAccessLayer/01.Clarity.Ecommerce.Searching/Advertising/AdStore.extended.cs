// <copyright file="AdStore.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad store search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An ad store search extensions.</summary>
    public static class AdStoreSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter ad stores by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAdStoresByUserID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAdStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.Users!.Any(y => y.SlaveID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter ad stores by zone identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAdStoresByZoneID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAdStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.AdZones!.Any(y => y.SlaveID == id!.Value));
        }
    }
}
