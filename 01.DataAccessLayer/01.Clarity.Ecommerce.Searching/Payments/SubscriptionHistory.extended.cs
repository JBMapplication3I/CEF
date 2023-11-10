// <copyright file="SubscriptionHistory.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription history search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A subscription history search extensions.</summary>
    public static class SubscriptionHistorySearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter subscription histories by minimum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="min">  The minimum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionHistoriesByMinDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? min)
            where TEntity : class, ISubscriptionHistory
        {
            if (!Contract.CheckValidDate(min))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.PaymentDate >= min);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscription histories by maximum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="max">  The maximum.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionHistoriesByMaxDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? max)
            where TEntity : class, ISubscriptionHistory
        {
            if (!Contract.CheckValidDate(max))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.PaymentDate <= max);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscription histories by subscription
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionHistoriesBySubscriptionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISubscriptionHistory
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscription histories by payment identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionHistoriesByPaymentID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISubscriptionHistory
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscription histories by memo.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="memo"> The memo.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionHistoriesByMemo<TEntity>(
                this IQueryable<TEntity> query,
                string? memo)
            where TEntity : class, ISubscriptionHistory
        {
            if (!Contract.CheckValidKey(memo))
            {
                return query;
            }
            var search = memo!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Memo!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscription histories by succeeded state.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="state">The state.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionHistoriesBySucceededState<TEntity>(
                this IQueryable<TEntity> query,
                bool? state)
            where TEntity : class, ISubscriptionHistory
        {
            if (!state.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.PaymentSuccess == state.Value);
        }
    }
}
