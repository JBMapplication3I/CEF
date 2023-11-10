// <copyright file="AmFilterableByFranchiseSlaveTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by franchise T-SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by franchise slave tsql search extensions.</summary>
    public static class AmFilterableByFranchiseSlaveTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by slave franchises by search model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TFranchiseLink">Type of the franchise link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableBySlaveFranchisesBySearchModel<TEntity, TFranchiseLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByFranchiseSearchModel model)
            where TEntity : class, IAmFilterableByFranchise<TFranchiseLink>
            where TFranchiseLink : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByFranchiseID<TEntity, TFranchiseLink>(model.FranchiseID)
                .FilterByFranchiseKey<TEntity, TFranchiseLink>(model.FranchiseKey)
                .FilterByFranchiseName<TEntity, TFranchiseLink>(model.FranchiseName)
                .FilterByAnyFranchiseWithCategoryID<TEntity, TFranchiseLink>(model.FranchiseCategoryID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by franchise identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TFranchiseLink">Type of the franchise link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByFranchiseID<TEntity, TFranchiseLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByFranchise<TFranchiseLink>
            where TFranchiseLink : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchises!
                    .Any(y => y.Active && y.Slave!.Active && y.SlaveID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by franchise key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TFranchiseLink">Type of the franchise link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByFranchiseKey<TEntity, TFranchiseLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByFranchise<TFranchiseLink>
            where TFranchiseLink : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchises!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by franchise name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TFranchiseLink">Type of the franchise link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByFranchiseName<TEntity, TFranchiseLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByFranchise<TFranchiseLink>
            where TFranchiseLink : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchises!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.Name == name));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any franchise with category identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TFranchiseLink">Type of the franchise link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAnyFranchiseWithCategoryID<TEntity, TFranchiseLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByFranchise<TFranchiseLink>
            where TFranchiseLink : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchises!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.Categories!
                        .Any(z => z.Active && z.Slave!.Active && z.SlaveID == id)));
        }
    }
}
