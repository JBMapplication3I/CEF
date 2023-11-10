// <copyright file="AdZoneAccess.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad zone access search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An ad zone access search extensions.</summary>
    public static class AdZoneAccessSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter ad zone accesses by subscription identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAdZoneAccessesBySubscriptionID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAdZoneAccess
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SubscriptionID.HasValue && x.SubscriptionID.Value == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter ad zone accesses by zone identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAdZoneAccessesByZoneID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAdZoneAccess
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ZoneID.HasValue && x.ZoneID.Value == id!.Value);
        }
    }
}
