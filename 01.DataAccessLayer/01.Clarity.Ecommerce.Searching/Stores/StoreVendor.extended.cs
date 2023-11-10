// <copyright file="StoreVendor.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store vendor search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A store vendor search extensions.</summary>
    public static class StoreVendorSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter store vendors by active stores.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreVendorsByActiveStores<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreVendor
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store vendors by active vendors.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreVendorsByActiveVendors<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreVendor
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store vendors by vendor identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreVendorsByVendorID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreVendor
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id!.Value);
        }
    }
}
