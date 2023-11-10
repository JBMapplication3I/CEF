// <copyright file="CartType.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart type search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A cart type search extensions.</summary>
    public static class CartTypeSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter cart types by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartTypesByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartType
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CreatedByUserID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart types by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="filterOrderRequests">The bool to return order requests.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartTypesByOrderRequests<TEntity>(
                this IQueryable<TEntity> query,
                bool? filterOrderRequests)
            where TEntity : class, ICartType
        {
            if (filterOrderRequests == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey!.Contains("OrderRequest"));
            }
            else
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => !x.CustomKey!.Contains("OrderRequest"));
            }
        }
    }
}
