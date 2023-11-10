// <copyright file="ProductImage.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product image search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A product image search extensions.</summary>
    public static class ProductImageSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter product images by master identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductImagesByMasterID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IProductImage
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(p => p.MasterID == id!.Value);
        }
    }
}
