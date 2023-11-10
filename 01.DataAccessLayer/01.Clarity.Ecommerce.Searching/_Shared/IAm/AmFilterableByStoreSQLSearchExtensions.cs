// <copyright file="AmFilterableByStoreSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by store SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by store SQL search extensions.</summary>
    public static class AmFilterableByStoreSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by store search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByStoreSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByStoreSearchModel model)
            where TEntity : class, IAmFilterableByStore
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByStoreID(model.StoreID)
                .FilterByStoreKey(model.StoreKey)
                .FilterByStoreName(model.StoreName)
                .FilterByStoreSeoUrl(model.StoreSeoUrl)
                .FilterByStoreWithContactAddressMatchingCountryID(model.StoreCountryID)
                .FilterByStoreWithContactAddressMatchingRegionID(model.StoreRegionID)
                .FilterByStoreWithContactAddressMatchingCity(model.StoreCity)
                .FilterByStoreWithAnyContactAddressMatchingCountryID(model.StoreAnyCountryID)
                .FilterByStoreWithAnyContactAddressMatchingRegionID(model.StoreAnyRegionID)
                .FilterByStoreWithAnyContactAddressMatchingCity(model.StoreAnyCity);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store!.Active && x.StoreID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByStore
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByStore
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.Name == name);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store seo URL.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="seoUrl">URL of the seo.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreSeoUrl<TEntity>(
                this IQueryable<TEntity> query,
                string? seoUrl)
            where TEntity : class, IAmFilterableByStore
        {
            if (!Contract.CheckValidKey(seoUrl))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.SeoUrl == seoUrl);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store with contact address matching country
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreWithContactAddressMatchingCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.Contact!.Address!.CountryID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store with contact address matching region
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreWithContactAddressMatchingRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.Contact!.Address!.RegionID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store with contact address matching city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreWithContactAddressMatchingCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IAmFilterableByStore
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.Contact!.Address!.City == city);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store with any contact address matching
        /// country identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreWithAnyContactAddressMatchingCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.StoreContacts!
                    .Any(z => z.Active && z.Slave!.Active && z.Slave.Address!.CountryID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store with any contact address matching
        /// region identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreWithAnyContactAddressMatchingRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.StoreContacts!
                    .Any(z => z.Active && z.Slave!.Active && z.Slave.Address!.RegionID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store with any contact address matching
        /// city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreWithAnyContactAddressMatchingCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IAmFilterableByStore
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active && x.Store != null && x.Store.Active && x.Store.StoreContacts!
                    .Any(z => z.Active && z.Slave!.Active && z.Slave.Address!.City == city));
        }
    }
}
