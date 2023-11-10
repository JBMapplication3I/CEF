// <copyright file="SalesOrder.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Ecommerce.DataModel;
    using Utilities;

    /// <summary>The sales order search extensions.</summary>
    public static class SalesOrderSearchExtensions
    {
        /// <summary>An IQueryable{SalesOrder} extension method that filter sales order by search model.</summary>
        /// <param name="query"> The query to act on.</param>
        /// <param name="search">The search.</param>
        /// <returns>An IQueryable{SalesOrder}.</returns>
        public static IQueryable<SalesOrder> FilterSalesOrderBySearchModel(
            this IQueryable<SalesOrder> query,
            ISalesOrderSearchModel search)
        {
            if (search == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterSalesCollectionsBySearchModel<SalesOrder,
                    SalesOrderStatus,
                    SalesOrderType,
                    SalesOrderItem,
                    AppliedSalesOrderDiscount,
                    SalesOrderState,
                    SalesOrderFile,
                    SalesOrderContact,
                    AppliedSalesOrderItemDiscount,
                    SalesOrderItemTarget,
                    SalesOrderEvent,
                    // ReSharper disable once StyleCop.SA1110
                    SalesOrderEventType>(search)
                .FilterSalesOrdersByHasSalesGroupAsMaster(search.HasSalesGroupAsMaster)
                .FilterSalesOrdersByHasSalesGroupAsSub(search.HasSalesGroupAsSub)
                .FilterSalesOrdersByTrackingNumber(search.TrackingNumber);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales orders by has sales group as master.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesOrdersByHasSalesGroupAsMaster<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, ISalesOrder
        {
            if (has == null)
            {
                return query;
            }
            return has.Value
                ? Contract.RequiresNotNull(query).Where(x => x.SalesGroupAsMasterID.HasValue)
                : Contract.RequiresNotNull(query).Where(x => !x.SalesGroupAsMasterID.HasValue);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales orders by has sales group as sub.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesOrdersByHasSalesGroupAsSub<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, ISalesOrder
        {
            if (has == null)
            {
                return query;
            }
            return has.Value
                ? Contract.RequiresNotNull(query).Where(x => x.SalesGroupAsSubID.HasValue)
                : Contract.RequiresNotNull(query).Where(x => !x.SalesGroupAsSubID.HasValue);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales orders by tracking number.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">                 The query to act on.</param>
        /// <param name="shippingTrackingNumber">The shipping tracking number.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesOrdersByTrackingNumber<TEntity>(
                this IQueryable<TEntity> query,
                string? shippingTrackingNumber)
            where TEntity : class, ISalesOrder
        {
            if (!Contract.CheckValidKey(shippingTrackingNumber))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AssociatedPurchaseOrders!
                    .Any(y => y.Slave != null
                           && y.Slave.TrackingNumber == shippingTrackingNumber));
        }
    }
}
