// <copyright file="HavePagingFilters.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have paging filters class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A have paging filters.</summary>
    public static class HavePagingFilters
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by paging.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">     The query to act on.</param>
        /// <param name="paging">    The paging.</param>
        /// <param name="totalPages">The total pages.</param>
        /// <param name="totalCount">Number of totals.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByPaging<TEntity>(
                this IQueryable<TEntity> query,
                Paging? paging,
                out int totalPages,
                out int totalCount)
            where TEntity : class, IBase
        {
            totalCount = Contract.RequiresNotNull(query).Count();
            totalPages = 1;
            if (paging == null)
            {
                return query;
            }
            if (paging.Size == null
                || paging.StartIndex == null
                || paging.Size <= 0
                || paging.StartIndex < 0)
            {
                return query;
            }
            totalPages = (int)Math.Ceiling(totalCount / (double)paging.Size.Value);
            return query
                .Skip(paging.Size.Value * (paging.StartIndex.Value - 1))
                .Take(paging.Size.Value);
            // ReSharper restore PossibleMultipleEnumeration
        }
    }
}
