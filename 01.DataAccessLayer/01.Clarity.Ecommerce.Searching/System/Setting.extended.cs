// <copyright file="Setting.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A setting search extensions.</summary>
    public static class SettingSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter settings by value.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSettingsByValue<TEntity>(
                this IQueryable<TEntity> query,
                string? value)
            where TEntity : class, ISetting
        {
            if (!Contract.CheckValidKey(value))
            {
                return query;
            }
            var search = value!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Value != null
                         && x.Value.Trim().ToLower().Contains(search));
        }
    }
}
