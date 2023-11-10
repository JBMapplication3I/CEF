// <copyright file="RateQuote.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the rate quote search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A rate quote search extensions.</summary>
    public static class RateQuoteSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter rate quotes by search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="search">The search.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRateQuotesBySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IRateQuoteSearchModel search)
            where TEntity : class, IRateQuote
        {
            return Contract.RequiresNotNull(query)
                .FilterRateQuotesByShipCarrierID(search.ShipCarrierID)
                .FilterRateQuotesByShipCarrierKey(search.ShipCarrierKey)
                .FilterRateQuotesByShipCarrierName(search.ShipCarrierName)
                .FilterRateQuotesByShipCarrierMethodID(search.ShipCarrierMethodID)
                .FilterRateQuotesByShipCarrierMethodKey(search.ShipCarrierMethodKey)
                .FilterRateQuotesByShipCarrierMethodName(search.ShipCarrierMethodName)
                .FilterRateQuotesByCartID(search.CartID)
                .FilterRateQuotesBySalesOrderID(search.SalesOrderID)
                .FilterRateQuotesBySalesQuoteID(search.SalesQuoteID)
                .FilterRateQuotesBySalesInvoiceID(search.SalesInvoiceID)
                .FilterRateQuotesBySalesReturnID(search.SalesReturnID)
                .FilterRateQuotesByPurchaseOrderID(search.PurchaseOrderID)
                .FilterRateQuotesBySampleRequestID(search.SampleRequestID)
                .FilterRateQuotesBySelected(search.Selected);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter rate quotes by cart identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRateQuotesByCartID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CartID == id!.Value);
        }

        private static IQueryable<TEntity> FilterRateQuotesByShipCarrierMethodKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, IRateQuote
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ShipCarrierMethod != null
                        && x.ShipCarrierMethod.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShipCarrierMethod != null
                    && x.ShipCarrierMethod.CustomKey != null
                    && x.ShipCarrierMethod.CustomKey.Contains(search));
        }

        private static IQueryable<TEntity> FilterRateQuotesByShipCarrierMethodName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IRateQuote
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ShipCarrierMethod != null
                        && x.ShipCarrierMethod.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShipCarrierMethod != null
                    && x.ShipCarrierMethod.Name != null
                    && x.ShipCarrierMethod.Name.Contains(search));
        }

        private static IQueryable<TEntity> FilterRateQuotesByShipCarrierID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShipCarrierMethod != null
                    && x.ShipCarrierMethod.ShipCarrierID == id!.Value);
        }

        private static IQueryable<TEntity> FilterRateQuotesByShipCarrierKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, IRateQuote
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ShipCarrierMethod != null
                        && x.ShipCarrierMethod.ShipCarrier != null
                        && x.ShipCarrierMethod.ShipCarrier.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShipCarrierMethod != null
                    && x.ShipCarrierMethod.ShipCarrier != null
                    && x.ShipCarrierMethod.ShipCarrier.CustomKey != null
                    && x.ShipCarrierMethod.CustomKey!.Contains(search));
        }

        private static IQueryable<TEntity> FilterRateQuotesByShipCarrierName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IRateQuote
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ShipCarrierMethod != null
                        && x.ShipCarrierMethod.ShipCarrier != null
                        && x.ShipCarrierMethod.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShipCarrierMethod != null
                    && x.ShipCarrierMethod.ShipCarrier != null
                    && x.ShipCarrierMethod.ShipCarrier.Name != null
                    && x.ShipCarrierMethod.ShipCarrier.Name.Contains(search));
        }

        private static IQueryable<TEntity> FilterRateQuotesBySalesOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesOrderID == id!.Value);
        }

        private static IQueryable<TEntity> FilterRateQuotesBySalesInvoiceID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesInvoiceID == id!.Value);
        }

        private static IQueryable<TEntity> FilterRateQuotesBySalesQuoteID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesQuoteID == id!.Value);
        }

        private static IQueryable<TEntity> FilterRateQuotesBySalesReturnID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesReturnID == id!.Value);
        }

        private static IQueryable<TEntity> FilterRateQuotesByPurchaseOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.PurchaseOrderID == id!.Value);
        }

        private static IQueryable<TEntity> FilterRateQuotesBySampleRequestID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRateQuote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SampleRequestID == id!.Value);
        }
    }
}
