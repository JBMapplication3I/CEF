// <copyright file="StatusableWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflow for statusable bases class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;

    /// <summary>A workflow for statusable bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="DisplayableWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public abstract class StatusableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : DisplayableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>,
            IStatusableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, IStatusableBaseModel
        where TISearchModel : IStatusableBaseSearchModel
        where TIEntity : IStatusableBase
        where TEntity : class, TIEntity, new()
    {
    }
}
