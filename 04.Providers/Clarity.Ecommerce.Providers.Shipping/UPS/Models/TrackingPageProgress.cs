// <copyright file="TrackingPageProgress.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tracking page progress class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.UPS.Models
{
    using System;

    /// <summary>A tracking page progress.</summary>
    public class TrackingPageProgress
    {
        /// <summary>Gets or sets the date time.</summary>
        /// <value>The date time.</value>
        public DateTime? DateTime { get; set; }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        public string? Location { get; set; }

        /// <summary>Gets or sets the activity.</summary>
        /// <value>The activity.</value>
        public string? Activity { get; set; }
    }
}
