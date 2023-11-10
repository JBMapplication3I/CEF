// <copyright file="HaveAStatusSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have the status SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A have the status SQL search extensions.</summary>
    public static class HaveAStatusSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by have the status search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="search">The search.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByHaveAStatusSearchModel<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                IHaveAStatusBaseSearchModel search)
            where TEntity : class, IBase, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            Contract.RequiresNotNull(search);
            return Contract.RequiresNotNull(query)
                .FilterByStatusID(search.StatusID)
                .FilterByStatusIDs<TEntity, TStatus>(search.StatusIDs)
                .FilterByExcludedStatusID(search.ExcludedStatusID)
                .FilterByExcludedStatusIDs<TEntity, TStatus>(search.ExcludedStatusIDs)
                .FilterByStatusKey<TEntity, TStatus>(search.StatusKey)
                .FilterByStatusKeys<TEntity, TStatus>(search.StatusKeys)
                .FilterByExcludedStatusKey<TEntity, TStatus>(search.ExcludedStatusKey)
                .FilterByExcludedStatusKeys<TEntity, TStatus>(search.ExcludedStatusKeys)
                .FilterByStatusName<TEntity, TStatus>(search.StatusName)
                .FilterByStatusNames<TEntity, TStatus>(search.StatusNames)
                .FilterByExcludedStatusName<TEntity, TStatus>(search.ExcludedStatusName)
                .FilterByExcludedStatusNames<TEntity, TStatus>(search.ExcludedStatusNames)
                .FilterByStatusDisplayName<TEntity, TStatus>(search.StatusDisplayName)
                .FilterByStatusDisplayNames<TEntity, TStatus>(search.StatusDisplayNames)
                .FilterByExcludedStatusDisplayName<TEntity, TStatus>(search.ExcludedStatusDisplayName)
                .FilterByExcludedStatusDisplayNames<TEntity, TStatus>(search.ExcludedStatusDisplayNames);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by status identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByStatusID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveAStatusBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.StatusID == id!.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by status key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByStatusKey<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Status != null
                        && p.Status.CustomKey != null
                        && p.Status.CustomKey == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Status != null
                    && p.Status.CustomKey != null
                    && p.Status.CustomKey.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByExcludedStatusKey<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Status != null
                        && p.Status.CustomKey != null
                        && p.Status.CustomKey != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Status != null
                    && p.Status.CustomKey != null
                    && !p.Status.CustomKey.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by status name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByStatusName<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Status != null
                        && p.Status.Name != null
                        && p.Status.Name == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Status != null
                    && p.Status.Name != null
                    && p.Status.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable status names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByStatusNames<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Status != null
                    && names.Contains(x.Status.Name));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable status IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStatusIDs<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                int?[]? ids)
            where TEntity : class, IBase, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (ids?.Any(x => x is > 0 && x.Value != int.MaxValue) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildStatusIDsPredicate<TEntity, TStatus>(ids));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStatusID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveAStatusBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.StatusID != id!.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable status keys.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="keys"> The keys.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStatusKeys<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string?[]? keys)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (keys?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Status != null
                    && keys.Contains(x.Status.CustomKey));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status keys.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="keys"> The keys.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStatusKeys<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string?[]? keys)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (keys?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Status != null
                    && !keys.Contains(x.Status.CustomKey));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStatusName<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Status != null
                        && p.Status.Name != null
                        && p.Status.Name != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Status != null
                    && p.Status.Name != null
                    && !p.Status.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStatusNames<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Status != null
                    && !names.Contains(x.Status.Name));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by status display name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStatusDisplayName<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Status != null
                        && p.Status.DisplayName != null
                        && p.Status.DisplayName == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Status != null
                    && p.Status.DisplayName != null
                    && p.Status.DisplayName.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable status display names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStatusDisplayNames<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Status != null
                    && names.Contains(x.Status.DisplayName));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status display name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStatusDisplayName<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Status != null
                        && p.Status.DisplayName != null
                        && p.Status.DisplayName != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Status != null
                    && p.Status.DisplayName != null
                    && !p.Status.DisplayName.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status display names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStatusDisplayNames<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Status != null
                    && !names.Contains(x.Status.DisplayName));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded status IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TStatus">Type of the status.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStatusIDs<TEntity, TStatus>(
                this IQueryable<TEntity> query,
                int?[]? ids)
            where TEntity : class, IBase, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            if (ids?.Any(x => x is > 0 && x.Value != int.MaxValue) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildNotStatusIDsPredicate<TEntity, TStatus>(ids));
        }

        private static Expression<Func<TEntity, bool>> BuildStatusIDsPredicate<TEntity, TStatus>(
                IEnumerable<int?> ids)
            where TEntity : class, IBase, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(false), (c, id) => c.Or(p => p.StatusID == id));
        }

        private static Expression<Func<TEntity, bool>> BuildNotStatusIDsPredicate<TEntity, TStatus>(
                IEnumerable<int?> ids)
            where TEntity : class, IBase, IHaveAStatusBase<TStatus>
            where TStatus : class, IStatusableBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(true), (c, id) => c.And(p => p.StatusID != id));
        }
    }
}
