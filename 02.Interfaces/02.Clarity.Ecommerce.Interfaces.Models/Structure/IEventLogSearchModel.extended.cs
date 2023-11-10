// <copyright file="IEventLogSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEventLogSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    public partial interface IEventLogSearchModel
    {
        /// <summary>Gets or sets the minimum date.</summary>
        /// <value>The minimum date.</value>
        DateTime? MinDate { get; set; }

        /// <summary>Gets or sets the maximum date.</summary>
        /// <value>The maximum date.</value>
        DateTime? MaxDate { get; set; }

        /// <summary>Gets or sets the log level.</summary>
        /// <value>The log level.</value>
        int? LogLevel { get; set; }
    }
}
