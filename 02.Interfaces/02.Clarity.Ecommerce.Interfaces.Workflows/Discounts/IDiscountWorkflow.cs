// <copyright file="IDiscountWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface IDiscountWorkflow
    {
        /// <summary>Gets discount identifier by code.</summary>
        /// <param name="code">              The code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The discount identifier by code.</returns>
        Task<int?> GetDiscountIDByCodeAsync(string code, string? contextProfileName);

        /// <summary>Searches for the first match.</summary>
        /// <param name="searchTerm">        The search term.</param>
        /// <param name="active">            The active.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{IDiscountModel}.</returns>
        Task<List<IDiscountModel>> SearchAsync(string? searchTerm, bool? active, string? contextProfileName);

        /// <summary>Ends an existing discount by ID.</summary>
        /// <param name="id">                The id.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{IDiscountModel}.</returns>
        Task<CEFActionResponse<bool>> EndDiscountByIDAsync(int id, string? contextProfileName);
    }
}
