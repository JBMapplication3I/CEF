// <copyright file="InventoryLocation.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Inventory Location Search Extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A search extensions.</summary>
    public static class InventoryLocationSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter IL by region code.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="code">  The code.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterILByRegionCode<TEntity>(
                this IQueryable<TEntity> query,
                string? code)
            where TEntity : class, IInventoryLocation
        {
            if (!Contract.CheckValidKey(code))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                         && x.Contact.Address != null
                         && x.Contact.Address.Region != null
                         && x.Contact.Address.Region.Code != null
                         && x.Contact.Address.Region.Code == code);
        }
    }
}
