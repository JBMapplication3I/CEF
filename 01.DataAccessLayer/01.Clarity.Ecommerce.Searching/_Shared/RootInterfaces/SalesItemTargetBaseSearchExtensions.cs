// <copyright file="SalesItemTargetBaseSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item target base search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The sales item target base search extensions.</summary>
    public static class SalesItemTargetBaseSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales item target bases by master identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesItemTargetBasesByMasterID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesItemTargetBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }
    }
}
