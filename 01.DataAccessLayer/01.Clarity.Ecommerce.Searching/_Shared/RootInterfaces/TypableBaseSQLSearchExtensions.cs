// <copyright file="TypableBaseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the typable base SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Models;
    using Utilities;

    /// <summary>A typable base SQL search extensions.</summary>
    public static class TypableBaseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by typable base search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterByTypableBaseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                ITypableBaseSearchModel model)
            where TEntity : class, ITypableBase
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
