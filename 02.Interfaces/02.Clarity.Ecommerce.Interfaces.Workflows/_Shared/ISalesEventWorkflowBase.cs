// <copyright file="ISalesEventWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesEventWorkflowBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using DataModel;
    using Models;

    /// <summary>Interface for workflow for sales collection bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <typeparam name="TType">        Type of the type.</typeparam>
    /// <typeparam name="TMaster">      Type of the entity's master.</typeparam>
    /// <seealso cref="INameableWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public interface ISalesEventWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity, TType, TMaster>
        : INameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, ISalesEventBaseModel
        where TISearchModel : class, ISalesEventBaseSearchModel
        where TIEntity : ISalesEventBase<TMaster, TType>
        where TEntity : class, TIEntity, new()
        where TType : class, ITypableBase, new()
        where TMaster : IBase
    {
    }
}
