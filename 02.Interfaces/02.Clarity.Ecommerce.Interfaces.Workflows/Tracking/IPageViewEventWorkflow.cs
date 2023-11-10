// <copyright file="IPageViewEventWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPageViewEventWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for page view event workflow.</summary>
    public partial interface IPageViewEventWorkflow
    {
        /// <summary>Creates from end user event.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new from end user event.</returns>
        Task<CEFActionResponse<IEndUserEventModel>> CreateFromEndUserEventAsync(
            IEndUserEventModel request,
            string? contextProfileName);

        /// <summary>Gets recently viewed product IDs for current visitor.</summary>
        /// <param name="requestUserHostAddress">The request user host address.</param>
        /// <param name="sessionVisitGuid">      Unique identifier for the session visit.</param>
        /// <param name="sessionVisitorGuid">    Unique identifier for the session visitor.</param>
        /// <param name="paging">                The paging.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>The recently viewed product IDs for current visitor.</returns>
        Task<List<(int ProductID, DateTime CreatedDate)>> GetRecentlyViewedProductIDsForCurrentVisitorAsync(
            string requestUserHostAddress,
            Guid? sessionVisitGuid,
            Guid? sessionVisitorGuid,
            Paging paging,
            string? contextProfileName);
    }
}
