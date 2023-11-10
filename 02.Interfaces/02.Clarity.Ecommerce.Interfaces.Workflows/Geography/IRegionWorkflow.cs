// <copyright file="IRegionWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRegionWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for region workflow.</summary>
    public partial interface IRegionWorkflow
    {
        /// <summary>Check exists by code.</summary>
        /// <param name="code">              The code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByCodeAsync(string code, string? contextProfileName);

        /// <summary>Check exists by code.</summary>
        /// <param name="code">   The code.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByCodeAsync(string code, IClarityEcommerceEntities context);

        /// <summary>Gets by code.</summary>
        /// <param name="code">              The code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by code.</returns>
        Task<IRegionModel?> GetByCodeAsync(string code, string? contextProfileName);

        /// <summary>Gets by code.</summary>
        /// <param name="code">   The code.</param>
        /// <param name="context">The context.</param>
        /// <returns>The by code.</returns>
        Task<IRegionModel?> GetByCodeAsync(string code, IClarityEcommerceEntities context);

        /// <summary>Validates the region for approved shipping.</summary>
        /// <param name="countryID">         The country ID.</param>
        /// <param name="regionID">          The region ID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ValidateRestrictedRegionAsync(int countryID, int regionID, string? contextProfileName);
    }
}
