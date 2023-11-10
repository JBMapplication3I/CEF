// <copyright file="AmFilterableByManufacturerMasterTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by manufacturer t-sql search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by manufacturer master tsql search extensions.</summary>
    public static class AmFilterableByManufacturerMasterTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by master manufacturers by search
        /// model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TManufacturerLink">Type of the manufacturer link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableByMasterManufacturersBySearchModel<TEntity, TManufacturerLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByManufacturerSearchModel model)
            where TEntity : class, IAmFilterableByManufacturer<TManufacturerLink>
            where TManufacturerLink : class, IAmAManufacturerRelationshipTableWhereManufacturerIsTheMaster<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByManufacturerID<TEntity, TManufacturerLink>(model.ManufacturerID)
                .FilterByManufacturerKey<TEntity, TManufacturerLink>(model.ManufacturerKey)
                .FilterByManufacturerName<TEntity, TManufacturerLink>(model.ManufacturerName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by manufacturer identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TManufacturerLink">Type of the manufacturer link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByManufacturerID<TEntity, TManufacturerLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByManufacturer<TManufacturerLink>
            where TManufacturerLink : class, IAmAManufacturerRelationshipTableWhereManufacturerIsTheMaster<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Manufacturers!
                    .Any(y => y.Active && y.Master!.Active && y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by manufacturer key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TManufacturerLink">Type of the manufacturer link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByManufacturerKey<TEntity, TManufacturerLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByManufacturer<TManufacturerLink>
            where TManufacturerLink : class, IAmAManufacturerRelationshipTableWhereManufacturerIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Manufacturers!
                    .Any(y => y.Active && y.Master!.Active && y.Master.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by manufacturer name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TManufacturerLink">Type of the manufacturer link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByManufacturerName<TEntity, TManufacturerLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByManufacturer<TManufacturerLink>
            where TManufacturerLink : class, IAmAManufacturerRelationshipTableWhereManufacturerIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Manufacturers!
                    .Any(y => y.Active && y.Master!.Active && y.Master.Name == name));
        }
    }
}
