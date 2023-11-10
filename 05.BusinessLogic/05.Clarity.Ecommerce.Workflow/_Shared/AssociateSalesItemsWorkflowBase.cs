// <copyright file="AssociateSalesItemsWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate sales items workflow base class</summary>
// ReSharper disable StyleCop.SA1618
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    /// <summary>An associate sales items workflow base.</summary>
    /// <typeparam name="TIMasterModel">              Type of the master's model interface.</typeparam>
    /// <typeparam name="TMasterEntity">              Type of the master's entity.</typeparam>
    /// <typeparam name="TMasterStatusEntity">        Type of the master's status entity.</typeparam>
    /// <typeparam name="TMasterTypeEntity">          Type of the master's type entity.</typeparam>
    /// <typeparam name="TMasterStateEntity">         Type of the master's state entity.</typeparam>
    /// <typeparam name="TMasterStoredFileEntity">    Type of the master's Stored File entity.</typeparam>
    /// <typeparam name="TIMasterStoredFileModel">    Type of the master's stored file model interface.</typeparam>
    /// <typeparam name="TMasterContactEntity">       Type of the master's model entity.</typeparam>
    /// <typeparam name="TIMasterContactModel">       Type of the master's contact model interface.</typeparam>
    /// <typeparam name="TMasterSalesEventEntity">    Type of the master's sales event entity.</typeparam>
    /// <typeparam name="TIMasterSalesEventModel">    Type of the master's sales event model's interface.</typeparam>
    /// <typeparam name="TMasterSalesEventTypeEntity">Type of the master's sales event type entity.</typeparam>
    /// <typeparam name="TMasterDiscountEntity">      Type of the master's discount entity.</typeparam>
    /// <typeparam name="TIMasterDiscountModel">      Type of the master's discount model interface.</typeparam>
    /// <typeparam name="TISlaveEntity">              Type of the slave's entity interface.</typeparam>
    /// <typeparam name="TSlaveEntity">               Type of the slave's entity.</typeparam>
    /// <typeparam name="TSlaveTarget">               Type of the slave's model interface.</typeparam>
    /// <typeparam name="TSlaveDiscountEntity">       Type of the slave's discount entity.</typeparam>
    /// <typeparam name="TISlaveDiscountModel">       Type of the slave's discount model interface.</typeparam>
    /// <seealso cref="AssociateObjectsWorkflowBase{TIMasterModel, TMasterEntity, ISalesItemBaseModel, TISlaveEntity, TSlaveEntity}"/>
    public abstract class AssociateSalesItemsWorkflowBase<TIMasterModel,
          TMasterEntity, TMasterStatusEntity, TMasterTypeEntity, TMasterStateEntity, TMasterStoredFileEntity,
          TIMasterStoredFileModel, TMasterContactEntity, TIMasterContactModel, TMasterSalesEventEntity, TIMasterSalesEventModel,
          TMasterSalesEventTypeEntity, TMasterDiscountEntity, TIMasterDiscountModel, TISlaveEntity, TSlaveEntity, TSlaveTarget,
          TSlaveDiscountEntity, TISlaveDiscountModel>
        : AssociateObjectsWorkflowBase<TIMasterModel, TMasterEntity, ISalesItemBaseModel<TISlaveDiscountModel>, TISlaveEntity, TSlaveEntity>
        // Master
        where TIMasterModel : ISalesCollectionBaseModel<ITypeModel, TIMasterStoredFileModel, TIMasterContactModel, TIMasterSalesEventModel, TIMasterDiscountModel, TISlaveDiscountModel>
        where TMasterEntity : class, ISalesCollectionBase<TMasterEntity, TMasterStatusEntity, TMasterTypeEntity, TSlaveEntity,
            TMasterDiscountEntity, TMasterStateEntity, TMasterStoredFileEntity, TMasterContactEntity, TMasterSalesEventEntity, TMasterSalesEventTypeEntity>
        where TIMasterStoredFileModel : IAmAStoredFileRelationshipTableModel
        where TIMasterContactModel : IAmAContactRelationshipTableModel
        where TMasterDiscountEntity : IAppliedDiscountBase<TMasterEntity, TMasterDiscountEntity>
        where TIMasterDiscountModel : IAppliedDiscountBaseModel
        // Slave
        where TISlaveEntity : ISalesItemBase<TSlaveEntity, TSlaveDiscountEntity, TSlaveTarget>
        where TSlaveEntity : class, TISlaveEntity, new()
        where TSlaveTarget : ISalesItemTargetBase
        where TSlaveDiscountEntity : IAppliedDiscountBase<TSlaveEntity, TSlaveDiscountEntity>
        where TISlaveDiscountModel : IAppliedDiscountBaseModel
        where TMasterTypeEntity : class, ITypableBase, new()
        where TMasterStatusEntity : class, IStatusableBase
        where TMasterStateEntity : class, IStateableBase
        where TMasterStoredFileEntity : IAmAStoredFileRelationshipTable<TMasterEntity>
        where TMasterContactEntity : IAmAContactRelationshipTable<TMasterEntity, TMasterContactEntity>
        where TMasterSalesEventEntity : ISalesEventBase<TMasterEntity, TMasterSalesEventTypeEntity>
        where TMasterSalesEventTypeEntity : ITypableBase
        where TIMasterSalesEventModel : ISalesEventBaseModel
    {
        /// <inheritdoc/>
        protected override List<ISalesItemBaseModel<TISlaveDiscountModel>>? GetModelObjectsList(TIMasterModel model)
        {
            return model.SalesItems;
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(ISalesItemBaseModel<TISlaveDiscountModel> model)
        {
            return model.TotalQuantity > 0m
                && Contract.CheckAllValid(model.ProductID, model.Name, model.ProductKey, model.Sku);
        }

        /// <inheritdoc/>
        protected override Task AddObjectToObjectsListAsync(TMasterEntity entity, TISlaveEntity newObject)
        {
            if (newObject == null)
            {
                return Task.CompletedTask;
            }
            InitializeObjectListIfNull(entity);
            GetObjectsCollection(entity)!.Add((TSlaveEntity)newObject);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(TMasterEntity entity)
        {
            // New DataModel.Product may not have ProductAttributes initialized yet
            if (GetObjectsCollection(entity) == null)
            {
                entity.SalesItems = new HashSet<TSlaveEntity>();
            }
        }

        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAsync(
            ISalesItemBaseModel<TISlaveDiscountModel> model,
            TISlaveEntity entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.Sku?.Trim() == model.Sku?.Trim()
                    && entity.ForceUniqueLineItemKey?.Trim() == model.ForceUniqueLineItemKey?.Trim());
        }

        /// <inheritdoc/>
        protected override ICollection<TSlaveEntity>? GetObjectsCollection(TMasterEntity entity)
        {
            return entity.SalesItems;
        }

        /// <inheritdoc/>
        protected override Task DeactivateObjectAsync(TISlaveEntity entity, DateTime timestamp)
        {
            entity.UpdatedDate = timestamp;
            entity.Active = false;
            return Task.CompletedTask;
        }
    }
}
