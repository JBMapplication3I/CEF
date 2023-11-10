// <copyright file="ProductInventoryLocationSection.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Product Inventory Location Section Search Extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A search extensions.</summary>
    public static class ProductInventoryLocationSectionSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter PILS by inventory location section identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByInventoryLocationSectionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by inventory location section key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="key">   The key.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByInventoryLocationSectionKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Slave != null && x.Slave.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null && x.Slave.CustomKey!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by inventory location section name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByInventoryLocationSectionName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Slave != null && x.Slave.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null && x.Slave.Name!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by inventory location section inventory
        /// location identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByILSInventoryLocationID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null && x.Slave.InventoryLocationID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by inventory location section inventory
        /// location key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="key">   The key.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByILSInventoryLocationKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Slave != null
                        && x.Slave.InventoryLocation != null
                        && x.Slave.InventoryLocation.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.InventoryLocation != null
                    && x.Slave.InventoryLocation.CustomKey!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by inventory location section inventory
        /// location name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByILSInventoryLocationName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Slave != null
                        && x.Slave.InventoryLocation != null
                        && x.Slave.InventoryLocation.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.InventoryLocation != null
                    && x.Slave.InventoryLocation.Name!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByProductID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="key">   The key.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Master != null && x.Master.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.CustomKey!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by product name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByProductName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Master != null && x.Master.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null && x.Master.Name!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by has quantity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByHasQuantity<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!has.HasValue)
            {
                return query;
            }
            if (has.Value)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Quantity > 0);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Quantity <= 0);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by has quantity broken.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByHasQuantityBroken<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!has.HasValue)
            {
                return query;
            }
            if (has.Value)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.QuantityBroken > 0);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityBroken <= 0);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by has quantity allocated.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByHasQuantityAllocated<TEntity>(
                this IQueryable<TEntity> query,
                bool? has)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (!has.HasValue)
            {
                return query;
            }
            if (has.Value)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.QuantityAllocated > 0);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityAllocated <= 0);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.InventoryLocation != null
                    && x.Slave.InventoryLocation.Stores!.Any(y => y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByFranchiseID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.InventoryLocation != null
                    && x.Slave.InventoryLocation.Franchises!.Any(y => y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by brand identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByBrandID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                    && x.Slave.InventoryLocation != null
                    && x.Slave.InventoryLocation.Brands!.Any(y => y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by store IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByStoreIDs<TEntity>(
                this IQueryable<TEntity> query,
                ICollection<int>? ids)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckEmpty(ids))
            {
                return query;
            }
            var predicate = BuildPILSStoreIDsPredicate<TEntity>(ids!);
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(predicate);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter PILS by product IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterPILSByProductIDs<TEntity>(
                this IQueryable<TEntity> query,
                ICollection<int>? ids)
            where TEntity : class, IProductInventoryLocationSection
        {
            if (Contract.CheckEmpty(ids))
            {
                return query;
            }
            var predicate = BuildPILSProductIDsPredicate<TEntity>(ids!);
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(predicate);
        }

        /// <summary>Builds PILS store IDs predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="ids">The identifiers.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildPILSStoreIDsPredicate<TEntity>(
            IEnumerable<int> ids)
            where TEntity : class, IProductInventoryLocationSection
        {
            return ids.Aggregate(
                PredicateBuilder.New<TEntity>(false),
                (c, id) => c.Or(pils => pils.Master!.Stores!.Select(ps => ps.MasterID).Any(sid => sid == id)
                                     && pils.Slave!.InventoryLocation!.Stores!.Any(s => s.MasterID == id)));
        }

        /// <summary>Builds PILS product IDs predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="ids">The identifiers.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildPILSProductIDsPredicate<TEntity>(
            IEnumerable<int> ids)
            where TEntity : class, IProductInventoryLocationSection
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(false), (c, id) => c.Or(pils => pils.MasterID == id));
        }
    }
}
