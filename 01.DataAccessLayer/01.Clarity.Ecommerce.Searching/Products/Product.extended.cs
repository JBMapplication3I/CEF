// <copyright file="Product.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product search extensions class</summary>
#pragma warning disable SA1009 // Closing parenthesis should be spaced correctly
#pragma warning disable SA1111 // Closing parenthesis should be on line of last parameter
// ReSharper disable CyclomaticComplexity, StyleCop.SA1009, StyleCop.SA1011, StyleCop.SA1111
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A product search extensions.</summary>
    public static class ProductSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter products by ancestor category identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByAncestorCategoryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Categories!
                    .Any(y =>
                        y.Active
                        // Level 7
                        && y.Slave!.Active
                        && (y.SlaveID == id!.Value
                            // Level 6
                            || y.Slave.ParentID.HasValue && y.Slave.Parent!.Active
                            && (y.Slave.ParentID.Value == id.Value
                                // Level 5
                                || y.Slave.Parent.ParentID.HasValue && y.Slave.Parent.Parent!.Active
                                && (y.Slave.Parent.ParentID.Value == id.Value
                                    // Level 4
                                    || y.Slave.Parent.Parent.ParentID.HasValue && y.Slave.Parent.Parent.Parent!.Active
                                    && (y.Slave.Parent.Parent.ParentID.Value == id.Value
                                        // Level 3
                                        || y.Slave.Parent.Parent.Parent.ParentID.HasValue && y.Slave.Parent.Parent.Parent.Parent!.Active
                                        && (y.Slave.Parent.Parent.Parent.ParentID!.Value == id.Value
                                            // Level 2
                                            || y.Slave.Parent.Parent.Parent.Parent!.ParentID.HasValue && y.Slave.Parent.Parent.Parent.Parent!.Parent!.Active
                                            && (y.Slave.Parent.Parent.Parent.Parent!.ParentID!.Value == id.Value
                                                // Level 1
                                                || y.Slave.Parent.Parent.Parent.Parent!.Parent!.ParentID.HasValue && y.Slave.Parent.Parent.Parent.Parent!.Parent!.Parent!.Active
                                                && y.Slave.Parent.Parent.Parent.Parent!.Parent!.ParentID!.Value == id.Value))))))));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by ancestor category name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByAncestorCategoryName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IProduct
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!;
            return Contract.RequiresNotNull(query)
                .Where(x => x.Categories!
                    .Any(y =>
                        y.Active
                        // Level 7
                        && y.Slave!.Active
                        && (y.Slave!.Name!.Contains(search)
                            // Level 6
                            || y.Slave.ParentID.HasValue && y.Slave.Parent!.Active
                            && (y.Slave.Parent.Name!.Contains(search)
                                // Level 5
                                || y.Slave.Parent.ParentID.HasValue && y.Slave.Parent.Parent!.Active
                                && (y.Slave.Parent.Parent.Name!.Contains(search)
                                    // Level 4
                                    || y.Slave.Parent.Parent.ParentID.HasValue && y.Slave.Parent.Parent.Parent!.Active
                                    && (y.Slave.Parent.Parent.Parent.Name!.Contains(search)
                                        // Level 3
                                        || y.Slave.Parent.Parent.Parent.ParentID.HasValue && y.Slave.Parent.Parent.Parent.Parent!.Active
                                        && (y.Slave.Parent.Parent.Parent.Parent!.Name!.Contains(search)
                                            // Level 2
                                            || y.Slave.Parent.Parent.Parent.Parent!.ParentID.HasValue && y.Slave.Parent.Parent.Parent.Parent!.Parent!.Active
                                            && (y.Slave.Parent.Parent.Parent.Parent!.Parent!.Name!.Contains(search)
                                                // Level 1
                                                || y.Slave.Parent.Parent.Parent.Parent!.Parent!.ParentID.HasValue && y.Slave.Parent.Parent.Parent.Parent!.Parent!.Parent!.Active
                                                && y.Slave.Parent.Parent.Parent.Parent!.Parent!.Parent!.Name!.Contains(search)))))))));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by ancestor category i ds.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">            The query to act on.</param>
        /// <param name="parentCategoryIDs">The parent category i ds.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByAncestorCategoryIDs<TEntity>(
                this IQueryable<TEntity> query,
                int[]? parentCategoryIDs)
            where TEntity : class, IProduct
        {
            if (Contract.CheckEmpty(parentCategoryIDs))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(parentCategoryIDs!
                    .Where(x => Contract.CheckValidID(x))
                    .Aggregate(
                        PredicateBuilder.New<TEntity>(false),
                        (c, id) => c.Or(p => p.Categories!
                            .Where(pc => pc.Active && pc.Slave!.ParentID.HasValue)
                            .Select(pc => pc.Slave!.Parent)
                            .Any(pcp => pcp!.Active && pcp.ID == id))));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by category JSON attributes by
        /// values.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">         The query to act on.</param>
        /// <param name="jsonAttributes">The JSON attributes.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByCategoryJsonAttributesByValues<TEntity>(
                this IQueryable<TEntity> query,
                Dictionary<string, string?[]?>? jsonAttributes)
            where TEntity : class, IProduct
        {
            if (Contract.CheckEmpty(jsonAttributes))
            {
                return query;
            }
            var predicate = jsonAttributes!.BuildJsonAttributePredicateForValues<Ecommerce.DataModel.Category>();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Categories!.AsQueryable().Select(y => y.Slave).Any(predicate!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by product requires roles.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByProductRequiresRoles<TEntity>(
                this IQueryable<TEntity> query,
                List<string>? roles = null)
            where TEntity : class, IProduct
        {
            var rolesIsNotNull = roles != null;
            return Contract.RequiresNotNull(query)
                .Where(x => x.RequiresRoles == null
                    || x.RequiresRoles == string.Empty
                    || rolesIsNotNull && roles!.Any(y => x.RequiresRoles.Contains(y)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by product requires roles alternate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByProductRequiresRolesAlt<TEntity>(
                this IQueryable<TEntity> query,
                List<string>? roles = null)
            where TEntity : class, IProduct
        {
            var rolesIsNotNull = roles != null;
            return Contract.RequiresNotNull(query)
                .Where(x => x.RequiresRolesAlt == null
                    || x.RequiresRolesAlt == string.Empty
                    || rolesIsNotNull && roles!.Any(y => x.RequiresRolesAlt.Contains(y)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by category identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">     The query to act on.</param>
        /// <param name="categoryID">Identifier for the category.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByCategoryID<TEntity>(
                this IQueryable<TEntity> query,
                int? categoryID)
            where TEntity : class, IProduct
        {
            if (!categoryID.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Categories!
                    .Any(y => y.Active
                        && y.Slave!.Active
                        && y.SlaveID == categoryID.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by price.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="price">The price.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByPrice<TEntity>(
                this IQueryable<TEntity> query,
                decimal? price)
            where TEntity : class, IProduct
        {
            if (!price.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.PriceBase == price);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by type names.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="typeNames">List of names of the types.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByTypeNames<TEntity>(
                this IQueryable<TEntity> query,
                string?[]? typeNames)
            where TEntity : class, IProduct
        {
            if (typeNames == null || typeNames.Length == 0)
            {
                return query;
            }
            // TODO: Replace with predicate
            return Contract.RequiresNotNull(query)
                .Where(x => typeNames.Contains(x.Type!.Name));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by short description.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByShortDescription<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IProduct
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ShortDescription != null
                         && x.ShortDescription.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by vendor identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByVendorID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProduct
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Vendors!
                    .Any(y => y.Active
                           && y.MasterID == id!.Value
                           && y.Master != null
                           && y.Master.Active));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by manufacturer part number.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByManufacturerPartNumber<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IProduct
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ManufacturerPartNumber != null
                         && x.ManufacturerPartNumber.Contains(key!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by brand category i ds.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TBrandLink">Type of the brand link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByBrandCategoryIDs<TEntity, TBrandLink>(
                this IQueryable<TEntity> query,
                int[]? ids)
            where TEntity : class, IProduct, IAmFilterableByBrand<TBrandLink>
            where TBrandLink : class, IAmABrandRelationshipTableWhereBrandIsTheMaster<TEntity>
        {
            if (ids?.Any(x => x > 0 && x != int.MaxValue) != true)
            {
                return query;
            }
            Contract.RequiresNotNull(query);
            // TODO: Is there a better performance way to do this in C#?
            // Answer: Yes, LinqKit PredicateBuilder
            foreach (var id in ids)
            {
                // We have to make sure our query contains all of the required categories, so we filter each
                // categoryID one at a time
                query = query
                    .FilterByAnyBrandWithCategoryID<TEntity, TBrandLink>(id);
            }
            return query;
        }

        /// <summary>An IQueryable{TEntity} extension method that filter products by franchise category IDs.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TFranchiseLink">Type of the franchise link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterProductsByFranchiseCategoryIDs<TEntity, TFranchiseLink>(
                this IQueryable<TEntity> query,
                int[]? ids)
            where TEntity : class, IProduct, IAmFilterableByFranchise<TFranchiseLink>
            where TFranchiseLink : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<TEntity>
        {
            if (ids?.Any(x => x > 0 && x != int.MaxValue) != true)
            {
                return query;
            }
            Contract.RequiresNotNull(query);
            // TODO: Is there a better performance way to do this in C#?
            // Answer: Yes, LinqKit PredicateBuilder
            foreach (var id in ids)
            {
                // We have to make sure our query contains all of the required categories, so we filter each
                // categoryID one at a time
                query = query
                    .FilterByAnyFranchiseWithCategoryID<TEntity, TFranchiseLink>(id);
            }
            return query;
        }
    }
}
