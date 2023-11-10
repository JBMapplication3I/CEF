// <copyright file="AmARelationshipTableSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a relationship table SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>An am a relationship table SQL search extensions.</summary>
    public static class AmARelationshipTableSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am a relation ship table search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmARelationshipTableBaseSearchModel<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                IAmARelationshipTableBaseSearchModel model)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterIAmARelationshipTableByMasterID<TEntity, TMaster, TSlave>(model.MasterID)
                .FilterIAmARelationshipTableByMasterIDs<TEntity, TMaster, TSlave>(model.MasterIDs)
                .FilterIAmARelationshipTableByMasterKey<TEntity, TMaster, TSlave>(model.MasterKey)
                .FilterIAmARelationshipTableBySlaveID<TEntity, TMaster, TSlave>(model.SlaveID)
                .FilterIAmARelationshipTableBySlaveIDs<TEntity, TMaster, TSlave>(model.SlaveIDs)
                .FilterIAmARelationshipTableBySlaveKey<TEntity, TMaster, TSlave>(model.SlaveKey);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relation ship table by master identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableByMasterID<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relation ship table by slave identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableBySlaveID<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id!.Value && x.Slave!.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relationship table by master IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableByMasterIDs<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                IEnumerable<int>? ids)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            var enumerable = ids?.ToList();
            if (enumerable?.Any(x => Contract.CheckValidID(x)) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildMasterIDsPredicate<TEntity, TMaster, TSlave>(enumerable));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relationship table by slave IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableBySlaveIDs<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                IEnumerable<int>? ids)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            var enumerable = ids?.ToList();
            if (enumerable?.Any(x => Contract.CheckValidID(x)) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildSlaveIDsPredicate<TEntity, TMaster, TSlave>(enumerable));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relation ship table by master key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableByMasterKey<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                         // && x.Master.Active
                         && x.Master.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relation ship table by master name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter value to filter by.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableByMasterName<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                string? parameter)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, INameableBase
            where TSlave : class, IBase
        {
            if (!Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                         // && x.Master.Active
                         && x.Master.Name == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relation ship table by slave key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableBySlaveKey<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                         // && x.Slave.Active
                         && x.Slave.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i am a relation ship table by slave name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter value to filter by.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmARelationshipTableBySlaveName<TEntity, TMaster, TSlave>(
                this IQueryable<TEntity> query,
                string? parameter)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, INameableBase
        {
            if (!Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null
                         // && x.Slave.Active
                         && x.Slave.Name == search);
        }

        /// <summary>Builds master IDs predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="ids">The identifiers.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildMasterIDsPredicate<TEntity, TMaster, TSlave>(
                IEnumerable<int> ids)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            return ids.Aggregate(
                PredicateBuilder.New<TEntity>(false),
                (c, id) => c.Or(p => p.MasterID == id));
        }

        /// <summary>Builds slave IDs predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the master.</typeparam>
        /// <typeparam name="TSlave"> Type of the slave.</typeparam>
        /// <param name="ids">The identifiers.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildSlaveIDsPredicate<TEntity, TMaster, TSlave>(
                IEnumerable<int> ids)
            where TEntity : class, IAmARelationshipTable<TMaster, TSlave>
            where TMaster : class, IBase
            where TSlave : class, IBase
        {
            return ids.Aggregate(
                PredicateBuilder.New<TEntity>(false),
                (c, id) => c.Or(p => p.SlaveID == id));
        }
    }
}
