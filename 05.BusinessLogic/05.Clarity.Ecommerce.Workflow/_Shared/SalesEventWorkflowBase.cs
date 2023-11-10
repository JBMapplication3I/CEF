// <copyright file="SalesEventWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflow for sales event bases class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;

    /// <summary>An abstract workflow base for sales event bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <typeparam name="TType">        Type of the type.</typeparam>
    /// <typeparam name="TMaster">      Type of the entity's master.</typeparam>
    public abstract class SalesEventWorkflowBase<TIModel,
            TISearchModel,
            TIEntity,
            TEntity,
            TType,
            TMaster>
        : NameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>,
            ISalesEventWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity, TType, TMaster>
        where TIModel : class, ISalesEventBaseModel
        where TISearchModel : class, ISalesEventBaseSearchModel
        where TIEntity : ISalesEventBase<TMaster, TType>
        where TEntity : class, TIEntity, new()
        where TType : class, ITypableBase, new()
        where TMaster : class, IBase
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<TEntity>> FilterQueryByModelExtensionAsync(
            IQueryable<TEntity> query,
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterBySalesEventBaseSearchModel<TEntity, TMaster, TType>(search);
        }
    }
}
