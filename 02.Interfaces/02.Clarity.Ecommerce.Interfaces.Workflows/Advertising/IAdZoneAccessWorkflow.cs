// <copyright file="IAdZoneAccessWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAdZoneAccessWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for ad zone access workflow.</summary>
    public partial interface IAdZoneAccessWorkflow
    {
        /// <summary>Gets by user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by user identifier.</returns>
        Task<List<IAdZoneAccessModel>> GetByUserIDAsync(int userID, string? contextProfileName);
    }
}
