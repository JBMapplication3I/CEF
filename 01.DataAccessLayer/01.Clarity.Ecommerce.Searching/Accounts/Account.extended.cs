// <copyright file="Account.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An account search extensions.</summary>
    public static partial class AccountSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter accounts by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterAccountsByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAccount
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!.Any(y => y.Active && y.ID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter accounts by user name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">   The query to act on.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterAccountsByUserUserName<TEntity>(
                this IQueryable<TEntity> query,
                string? userName)
            where TEntity : class, IAccount
        {
            if (!Contract.CheckValidKey(userName))
            {
                return query;
            }
            var search = userName!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!
                    .Any(y => y.Active
                           && (y.UserName != null && y.UserName == search
                               || y.CustomKey != null && y.CustomKey == search
                               || y.Email != null && y.Email == search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter accounts by user name or email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">          The query to act on.</param>
        /// <param name="userNameOrEmail">The user name or email.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterAccountsByUserUserNameOrEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? userNameOrEmail)
            where TEntity : class, IAccount
        {
            if (!Contract.CheckValidKey(userNameOrEmail))
            {
                return query;
            }
            var search = userNameOrEmail!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!
                    .Any(y => (y.UserName != null || y.Email != null)
                           && (y.UserName == search || y.Email == search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter accounts by accessible from account
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterAccountsByAccessibleFromAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAccount
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountsAssociatedWith!.Any(y => y.Active && y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter accounts by region identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterAccountsByRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAccount
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountContacts!
                    .Any(y => y.Slave != null
                        && y.Slave.Address != null
                        && y.Slave.Address.RegionID != null
                        && y.Slave.Address.RegionID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter accounts by city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterAccountsByCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IAccount
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            city = city!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountContacts!
                    .Any(y => y.Slave != null
                           && y.Slave.Address != null
                           && y.Slave.Address.City != null
                           && y.Slave.Address.City == city));
        }
    }
}
