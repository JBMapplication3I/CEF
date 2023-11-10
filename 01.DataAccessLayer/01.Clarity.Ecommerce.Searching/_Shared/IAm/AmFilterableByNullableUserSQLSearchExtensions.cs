// <copyright file="AmFilterableByNullableUserSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by nullable user SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by nullable user SQL search extensions.</summary>
    public static class AmFilterableByNullableUserSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by nullable user search
        /// model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByNullableUserSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByUserSearchModel model)
            where TEntity : class, IAmFilterableByNullableUser
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByNullableUserID(model.UserID)
                .FilterByNullableUserKey(model.UserKey)
                .FilterByNullableUserName(model.UserUsername);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByNullableUser
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null && x.User.Active && x.UserID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable user key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableUserKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByNullableUser
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null && x.User.Active && x.User.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable user username.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableUserName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByNullableUser
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
