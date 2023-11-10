// <copyright file="IPriceRoundingWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPriceRoundingWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for price rounding workflow.</summary>
    public partial interface IPriceRoundingWorkflow
    {
        /// <summary>Gets the record.</summary>
        /// <param name="searchModel">       The search model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPriceRoundingModel.</returns>
        Task<IPriceRoundingModel?> GetAsync(IPriceRoundingSearchModel searchModel, string? contextProfileName);
    }
}
