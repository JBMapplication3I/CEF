// <copyright file="StoreAccount.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store account search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A store account search extensions.</summary>
    public static class StoreAccountSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter store accounts by active stores.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreAccountsByActiveStores<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IStoreAccount
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store accounts by active accounts.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreAccountsByActiveAccounts<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IStoreAccount
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store accounts by account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreAccountsByAccountID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IStoreAccount
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
