// <copyright file="IWorkflowsController.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWorkflowsController interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using Ecommerce.Providers.Discounts;
    using Providers.CartValidation;
    using Providers.Pricing;

    public partial interface IWorkflowsController
    {
        /// <summary>Gets the manager for discount.</summary>
        /// <value>The discount manager.</value>
        IDiscountManager DiscountManager { get; }

        /// <summary>Gets the address books.</summary>
        /// <value>The address books.</value>
        IAddressBookWorkflow AddressBooks { get; }

        /// <summary>Gets the authentication.</summary>
        /// <value>The authentication.</value>
        IAuthenticationWorkflow Authentication { get; }

        /// <summary>Gets the product kits.</summary>
        /// <value>The product kits.</value>
        IProductKitWorkflow ProductKits { get; }

        /// <summary>Gets the uploads.</summary>
        /// <value>The uploads.</value>
        IUploadWorkflow Uploads { get; }

        /// <summary>Gets the pricing factory.</summary>
        /// <value>The pricing factory.</value>
        IPricingFactory PricingFactory { get; }

        /// <summary>Gets the cart validator.</summary>
        /// <value>The cart validator.</value>
        ICartValidator CartValidator { get; }

        /// <summary>Gets the associate JSON attributes.</summary>
        /// <value>The associate JSON attributes.</value>
        IAssociateJsonAttributesWorkflow AssociateJsonAttributes { get; }

        /// <summary>Gets the workflow.</summary>
        /// <typeparam name="TIWorkflow">Type of the ti workflow.</typeparam>
        /// <returns>The workflow.</returns>
        TIWorkflow GetWorkflow<TIWorkflow>();
    }
}
