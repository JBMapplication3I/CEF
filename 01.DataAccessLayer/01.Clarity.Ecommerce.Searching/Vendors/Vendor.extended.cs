// <copyright file="Vendor.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A vendor search extensions.</summary>
    public static class VendorSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter vendors by notes.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="notes">The notes.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterVendorsByNotes<TEntity>(
                this IQueryable<TEntity> query,
                string? notes)
            where TEntity : class, IVendor
        {
            if (!Contract.CheckValidKey(notes))
            {
                return query;
            }
            var search = notes!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Notes1 != null && x.Notes1.Contains(search));
        }
    }
}
