// <copyright file="StoreProduct.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store product search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A store product search extensions.</summary>
    public static class StoreProductSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter store products by active stores.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreProductsByActiveStores<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreProduct
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master!.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store products by active products.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreProductsByActiveProducts<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreProduct
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave!.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store products by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreProductsByProductID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store products by store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreProductsByStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store products by product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreProductsByProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IStoreProduct
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave!.CustomKey != null
                         && x.Slave.CustomKey.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store products by product name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreProductsByProductName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IStoreProduct
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave!.Name != null
                         && x.Slave.Name.Contains(search));
        }
    }
}
