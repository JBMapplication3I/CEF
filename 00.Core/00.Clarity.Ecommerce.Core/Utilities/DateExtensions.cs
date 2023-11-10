// <copyright file="DateExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the date extensions class</summary>
namespace Clarity.Ecommerce
{
    using System;

    /// <summary>A date extensions.</summary>
    public static class DateExtensions
    {
        /// <summary>The minimum date time.</summary>
        private static readonly DateTime MinDateTime = new(1900, 1, 1);

        /// <summary>The maximum date time.</summary>
        private static readonly DateTime MaxDateTime = new(2076, 6, 1);

        /// <summary>Sets a value indicating whether this DateExtensions use UTC.</summary>
        /// <value>true if use UTC, false if not.</value>
        /// <remarks>This is set in Global.asax.cs.</remarks>
        public static bool UseUtc { private get; set; }

#if USE_TIMEZONE
        /// <summary>Gets or sets a value indicating whether this DateExtensions uses a specified TimeZone</summary>
        /// <value>true if use specific timezone, false if not</value>
        /// <remarks>This is set in Global.asas.cs</remarks>
        public static bool UseSpecificTimeZone { private get; set; }

        /// <summary>Gets or sets a the time zone information if UseSpecificTimeZone is set to true</summary>
        /// <value>The value of the specified System.TimeZoneInfo</value>
        /// <remarks>This is set in Global.asas.cs</remarks>
        public static TimeZoneInfo TimeZoneInfo { private get; set; }

        /// <summary>The date time. UTC now.</summary>
        public static DateTime GenDateTime => UseSpecificTimeZone
            ? TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo)
            : UseUtc
                ? DateTime.UtcNow
                : DateTime.Now;
#else
        /// <summary>Gets the current date time (by setting, could be UTC).</summary>
        /// <value>The generate date time.</value>
        public static DateTime GenDateTime => UseUtc ? DateTime.UtcNow : DateTime.Now;
#endif

        /// <summary>A DateTime extension method that strips the time element from the object.</summary>
        /// <param name="original">The original to act on.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime StripTime(this DateTime original)
        {
            return original
                .AddHours(-1 * original.Hour)
                .AddMinutes(-1 * original.Minute)
                .AddSeconds(-1 * original.Second);
        }

        /// <summary>A DateTime? extension method that constrain date time.</summary>
        /// <param name="original">The original to act on.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime ConstrainDateTime(this DateTime original)
        {
            return BiggestDateTime(SmallestDateTime(original));
        }

        /// <summary>A DateTime? extension method that constrain date time.</summary>
        /// <param name="original">The original to act on.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime ConstrainDateTime(this DateTime? original)
        {
            return BiggestDateTime(SmallestDateTime(original));
        }

        /// <summary>Biggest date time.</summary>
        /// <param name="original">The original to act on.</param>
        /// <returns>A DateTime.</returns>
        private static DateTime BiggestDateTime(DateTime original)
        {
            return original < MaxDateTime ? original : MaxDateTime;
        }

        /// <summary>Smallest date time.</summary>
        /// <param name="original">The original to act on.</param>
        /// <returns>A DateTime.</returns>
        private static DateTime SmallestDateTime(DateTime original)
        {
            return original > MinDateTime ? original : MinDateTime;
        }

        /// <summary>Smallest date time.</summary>
        /// <param name="original">The original to act on.</param>
        /// <returns>A DateTime.</returns>
        private static DateTime SmallestDateTime(DateTime? original)
        {
            return original == null ? MinDateTime : SmallestDateTime(original.Value);
        }
    }
}
