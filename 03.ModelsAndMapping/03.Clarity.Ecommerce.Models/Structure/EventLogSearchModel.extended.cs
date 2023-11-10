// <copyright file="EventLogSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event log search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.ComponentModel;

    public partial class EventLogSearchModel
    {
        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? MinDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? MaxDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? LogLevel { get; set; }
    }
}
