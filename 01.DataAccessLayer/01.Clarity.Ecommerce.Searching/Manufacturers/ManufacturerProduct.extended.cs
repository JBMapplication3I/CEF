// <copyright file="ManufacturerProduct.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer product search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A manufacturer product search extensions.</summary>
    public static class ManufacturerProductSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by active
        /// manufacturers.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByActiveManufacturers<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IManufacturerProduct
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by manufacturer
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByManufacturerID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IManufacturerProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by manufacturer key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByManufacturerKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IManufacturerProduct
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.CustomKey != null
                    && x.Master.CustomKey.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by manufacturer name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByManufacturerName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IManufacturerProduct
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.Name != null
                    && x.Master.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by active products.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByActiveProducts<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IManufacturerProduct
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByProductID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IManufacturerProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IManufacturerProduct
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

        /// <summary>An IQueryable{TEntity} extension method that filter manufacturer products by product name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterManufacturerProductsByProductName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IManufacturerProduct
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.Name != null
                    && x.Slave.Name.Contains(search));
        }
    }
}
