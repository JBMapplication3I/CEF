// <copyright file="InventoryLocationSection.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Inventory Location Section Search Extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A search extensions.</summary>
    public static class InventoryLocationSectionSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter ILS by IL identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterILSByInventoryLocationID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IInventoryLocationSection
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.InventoryLocationID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter ILS by IL name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterILSByInventoryLocationName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IInventoryLocationSection
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.InventoryLocation != null
                             && x.InventoryLocation.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.InventoryLocation != null
                    && x.InventoryLocation.Name!.Contains(search));
        }
    }
}
