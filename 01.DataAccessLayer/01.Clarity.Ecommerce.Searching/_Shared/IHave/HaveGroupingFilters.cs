// <copyright file="HaveGroupingFilters.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have grouping filters class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using Utilities;

    /// <summary>A have grouping filters.</summary>
    public static class HaveGroupingFilters
    {
        /// <summary>An IQueryable{TEntity} extension method that applies the grouping.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="groups">The groups.</param>
        /// <param name="sorts"> The sorts.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> ApplyGrouping<TEntity>(
            this IQueryable<TEntity> query,
            Grouping[] groups,
            Sort[] sorts)
        {
            Contract.RequiresNotNull(query);
            if (groups == null || groups.Length == 0)
            {
                return query;
            }
            if (sorts == null || sorts.Length == 0)
            {
                return query;
            }
            return query;
        }
    }
}
