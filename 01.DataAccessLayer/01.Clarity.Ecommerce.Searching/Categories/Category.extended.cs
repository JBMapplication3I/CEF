// <copyright file="Category.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using Ecommerce.DataModel;
    using Utilities;

    /// <summary>A category search extensions.</summary>
    public static class CategorySearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter categories by child categories JSON attributes
        /// by values.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">         The query to act on.</param>
        /// <param name="jsonAttributes">The JSON attributes.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCategoriesByChildCategoriesJsonAttributesByValues<TEntity>(
            this IQueryable<TEntity> query,
            Dictionary<string, string?[]?>? jsonAttributes)
            where TEntity : class, ICategory
        {
            if (jsonAttributes == null || jsonAttributes.Count == 0)
            {
                return query;
            }
            var predicate = jsonAttributes!.BuildJsonAttributePredicateForValues<Category>();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Children!.AsQueryable().Any(predicate)
                    || x.Children!.AsQueryable().SelectMany(y => y.Children!).Any(predicate)
                    || x.Children!.AsQueryable().SelectMany(y => y.Children!).SelectMany(y => y.Children!).Any(predicate));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter categories by has products under another
        /// category.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">          The query to act on.</param>
        /// <param name="id">             The identifier.</param>
        /// <param name="key">            The key.</param>
        /// <param name="name">           The name.</param>
        /// <param name="includeChildren">True to include, false to exclude the children.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCategoriesByHasProductsUnderAnotherCategory<TEntity>(
                this IQueryable<TEntity> query,
                int? id,
                string? key,
                string? name,
                bool includeChildren)
            where TEntity : class, ICategory
        {
            Contract.RequiresNotNull(query);
            var hasID = Contract.CheckValidID(id);
            var hasKey = Contract.CheckValidKey(key);
            var hasName = Contract.CheckValidKey(name);
            if (!hasID && !hasKey && !hasName)
            {
                return query;
            }
            if (hasID)
            {
                if (includeChildren)
                {
                    return query.Where(category => category.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.SlaveID == id!.Value))
                        || category.Children!.Any(childCategory => childCategory.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.SlaveID == id!.Value))
                        || category.Children!.Any(childChildCategory => childChildCategory.Children!.Any(childCategoryChildCategory => childCategoryChildCategory.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.SlaveID == id!.Value))))));
                }
                return query.Where(category => category.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.SlaveID == id!.Value)));
            }
            if (hasKey)
            {
                if (includeChildren)
                {
                    return query.Where(category => category.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.CustomKey == key))
                        || category.Children!.Any(childCategory => childCategory.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.CustomKey == key))
                        || category.Children!.Any(childChildCategory => childChildCategory.Children!.Any(childCategoryChildCategory => childCategoryChildCategory.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.CustomKey == key))))));
                }
                return query.Where(category => category.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.CustomKey == key)));
            }
            // hasName
            if (includeChildren)
            {
                return query.Where(category => category.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.Name == name))
                    || category.Children!.Any(childCategory => childCategory.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.Name == name))
                    || category.Children!.Any(childChildCategory => childChildCategory.Children!.Any(childCategoryChildCategory => childCategoryChildCategory.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.Name == name))))));
            }
            return query.Where(category => category.Products!.Any(cpc => cpc.Active && cpc.Master!.Active && cpc.Master.Categories!.Any(cpcppc => cpcppc.Active && cpcppc.Slave!.Active && cpcppc.Slave.Name == name)));
            // ReSharper restore IdentifierTypo
        }

        /// <summary>An IQueryable{TEntity} extension method that order categories by default.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> OrderCategoriesByDefault<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, ICategory
        {
            return Contract.RequiresNotNull(query)
                .OrderBy(c => c.SortOrder)
                .ThenBy(c => c.Name);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter categories by requires roles.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCategoriesByRequiresRoles<TEntity>(
                this IQueryable<TEntity> query,
                List<string?>? roles)
            where TEntity : class, ICategory
        {
            if (roles == null)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.RequiresRoles == string.Empty || x.RequiresRoles == null);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.RequiresRoles == string.Empty
                    || x.RequiresRoles == null
                    || roles!.Any(y => x.RequiresRoles.Contains(y!)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter categories by requires roles alternate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterCategoriesByRequiresRolesAlt<TEntity>(
                this IQueryable<TEntity> query,
                List<string?>? roles)
            where TEntity : class, ICategory
        {
            if (roles == null)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.RequiresRolesAlt == string.Empty || x.RequiresRolesAlt == null);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.RequiresRolesAlt == string.Empty
                    || x.RequiresRolesAlt == null
                    || roles!.Any(y => x.RequiresRolesAlt.Contains(y!)));
        }
    }
}
