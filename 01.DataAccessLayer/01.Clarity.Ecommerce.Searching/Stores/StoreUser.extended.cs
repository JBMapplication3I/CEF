// <copyright file="StoreUser.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store user search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A store user search extensions.</summary>
    public static class StoreUserSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter store users by active stores.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreUsersByActiveStores<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreUser
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                         && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store users by active users.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreUsersByActiveUsers<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreUser
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                         && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store users by store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreUsersByStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreUser
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store users by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreUsersByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreUser
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store users by user key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreUsersByUserKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IStoreUser
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                         && x.Slave.CustomKey != null
                         && x.Slave.CustomKey.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store users by user name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreUsersByUserName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IStoreUser
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && (x.Slave.UserName != null && x.Slave.UserName.Contains(search)
                        || x.Slave.CustomKey != null && x.Slave.CustomKey.Contains(search)));
        }
    }
}
