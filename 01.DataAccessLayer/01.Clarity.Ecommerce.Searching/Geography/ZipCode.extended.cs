// <copyright file="ZipCode.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zip code search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A zip code search extensions.</summary>
    public static class ZipCodeSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter zip codes by zip code.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="strict"> True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterZipCodesByZipCode<TEntity>(
                this IQueryable<TEntity> query,
                string? zipCode,
                bool strict = false)
            where TEntity : class, IZipCode
        {
            if (!Contract.CheckValidKey(zipCode))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ZipCodeValue != null && x.ZipCodeValue == zipCode);
            }
            var search = zipCode!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ZipCodeValue != null && x.ZipCodeValue.Contains(search));
        }
    }
}
