// <copyright file="AbstractStatusableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the abstract statusable base class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;

    public abstract class AbstractStatusableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        : AbstractDisplayableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        where TIEntity : IStatusableBase
        where TEntity : class, TIEntity, new()
        where TIModel : IStatusableBaseModel
        where TModel : class, TIModel, new()
        where TISearchModel : IStatusableBaseSearchModel
        where TSearchModel : class, TISearchModel, new()
        where TWorkflow : IStatusableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>, new()
    {
        protected AbstractStatusableBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }
    }
}
