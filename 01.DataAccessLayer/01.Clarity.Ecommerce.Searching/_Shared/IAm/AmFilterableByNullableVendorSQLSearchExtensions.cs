// <copyright file="AmFilterableByNullableVendorSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by nullable vendor SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by nullable vendor SQL search extensions.</summary>
    public static class AmFilterableByNullableVendorSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by nullable vendor search
        /// model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByNullableVendorSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByVendorSearchModel model)
            where TEntity : class, IAmFilterableByNullableVendor
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByNullableVendorID(model.VendorID)
                .FilterByNullableVendorKey(model.VendorKey)
                .FilterByNullableVendorName(model.VendorName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable vendor identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableVendorID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByNullableVendor
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Vendor != null && x.Vendor.Active && x.VendorID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable vendor key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableVendorKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByNullableVendor
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Vendor != null && x.Vendor.Active && x.Vendor.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable vendor vendorname.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableVendorName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByNullableVendor
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Vendor != null && x.Vendor.Active && x.Vendor.Name == name);
        }
    }
}
