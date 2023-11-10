// <copyright file="IAssociateObjectsWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAssociateObjectsWorkflowBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Models;

    /// <summary>Interface for associate objects workflow base.</summary>
    /// <typeparam name="TIMasterModel"> Type of the master's model interface.</typeparam>
    /// <typeparam name="TIMasterEntity">Type of the master's entity interface.</typeparam>
    public interface IAssociateObjectsWorkflowBase<in TIMasterModel, in TIMasterEntity>
        where TIMasterModel : IBaseModel
        where TIMasterEntity : IBase
    {
        /// <summary>Associate objects.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task AssociateObjectsAsync(
            TIMasterEntity entity,
            TIMasterModel model,
            DateTime timestamp,
            string? contextProfileName);

        /// <summary>Associate objects.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        Task AssociateObjectsAsync(
            TIMasterEntity entity,
            TIMasterModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context);
    }
}
