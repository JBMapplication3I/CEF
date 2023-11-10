// <copyright file="AmFilterableByFranchiseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by franchise SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by franchise SQL search extensions.</summary>
    public static class AmFilterableByFranchiseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by franchise search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByFranchiseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByFranchiseSearchModel model)
            where TEntity : class, IAmFilterableByFranchise
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByFranchiseID(model.FranchiseID)
                .FilterByFranchiseKey(model.FranchiseKey)
                .FilterByFranchiseName(model.FranchiseName)
                .FilterByFranchiseWithCategoryID(model.FranchiseCategoryID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByFranchiseID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchise != null && x.Franchise.Active && x.FranchiseID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by franchise key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByFranchiseKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByFranchise
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchise != null && x.Franchise.Active && x.Franchise.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by franchise name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByFranchiseName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByFranchise
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchise != null && x.Franchise.Active && x.Franchise.Name == name);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by franchise with category identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByFranchiseWithCategoryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchise != null && x.Franchise.Active && x.Franchise.Categories!
                    .Any(z => z.Active && z.Slave!.Active && z.SlaveID == id));
        }
    }
}
