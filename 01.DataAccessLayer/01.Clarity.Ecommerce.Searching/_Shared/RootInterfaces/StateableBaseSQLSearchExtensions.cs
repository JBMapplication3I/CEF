// <copyright file="StateableBaseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stateable base SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Models;
    using Utilities;

    /// <summary>A Stateable base SQL search extensions.</summary>
    public static class StateableBaseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by stateable base search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterByStateableBaseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IStateableBaseSearchModel model)
            where TEntity : class, IStateableBase
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
