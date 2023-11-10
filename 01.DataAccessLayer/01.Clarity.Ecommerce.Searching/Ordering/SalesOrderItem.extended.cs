// <copyright file="SalesOrderItem.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order item search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The sales order item search extensions.</summary>
    public static class SalesOrderItemSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales order items by sales order identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesOrderItemsBySalesOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesOrderItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales order items by sales order key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesOrderItemsBySalesOrderKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, ISalesOrderItem
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales order items by user external
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesOrderItemsByUserExternalID<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, ISalesOrderItem
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.User != null
                    && (x.Master.User.CustomKey == key || x.Master.User.UserName == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales order items by sales order active.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesOrderItemsBySalesOrderActive<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, ISalesOrderItem
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master!.Active);
        }
    }
}
