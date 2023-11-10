// <copyright file="Country.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A country search extensions.</summary>
    public static class CountrySearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter countries by code.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="code">  The code.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCountriesByCode<TEntity>(
                this IQueryable<TEntity> query,
                string? code,
                bool strict = false)
            where TEntity : class, ICountry
        {
            if (!Contract.CheckValidKey(code))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Code != null && x.Code == code);
            }
            var search = code!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Code != null && x.Code.Contains(search));
        }
    }
}
