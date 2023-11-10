// <copyright file="ITypableWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITypableWorkflowBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using DataModel;
    using Models;

    /// <summary>Interface for workflow for typable bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="IDisplayableWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public interface ITypableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : IDisplayableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : ITypableBaseModel
        where TIEntity : ITypableBase
        where TEntity : class, TIEntity, new()
    {
    }
}
