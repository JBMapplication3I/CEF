// <copyright file="Address.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The address search extensions.</summary>
    public static class AddressSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter addresses by country identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAddress
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CountryID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by country custom key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByCountryCustomKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAddress
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Country != null
                    && x.Country.CustomKey != null
                    && x.Country.CustomKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by country name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByCountryName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAddress
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Country != null
                    && x.Country.Name != null
                    && x.Country.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by country description.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="description">The description.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByCountryDescription<TEntity>(
                this IQueryable<TEntity> query,
                string? description)
            where TEntity : class, IAddress
        {
            if (!Contract.CheckValidKey(description))
            {
                return query;
            }
            var search = description!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Country != null
                    && x.Country.Description != null
                    && x.Country.Description.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by region identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAddress
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.RegionID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by region custom key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByRegionCustomKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAddress
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Region != null
                    && x.Region.CustomKey != null
                    && x.Region.CustomKey == search);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by region name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByRegionName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAddress
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Region != null
                    && x.Region.Name != null
                    && x.Region.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by region description.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="description">The description.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByRegionDescription<TEntity>(
                this IQueryable<TEntity> query,
                string? description)
            where TEntity : class, IAddress
        {
            if (!Contract.CheckValidKey(description))
            {
                return query;
            }
            var search = description!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Region != null
                    && x.Region.Description != null
                    && x.Region.Description.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by zip code radius.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude"> The latitude.</param>
        /// <param name="radius">   The radius.</param>
        /// <param name="units">    The units.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByZipCodeRadius<TEntity>(
                this IQueryable<TEntity> query,
                decimal? longitude,
                decimal? latitude,
                int? radius,
                Enums.LocatorUnits? units)
            where TEntity : class, IAddress
        {
            if (!latitude.HasValue || !longitude.HasValue || radius is not > 0)
            {
                return query;
            }
            return FilterAddressesByLatitudeLongitudeRadius(
                Contract.RequiresNotNull(query),
                (double)latitude.Value,
                (double)longitude.Value,
                radius,
                units);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter addresses by latitude longitude radius.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="latitude"> The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="radius">   The radius.</param>
        /// <param name="units">    The units.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAddressesByLatitudeLongitudeRadius<TEntity>(
                this IQueryable<TEntity> query,
                double? latitude,
                double? longitude,
                int? radius,
                Enums.LocatorUnits? units)
            where TEntity : class, IAddress
        {
            if (!double.TryParse(latitude.ToString(), out var lat)
                || !double.TryParse(longitude.ToString(), out var lon)
                || radius is null or 0)
            {
                return query;
            }
            var halfSideInMeters = GeographicDistance.ConvertLocatorUnits(
                radius.Value,
                units ?? Enums.LocatorUnits.Meters,
                Enums.LocatorUnits.Meters);
            var boundingBox = GeographicDistance.GetBoundingBox(lat, lon, halfSideInMeters);
            var minLat = boundingBox.MinPoint?.Latitude;
            var maxLat = boundingBox.MaxPoint?.Latitude;
            var minLon = boundingBox.MinPoint?.Longitude;
            var maxLon = boundingBox.MaxPoint?.Longitude;
            // Pre-filter by Lat/Lon Box
            var preFilter = Contract.RequiresNotNull(query)
                .Where(i => (double?)i.Latitude >= minLat
                    && (double?)i.Latitude <= maxLat
                    && (double?)i.Longitude >= minLon
                    && (double?)i.Longitude <= maxLon)
                // Now ToList because the distance calculation can't be done in SQL using LINQ
                .ToList();
            // Filter by Calculated Distance
            // ReSharper disable PossibleInvalidOperationException
            var filterDistance = preFilter
                .Where(x => GeographicDistance.BetweenLocations(
                    (double)x.Latitude!,
                    (double)x.Longitude!,
                    lat,
                    lon,
                    units) <= radius);
            // ReSharper restore PossibleInvalidOperationException
            return filterDistance.AsQueryable();
        }
    }
}
