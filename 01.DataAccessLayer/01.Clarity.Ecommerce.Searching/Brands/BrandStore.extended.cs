// <copyright file="BrandStore.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand store search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A brand store search extensions.</summary>
    public static class BrandStoreSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter brand stores by active stores.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandStoresByActiveStores<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IBrandStore
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter brand stores by active brands.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandStoresByActiveBrands<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IBrandStore
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Master != null && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter brand stores by brand identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandStoresByBrandID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IBrandStore
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
