// <copyright file="StoreManufacturer.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store manufacturer search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A store manufacturer search extensions.</summary>
    public static class StoreManufacturerSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter store manufacturers by active stores.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreManufacturersByActiveStores<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreManufacturer
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                         && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store manufacturers by active manufacturers.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreManufacturersByActiveManufacturers<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreManufacturer
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                         && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store manufacturers by manufacturer
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreManufacturersByManufacturerID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreManufacturer
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
