// <copyright file="IDistrictWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDistrictWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Models;

    public partial interface IDistrictWorkflow
    {
        /// <summary>
        /// Gets a district by name and region ID.
        /// </summary>
        /// <param name="regionID">ID of the region the district should be in.</param>
        /// <param name="name">The name of the district.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>Returns a district model or null if none match.</returns>
        Task<IDistrictModel?> GetDistrictByNameAndRegionIDAsync(int regionID, string name, string? contextProfileName);
    }
}
