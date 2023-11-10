// <copyright file="PriceRounding.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rounding search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A price rounding search extensions.</summary>
    public static class PriceRoundingSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter price roundings by price point key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRoundingsByPricePointKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IPriceRounding
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.PricePointKey != null
                    && x.PricePointKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price roundings by currency key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRoundingsByCurrencyKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IPriceRounding
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.CurrencyKey != null
                    && x.CurrencyKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price roundings by product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRoundingsByProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IPriceRounding
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ProductKey != null
                    && x.ProductKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price roundings by unit of measure.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">        The query to act on.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRoundingsByUnitOfMeasure<TEntity>(
                this IQueryable<TEntity> query,
                string? unitOfMeasure)
            where TEntity : class, IPriceRounding
        {
            if (!Contract.CheckValidKey(unitOfMeasure))
            {
                return query;
            }
            var search = unitOfMeasure!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitOfMeasure != null
                    && x.UnitOfMeasure == search);
        }
    }
}
