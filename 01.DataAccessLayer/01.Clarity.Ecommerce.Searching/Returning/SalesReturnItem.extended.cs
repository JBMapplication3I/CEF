// <copyright file="SalesReturnItem.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return item search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The sales return item search extensions.</summary>
    public static class SalesReturnItemSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales return items by sales return
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesReturnItemsBySalesReturnID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, ISalesReturnItem
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales return items by sales return key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesReturnItemsBySalesReturnKey<TEntity>(this IQueryable<TEntity> query, string? key)
            where TEntity : class, ISalesReturnItem
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return query.Where(x => x.Master != null && x.Master.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales return items by user external
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesReturnItemsByUserExternalID<TEntity>(this IQueryable<TEntity> query, string? id)
            where TEntity : class, ISalesReturnItem
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(id))
            {
                return query;
            }
            return query.Where(x => x.Master != null && x.Master.User != null && (x.Master.User.CustomKey == id || x.Master.User.UserName == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales return items by sales return active.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesReturnItemsBySalesReturnActive<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, ISalesReturnItem
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Master != null && x.Master.Active);
        }
    }
}
