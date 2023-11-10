// <copyright file="SystemClock.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the system clock class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;

    /// <summary>Provides access to the normal system clock.</summary>
    /// <seealso cref="ISystemClock"/>
    /// <seealso cref="ISystemClock"/>
    public class SystemClock : ISystemClock
    {
        /// <summary>Retrieves the current system time in UTC.</summary>
        /// <value>The UTC now.</value>
        public DateTimeOffset UtcNow
        {
            get
            {
                var utcNow = DateTimeOffset.UtcNow;
                return utcNow.AddMilliseconds(-utcNow.Millisecond);
            }
        }
    }
}
