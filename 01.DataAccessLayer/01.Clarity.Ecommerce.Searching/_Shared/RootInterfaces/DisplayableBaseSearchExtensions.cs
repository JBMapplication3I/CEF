// <copyright file="DisplayableBaseSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Models;
    using Utilities;

    /// <summary>A displayable base search extensions.</summary>
    public static class DisplayableBaseSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by displayable base search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterByDisplayableBaseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IDisplayableBaseSearchModel model)
            where TEntity : class, IDisplayableBase
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterDisplayablesByDisplayName(model.DisplayName, model.DisplayNameStrict, model.DisplayNameIncludeNull)
                .FilterDisplayablesByTranslationKey(model.TranslationKey, model.TranslationKeyStrict, model.TranslationKeyIncludeNull);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter display-ables by display name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter value to filter by.</param>
        /// <param name="strict">     True to strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterDisplayablesByDisplayName<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IDisplayableBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.DisplayName != null
                             && x.DisplayName == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.DisplayName == null || x.DisplayName == string.Empty);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.DisplayName != null
                         && x.DisplayName.Contains(search!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter display-ables by translation key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter value to filter by.</param>
        /// <param name="strict">     True to strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterDisplayablesByTranslationKey<TEntity>(
            this IQueryable<TEntity> query,
            string? parameter,
            bool? strict,
            bool? includeNull)
            where TEntity : class, IDisplayableBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.TranslationKey != null
                        && x.TranslationKey == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.TranslationKey == null || x.TranslationKey == string.Empty);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.TranslationKey != null
                    && x.TranslationKey.Contains(search!));
        }
    }
}
