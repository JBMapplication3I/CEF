// <copyright file="AmFilterableByCategorySlaveTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by category T-SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by category slave tsql search extensions.</summary>
    public static class AmFilterableByCategorySlaveTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by slave categories by search model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TCategoryLink">Type of the category link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableBySlaveCategoriesBySearchModel<TEntity, TCategoryLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByCategorySearchModel model)
            where TEntity : class, IAmFilterableByCategory<TCategoryLink>
            where TCategoryLink : class, IAmACategoryRelationshipTableWhereCategoryIsTheSlave<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByCategoryID<TEntity, TCategoryLink>(model.CategoryID)
                .FilterByCategoryKey<TEntity, TCategoryLink>(model.CategoryKey)
                .FilterByCategoryName<TEntity, TCategoryLink>(model.CategoryName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by category identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TCategoryLink">Type of the category link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCategoryID<TEntity, TCategoryLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByCategory<TCategoryLink>
            where TCategoryLink : class, IAmACategoryRelationshipTableWhereCategoryIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Categories!
                    .Any(y => y.Active && y.Slave!.Active && y.SlaveID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by category key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TCategoryLink">Type of the category link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCategoryKey<TEntity, TCategoryLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByCategory<TCategoryLink>
            where TCategoryLink : class, IAmACategoryRelationshipTableWhereCategoryIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Categories!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by category name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TCategoryLink">Type of the category link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCategoryName<TEntity, TCategoryLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByCategory<TCategoryLink>
            where TCategoryLink : class, IAmACategoryRelationshipTableWhereCategoryIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Categories!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.Name == name));
        }
    }
}
