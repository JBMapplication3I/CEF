// <copyright file="IAmATrackedEventWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmATrackedEventWorkflowBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using DataModel;
    using Models;

    /// <summary>Interface for workflow for tracked event bases.</summary>
    /// <typeparam name="TIModel">       Type of the ti model.</typeparam>
    /// <typeparam name="TISearchModel"> Type of the ti search model.</typeparam>
    /// <typeparam name="TIEntity">      Type of the ti entity.</typeparam>
    /// <typeparam name="TEntity">       Type of the entity.</typeparam>
    /// <typeparam name="TIStatusModel"> Type of the ti status model.</typeparam>
    /// <typeparam name="TIStatusEntity">Type of the ti status entity.</typeparam>
    /// <seealso cref="INameableWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public interface IAmATrackedEventWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity, TIStatusModel, TIStatusEntity>
        : INameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, IAmATrackedEventBaseModel<TIStatusModel>
        where TIEntity : IAmATrackedEventBase<TIStatusEntity>
        where TEntity : class, TIEntity, new()
        where TIStatusModel : IStatusableBaseModel
        where TIStatusEntity : IStatusableBase
    {
    }
}
