// <copyright file="FranchiseSiteDomain.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise site domain search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A franchise site domain search extensions.</summary>
    public static class FranchiseSiteDomainSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter franchise site domains by franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchiseSiteDomainsByFranchiseID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IFranchiseSiteDomain
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter franchise site domains by active franchises.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchiseSiteDomainsByActiveFranchises<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IFranchiseSiteDomain
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Master != null && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter franchise site domains by active site domains.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchiseSiteDomainsByActiveSiteDomains<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IFranchiseSiteDomain
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter franchise site domains by site domain identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchiseSiteDomainsBySiteDomainID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IFranchiseSiteDomain
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
