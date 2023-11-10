// <copyright file="WeatherForecast.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the weather forecast class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using System;

    /// <summary>A weather forecast.</summary>
    public class WeatherForecast
    {
        /// <summary>Gets or sets the Date/Time of the date.</summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>Gets or sets the temperature c.</summary>
        /// <value>The temperature c.</value>
        public int TemperatureC { get; set; }

        /// <summary>Gets the temperature f.</summary>
        /// <value>The temperature f.</value>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>Gets or sets the summary.</summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }
    }
}
