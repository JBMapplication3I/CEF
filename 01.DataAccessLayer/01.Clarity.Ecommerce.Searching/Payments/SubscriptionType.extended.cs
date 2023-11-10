// <copyright file="SubscriptionType.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription type search extension class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A subscription type search extensions.</summary>
    public static class SubscriptionTypeSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter subscription types by greater sort order.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionTypesByGreaterSortOrder<TEntity>(
                this IQueryable<TEntity> query,
                int? sortOrder)
            where TEntity : class, ISubscriptionType
        {
            return Contract.RequiresNotNull(query).Where(x => x.SortOrder > sortOrder);
        }
    }
}
