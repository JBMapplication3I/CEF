// <copyright file="DiscountCodes.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount codes search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A base search extensions.</summary>
    public static class DiscountCodesSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filters by a code value.</summary>
        /// <typeparam name="TEntity">The type of the discount code entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="code"> The code.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterDiscountCodesByCode<TEntity>(
                this IQueryable<TEntity> query,
                string? code)
            where TEntity : class, IDiscountCode
        {
            if (!Contract.CheckValidKey(code))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Code == code);
        }
    }
}
