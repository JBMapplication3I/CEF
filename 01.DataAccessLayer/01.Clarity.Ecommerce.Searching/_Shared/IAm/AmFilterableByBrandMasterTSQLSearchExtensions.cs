// <copyright file="AmFilterableByBrandMasterTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by brand t-sql search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by brand master tsql search extensions.</summary>
    public static class AmFilterableByBrandMasterTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by master brands by search
        /// model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TBrandLink">Type of the brand link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableByMasterBrandsBySearchModel<TEntity, TBrandLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByBrandSearchModel model)
            where TEntity : class, IAmFilterableByBrand<TBrandLink>
            where TBrandLink : class, IAmABrandRelationshipTableWhereBrandIsTheMaster<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByBrandID<TEntity, TBrandLink>(model.BrandID)
                .FilterByBrandKey<TEntity, TBrandLink>(model.BrandKey)
                .FilterByBrandName<TEntity, TBrandLink>(model.BrandName)
                .FilterByAnyBrandWithCategoryID<TEntity, TBrandLink>(model.BrandCategoryID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any brand with category identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TBrandLink">Type of the brand link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByAnyBrandWithCategoryID<TEntity, TBrandLink>(
            this IQueryable<TEntity> query,
            int? id)
            where TEntity : class, IAmFilterableByBrand<TBrandLink>
            where TBrandLink : class, IAmABrandRelationshipTableWhereBrandIsTheMaster<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Brands!
                    .Any(y => y.Active && y.Master!.Active && y.Master.Categories!
                        .Any(z => z.Active && z.Slave!.Active && z.SlaveID == id)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by brand identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TBrandLink">Type of the brand link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByBrandID<TEntity, TBrandLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByBrand<TBrandLink>
            where TBrandLink : class, IAmABrandRelationshipTableWhereBrandIsTheMaster<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Brands!
                    .Any(y => y.Active && y.Master!.Active && y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by brand key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TBrandLink">Type of the brand link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByBrandKey<TEntity, TBrandLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByBrand<TBrandLink>
            where TBrandLink : class, IAmABrandRelationshipTableWhereBrandIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Brands!
                    .Any(y => y.Active && y.Master!.Active && y.Master.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by brand name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TBrandLink">Type of the brand link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByBrandName<TEntity, TBrandLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByBrand<TBrandLink>
            where TBrandLink : class, IAmABrandRelationshipTableWhereBrandIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Brands!
                    .Any(y => y.Active && y.Master!.Active && y.Master.Name == name));
        }
    }
}
