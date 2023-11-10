// <copyright file="Store.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A store search extensions.</summary>
    public static class StoreSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter stores by zip code radius.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="latitude"> The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="radius">   The radius.</param>
        /// <param name="units">    The units.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoresByZipCodeRadius<TEntity>(
                this IQueryable<TEntity> query,
                decimal? latitude,
                decimal? longitude,
                int? radius,
                Enums.LocatorUnits? units)
            where TEntity : class, IStore
        {
            if (!latitude.HasValue || !longitude.HasValue || radius is not > 0)
            {
                return query;
            }
            return FilterStoresByLatitudeLongitudeRadius(
                Contract.RequiresNotNull(query),
                (double)latitude.Value,
                (double)longitude.Value,
                radius,
                units);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by latitude longitude radius.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="latitude"> The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="radius">   The radius.</param>
        /// <param name="units">    The units.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoresByLatitudeLongitudeRadius<TEntity>(
                this IQueryable<TEntity> query,
                double? latitude,
                double? longitude,
                int? radius,
                Enums.LocatorUnits? units)
            where TEntity : class, IStore
        {
            if (!double.TryParse(latitude.ToString(), out var lat)
                || !double.TryParse(longitude.ToString(), out var lon)
                || radius is not > 0)
            {
                return query;
            }
            var boundingBox = GeographicDistance.GetBoundingBox(
                lat,
                lon,
                // ReSharper disable once StyleCop.SA1118
                GeographicDistance.ConvertLocatorUnits(
                    radius.Value,
                    units ?? Enums.LocatorUnits.Meters,
                    Enums.LocatorUnits.Meters));
            // Pre-filter by Lat/Long Box
            var preFilter = Contract.RequiresNotNull(query)
                .Where(i => i.Contact != null
                         && i.Contact.Address != null
                         && i.Contact.Address.Latitude.HasValue
                         && i.Contact.Address.Longitude.HasValue
                         && (double)i.Contact.Address.Latitude >= boundingBox.MinPoint!.Latitude
                         && (double)i.Contact.Address.Latitude <= boundingBox.MaxPoint!.Latitude
                         && (double)i.Contact.Address.Longitude >= boundingBox.MinPoint.Longitude
                         && (double)i.Contact.Address.Longitude <= boundingBox.MaxPoint.Longitude)
                // Now ToList because the distance calculation can't be done in SQL using LINQ
                .ToList();
            // Filter by Calculated Distance
            // ReSharper disable PossibleInvalidOperationException
            var filterDistance = preFilter
                .Where(i => GeographicDistance.BetweenLocations(
                                (double)i.Contact!.Address!.Latitude!.Value,
                                (double)i.Contact.Address.Longitude!.Value,
                                lat,
                                lon,
                                units)
                            <= radius);
            // ReSharper restore PossibleInvalidOperationException
            return filterDistance.AsQueryable();
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by region identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoresByRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && x.Contact.Address != null
                    && x.Contact.Address.RegionID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by contact address country identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByContactAddressCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && x.Contact.Address != null
                    && x.Contact.Address.CountryID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by contact address city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByContactAddressCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IStore
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && x.Contact.Address != null
                    && x.Contact.Address.City == city);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by any contact address matching region
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoresByAnyContactAddressMatchingRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.StoreContacts!
                    .Any(y => y.Slave != null
                        && y.Slave.Address != null
                        && y.Slave.Address.RegionID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by any contact address matching country
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoresByAnyContactAddressMatchingCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.StoreContacts!
                    .Any(y => y.Slave != null
                        && y.Slave.Address != null
                        && y.Slave.Address.CountryID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by any contact address matching city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoresByAnyContactAddressMatchingCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IStore
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.StoreContacts!
                    .Any(y => y.Slave != null
                           && y.Slave.Address != null
                           && y.Slave.Address.City == city));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by district identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoresByDistrictID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStore
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.StoreDistricts!
                    .Any(y => y.Slave != null
                        && y.Slave.ID == id));
        }
    }
}
