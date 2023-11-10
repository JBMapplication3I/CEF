// <copyright file="AssociateStoredFilesWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate stored files workflow base class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    /// <summary>An associate stored files workflow base.</summary>
    /// <typeparam name="TIMasterModel"> Type of the ti master model.</typeparam>
    /// <typeparam name="TIMasterEntity">Type of the ti master entity.</typeparam>
    /// <typeparam name="TISlaveModel">  Type of the ti slave model.</typeparam>
    /// <typeparam name="TISlaveEntity"> Type of the ti slave entity.</typeparam>
    /// <typeparam name="TSlaveEntity">  Type of the slave entity.</typeparam>
    /// <seealso cref="AssociateObjectsWorkflowBase{TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity}"/>
    public abstract class AssociateStoredFilesWorkflowBase<TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity>
        : AssociateObjectsWorkflowBase<TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity>
        where TIMasterModel : IBaseModel
        where TIMasterEntity : IBase
        where TISlaveModel : IAmAStoredFileRelationshipTableModel
        where TISlaveEntity : IBase, IAmAStoredFileRelationshipTable<TIMasterEntity>
        where TSlaveEntity : class, TISlaveEntity, new()
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            TISlaveModel model,
            TISlaveEntity entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                model.Name == entity.Name
                && model.Description == entity.Description
                && model.SortOrder == entity.SortOrder
                && model.FileAccessTypeID == entity.FileAccessTypeID
                && model.SeoMetaData == entity.SeoMetaData
                && model.SeoKeywords == entity.SeoKeywords
                && model.SeoPageTitle == entity.SeoPageTitle
                && model.SeoUrl == entity.SeoUrl
                && model.SeoDescription == entity.SeoDescription
                && model.SlaveID == entity.SlaveID);
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            TISlaveEntity newEntity,
            TISlaveModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(newEntity);
            model.Slave ??= context.StoredFiles.FilterByID(model.SlaveID).SelectFirstFullStoredFileAndMapToStoredFileModel(context.ContextProfileName)!;
            Contract.RequiresNotNull(model.Slave);
            model.SlaveID = 0;
            model.Slave.ID = 0;
            // Base Properties
            newEntity.Active = Contract.RequiresNotNull(model).Active;
            newEntity.CustomKey = model.CustomKey;
            newEntity.CreatedDate = model.CreatedDate;
            newEntity.UpdatedDate = model.UpdatedDate;
            newEntity.Hash = model.Hash;
            newEntity.JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary();
            // NameableBase Properties
            newEntity.Name = model.Name;
            newEntity.Description = model.Description;
            // IHaveSeoBase Properties
            newEntity.SeoMetaData = model.SeoMetaData;
            newEntity.SeoKeywords = model.SeoKeywords;
            newEntity.SeoPageTitle = model.SeoPageTitle;
            newEntity.SeoUrl = model.SeoUrl;
            newEntity.SeoDescription = model.SeoDescription;
            // Image Properties
            newEntity.SortOrder = model.SortOrder;
            newEntity.FileAccessTypeID = model.FileAccessTypeID;
            // Related Objects
            newEntity.Slave = (StoredFile)model.Slave.CreateStoredFileEntity(timestamp, context.ContextProfileName);
            // Exit
            return Task.CompletedTask;
        }
    }
}
