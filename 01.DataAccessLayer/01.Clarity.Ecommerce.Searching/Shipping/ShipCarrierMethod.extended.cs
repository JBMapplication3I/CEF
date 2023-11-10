// <copyright file="ShipCarrierMethod.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship carrier method search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A ship carrier method search extensions.</summary>
    public static class ShipCarrierMethodSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter ship carrier methods by ship carrier name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterShipCarrierMethodsByShipCarrierName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IShipCarrierMethod
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ShipCarrier != null && x.ShipCarrier.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShipCarrier != null && x.ShipCarrier.Name!.Contains(search));
        }
    }
}
