// <copyright file="AmFilterableByNullableStoreSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by nullable store SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by nullable store SQL search extensions.</summary>
    public static class AmFilterableByNullableStoreSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by i am filterable by nullable store search
        /// model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIAmFilterableByNullableStoreSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IAmFilterableByStoreSearchModel model)
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByNullableStoreID(model.StoreID)
                .FilterByNullableStoreKey(model.StoreKey)
                .FilterByNullableStoreName(model.StoreName)
                .FilterByNullableStoreSeoUrl(model.StoreSeoUrl)
                .FilterByStoreWithContactAddressMatchingCountryID(model.StoreCountryID)
                .FilterByStoreWithContactAddressMatchingRegionID(model.StoreRegionID)
                .FilterByStoreWithContactAddressMatchingCity(model.StoreCity)
                .FilterByStoreWithAnyContactAddressMatchingCountryID(model.StoreAnyCountryID)
                .FilterByStoreWithAnyContactAddressMatchingRegionID(model.StoreAnyRegionID)
                .FilterByStoreWithAnyContactAddressMatchingCity(model.StoreAnyCity);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByNullableStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.StoreID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable store key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableStoreKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable store name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableStoreName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.Name == name);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by nullable store seo URL.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="seoUrl">URL of the seo.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByNullableStoreSeoUrl<TEntity>(
                this IQueryable<TEntity> query,
                string? seoUrl)
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (!Contract.CheckValidKey(seoUrl))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.SeoUrl == seoUrl);
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
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null
                         && x.Store.Active
                         && x.Store.Contact!.Active
                         && x.Store.Contact.Address!.Active
                         && x.Store.Contact.Address.CountryID == id);
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
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.Contact!.Address!.RegionID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by store with contact address matching city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByStoreWithContactAddressMatchingCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.Contact!.Address!.City == city);
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
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.StoreContacts!
                    .Any(z => z.Slave!.Active && z.Slave.Address!.CountryID == id));
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
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.StoreContacts!
                    .Any(z => z.Slave!.Active && z.Slave.Address!.RegionID == id));
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
            where TEntity : class, IAmFilterableByNullableStore
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Store != null && x.Store.Active && x.Store.StoreContacts!
                    .Any(z => z.Slave!.Active && z.Slave.Address!.City == city));
        }
    }
}
