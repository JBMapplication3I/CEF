// <copyright file="Subscription.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A subscription search extensions.</summary>
    public static class SubscriptionSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter subscriptions by sales invoice identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionsBySalesInvoiceID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, ISubscription
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.SalesInvoiceID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscriptions by repeat type key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionsByRepeatTypeKey<TEntity>(this IQueryable<TEntity> query, string? key)
            where TEntity : class, ISubscription
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return query.Where(x => x.RepeatType != null && x.RepeatType.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscriptions by repeat type name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionsByRepeatTypeName<TEntity>(this IQueryable<TEntity> query, string? name)
            where TEntity : class, ISubscription
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return query.Where(x => x.RepeatType != null && x.RepeatType.Name == name);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscriptions by account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionsByAccountID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, ISubscription
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.AccountID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscriptions by account key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionsByAccountKey<TEntity>(this IQueryable<TEntity> query, string? key)
            where TEntity : class, ISubscription
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return query.Where(x => x.Account != null && x.Account.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscriptions by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionsByUserID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, ISubscription
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.UserID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter subscriptions by user key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSubscriptionsByUserKey<TEntity>(this IQueryable<TEntity> query, string? key)
            where TEntity : class, ISubscription
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return query.Where(x => x.User != null && x.User.CustomKey == key);
        }
    }
}
