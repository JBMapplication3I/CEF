// <copyright file="IFranchiseWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFranchiseWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for franchise workflow.</summary>
    public partial interface IFranchiseWorkflow
    {
        /// <summary>Check exists by host URL.</summary>
        /// <param name="hostUrl">           URL of the host.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByHostUrlAsync(string hostUrl, string? contextProfileName);

        /// <summary>Gets by host URL.</summary>
        /// <param name="hostUrl">           URL of the host.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by host URL.</returns>
        Task<IFranchiseModel?> GetByHostUrlAsync(string hostUrl, string? contextProfileName);

        /// <summary>Gets a full.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The full.</returns>
        Task<IFranchiseModel?> GetFullAsync(int id, string? contextProfileName);

        /// <summary>Gets a full.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The full.</returns>
        Task<IFranchiseModel?> GetFullAsync(string key, string? contextProfileName);
    }
}
