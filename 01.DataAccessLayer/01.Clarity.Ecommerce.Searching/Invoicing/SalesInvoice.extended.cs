// <copyright file="SalesInvoice.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The sales invoice search extensions.</summary>
    public static class SalesInvoiceSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales invoices to those that have a balance due.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterSalesInvoicesByHasBalanceDue<TEntity>(
            this IQueryable<TEntity> query,
            bool? has)
            where TEntity : class, ISalesInvoice
        {
            if (!has.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.BalanceDue > 0m);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales invoices to those that have a due date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterSalesInvoicesByHasDueDate<TEntity>(
            this IQueryable<TEntity> query,
            bool? has)
            where TEntity : class, ISalesInvoice
        {
            if (!has.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.DueDate.HasValue);
        }
    }
}
