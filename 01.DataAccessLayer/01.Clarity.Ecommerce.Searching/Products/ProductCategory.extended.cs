// <copyright file="ProductCategory.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product category search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A product category search extensions.</summary>
    public static class ProductCategorySearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter product categories by JSON attribute value.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductCategoriesByJsonAttributeValue<TEntity>(this IQueryable<TEntity> query, string? name, string? value)
            where TEntity : class, IProductCategory
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(name) || !Contract.CheckValidKey(value))
            {
                return query;
            }
            var search = $"\"{name}\":\"{value}\"".ToLower();
            return query.Where(x => x.JsonAttributes != null && x.JsonAttributes != string.Empty && x.JsonAttributes.ToLower().Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product categories by category identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductCategoriesByCategoryID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IProductCategory
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.SlaveID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product categories by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductCategoriesByProductID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IProductCategory
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.MasterID == id!.Value);
        }
    }
}
