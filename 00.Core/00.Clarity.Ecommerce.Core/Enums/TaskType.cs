// <copyright file="TaskType.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the task type class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    /// <summary>The type of a task for organization purpose.</summary>
    public enum TaskType
    {
        /// <summary>An enum constant representing the recurring option.</summary>
        Recurring,

        /// <summary>An enum constant representing the one time run option.</summary>
        OneTimeRun,
    }
}
