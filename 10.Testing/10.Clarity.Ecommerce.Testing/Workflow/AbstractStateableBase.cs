// <copyright file="AbstractStateableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the abstract stateable base class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;

    public abstract class AbstractStateableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        : AbstractDisplayableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        where TIEntity : IStateableBase
        where TEntity : class, TIEntity, new()
        where TIModel : IStateableBaseModel
        where TModel : class, TIModel, new()
        where TISearchModel : IStateableBaseSearchModel
        where TSearchModel : class, TISearchModel, new()
        where TWorkflow : IStateableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>, new()
    {
        protected AbstractStateableBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }
    }
}
