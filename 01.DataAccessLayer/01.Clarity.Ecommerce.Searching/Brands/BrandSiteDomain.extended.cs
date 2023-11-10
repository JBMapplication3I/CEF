// <copyright file="BrandSiteDomain.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand site domain search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A brand site domain search extensions.</summary>
    public static class BrandSiteDomainSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter brand site domains by brand identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandSiteDomainsByBrandID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IBrandSiteDomain
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter brand site domains by active brands.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandSiteDomainsByActiveBrands<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IBrandSiteDomain
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Master != null && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter brand site domains by active site domains.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandSiteDomainsByActiveSiteDomains<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IBrandSiteDomain
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter brand site domains by site domain identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandSiteDomainsBySiteDomainID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IBrandSiteDomain
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
