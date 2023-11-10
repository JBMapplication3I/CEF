// <copyright file="GeographicDistance.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the geographic distance class</summary>
namespace Clarity.Ecommerce.Utilities
{
    using System;
    using Enums;

    /// <summary>A geographic distance.</summary>
    public static class GeographicDistance
    {
        // Semi-axes of WGS-84 geoidal reference
        private const double WGS84Major = 6378137.0; // Major semi-axis [m]

        private const double WGS84Minor = 6356752.3; // Minor semi-axis [m]

        /// <summary>Calculate the distance between two locations.</summary>
        /// <param name="latD1">The first lat d.</param>
        /// <param name="lonD1">The first lon d.</param>
        /// <param name="latD2">The second lat d.</param>
        /// <param name="lonD2">The second lon d.</param>
        /// <param name="units">The units.</param>
        /// <returns>The distance as a double.</returns>
        public static double BetweenLocations(double latD1, double lonD1, double latD2, double lonD2, LocatorUnits? units = null)
        {
            var latR = Deg2Rad(latD2 - latD1);
            var lonR = Deg2Rad(lonD2 - lonD1);
            var latRAverage = Deg2Rad((latD1 + latD2) / 2);
            var a = Math.Sin(latR / 2) * Math.Sin(latR / 2)
                + Math.Cos(Deg2Rad(latD1)) * Math.Cos(Deg2Rad(latD2)) * (Math.Sin(lonR / 2) * Math.Sin(lonR / 2));
            var angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = angle * WGS84EarthRadius(latRAverage);
            return ConvertLocatorUnits(distance, LocatorUnits.Meters, units ?? LocatorUnits.Meters);
        }

        /// <summary>Between locations.</summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        /// <param name="units"> The units.</param>
        /// <returns>A double.</returns>
        public static double BetweenLocations(MapPoint point1, MapPoint point2, LocatorUnits? units = null)
        {
            return BetweenLocations(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude, units);
        }

        /// <summary>Gets bounding box.</summary>
        /// <param name="lat">             The lat.</param>
        /// <param name="lon">             The lon.</param>
        /// <param name="halfSideInMeters">The half side in meters.</param>
        /// <returns>The bounding box.</returns>
        public static BoundingBox GetBoundingBox(double lat, double lon, double halfSideInMeters)
        {
            return GetBoundingBox(new(lat, lon), halfSideInMeters);
        }

        /// <summary>Gets bounding box.</summary>
        /// <param name="point">           The point.</param>
        /// <param name="halfSideInMeters">The half side in meters.</param>
        /// <returns>The bounding box.</returns>
        // 'halfSideInMeters' is the half length of the bounding box you want in meters.
        public static BoundingBox GetBoundingBox(MapPoint point, double halfSideInMeters)
        {
            // Bounding box surrounding the point at given coordinates,
            // assuming local approximation of Earth surface as a sphere
            // of radius given by WGS84
            var lat = Deg2Rad(point.Latitude);
            var lon = Deg2Rad(point.Longitude);
            var halfSide = halfSideInMeters;
            // Radius of Earth at given latitude
            var radius = WGS84EarthRadius(lat);
            // Radius of the parallel at given latitude
            var parallelRadius = radius * Math.Cos(lat);
            var latMin = lat - halfSide / radius;
            var latMax = lat + halfSide / radius;
            var lonMin = lon - halfSide / parallelRadius;
            var lonMax = lon + halfSide / parallelRadius;
            return new()
            {
                MinPoint = new(Rad2Deg(latMin), Rad2Deg(lonMin)),
                MaxPoint = new(Rad2Deg(latMax), Rad2Deg(lonMax)),
            };
        }

        /// <summary>Convert locator units.</summary>
        /// <param name="value">    The value.</param>
        /// <param name="fromUnits">from units.</param>
        /// <param name="toUnits">  to units.</param>
        /// <returns>The locator converted units.</returns>
        public static double ConvertLocatorUnits(double value, LocatorUnits fromUnits, LocatorUnits toUnits)
        {
            // TODO: Refactor w/ Library
            switch (fromUnits)
            {
                case LocatorUnits.Meters:
                {
                    switch (toUnits)
                    {
                        case LocatorUnits.Meters:
                        {
                            return value;
                        }
                        case LocatorUnits.Kilometers:
                        {
                            return value / 1000;
                        }
                        case LocatorUnits.Miles:
                        {
                            return value * 0.000621371;
                        }
                    }
                    break;
                }
                case LocatorUnits.Kilometers:
                {
                    switch (toUnits)
                    {
                        case LocatorUnits.Meters:
                        {
                            return value * 1000;
                        }
                        case LocatorUnits.Kilometers:
                        {
                            return value;
                        }
                        case LocatorUnits.Miles:
                        {
                            return value * 0.621371;
                        }
                    }
                    break;
                }
                case LocatorUnits.Miles:
                {
                    switch (toUnits)
                    {
                        case LocatorUnits.Meters:
                        {
                            return value * 1609.34;
                        }
                        case LocatorUnits.Kilometers:
                        {
                            return value * 1.60934;
                        }
                        case LocatorUnits.Miles:
                        {
                            return value;
                        }
                    }
                    break;
                }
            }
            // Should never get here
            throw new ArgumentOutOfRangeException(
                paramName: nameof(fromUnits),
                actualValue: fromUnits,
                message: "ConvertLocatorUnits: from or to units are invalid.");
        }

        // Earth radius at a given latitude, according to the WGS-84 ellipsoid [m]
        private static double WGS84EarthRadius(double lat)
        {
            // http://en.wikipedia.org/wiki/Earth_radius
            var an = WGS84Major * WGS84Major * Math.Cos(lat);
            var bn = WGS84Minor * WGS84Minor * Math.Sin(lat);
            var ad = WGS84Major * Math.Cos(lat);
            var bd = WGS84Minor * Math.Sin(lat);
            return Math.Sqrt((an * an + bn * bn) / (ad * ad + bd * bd));
        }

        // degrees to radians
        private static double Deg2Rad(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        // radians to degrees
        private static double Rad2Deg(double radians)
        {
            return 180.0 * radians / Math.PI;
        }

        /// <summary>A map point.</summary>
        public class MapPoint
        {
            /// <summary>Initializes a new instance of the <see cref="MapPoint"/> class.</summary>
            /// <param name="lat">The lat.</param>
            /// <param name="lon">The lon.</param>
            public MapPoint(double lat, double lon)
            {
                Latitude = lat;
                Longitude = lon;
            }

            /// <summary>Gets or sets the latitude in degrees.</summary>
            /// <value>The latitude.</value>
            public double Latitude { get; set; }

            /// <summary>Gets or sets the longitude in degrees.</summary>
            /// <value>The longitude.</value>
            public double Longitude { get; set; } // In Degrees
        }

        /// <summary>A bounding box.</summary>
        public class BoundingBox
        {
            /// <summary>Gets or sets the minimum point.</summary>
            /// <value>The minimum point.</value>
            public MapPoint? MinPoint { get; set; }

            /// <summary>Gets or sets the maximum point.</summary>
            /// <value>The maximum point.</value>
            public MapPoint? MaxPoint { get; set; }
        }
    }
}
