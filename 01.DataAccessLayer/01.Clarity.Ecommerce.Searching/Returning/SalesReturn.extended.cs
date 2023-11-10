// <copyright file="SalesReturn.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Ecommerce.DataModel;
    using Utilities;

    /// <summary>The sales return search extensions.</summary>
    public static class SalesReturnSearchExtensions
    {
        /// <summary>An IQueryable{SalesReturn} extension method that filter sales return by search model.</summary>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{SalesReturn}.</returns>
        public static IQueryable<SalesReturn> FilterSalesReturnBySearchModel(
            this IQueryable<SalesReturn> query,
            ISalesReturnSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            return query
                .FilterSalesCollectionsBySearchModel<SalesReturn,
                    SalesReturnStatus,
                    SalesReturnType,
                    SalesReturnItem,
                    AppliedSalesReturnDiscount,
                    SalesReturnState,
                    SalesReturnFile,
                    SalesReturnContact,
                    AppliedSalesReturnItemDiscount,
                    SalesReturnItemTarget,
                    SalesReturnEvent,
                    // ReSharper disable once StyleCop.SA1110
                    SalesReturnEventType>(model)
                .FilterSalesReturnsByTrackingNumber(model.TrackingNumber)
                .FilterSalesReturnsBySalesOrderID(model.SalesOrderID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales returns by tracking number.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">                 The query to act on.</param>
        /// <param name="shippingTrackingNumber">The shipping tracking number.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterSalesReturnsByTrackingNumber<TEntity>(
                this IQueryable<TEntity> query,
                string? shippingTrackingNumber)
            where TEntity : class, ISalesReturn
        {
            if (!Contract.CheckValidKey(shippingTrackingNumber))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.TrackingNumber == shippingTrackingNumber);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales returns by sales order identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterSalesReturnsBySalesOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesReturn
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AssociatedSalesOrders!.Any(y => y.Active && y.SlaveID == id));
        }
    }
}
