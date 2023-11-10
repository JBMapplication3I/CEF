// <copyright file="ProductSubscriptionType.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product subscription type search extension class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A product subscription type search extension.</summary>
    public static class ProductSubscriptionTypeSearchExtension
    {
        /// <summary>An IQueryable{TEntity} extension method that filter product subscription types by subscription type
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductSubscriptionTypesBySubscriptionTypeID<TEntity>(
                this IQueryable<TEntity> query, int? id)
            where TEntity : class, IProductSubscriptionType
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.SlaveID == id!.Value);
        }
    }
}
