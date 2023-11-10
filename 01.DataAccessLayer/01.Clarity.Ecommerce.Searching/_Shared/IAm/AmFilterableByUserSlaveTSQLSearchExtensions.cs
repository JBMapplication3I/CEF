// <copyright file="AmFilterableByUserSlaveTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by user T-SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by user slave tsql search extensions.</summary>
    public static class AmFilterableByUserSlaveTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by slave users by search model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TUserLink">Type of the user link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableBySlaveUsersBySearchModel<TEntity, TUserLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByUserSearchModel model)
            where TEntity : class, IAmFilterableByUser<TUserLink>
            where TUserLink : class, IAmAUserRelationshipTableWhereUserIsTheSlave<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByUserID<TEntity, TUserLink>(model.UserID)
                .FilterByUserKey<TEntity, TUserLink>(model.UserKey)
                .FilterByUserName<TEntity, TUserLink>(model.UserUsername);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by user identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TUserLink">Type of the user link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByUserID<TEntity, TUserLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByUser<TUserLink>
            where TUserLink : class, IAmAUserRelationshipTableWhereUserIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!
                    .Any(y => y.Active && y.Slave!.Active && y.SlaveID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by user key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TUserLink">Type of the user link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByUserKey<TEntity, TUserLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByUser<TUserLink>
            where TUserLink : class, IAmAUserRelationshipTableWhereUserIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by user name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TUserLink">Type of the user link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByUserName<TEntity, TUserLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByUser<TUserLink>
            where TUserLink : class, IAmAUserRelationshipTableWhereUserIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.UserName == name));
        }
    }
}
