// <copyright file="Cart.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A cart search extensions.</summary>
    public static class CartSearchExtensions
    {
        public static IQueryable<TEntity> FilterCartsByLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                SessionCartBySessionAndTypeLookupKey? lookupKey)
            where TEntity : class, ICart
        {
            if (lookupKey is null)
            {
                return query;
            }
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type!.Name == lookupTypeName)
                .FilterCartsBySessionID(lookupKey.SessionID)
                .FilterCartsByUserID(lookupKey.UserID)
                .FilterCartsByAccountID(lookupKey.AccountID)
                .FilterCartsByBrandID(lookupKey.BrandID)
                .FilterCartsByFranchiseID(lookupKey.FranchiseID)
                .FilterCartsByStoreID(lookupKey.StoreID);
        }

        public static IQueryable<TEntity> FilterCartsByLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                TargetCartLookupKey? lookupKey,
                bool useStartsWithPrefix)
            where TEntity : class, ICart
        {
            if (lookupKey is null)
            {
                return query;
            }
            query = Contract.RequiresNotNull(query)
                .FilterCartsBySessionID(lookupKey.SessionID)
                .FilterCartsByUserID(lookupKey.UserID)
                .FilterCartsByAccountID(lookupKey.AccountID)
                .FilterCartsByBrandID(lookupKey.BrandID)
                .FilterCartsByFranchiseID(lookupKey.FranchiseID)
                .FilterCartsByStoreID(lookupKey.StoreID);
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return useStartsWithPrefix
                ? query.Where(x => x.Type!.Name!.StartsWith("Target-Grouping-"))
                : query.Where(x => x.Type!.Name == lookupTypeName);
        }

        public static IQueryable<TEntity> FilterCartsByLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                StaticCartLookupKey? lookupKey)
            where TEntity : class, ICart
        {
            if (lookupKey is null)
            {
                return query;
            }
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type!.Name == lookupTypeName)
                .FilterCartsByUserID(lookupKey.UserID)
                .FilterCartsByAccountID(lookupKey.AccountID)
                .FilterCartsByBrandID(lookupKey.BrandID)
                .FilterCartsByFranchiseID(lookupKey.FranchiseID)
                .FilterCartsByStoreID(lookupKey.StoreID);
        }

        public static IQueryable<TEntity> FilterCartsByLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                CompareCartBySessionLookupKey? lookupKey)
            where TEntity : class, ICart
        {
            if (lookupKey is null)
            {
                return query;
            }
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type!.Name == lookupTypeName)
                .FilterCartsBySessionID(lookupKey.SessionID)
                .FilterCartsByUserID(lookupKey.UserID)
                .FilterCartsByAccountID(lookupKey.AccountID)
                .FilterCartsByBrandID(lookupKey.BrandID)
                .FilterCartsByFranchiseID(lookupKey.FranchiseID)
                .FilterCartsByStoreID(lookupKey.StoreID);
        }

        public static IQueryable<TEntity> FilterCartsByLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                CartByIDLookupKey? lookupKey)
            where TEntity : class, ICart
        {
            if (lookupKey is null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByID(lookupKey.CartID)
                .FilterCartsByUserID(lookupKey.UserID)
                .FilterCartsByAccountID(lookupKey.AccountID)
                .FilterCartsByBrandID(lookupKey.BrandID)
                .FilterCartsByFranchiseID(lookupKey.FranchiseID)
                .FilterCartsByStoreID(lookupKey.StoreID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by have active items.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="have"> The have.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsByHaveActiveItems<TEntity>(
                this IQueryable<TEntity> query,
                bool? have)
            where TEntity : class, ICart
        {
            if (!have.HasValue)
            {
                return query;
            }
            if (have.Value)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SalesItems!.Any(y => y.Active));
            }
            return Contract.RequiresNotNull(query)
                .Where(x => !x.SalesItems!.Any() || x.SalesItems!.All(y => !y.Active));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by session identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="sessionID">Identifier for the session.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsBySessionID<TEntity>(
                this IQueryable<TEntity> query,
                Guid? sessionID)
            where TEntity : class, ICart
        {
            if (sessionID is null || sessionID == default(Guid))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SessionID == sessionID.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="userID">Identifier for the user.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? userID)
            where TEntity : class, ICart
        {
            if (Contract.CheckInvalidID(userID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserID == userID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="filterOrderRequests">The bool to return order requests.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsByOrderRequest<TEntity>(
                this IQueryable<TEntity> query,
                bool? filterOrderRequests)
            where TEntity : class, ICart
        {
            if (filterOrderRequests == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey!.Contains("OrderRequest"));
            }
            else
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => !x.CustomKey!.Contains("OrderRequest"));
            }
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="accountID">Identifier for the account.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsByAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? accountID)
            where TEntity : class, ICart
        {
            if (Contract.CheckInvalidID(accountID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountID == accountID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by brand identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="brandID">Identifier for the brand.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsByBrandID<TEntity>(
                this IQueryable<TEntity> query,
                int? brandID)
            where TEntity : class, ICart
        {
            if (Contract.CheckInvalidID(brandID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.BrandID == brandID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="franchiseID">Identifier for the franchise.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsByFranchiseID<TEntity>(
                this IQueryable<TEntity> query,
                int? franchiseID)
            where TEntity : class, ICart
        {
            if (Contract.CheckInvalidID(franchiseID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.FranchiseID == franchiseID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter carts by store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="storeID">Identifier for the store.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCartsByStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? storeID)
            where TEntity : class, ICart
        {
            if (Contract.CheckInvalidID(storeID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.StoreID == storeID);
        }
    }
}
