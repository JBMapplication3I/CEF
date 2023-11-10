// <copyright file="IUiTranslationWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUiTranslationWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for user interface translation workflow.</summary>
    public partial interface IUiTranslationWorkflow
    {
        /// <summary>Searches for the first and return dictionary.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The found and return dictionary.</returns>
        Task<Dictionary<string, Dictionary<string, string>>> SearchAndReturnDictionaryAsync(
            IUiTranslationSearchModel request,
            string? contextProfileName);
    }
}
