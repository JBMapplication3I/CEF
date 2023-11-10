// <copyright file="TypableWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflow for typable bases class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;

    /// <summary>A workflow for typable bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="DisplayableWorkflowBase{TIModel, TISearchModel, TIEntity, TEntity}"/>
    public abstract class TypableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : DisplayableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>,
            ITypableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, ITypableBaseModel
        where TISearchModel : ITypableBaseSearchModel
        where TIEntity : ITypableBase
        where TEntity : class, TIEntity, new()
    {
    }
}
