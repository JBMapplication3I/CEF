// <copyright file="Currencies.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currencies search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The currencies search extensions.</summary>
    public static class CurrenciesSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter historical currency rates by starting currency
        /// key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterHistoricalCurrencyRatesByStartingCurrencyKey<TEntity>(this IQueryable<TEntity> query, string? key)
            where TEntity : class, IHistoricalCurrencyRate
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.StartingCurrency!.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter historical currency rates by ending currency
        /// key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterHistoricalCurrencyRatesByEndingCurrencyKey<TEntity>(this IQueryable<TEntity> query, string? key)
            where TEntity : class, IHistoricalCurrencyRate
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.EndingCurrency!.CustomKey == key);
        }
    }
}
