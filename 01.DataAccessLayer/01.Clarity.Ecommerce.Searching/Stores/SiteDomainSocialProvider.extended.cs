// <copyright file="SiteDomainSocialProvider.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the site domain social provider search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A site domain social provider search extensions.</summary>
    public static class SiteDomainSocialProviderSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter site domain social providers by social provider
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSiteDomainSocialProvidersBySocialProviderID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, ISiteDomainSocialProvider
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.SlaveID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter site domain social providers by active social
        /// providers.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSiteDomainSocialProvidersByActiveSocialProviders<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, ISiteDomainSocialProvider
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter site domain social providers by active site
        /// domains.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSiteDomainSocialProvidersByActiveSiteDomains<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, ISiteDomainSocialProvider
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Master != null && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter site domain social providers by site domain
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSiteDomainSocialProvidersBySiteDomainID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, ISiteDomainSocialProvider
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.MasterID == id);
        }
    }
}
