// <copyright file="ISystemClock.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISystemClock interface</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;

    /// <summary>Abstracts the system clock to facilitate testing.</summary>
    public interface ISystemClock
    {
        /// <summary>Retrieves the current system time in UTC.</summary>
        /// <value>The UTC now.</value>
        DateTimeOffset UtcNow
        {
            get;
        }
    }
}
