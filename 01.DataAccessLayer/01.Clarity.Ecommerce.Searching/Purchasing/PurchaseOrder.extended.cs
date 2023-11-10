// <copyright file="PurchaseOrder.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A purchase order extensions.</summary>
    public static class PurchaseOrderExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter purchase orders by shipping carrier
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPurchaseOrdersByShippingCarrierID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IPurchaseOrder
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(po => po.ShipCarrierID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter purchase orders by shipping carrier name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPurchaseOrdersByShippingCarrierName<TEntity>(this IQueryable<TEntity> query, string? name)
            where TEntity : class, IPurchaseOrder
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(po => po.ShipCarrier != null && po.ShipCarrier.Name == search);
        }
    }
}
