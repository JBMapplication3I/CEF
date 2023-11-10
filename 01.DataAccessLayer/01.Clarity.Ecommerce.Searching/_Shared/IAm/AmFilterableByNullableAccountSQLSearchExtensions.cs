// <copyright file="AmFilterableByNullableAccountSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by nullable account SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by nullable account SQL search extensions.</summary>
    public static class AmFilterableByNullableAccountSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by nullable account search
        /// model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByNullableAccountSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByAccountSearchModel model)
            where TEntity : class, IAmFilterableByNullableAccount
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByNullableAccountID(model.AccountID)
                .FilterByNullableAccountKey(model.AccountKey)
                .FilterByNullableAccountName(model.AccountName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByNullableAccount
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null && x.Account.Active && x.AccountID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable account key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableAccountKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByNullableAccount
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null && x.Account.Active && x.Account.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable account name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableAccountName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByNullableAccount
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null && x.Account.Active && x.Account.Name == name);
        }
    }
}
