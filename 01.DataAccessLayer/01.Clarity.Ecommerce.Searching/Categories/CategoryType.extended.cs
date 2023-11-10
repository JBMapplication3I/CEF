// <copyright file="CategoryType.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category type search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A category type search extensions.</summary>
    public static class CategoryTypeSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that order category types by default.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> OrderCategoryTypesByDefault<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, ICategoryType
        {
            Contract.RequiresNotNull(query);
            return query.OrderBy(ct => ct.Name);
        }
    }
}
