// <copyright file="Region.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A region search extensions.</summary>
    public static class RegionSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter regions by identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="regionID"> Identifier for the region.</param>
        /// <param name="countryID">Identifier for the country.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRegionsByID<TEntity>(
                this IQueryable<TEntity> query,
                int? regionID,
                int? countryID = null)
            where TEntity : class, IRegion
        {
            if (Contract.CheckValidID(regionID))
            {
                return query.Where(x => x.ID == regionID!.Value);
            }
            if (Contract.CheckValidID(countryID))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CountryID == countryID!.Value);
            }
            return Contract.RequiresNotNull(query);
        }
    }
}
