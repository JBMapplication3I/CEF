// <copyright file="ReportType.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the report type search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A report type search extensions.</summary>
    public static class ReportTypeSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter report types by has template.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterReportTypesByHasTemplate<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IReportType
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Template != null);
        }
    }
}
