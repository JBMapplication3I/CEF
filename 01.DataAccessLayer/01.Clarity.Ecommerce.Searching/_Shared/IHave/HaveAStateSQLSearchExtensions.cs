// <copyright file="HaveAStateSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have a state SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A have a state SQL search extensions.</summary>
    public static class HaveAStateSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by have a state search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="search">The search.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByHaveAStateSearchModel<TEntity, TState>(
                this IQueryable<TEntity> query,
                IHaveAStateBaseSearchModel search)
            where TEntity : class, IBase, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            Contract.RequiresNotNull(search);
            return Contract.RequiresNotNull(query)
                .FilterByStateID(search.StateID)
                .FilterByStateIDs<TEntity, TState>(search.StateIDs)
                .FilterByExcludedStateID(search.ExcludedStateID)
                .FilterByExcludedStateIDs<TEntity, TState>(search.ExcludedStateIDs)
                .FilterByStateKey<TEntity, TState>(search.StateKey)
                .FilterByStateKeys<TEntity, TState>(search.StateKeys)
                .FilterByExcludedStateKey<TEntity, TState>(search.ExcludedStateKey)
                .FilterByExcludedStateKeys<TEntity, TState>(search.ExcludedStateKeys)
                .FilterByStateName<TEntity, TState>(search.StateName)
                .FilterByStateNames<TEntity, TState>(search.StateNames)
                .FilterByExcludedStateName<TEntity, TState>(search.ExcludedStateName)
                .FilterByExcludedStateNames<TEntity, TState>(search.ExcludedStateNames)
                .FilterByStateDisplayName<TEntity, TState>(search.StateDisplayName)
                .FilterByStateDisplayNames<TEntity, TState>(search.StateDisplayNames)
                .FilterByExcludedStateDisplayName<TEntity, TState>(search.ExcludedStateDisplayName)
                .FilterByExcludedStateDisplayNames<TEntity, TState>(search.ExcludedStateDisplayNames);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by state identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveAStateBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.StateID == id!.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable state IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateIDs<TEntity, TState>(
                this IQueryable<TEntity> query,
                int?[]? ids)
            where TEntity : class, IBase, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (ids?.Any(x => x is > 0 && x.Value != int.MaxValue) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildStateIDsPredicate<TEntity, TState>(ids));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveAStateBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.StateID != id!.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by state key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateKey<TEntity, TState>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.State != null
                             && p.State.CustomKey != null
                             && p.State.CustomKey.Trim() == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.State != null
                         && p.State.CustomKey != null
                         && p.State.CustomKey.ToLower().Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable state keys.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="keys"> The keys.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateKeys<TEntity, TState>(
                this IQueryable<TEntity> query,
                string?[]? keys)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (keys?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.State != null
                         && keys.Contains(x.State.CustomKey));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateKey<TEntity, TState>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.State != null
                        && p.State.CustomKey != null
                        && p.State.CustomKey != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.State != null
                    && p.State.CustomKey != null
                    && !p.State.CustomKey.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state keys.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="keys"> The keys.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateKeys<TEntity, TState>(
                this IQueryable<TEntity> query,
                string?[]? keys)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (keys?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.State != null
                    && !keys.Contains(x.State.CustomKey));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by state name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateName<TEntity, TState>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.State != null
                             && p.State.Name != null
                             && p.State.Name == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.State != null
                         && p.State.Name != null
                         && p.State.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable state names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateNames<TEntity, TState>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.State != null
                         && names.Contains(x.State.Name));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateName<TEntity, TState>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.State != null
                        && p.State.Name != null
                        && p.State.Name != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.State != null
                    && p.State.Name != null
                    && !p.State.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateNames<TEntity, TState>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.State != null
                    && !names.Contains(x.State.Name));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by state display name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateDisplayName<TEntity, TState>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.State != null
                        && p.State.DisplayName != null
                        && p.State.DisplayName == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.State != null
                    && p.State.DisplayName != null
                    && p.State.DisplayName.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable state display names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStateDisplayNames<TEntity, TState>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.State != null
                    && names.Contains(x.State.DisplayName));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state display name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateDisplayName<TEntity, TState>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.State != null
                        && p.State.DisplayName != null
                        && p.State.DisplayName != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.State != null
                    && p.State.DisplayName != null
                    && !p.State.DisplayName.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state display names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateDisplayNames<TEntity, TState>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.State != null
                    && !names.Contains(x.State.DisplayName));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded state IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TState"> Type of the state.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedStateIDs<TEntity, TState>(
                this IQueryable<TEntity> query,
                int?[]? ids)
            where TEntity : class, IBase, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            if (ids?.Any(x => x is > 0 && x.Value != int.MaxValue) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildNotStateIDsPredicate<TEntity, TState>(ids));
        }

        private static Expression<Func<TEntity, bool>> BuildStateIDsPredicate<TEntity, TState>(
                IEnumerable<int?> ids)
            where TEntity : class, IBase, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(false), (c, id) => c.Or(p => p.StateID == id));
        }

        private static Expression<Func<TEntity, bool>> BuildNotStateIDsPredicate<TEntity, TState>(
                IEnumerable<int?> ids)
            where TEntity : class, IBase, IHaveAStateBase<TState>
            where TState : class, IStateableBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(true), (c, id) => c.And(p => p.StateID != id));
        }
    }
}
