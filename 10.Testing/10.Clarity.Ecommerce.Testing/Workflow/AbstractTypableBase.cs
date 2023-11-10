// <copyright file="AbstractTypableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the abstract typable base class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;

    public abstract class AbstractTypableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        : AbstractDisplayableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        where TIEntity : ITypableBase
        where TEntity : class, TIEntity, new()
        where TIModel : ITypableBaseModel
        where TModel : class, TIModel, new()
        where TISearchModel : ITypableBaseSearchModel
        where TSearchModel : class, TISearchModel, new()
        where TWorkflow : ITypableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>, new()
    {
        protected AbstractTypableBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }
    }
}
