// <copyright file="HangfireJobState.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire job state class</summary>
namespace Clarity.Ecommerce.Tasks
{
    using System.ComponentModel;

    /// <summary>Values that represent hang fire job states.</summary>
    public enum HangFireJobState
    {
        /// <summary>An enum constant representing the enqueued option.</summary>
        [Description("This Job is Queued for work")]
        Enqueued,

        /// <summary>An enum constant representing the scheduled option.</summary>
        [Description("This Job is Scheduled to start at a specific time")]
        Scheduled,

        /// <summary>An enum constant representing the processing option.</summary>
        [Description("This Job is currently processing")]
        Processing,

        /// <summary>An enum constant representing the succeeded option.</summary>
        [Description("This Job is succeeded in completing it's work")]
        Succeeded,

        /// <summary>An enum constant representing the failed option.</summary>
        [Description("This Job is failed to complete it's work")]
        Failed,

        /// <summary>An enum constant representing the deleted option.</summary>
        [Description("This Job was Deleted")]
        Deleted,
    }
}