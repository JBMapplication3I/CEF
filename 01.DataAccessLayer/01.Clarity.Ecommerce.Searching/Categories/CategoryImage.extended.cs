// <copyright file="CategoryImage.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category image search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A category image search extensions.</summary>
    public static class CategoryImageSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter category images by master identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCategoryImagesByMasterID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICategoryImage
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.MasterID == id!.Value);
        }
    }
}
