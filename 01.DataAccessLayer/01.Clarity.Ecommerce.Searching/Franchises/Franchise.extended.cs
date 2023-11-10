// <copyright file="Franchise.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A franchise search extensions.</summary>
    public static class FranchiseSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter franchises by host URL.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="hostUrl">URL of the host.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByHostUrl<TEntity>(this IQueryable<TEntity> query, string? hostUrl)
            where TEntity : class, IFranchise
        {
            if (!Contract.CheckValidKey(hostUrl))
            {
                return query;
            }
            var theHostUrl = hostUrl!;
            if (!theHostUrl.StartsWith("http://") && !theHostUrl.StartsWith("https://"))
            {
                theHostUrl = "http://" + theHostUrl;
            }
            var uri = new Uri(theHostUrl.ToLower());
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildFranchisesByHostUrlPredicate<TEntity>(
                    uri.ToString().Replace("https://", string.Empty).Replace("http://", string.Empty),
                    uri.Host.Replace("https://", string.Empty).Replace("http://", string.Empty)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by zip code radius.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="latitude"> The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="radius">   The radius.</param>
        /// <param name="units">    The units.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreZipCodeRadius<TEntity>(
                this IQueryable<TEntity> query,
                decimal? latitude,
                decimal? longitude,
                int? radius,
                Enums.LocatorUnits? units)
            where TEntity : class, IFranchise
        {
            if (!latitude.HasValue || !longitude.HasValue || radius is not > 0)
            {
                return query;
            }
            return FilterFranchisesByStoreLatitudeLongitudeRadius(
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
        public static IQueryable<TEntity> FilterFranchisesByStoreLatitudeLongitudeRadius<TEntity>(
                this IQueryable<TEntity> query,
                double? latitude,
                double? longitude,
                int? radius,
                Enums.LocatorUnits? units)
            where TEntity : class, IFranchise
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
                .Where(i => i.Stores!
                         .Any(y => y.Slave != null
                                && y.Slave.Contact != null
                                && y.Slave.Contact.Address != null
                                && y.Slave.Contact.Address.Latitude.HasValue
                                && y.Slave.Contact.Address.Longitude.HasValue
                                && (double)y.Slave.Contact.Address.Latitude >= boundingBox.MinPoint!.Latitude
                                && (double)y.Slave.Contact.Address.Latitude <= boundingBox.MaxPoint!.Latitude
                                && (double)y.Slave.Contact.Address.Longitude >= boundingBox.MinPoint.Longitude
                                && (double)y.Slave.Contact.Address.Longitude <= boundingBox.MaxPoint.Longitude))
                // Now ToList because the distance calculation can't be done in SQL using LINQ
                .ToList();
            // Filter by Calculated Distance
            // ReSharper disable PossibleInvalidOperationException
            var filterDistance = preFilter
                .Where(i => i.Stores != null
                         && i.Stores
                         .Any(y => y.Slave != null
                                && y.Slave.Contact != null
                                && y.Slave.Contact.Address != null
                                && GeographicDistance.BetweenLocations(
                                (double)y.Slave.Contact.Address!.Latitude!.Value,
                                (double)y.Slave.Contact.Address.Longitude!.Value,
                                lat,
                                lon,
                                units)
                            <= radius));
            // ReSharper restore PossibleInvalidOperationException
            return filterDistance.AsQueryable();
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by region identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                        .Any(y => y.Slave != null
                               && y.Slave.Contact != null
                               && y.Slave.Contact.Address != null
                               && y.Slave.Contact.Address.RegionID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by contact address country identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreContactAddressCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores != null
                         && x.Stores
                        .Any(y => y.Slave != null
                               && y.Slave.Contact != null
                               && y.Slave.Contact.Address != null
                               && y.Slave.Contact.Address.CountryID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by contact address city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreContactAddressCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IFranchise
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores != null
                         && x.Stores
                         .Any(y => y.Slave != null
                                && y.Slave.Contact != null
                                && y.Slave.Contact.Address != null
                                && y.Slave.Contact.Address.City == city));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by any contact address matching region
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreAnyContactAddressMatchingRegionID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                         .Any(y => y.Slave != null
                                && y.Slave.Contact != null
                                && y.Slave.Contact.Address != null
                                && y.Slave.Contact.Address.RegionID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by any contact address matching country
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreAnyContactAddressMatchingCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                         .Any(y => y.Slave != null
                                && y.Slave.Contact != null
                                && y.Slave.Contact.Address != null
                                && y.Slave.Contact.Address.CountryID == id!.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by any contact address matching city.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="city"> The city.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreAnyContactAddressMatchingCity<TEntity>(
                this IQueryable<TEntity> query,
                string? city)
            where TEntity : class, IFranchise
        {
            if (!Contract.CheckValidKey(city))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Stores!
                    .Any(y => y.Slave != null
                           && y.Slave.Contact != null
                           && y.Slave.Contact.Address != null
                           && y.Slave.Contact.Address.City == city));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter stores by district identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFranchisesByStoreAnyDistrictID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IFranchise
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.FranchiseDistricts!
                    .Any(y => y.Slave != null
                        && y.Slave.ID == id));
        }

        /// <summary>Builds franchises by host URL predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="url">     URL of the document.</param>
        /// <param name="hostOnly">The host only.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildFranchisesByHostUrlPredicate<TEntity>(string? url, string? hostOnly)
            where TEntity : class, IFranchise
        {
            return y => y.FranchiseSiteDomains!.Any(x => x.Active
                && (x.Slave!.Url != null && (x.Slave.Url.StartsWith("http://" + url) || x.Slave.Url.StartsWith("https://" + url) || x.Slave.Url.StartsWith("http://" + hostOnly) || x.Slave.Url.StartsWith("https://" + hostOnly))
                || x.Slave.AlternateUrl1 != null && (x.Slave.AlternateUrl1.StartsWith("http://" + url) || x.Slave.AlternateUrl1.StartsWith("https://" + url) || x.Slave.AlternateUrl1.StartsWith("http://" + hostOnly) || x.Slave.AlternateUrl1.StartsWith("https://" + hostOnly))
                || x.Slave.AlternateUrl2 != null && (x.Slave.AlternateUrl2.StartsWith("http://" + url) || x.Slave.AlternateUrl2.StartsWith("https://" + url) || x.Slave.AlternateUrl2.StartsWith("http://" + hostOnly) || x.Slave.AlternateUrl2.StartsWith("https://" + hostOnly))
                || x.Slave.AlternateUrl3 != null && (x.Slave.AlternateUrl3.StartsWith("http://" + url) || x.Slave.AlternateUrl3.StartsWith("https://" + url) || x.Slave.AlternateUrl3.StartsWith("http://" + hostOnly) || x.Slave.AlternateUrl3.StartsWith("https://" + hostOnly))));
        }
    }
}
