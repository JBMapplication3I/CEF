// <copyright file="AmFilterableByNullableFranchiseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by nullable franchise SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by nullable franchise SQL search extensions.</summary>
    public static class AmFilterableByNullableFranchiseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by nullable franchise search
        /// model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByNullableFranchiseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByFranchiseSearchModel model)
            where TEntity : class, IAmFilterableByNullableFranchise
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByNullableFranchiseID(model.FranchiseID)
                .FilterByNullableFranchiseKey(model.FranchiseKey)
                .FilterByNullableFranchiseName(model.FranchiseName)
                .FilterByFranchiseWithCategoryID(model.FranchiseCategoryID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByNullableFranchiseID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByNullableFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchise != null && x.Franchise.Active && x.FranchiseID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable franchise key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableFranchiseKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByNullableFranchise
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Franchise != null && x.Franchise.Active && x.Franchise.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable franchise name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableFranchiseName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByNullableFranchise
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
            where TEntity : class, IAmFilterableByNullableFranchise
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
