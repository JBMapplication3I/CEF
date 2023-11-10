// <copyright file="ServiceArea.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service area search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>Service area search extensions.</summary>
    public static class ServiceAreaSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filters service areas by finding those whose radius
        /// includes a given lat/long coordinate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="latitude"> The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="units">    The units.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterServiceAreasByLatitudeLongitudeRadius<TEntity>(
                this IQueryable<TEntity> query,
                double? latitude,
                double? longitude,
                Enums.LocatorUnits? units)
            where TEntity : class, IServiceArea
        {
            if (latitude == null
                || longitude == null)
            {
                return query;
            }
            var preFilter = Contract.RequiresNotNull(query)
                .Select(x => new
                {
                    x.ID,
                    x.Address!.Longitude,
                    x.Address.Latitude,
                    x.Radius,
                })
                // Now ToList because the distance calculation can't be done in SQL using LINQ
                .ToList();
            // Filter by Calculated Distance
            // ReSharper disable PossibleInvalidOperationException
            var matchedIDs = preFilter
                .Where(x => GeographicDistance.BetweenLocations(
                    (double)x.Latitude!.Value,
                    (double)x.Longitude!.Value,
                    latitude.Value,
                    longitude.Value,
                    units) <= (double)x.Radius!.Value)
                .Select(x => x.ID)
                .ToList();
            // ReSharper restore PossibleInvalidOperationException
            // If no IDs match, passing the empty enumerable to FilterByIDs
            // actually doesn't filter anything, but we want to make sure the
            // query returns nothing in that case, hence the .Where(x => false)
            return Contract.CheckNotEmpty(matchedIDs)
                ? query.FilterByIDs(matchedIDs)
                : query.Where(x => false);
        }
    }
}
