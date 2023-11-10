// <copyright file="ProductAssociation.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product association search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A product association search extensions.</summary>
    public static class ProductAssociationSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by primary product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByPrimaryProductID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IProductAssociation
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(pa => pa.MasterID == id);
        }
    }
}
