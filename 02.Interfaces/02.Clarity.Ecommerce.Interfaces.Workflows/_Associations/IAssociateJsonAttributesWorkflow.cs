// <copyright file="IAssociateJsonAttributesWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAssociateJsonAttributesWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Models;

    /// <summary>Interface for associate JSON attributes workflow.</summary>
    public interface IAssociateJsonAttributesWorkflow
    {
        /// <summary>Associate objects.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task AssociateObjectsAsync(
            IHaveJsonAttributesBase entity,
            IHaveJsonAttributesBaseModel model,
            string? contextProfileName);

        /// <summary>Associate objects.</summary>
        /// <param name="entity"> The entity.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        Task AssociateObjectsAsync(
            IHaveJsonAttributesBase entity,
            IHaveJsonAttributesBaseModel model,
            IClarityEcommerceEntities context);
    }
}
