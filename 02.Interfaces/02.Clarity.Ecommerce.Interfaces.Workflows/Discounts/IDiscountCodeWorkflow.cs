// <copyright file="IDiscountCodeWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountCodeWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;

    public partial interface IDiscountCodeWorkflow
    {
        /// <summary>Check exists by code.</summary>
        /// <param name="code">              The code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByCodeAsync(string code, string? contextProfileName);
    }
}
