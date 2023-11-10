// <copyright file="Globalization.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the globalization search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A globalization search extensions.</summary>
    public static class GlobalizationSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter languages by locale.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="locale">The locale.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterLanguagesByLocale<TEntity>(
                this IQueryable<TEntity> query,
                string? locale)
            where TEntity : class, ILanguage
        {
            if (!Contract.CheckValidKey(locale))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.Locale == locale);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user interface keys by type.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="type"> The type.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUiKeysByType<TEntity>(
                this IQueryable<TEntity> query,
                string? type)
            where TEntity : class, IUiKey
        {
            if (!Contract.CheckValidKey(type))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.Type == type);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user interface translations by locale.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="locale">The locale.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUiTranslationsByLocale<TEntity>(
                this IQueryable<TEntity> query,
                string? locale)
            where TEntity : class, IUiTranslation
        {
            if (!Contract.CheckValidKey(locale))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.Locale == locale);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user interface translations by key starts
        /// with.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">     The query to act on.</param>
        /// <param name="startsWith">The starts with.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUiTranslationsByKeyStartsWith<TEntity>(
                this IQueryable<TEntity> query,
                string? startsWith)
            where TEntity : class, IUiTranslation
        {
            if (!Contract.CheckValidKey(startsWith))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.UiKey!.CustomKey!.StartsWith(startsWith!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user interface translations by key ends with.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">   The query to act on.</param>
        /// <param name="endsWith">The ends with.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUiTranslationsByKeyEndsWith<TEntity>(
                this IQueryable<TEntity> query,
                string? endsWith)
            where TEntity : class, IUiTranslation
        {
            if (!Contract.CheckValidKey(endsWith))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.UiKey!.CustomKey!.EndsWith(endsWith!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user interface translations by key contains.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">   The query to act on.</param>
        /// <param name="contains">The contains.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUiTranslationsByKeyContains<TEntity>(
                this IQueryable<TEntity> query,
                string? contains)
            where TEntity : class, IUiTranslation
        {
            if (!Contract.CheckValidKey(contains))
            {
                return query;
            }
            var search = contains!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.UiKey!.CustomKey!.ToLower().Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter user interface translations by value.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUiTranslationsByValue<TEntity>(
                this IQueryable<TEntity> query,
                string? value)
            where TEntity : class, IUiTranslation
        {
            if (!Contract.CheckValidKey(value))
            {
                return query;
            }
            var search = value!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Value!.Contains(search));
        }
    }
}
