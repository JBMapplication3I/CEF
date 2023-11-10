// <copyright file="IEventLogModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEventLogModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for event log model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface IEventLogModel
    {
        /// <summary>Gets or sets the identifier of the data.</summary>
        /// <value>The identifier of the data.</value>
        int? DataID { get; set; }

        /// <summary>Gets or sets the log level.</summary>
        /// <value>The log level.</value>
        int? LogLevel { get; set; }
    }
}
