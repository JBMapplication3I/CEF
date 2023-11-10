// <copyright file="AmFilterableByStoreSlaveTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by store {t} sql search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by store slave tsql search extensions.</summary>
    public static class AmFilterableByStoreSlaveTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by slave stores by search
        /// model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableBySlaveStoresBySearchModel<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByStoreSearchModel model)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByStoreID<TEntity, TStoreLink>(model.StoreID)
                .FilterByStoreKey<TEntity, TStoreLink>(model.StoreKey)
                .FilterByStoreName<TEntity, TStoreLink>(model.StoreName)
                .FilterByStoreSeoUrl<TEntity, TStoreLink>(model.StoreSeoUrl)
                .FilterByAnyStoreWithContactAddressMatchingCountryID<TEntity, TStoreLink>(model.StoreCountryID)
                .FilterByAnyStoreWithContactAddressMatchingRegionID<TEntity, TStoreLink>(model.StoreRegionID)
                .FilterByAnyStoreWithContactAddressMatchingCity<TEntity, TStoreLink>(model.StoreCity)
                .FilterByAnyStoreWithAnyContactAddressMatchingCountryID<TEntity, TStoreLink>(model.StoreAnyCountryID)
                .FilterByAnyStoreWithAnyContactAddressMatchingRegionID<TEntity, TStoreLink>(model.StoreAnyRegionID)
                .FilterByAnyStoreWithAnyContactAddressMatchingCity<TEntity, TStoreLink>(model.StoreAnyCity);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreID<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.SlaveID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreKey<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreName<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.Name == name));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store seo URL.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="seoUrl">URL of the seo.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreSeoUrl<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                string? seoUrl)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(seoUrl))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.SeoUrl == seoUrl));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any store with contact address matching
        /// country identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAnyStoreWithContactAddressMatchingCountryID<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.Contact!.Address!.CountryID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any store with contact address matching
        /// region identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAnyStoreWithContactAddressMatchingRegionID<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.Contact!.Address!.RegionID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any store with contact address matching
        /// city.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAnyStoreWithContactAddressMatchingCity<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.Contact!.Address!.City == city));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any store with any contact address matching
        /// country identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAnyStoreWithAnyContactAddressMatchingCountryID<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.StoreContacts!
                        .Any(z => z.Active && z.Slave!.Active && z.Slave.Address!.CountryID == id)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any store with any contact address matching
        /// region identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAnyStoreWithAnyContactAddressMatchingRegionID<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.StoreContacts!
                        .Any(z => z.Active && z.Slave!.Active && z.Slave.Address!.RegionID == id)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by any store with any contact address matching
        /// city.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TStoreLink">Type of the store link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByAnyStoreWithAnyContactAddressMatchingCity<TEntity, TStoreLink>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IAmFilterableByStore<TStoreLink>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TEntity>
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Active && y.Slave!.Active && y.Slave.StoreContacts!
                        .Any(z => z.Active && z.Slave!.Active && z.Slave.Address!.City == city)));
        }
    }
}
