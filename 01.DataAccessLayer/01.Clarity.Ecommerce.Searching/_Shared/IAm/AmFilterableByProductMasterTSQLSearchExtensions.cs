// <copyright file="AmFilterableByProductMasterTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by product master{T} sql search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by product master tsql search extensions.</summary>
    public static class AmFilterableByProductMasterTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by master products by search
        /// model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableByMasterProductsBySearchModel<TEntity, TProductLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByProductSearchModel model)
            where TEntity : class, IAmFilterableByProduct<TProductLink>
            where TProductLink : class, IAmAProductRelationshipTableWhereProductIsTheMaster<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByProductID<TEntity, TProductLink>(model.ProductID)
                .FilterByProductKey<TEntity, TProductLink>(model.ProductKey)
                .FilterByProductName<TEntity, TProductLink>(model.ProductName)
                .FilterByProductSeoUrl<TEntity, TProductLink>(model.ProductSeoUrl);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by product identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByProductID<TEntity, TProductLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByProduct<TProductLink>
            where TProductLink : class, IAmAProductRelationshipTableWhereProductIsTheMaster<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Products!
                    .Any(y => y.Active && y.Master!.Active && y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by product key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByProductKey<TEntity, TProductLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByProduct<TProductLink>
            where TProductLink : class, IAmAProductRelationshipTableWhereProductIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Products!
                    .Any(y => y.Active && y.Master!.Active && y.Master.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by product name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByProductName<TEntity, TProductLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByProduct<TProductLink>
            where TProductLink : class, IAmAProductRelationshipTableWhereProductIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Products!
                    .Any(y => y.Active && y.Master!.Active && y.Master.Name == name));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by product seo URL.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="seoUrl">URL of the seo.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByProductSeoUrl<TEntity, TProductLink>(
                this IQueryable<TEntity> query,
                string? seoUrl)
            where TEntity : class, IAmFilterableByProduct<TProductLink>
            where TProductLink : class, IAmAProductRelationshipTableWhereProductIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(seoUrl))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Products!
                    .Any(y => y.Active && y.Master!.Active && y.Master.SeoUrl == seoUrl));
        }
    }
}
