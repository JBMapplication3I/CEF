// <copyright file="ICountryWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICountryWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Models;

    /// <summary>Interface for country workflow.</summary>
    public partial interface ICountryWorkflow
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
        Task<ICountryModel?> GetByCodeAsync(string code, string? contextProfileName);

        /// <summary>Gets by code.</summary>
        /// <param name="code">   The code.</param>
        /// <param name="context">The context.</param>
        /// <returns>The by code.</returns>
        Task<ICountryModel?> GetByCodeAsync(string code, IClarityEcommerceEntities context);
    }
}
