// <copyright file="HaveSeoSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have seo SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A have seo SQL search extensions.</summary>
    public static class HaveSeoSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i have seo base by search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterIHaveSeoBaseBySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IHaveSeoBaseSearchModel model)
            where TEntity : class, IHaveSeoBase
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterBySeoUrl(model.SeoUrl, model.SeoUrlStrict, model.SeoUrlIncludeNull)
                .FilterBySeoKeywords(model.SeoKeywords, model.SeoKeywordsStrict, model.SeoKeywordsIncludeNull)
                .FilterBySeoMetaData(model.SeoMetaData, model.SeoMetaDataStrict, model.SeoMetaDataIncludeNull)
                .FilterBySeoDescription(model.SeoDescription, model.SeoDescriptionStrict, model.SeoDescriptionIncludeNull)
                .FilterBySeoPageTitle(model.SeoPageTitle, model.SeoPageTitleStrict, model.SeoPageTitleIncludeNull);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by the SEO URL.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  URL of the SEO.</param>
        /// <param name="strict">     true to strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBySeoUrl<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveSeoBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter;
            while (search?.StartsWith("/") == true)
            {
                search = search.Trim('/');
            }
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(c => c.SeoUrl == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(parameter))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SeoUrl == null || x.SeoUrl == string.Empty);
            }
            search = search?.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(c => c.SeoUrl != null && c.SeoUrl.Contains(search!));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by the SEO Keywords.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The paramater to filter by.</param>
        /// <param name="strict">     true to strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBySeoKeywords<TEntity>(
            this IQueryable<TEntity> query,
            string? parameter,
            bool? strict,
            bool? includeNull)
            where TEntity : class, IHaveSeoBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter;
            while (search?.StartsWith("/") == true)
            {
                search = search.Trim('/');
            }
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(c => c.SeoUrl == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(parameter))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SeoUrl == null || x.SeoUrl == string.Empty);
            }
            search = search?.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(c => c.SeoUrl != null && c.SeoUrl.Contains(search!));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by the SEO Meta Data.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The paramater to filter by.</param>
        /// <param name="strict">     true to strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBySeoMetaData<TEntity>(
            this IQueryable<TEntity> query,
            string? parameter,
            bool? strict,
            bool? includeNull)
            where TEntity : class, IHaveSeoBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter;
            while (search?.StartsWith("/") == true)
            {
                search = search.Trim('/');
            }
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(c => c.SeoUrl == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(parameter))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SeoUrl == null || x.SeoUrl == string.Empty);
            }
            search = search?.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(c => c.SeoUrl != null && c.SeoUrl.Contains(search!));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by the SEO Description.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The paramater to filter by.</param>
        /// <param name="strict">     true to strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBySeoDescription<TEntity>(
            this IQueryable<TEntity> query,
            string? parameter,
            bool? strict,
            bool? includeNull)
            where TEntity : class, IHaveSeoBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter;
            while (search?.StartsWith("/") == true)
            {
                search = search.Trim('/');
            }
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(c => c.SeoUrl == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(parameter))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SeoUrl == null || x.SeoUrl == string.Empty);
            }
            search = search?.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(c => c.SeoUrl != null && c.SeoUrl.Contains(search!));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by the SEO Page Title.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The paramater to filter by.</param>
        /// <param name="strict">     true to strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBySeoPageTitle<TEntity>(
            this IQueryable<TEntity> query,
            string? parameter,
            bool? strict,
            bool? includeNull)
            where TEntity : class, IHaveSeoBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter;
            while (search?.StartsWith("/") == true)
            {
                search = search.Trim('/');
            }
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(c => c.SeoUrl == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(parameter))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SeoUrl == null || x.SeoUrl == string.Empty);
            }
            search = search?.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(c => c.SeoUrl != null && c.SeoUrl.Contains(search!));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filters by the parent's SEO URL.</summary>
        /// <exception cref="System.ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="seoUrl">URL of the SEO.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByParentSeoUrl<TEntity>(
                this IQueryable<TEntity> query,
                string? seoUrl)
            where TEntity : class, IHaveAParentBase<TEntity>, IHaveSeoBase
        {
            if (!Contract.CheckValidKey(seoUrl))
            {
                return query;
            }
            while (seoUrl?.StartsWith("/") == true)
            {
                seoUrl = seoUrl.Trim('/');
            }
            var search = seoUrl?.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(c => c.Parent != null && c.Parent.SeoUrl!.Contains(search!));
        }

        /// <summary>An IQueryable{Entity} extension method that filters products by category SEO url.</summary>
        /// <exception cref="System.ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="seoUrl">URL of the SEO.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByCategorySeoUrl<TEntity>(
                this IQueryable<TEntity> query,
                string? seoUrl)
            where TEntity : class, IProduct
        {
            if (!Contract.CheckValidKey(seoUrl))
            {
                return query;
            }
            while (seoUrl?.StartsWith("/") == true)
            {
                seoUrl = seoUrl.Trim('/');
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.Categories!
                    .Any(pc => pc.Slave!.SeoUrl == seoUrl));
        }
    }
}
