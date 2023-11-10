// <copyright file="ISalesGroupWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesGroupWorkflow interface.</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public partial interface ISalesGroupWorkflow
    {
        /// <summary>Gets the sales group only if the supplied AccountID exists on the group.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="accountIDs">        List of identifiers for the accounts.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{ISalesGroupModel}.</returns>
        Task<ISalesGroupModel> SecureSalesGroupAsync(int id, List<int> accountIDs, string? contextProfileName);
    }
}
