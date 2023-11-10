// <copyright file="WorkflowsController.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflows controller class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Concurrent;
    using Interfaces.Providers.CartValidation;
    using Interfaces.Providers.Pricing;
    using Interfaces.Workflow;
    using Providers.Discounts;

    /// <summary>Provides access to all workflow classes and maintains a shared data context between them.</summary>
    public partial class WorkflowsController : IWorkflowsController
    {
        private readonly ConcurrentDictionary<Type, object> workflows = new();

        /// <inheritdoc/>
        public IDiscountManager DiscountManager => GetWorkflow<IDiscountManager>();

        /// <inheritdoc/>
        public IAuthenticationWorkflow Authentication => GetWorkflow<IAuthenticationWorkflow>();

        /// <inheritdoc/>
        public IAddressBookWorkflow AddressBooks => GetWorkflow<IAddressBookWorkflow>();

        /// <inheritdoc/>
        public IProductKitWorkflow ProductKits => GetWorkflow<IProductKitWorkflow>();

        /// <inheritdoc/>
        public IUploadWorkflow Uploads => GetWorkflow<IUploadWorkflow>();

        /// <inheritdoc/>
        public IPricingFactory PricingFactory => GetWorkflow<IPricingFactory>();

        /// <inheritdoc/>
        public ICartValidator CartValidator => GetWorkflow<ICartValidator>();

        /// <inheritdoc/>
        public IAssociateJsonAttributesWorkflow AssociateJsonAttributes => GetWorkflow<IAssociateJsonAttributesWorkflow>();

        /// <inheritdoc/>
        public TIWorkflow GetWorkflow<TIWorkflow>()
        {
            var type = typeof(TIWorkflow);
            if (workflows.ContainsKey(type))
            {
                return (TIWorkflow)workflows[type];
            }
            try
            {
                workflows[type] = RegistryLoaderWrapper.GetInstance<TIWorkflow>()!;
                return (TIWorkflow)workflows[type];
            }
            catch (ArgumentException ex)
            {
                if (!ex.Message.Contains("An item with the same key has already been added."))
                {
                    throw;
                }
                // Try again
                return GetWorkflow<TIWorkflow>();
            }
        }
    }
}
