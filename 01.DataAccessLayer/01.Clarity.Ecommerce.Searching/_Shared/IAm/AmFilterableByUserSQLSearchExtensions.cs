// <copyright file="AmFilterableByUserSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by user SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by user SQL search extensions.</summary>
    public static class AmFilterableByUserSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by user search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByUserSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByUserSearchModel model)
            where TEntity : class, IAmFilterableByUser
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByUserID(model.UserID)
                .FilterByUserKey(model.UserKey)
                .FilterByUserName(model.UserUsername);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByUser
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null && x.User.Active && x.UserID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by user key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByUserKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByUser
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null && x.User.Active && x.User.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by user name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByUserName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByUser
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null && x.User.Active && x.User.UserName == name);
        }
    }
}
