// <copyright file="SalesQuote.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>The sales quote search extensions.</summary>
    public static class SalesQuoteSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales quotes by has sales group as master.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesQuotesByHasSalesGroupAsRequest<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, ISalesQuote
        {
            if (has == null)
            {
                return query;
            }
            return has.Value
                ? Contract.RequiresNotNull(query)
                    .Where(x => x.SalesGroupAsRequestMasterID.HasValue || x.SalesGroupAsRequestSubID.HasValue)
                : Contract.RequiresNotNull(query)
                    .Where(x => !x.SalesGroupAsRequestMasterID.HasValue && !x.SalesGroupAsRequestSubID.HasValue);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales quotes by has sales group as response.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesQuotesByHasSalesGroupAsResponse<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, ISalesQuote
        {
            if (has == null)
            {
                return query;
            }
            return has.Value
                ? Contract.RequiresNotNull(query)
                    .Where(x => x.SalesGroupAsResponseMasterID.HasValue || x.SalesGroupAsResponseSubID.HasValue)
                : Contract.RequiresNotNull(query)
                    .Where(x => !x.SalesGroupAsResponseMasterID.HasValue && !x.SalesGroupAsResponseSubID.HasValue);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales quotes by has sales group as master.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterSalesQuotesByHasSalesGroupAsMaster<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, ISalesQuote
        {
            if (has == null)
            {
                return query;
            }
            return has.Value
                ? Contract.RequiresNotNull(query)
                    .Where(x => x.SalesGroupAsRequestMasterID.HasValue || x.SalesGroupAsResponseMasterID.HasValue)
                : Contract.RequiresNotNull(query)
                    .Where(x => !x.SalesGroupAsRequestMasterID.HasValue && !x.SalesGroupAsResponseMasterID.HasValue);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales quotes by has sales group as sub.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterSalesQuotesByHasSalesGroupAsSub<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, ISalesQuote
        {
            if (has == null)
            {
                return query;
            }
            return has.Value
                ? Contract.RequiresNotNull(query)
                    .Where(x => x.SalesGroupAsRequestSubID.HasValue || x.SalesGroupAsResponseSubID.HasValue)
                : Contract.RequiresNotNull(query)
                    .Where(x => !x.SalesGroupAsRequestSubID.HasValue && !x.SalesGroupAsResponseSubID.HasValue);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales quotes by category i ds.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesQuotesByCategoryIDs<TEntity>(
                this IQueryable<TEntity> query,
                int[]? ids)
            where TEntity : class, ISalesQuote
        {
            if (ids?.Any(x => Contract.CheckValidID(x)) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(ids.Aggregate(
                    PredicateBuilder.New<TEntity>(false),
                    (c, id) => c.Or(p => p.SalesQuoteCategories!.Any(soc => soc.Active && soc.SlaveID == id))));
        }
    }
}
