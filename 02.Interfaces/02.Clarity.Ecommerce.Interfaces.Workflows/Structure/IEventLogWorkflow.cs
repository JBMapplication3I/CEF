// <copyright file="IEventLogWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEventLogWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for event log workflow.</summary>
    public partial interface IEventLogWorkflow
    {
        /// <summary>Gets a last.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last.</returns>
        Task<IEventLogModel?> GetLastAsync(IEventLogSearchModel search, string? contextProfileName);

        /// <summary>Adds an event.</summary>
        /// <param name="message">           The message.</param>
        /// <param name="name">              The name.</param>
        /// <param name="customKey">         The custom key.</param>
        /// <param name="dataID">            Identifier for the data.</param>
        /// <param name="contextProfileName">The fifth parameter.</param>
        /// <returns>An Task.</returns>
        Task AddEventAsync(
            string message,
            string name,
            string? customKey,
            int? dataID,
            string? contextProfileName);
    }
}
