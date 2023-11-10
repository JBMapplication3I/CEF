// <copyright file="SalesInvoiceItem.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice item search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The sales invoice item search extensions.</summary>
    public static class SalesInvoiceItemSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales invoice items by sales invoice
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesInvoiceItemsBySalesInvoiceID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesInvoiceItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales invoice items by sales invoice key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesInvoiceItemsBySalesInvoiceKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, ISalesInvoiceItem
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                         && x.Master.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales invoice items by user external
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesInvoiceItemsByUserExternalID<TEntity>(
                this IQueryable<TEntity> query,
                string? id)
            where TEntity : class, ISalesInvoiceItem
        {
            if (!Contract.CheckValidKey(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                         && x.Master.User != null
                         && (x.Master.User.CustomKey == id || x.Master.User.UserName == id));
        }
    }
}
