// <copyright file="HaveAParentSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have a parent SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A have a parent SQL search extensions.</summary>
    public static class HaveAParentSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i have a parent bases by search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIHaveAParentBasesBySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IHaveAParentBaseSearchModel model)
            where TEntity : class, IHaveAParentBase<TEntity>
        {
            return Contract.RequiresNotNull(query)
                .FilterIHaveAParentBasesByParentID(Contract.RequiresNotNull(model).ParentID)
                .FilterIHaveAParentBasesByParentKey(model.ParentKey, false)
                .FilterIHaveAParentBasesDisregardingParents(model.DisregardParents, model.ParentID)
                .FilterIHaveAParentBasesByHasChildren(model.HasChildren)
                .FilterIHaveAParentBasesByChildID(model.ChildID)
                .FilterIHaveAParentBasesByChildKey(model.ChildKey, false);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a parent bases by parent identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIHaveAParentBasesByParentID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveAParentBase<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(c => c.ParentID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a parent bases by parent key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="key">   The key.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterIHaveAParentBasesByParentKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict)
            where TEntity : class, IHaveAParentBase<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(c => c.Parent != null && c.Parent.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(c => c.Parent != null && c.Parent.CustomKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a parent bases disregarding parents.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">           The query to act on.</param>
        /// <param name="disregardParents">The disregard parents.</param>
        /// <param name="id">              The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterIHaveAParentBasesDisregardingParents<TEntity>(
                this IQueryable<TEntity> query,
                bool? disregardParents,
                int? id)
            where TEntity : class, IHaveAParentBase<TEntity>
        {
            if (disregardParents.HasValue && disregardParents.Value)
            {
                return query;
            }
            // NOTE: Parent ID could be null if disregard was false
            return Contract.RequiresNotNull(query)
                .Where(c => c.ParentID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a parent bases by has children.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterIHaveAParentBasesByHasChildren<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, IHaveAParentBase<TEntity>
        {
            if (!has.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Children!.Any(y => y.Active));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a parent bases by child identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterIHaveAParentBasesByChildID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IHaveAParentBase<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Children!.Any(y => y.Active && y.ID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a parent bases by child key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="key">   The key.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterIHaveAParentBasesByChildKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict)
            where TEntity : class, IHaveAParentBase<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Children!.Any(y => y.Active && y.CustomKey == key));
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Children!.Any(y => y.CustomKey == search));
        }
    }
}
