﻿// <copyright file="AmFilterableByAccountMasterTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by account t-sql search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by account master tsql search extensions.</summary>
    public static class AmFilterableByAccountMasterTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by master accounts by search
        /// model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TAccountLink">Type of the account link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableByMasterAccountsBySearchModel<TEntity, TAccountLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByAccountSearchModel model)
            where TEntity : class, IAmFilterableByAccount<TAccountLink>
            where TAccountLink : class, IAmAnAccountRelationshipTableWhereAccountIsTheMaster<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByAccountID<TEntity, TAccountLink>(model.AccountID)
                .FilterByAccountKey<TEntity, TAccountLink>(model.AccountKey)
                .FilterByAccountName<TEntity, TAccountLink>(model.AccountName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by account identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TAccountLink">Type of the account link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAccountID<TEntity, TAccountLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByAccount<TAccountLink>
            where TAccountLink : class, IAmAnAccountRelationshipTableWhereAccountIsTheMaster<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Accounts!
                    .Any(y => y.Active && y.Master!.Active && y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by account key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TAccountLink">Type of the account link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAccountKey<TEntity, TAccountLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByAccount<TAccountLink>
            where TAccountLink : class, IAmAnAccountRelationshipTableWhereAccountIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Accounts!
                    .Any(y => y.Active && y.Master!.Active && y.Master.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by account name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TAccountLink">Type of the account link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAccountName<TEntity, TAccountLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByAccount<TAccountLink>
            where TAccountLink : class, IAmAnAccountRelationshipTableWhereAccountIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Accounts!
                    .Any(y => y.Active && y.Master!.Active && y.Master.Name == name));
        }
    }
}
