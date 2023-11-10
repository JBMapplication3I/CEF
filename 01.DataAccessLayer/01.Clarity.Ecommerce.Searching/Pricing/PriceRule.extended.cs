// <copyright file="PriceRule.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A price rule search extensions.</summary>
    public static class PriceRuleSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter price rules by unit of measure.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">        The query to act on.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByUnitOfMeasure<TEntity>(
            this IQueryable<TEntity> query, string? unitOfMeasure)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(unitOfMeasure))
            {
                return query;
            }
            return query.Where(x => x.UnitOfMeasure == unitOfMeasure);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by minimum quantity minimum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="min">  The minimum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByMinQuantityMin<TEntity>(
                this IQueryable<TEntity> query, decimal? min)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(min))
            {
                return query;
            }
            return query.Where(x => x.MinQuantity >= min!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by minimum quantity maximum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="max">  The maximum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByMinQuantityMax<TEntity>(
                this IQueryable<TEntity> query, decimal? max)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(max))
            {
                return query;
            }
            return query.Where(x => x.MinQuantity <= max!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by maximum quantity minimum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="min">  The minimum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByMaxQuantityMin<TEntity>(
                this IQueryable<TEntity> query, decimal? min)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(min))
            {
                return query;
            }
            return query.Where(x => x.MaxQuantity >= min!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by maximum quantity maximum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="max">  The maximum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByMaxQuantityMax<TEntity>(
                this IQueryable<TEntity> query, decimal? max)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(max))
            {
                return query;
            }
            return query.Where(x => x.MaxQuantity <= max!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by start date minimum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="min">  The minimum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByStartDateMin<TEntity>(
                this IQueryable<TEntity> query, DateTime? min)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidDate(min))
            {
                return query;
            }
            return query.Where(x => x.StartDate >= min!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by start date maximum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="max">  The maximum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByStartDateMax<TEntity>(
                this IQueryable<TEntity> query, DateTime? max)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidDate(max))
            {
                return query;
            }
            return query.Where(x => x.StartDate <= max!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by end date minimum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="min">  The minimum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByEndDateMin<TEntity>(
                this IQueryable<TEntity> query, DateTime? min)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidDate(min))
            {
                return query;
            }
            return query.Where(x => x.EndDate >= min!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by end date maximum.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="max">  The maximum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByEndDateMax<TEntity>(
                this IQueryable<TEntity> query, DateTime? max)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidDate(max))
            {
                return query;
            }
            return query.Where(x => x.EndDate <= max!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter price rules by priority.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">   The query to act on.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPriceRulesByPriority<TEntity>(
                this IQueryable<TEntity> query, int? priority)
            where TEntity : class, IPriceRule
        {
            Contract.RequiresNotNull(query);
            if (!priority.HasValue)
            {
                return query;
            }
            return query.Where(x => x.Priority == priority.Value);
        }
    }
}
