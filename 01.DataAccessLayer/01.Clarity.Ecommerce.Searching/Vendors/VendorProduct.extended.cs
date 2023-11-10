// <copyright file="VendorProduct.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor product search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A vendor product search extensions.</summary>
    public static class VendorProductSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter vendor products by vendor identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterVendorProductsByVendorID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IVendorProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter vendor products by active vendors.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterVendorProductsByActiveVendors<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IVendorProduct
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter vendor products by active products.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterVendorProductsByActiveProducts<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IVendorProduct
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null && x.Slave.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter vendor products by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterVendorProductsByProductID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IVendorProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter vendor products by product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterVendorProductsByProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IVendorProduct
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                         && x.Slave.CustomKey != null
                         && x.Slave.CustomKey.Trim().ToLower().Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter vendor products by product name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterVendorProductsByProductName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IVendorProduct
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                    .Where(x => x.Slave != null
                             && x.Slave.Name != null
                             && x.Slave.Name.Trim().ToLower().Contains(search));
        }
    }
}
