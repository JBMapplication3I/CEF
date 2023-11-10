// <copyright file="IProductSubscriptionTypeWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductSubscriptionTypeWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for product subscription type workflow.</summary>
    public partial interface IProductSubscriptionTypeWorkflow
    {
        /// <summary>Gets by subscription type.</summary>
        /// <param name="subscriptionTypeID">Identifier for the subscription type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by subscription type.</returns>
        Task<List<IProductSubscriptionTypeModel>> GetBySubscriptionTypeAsync(
            int subscriptionTypeID,
            string? contextProfileName);
    }
}
