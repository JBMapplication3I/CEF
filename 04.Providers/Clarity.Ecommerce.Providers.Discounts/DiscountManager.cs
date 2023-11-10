// <copyright file="DiscountManager.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount manager class</summary>
namespace Clarity.Ecommerce.Providers.Discounts
{
    using Interfaces.Workflow;

    /// <summary>Manager for discounts.</summary>
    /// <seealso cref="IDiscountManager"/>
    public partial class DiscountManager : IDiscountManager
    {
        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        private static ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        private static IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();
    }
}
