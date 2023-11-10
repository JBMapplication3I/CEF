// <copyright file="IPhonePrefixLookupWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the iPhone prefix lookup workflow class</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public partial interface IPhonePrefixLookupWorkflow
    {
        /// <summary>Reverse phone prefix to city region country.</summary>
        /// <param name="prefix">            The prefix.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{IPhonePrefixLookupModel}.</returns>
        Task<(List<IPhonePrefixLookupModel> results, int totalPages, int totalCount)> ReversePhonePrefixToCityRegionCountryAsync(
            string prefix,
            string? contextProfileName);
    }
}
