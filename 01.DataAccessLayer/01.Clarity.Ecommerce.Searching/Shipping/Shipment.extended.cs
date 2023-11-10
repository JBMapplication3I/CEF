// <copyright file="Shipment.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A shipment search extensions.</summary>
    public static class ShipmentSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by tracking number.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="number">Number of.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByTrackingNumber<TEntity>(this IQueryable<TEntity> query, string? number)
            where TEntity : class, IShipment
        {
            if (!Contract.CheckValidKey(number))
            {
                return query;
            }
            var search = number!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.TrackingNumber == search);
        }
    }
}
