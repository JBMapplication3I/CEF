// <copyright file="Discount.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The discount search extensions.</summary>
    public static class DiscountSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter discounts by code.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="code"> The code.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterDiscountsByCode<TEntity>(
                this IQueryable<TEntity> query,
                string? code)
            where TEntity : class, IDiscount
        {
            if (!Contract.CheckValidKey(code))
            {
                return query;
            }
            var search = code!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Codes!.Any(y => y.Active && y.Code == search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter discounts by product IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">     The query to act on.</param>
        /// <param name="productIDs">The product IDs.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterDiscountsByProductIDs<TEntity>(
                this IQueryable<TEntity> query,
                IEnumerable<int> productIDs)
            where TEntity : class, IDiscount
        {
            if (!Contract.CheckAllValid(productIDs))
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Products!.Any(y => productIDs.Any(z => z == y.SlaveID)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter discounts by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="userID">Identifier for the user.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterDiscountsByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? userID)
            where TEntity : class, IDiscount
        {
            if (Contract.CheckInvalidID(userID))
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!.Any(y => y.SlaveID == userID!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter discounts by ship carrier method IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">               The query to act on.</param>
        /// <param name="shipCarrierMethodIDs">The ship carrier method IDs.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterDiscountsByShipCarrierMethodIDs<TEntity>(
                this IQueryable<TEntity> query,
                IEnumerable<int> shipCarrierMethodIDs)
            where TEntity : class, IDiscount
        {
            if (!Contract.CheckAllValid(shipCarrierMethodIDs))
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShipCarrierMethods!.Count == 0
                         || x.ShipCarrierMethods.Any(y => shipCarrierMethodIDs.Any(z => z == y.SlaveID)));
        }
    }
}
