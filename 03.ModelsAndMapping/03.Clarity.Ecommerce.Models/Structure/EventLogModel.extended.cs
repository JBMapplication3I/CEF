// <copyright file="EventLogModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event log model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using ServiceStack;

    /// <summary>A data Model for the event log.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="Interfaces.Models.IEventLogModel"/>
    public partial class EventLogModel
    {
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? DataID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(LogLevel), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Integer to indicate the logging level, optional/nullable.")]
        public int? LogLevel { get; set; }
    }
}
