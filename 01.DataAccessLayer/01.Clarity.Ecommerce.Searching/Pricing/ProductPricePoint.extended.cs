// <copyright file="ProductPricePoint.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price point search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A product price point search extensions.</summary>
    public static class ProductPricePointSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter product price points by unit of measure.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">        The query to act on.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByUnitOfMeasure<TEntity>(
                this IQueryable<TEntity> query,
                string? unitOfMeasure)
            where TEntity : class, IProductPricePoint
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

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by minimum quantity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="minQuantity">The minimum quantity.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByMinQuantity<TEntity>(
                this IQueryable<TEntity> query,
                decimal? minQuantity)
            where TEntity : class, IProductPricePoint
        {
            if (!minQuantity.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MinQuantity >= minQuantity.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by maximum quantity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="maxQuantity">The maximum quantity.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByMaxQuantity<TEntity>(
                this IQueryable<TEntity> query,
                decimal? maxQuantity)
            where TEntity : class, IProductPricePoint
        {
            if (!maxQuantity.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MaxQuantity <= maxQuantity.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by from.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="from"> Source for the.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByFrom<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? from)
            where TEntity : class, IProductPricePoint
        {
            if (!from.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.From >= from.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by to.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="to">   To (the upper limit).</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByTo<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? to)
            where TEntity : class, IProductPricePoint
        {
            if (!to.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.To <= to.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="accountID">Identifier for the account.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? accountID)
            where TEntity : class, IProductPricePoint
        {
            if (!accountID.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.StoreAccounts!
                        .Any(a => a.Active
                            && a.SlaveID == accountID));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by account key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByAccountKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.StoreAccounts!
                            .Any(a => a.Active
                                && a.Slave != null
                                && a.Slave.CustomKey!.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by account name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByAccountName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.StoreAccounts!
                            .Any(a => a.Active
                                && a.Slave != null
                                && a.Slave.Name!.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByProductID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IProductPricePoint
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.CustomKey != null
                    && x.Master.CustomKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by product name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByProductName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.Name != null
                    && x.Master.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductPricePoint
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.StoreID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by store key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByStoreKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null
                    && x.Store.CustomKey != null
                    && x.Store.CustomKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by store name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByStoreName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null
                    && x.Store.Name != null
                    && x.Store.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by price point
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByPricePointID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductPricePoint
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by price point IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByPricePointIDs<TEntity>(
                this IQueryable<TEntity> query,
                List<int?>? ids)
            where TEntity : class, IProductPricePoint
        {
            if (ids == null || ids.Count == 0)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(ids.Aggregate(
                    PredicateBuilder.New<TEntity>(false),
                    (c, id) => c.Or(x => x.SlaveID == id)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by price point key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByPricePointKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.CustomKey != null
                    && x.Slave.CustomKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by price point name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByPricePointName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.Name != null
                    && x.Slave.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by currency identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByCurrencyID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductPricePoint
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CurrencyID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by currency key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByCurrencyKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim(); // .ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Currency != null
                    && x.Currency.CustomKey != null
                    && x.Currency.CustomKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter product price points by currency name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductPricePointsByCurrencyName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IProductPricePoint
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Currency != null
                    && x.Currency.Name != null
                    && x.Currency.Name.Contains(search));
        }
    }
}
