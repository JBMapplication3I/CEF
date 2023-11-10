// <copyright file="AmFilterableByCategorySQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by brand SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by brand SQL search extensions.</summary>
    public static class AmFilterableByCategorySQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by brand search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByCategorySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByCategorySearchModel model)
            where TEntity : class, IAmFilterableByCategory
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByCategoryID(model.CategoryID)
                .FilterByCategoryKey(model.CategoryKey)
                .FilterByCategoryName(model.CategoryName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by brand identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCategoryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByCategory
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Category != null && x.Category.Active && x.CategoryID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by brand key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCategoryKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByCategory
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Category != null && x.Category.Active && x.Category.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by brand name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCategoryName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByCategory
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Category != null && x.Category.Active && x.Category.Name == name);
        }
    }
}
