// <copyright file="IProductPricePointWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductPricePointWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for product price point workflow.</summary>
    public partial interface IProductPricePointWorkflow
    {
        /// <summary>Gets the record.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IProductPricePointModel.</returns>
        Task<IProductPricePointModel?> GetAsync(IProductPricePointSearchModel search, string? contextProfileName);

        /// <summary>Check exists by key.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsAsync(IProductPricePointSearchModel search, string? contextProfileName);
    }
}
