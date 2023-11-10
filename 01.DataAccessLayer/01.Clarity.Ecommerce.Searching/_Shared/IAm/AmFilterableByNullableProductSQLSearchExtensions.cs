// <copyright file="AmFilterableByNullableProductSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by nullable product SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by nullable product SQL search extensions.</summary>
    public static class AmFilterableByNullableProductSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by nullable product search
        /// model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByNullableProductSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByProductSearchModel model)
            where TEntity : class, IAmFilterableByNullableProduct
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByNullableProductID(model.ProductID)
                .FilterByNullableProductKey(model.ProductKey)
                .FilterByNullableProductName(model.ProductName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableProductID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByNullableProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Product != null && x.Product.Active && x.ProductID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByNullableProduct
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Product != null && x.Product.Active && x.Product.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable product name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableProductName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByNullableProduct
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Product != null && x.Product.Active && x.Product.Name == name);
        }
    }
}
