// <copyright file="HaveATypeSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have a type SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A have a type SQL search extensions.</summary>
    public static class HaveATypeSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by have a type search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="search">The search.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByHaveATypeSearchModel<TEntity, TType>(
                this IQueryable<TEntity> query,
                IHaveATypeBaseSearchModel search)
            where TEntity : class, IBase, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (search == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByTypeID(search.TypeID)
                .FilterByTypeIDs<TEntity, TType>(search.TypeIDs)
                .FilterByExcludedTypeID(search.ExcludedTypeID)
                .FilterByExcludedTypeIDs<TEntity, TType>(search.ExcludedTypeIDs)
                .FilterByTypeKey<TEntity, TType>(search.TypeKey)
                .FilterByTypeKeys<TEntity, TType>(search.TypeKeys)
                .FilterByExcludedTypeKey<TEntity, TType>(search.ExcludedTypeKey)
                .FilterByExcludedTypeKeys<TEntity, TType>(search.ExcludedTypeKeys)
                .FilterByTypeName<TEntity, TType>(search.TypeName)
                .FilterByTypeNames<TEntity, TType>(search.TypeNames)
                .FilterByExcludedTypeName<TEntity, TType>(search.ExcludedTypeName)
                .FilterByExcludedTypeNames<TEntity, TType>(search.ExcludedTypeNames)
                .FilterByTypeDisplayName<TEntity, TType>(search.TypeDisplayName)
                .FilterByTypeDisplayNames<TEntity, TType>(search.TypeDisplayNames)
                .FilterByExcludedTypeDisplayName<TEntity, TType>(search.ExcludedTypeDisplayName)
                .FilterByExcludedTypeDisplayNames<TEntity, TType>(search.ExcludedTypeDisplayNames);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by type identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByTypeID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveATypeBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.TypeID == id!.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by type key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByTypeKey<TEntity, TType>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Type != null
                        && p.Type.CustomKey != null
                        && p.Type.CustomKey == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Type != null
                    && p.Type.CustomKey != null
                    && p.Type.CustomKey.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable type keys.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="keys"> The keys.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByTypeKeys<TEntity, TType>(
                this IQueryable<TEntity> query,
                string?[]? keys)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (keys?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type != null
                    && keys.Contains(x.Type.CustomKey));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by type name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByTypeName<TEntity, TType>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Type != null
                        && p.Type.Name != null
                        && p.Type.Name == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Type != null
                    && p.Type.Name != null
                    && p.Type.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type keys.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="keys"> The keys.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByExcludedTypeKeys<TEntity, TType>(
                this IQueryable<TEntity> query,
                string?[]? keys)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (keys?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type != null && !keys.Contains(x.Type.CustomKey));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable type IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByTypeIDs<TEntity, TType>(
                this IQueryable<TEntity> query,
                int?[]? ids)
            where TEntity : class, IBase, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (ids?.Any(x => x > 0 && x.Value != int.MaxValue) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildTypeIDsPredicate<TEntity, TType>(ids));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedTypeID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveATypeBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.TypeID != id!.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedTypeKey<TEntity, TType>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Type != null
                        && p.Type.CustomKey != null
                        && p.Type.CustomKey != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Type != null
                    && p.Type.CustomKey != null
                    && !p.Type.CustomKey.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable type names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByTypeNames<TEntity, TType>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type != null && names.Contains(x.Type.Name));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedTypeName<TEntity, TType>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Type != null
                        && p.Type.Name != null
                        && p.Type.Name != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Type != null
                    && p.Type.Name != null
                    && !p.Type.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedTypeNames<TEntity, TType>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type != null
                    && !names.Contains(x.Type.Name));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by type display name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByTypeDisplayName<TEntity, TType>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Type != null
                        && p.Type.DisplayName != null
                        && p.Type.DisplayName == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Type != null
                    && p.Type.DisplayName != null
                    && p.Type.DisplayName.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by nullable type display names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByTypeDisplayNames<TEntity, TType>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type != null
                    && names.Contains(x.Type.DisplayName));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type display name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="customKey">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedTypeDisplayName<TEntity, TType>(
                this IQueryable<TEntity> query,
                string? customKey,
                bool strict = false)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (!Contract.CheckValidKey(customKey))
            {
                return query;
            }
            var search = customKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(p => p.Type != null
                        && p.Type.DisplayName != null
                        && p.Type.DisplayName != search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(p => p.Type != null
                    && p.Type.DisplayName != null
                    && !p.Type.DisplayName.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type display names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType">  Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="names">The names.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedTypeDisplayNames<TEntity, TType>(
                this IQueryable<TEntity> query,
                string?[]? names)
            where TEntity : class, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (names?.All(string.IsNullOrWhiteSpace) != false)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => x.Type != null
                    && !names.Contains(x.Type.DisplayName));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded type IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TType"> Type of the type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByExcludedTypeIDs<TEntity, TType>(
                this IQueryable<TEntity> query,
                int?[]? ids)
            where TEntity : class, IBase, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            if (ids?.Any(x => x > 0 && x.Value != int.MaxValue) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildNotTypeIDsPredicate<TEntity, TType>(ids));
        }

        private static Expression<Func<TEntity, bool>> BuildTypeIDsPredicate<TEntity, TType>(
                IEnumerable<int?> ids)
            where TEntity : class, IBase, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(false), (c, id) => c.Or(p => p.TypeID == id));
        }

        private static Expression<Func<TEntity, bool>> BuildNotTypeIDsPredicate<TEntity, TType>(
                IEnumerable<int?> ids)
            where TEntity : class, IBase, IHaveATypeBase<TType>
            where TType : class, ITypableBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(true), (c, id) => c.And(p => p.TypeID != id));
        }
    }
}
