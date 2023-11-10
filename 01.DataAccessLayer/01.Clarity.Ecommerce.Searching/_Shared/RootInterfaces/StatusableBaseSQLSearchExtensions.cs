// <copyright file="StatusableBaseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the statusable base SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Models;
    using Utilities;

    /// <summary>A Statusable base SQL search extensions.</summary>
    public static class StatusableBaseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by statusable base search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterByStatusableBaseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IStatusableBaseSearchModel model)
            where TEntity : class, IStatusableBase
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByDisplayableBaseSearchModel(model);
        }
    }
}
