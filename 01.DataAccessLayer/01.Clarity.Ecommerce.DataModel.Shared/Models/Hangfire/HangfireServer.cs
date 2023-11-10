// <copyright file="HangfireServer.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire server class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    public interface IHangfireServer : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        string? Id { get; set; }

        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        string? Data { get; set; }

        /// <summary>Gets or sets the Date/Time of the last heartbeat.</summary>
        /// <value>The last heartbeat.</value>
        DateTime LastHeartbeat { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "Server")]
    public class HangfireServer : IHangfireServer
    {
        /// <inheritdoc/>
        [Key, StringLength(100), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        /// <inheritdoc/>
        public string? Data { get; set; }

        /// <inheritdoc/>
        [Index("IX_HangFire_Server_LastHeartbeat", IsClustered = false)]
        public DateTime LastHeartbeat { get; set; }
    }
}
