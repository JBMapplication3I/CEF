// <copyright file="ICalendarWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Calendar Workflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;

    public partial interface ICalendarWorkflow
    {
        /// <summary>
        /// Gets the calendar ID assigned to an account. If none exists, a new (blank) calendar is generated.
        /// </summary>
        /// <param name="accountID">The ID of the account to retrieve the calendar ID from.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>The ID of the calendar assigned to the account.</returns>
        Task<CEFActionResponse<int>> GetCalendarIDForAccountAsync(
            int accountID,
            string? contextProfileName);

        /// <summary>
        /// Gets the calendar ID assigned to an account. If none exists, a new (blank) calendar is generated.
        /// </summary>
        /// <param name="accountID">The ID of the account to retrieve the calendar ID from.</param>
        /// <param name="context">The context.</param>
        /// <returns>The ID of the calendar assigned to the account.</returns>
        Task<CEFActionResponse<int>> GetCalendarIDForAccountAsync(
            int accountID,
            IClarityEcommerceEntities context);
    }
}
