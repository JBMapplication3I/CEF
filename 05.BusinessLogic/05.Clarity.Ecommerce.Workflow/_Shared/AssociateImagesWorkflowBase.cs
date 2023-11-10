// <copyright file="AssociateImagesWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate images workflow base class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    /// <summary>An associate images workflow base.</summary>
    /// <typeparam name="TIMasterModel"> Type of the ti master model.</typeparam>
    /// <typeparam name="TIMasterEntity">Type of the ti master entity.</typeparam>
    /// <typeparam name="TISlaveModel">  Type of the ti slave model.</typeparam>
    /// <typeparam name="TISlaveEntity"> Type of the ti slave entity.</typeparam>
    /// <typeparam name="TSlaveEntity">  Type of the slave entity.</typeparam>
    /// <seealso cref="AssociateObjectsWorkflowBase{TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity}"/>
    public abstract class AssociateImagesWorkflowBase<TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity>
        : AssociateObjectsWorkflowBase<TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity>
        where TIMasterModel : IBaseModel
        where TIMasterEntity : IBase
        where TISlaveModel : IBaseModel, IImageBaseModel<ITypeModel>
        where TISlaveEntity : IBase, IImageBase
        where TSlaveEntity : class, TISlaveEntity, new()
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            TISlaveModel model,
            TISlaveEntity entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.Name == model.Name
                && entity.Description == model.Description
                && entity.DisplayName == model.DisplayName
                && entity.SeoTitle == model.SeoTitle
                && entity.IsPrimary == model.IsPrimary
                && entity.OriginalIsStoredInDB == model.OriginalIsStoredInDB
                && entity.ThumbnailIsStoredInDB == model.ThumbnailIsStoredInDB
                && entity.TypeID == model.TypeID
                && entity.Author == model.Author
                && entity.Copyright == model.Copyright
                && entity.Latitude == model.Latitude
                && entity.Longitude == model.Longitude
                && entity.OriginalBytes == model.OriginalBytes
                && entity.OriginalFileFormat == model.OriginalFileFormat
                && entity.OriginalFileName == model.OriginalFileName
                && entity.OriginalHeight == model.OriginalHeight
                && entity.OriginalWidth == model.OriginalWidth
                && entity.ThumbnailBytes == model.ThumbnailBytes
                && entity.ThumbnailFileFormat == model.ThumbnailFileFormat
                && entity.ThumbnailFileName == model.ThumbnailFileName
                && entity.ThumbnailHeight == model.ThumbnailHeight
                && entity.ThumbnailWidth == model.ThumbnailWidth);
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            TISlaveEntity newEntity,
            TISlaveModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(newEntity);
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
            // Image Properties
            newEntity.Author = model.Author;
            newEntity.Copyright = model.Copyright;
            newEntity.DisplayName = model.DisplayName;
            newEntity.IsPrimary = model.IsPrimary;
            newEntity.Latitude = model.Latitude;
            newEntity.Location = model.Location;
            newEntity.Longitude = model.Longitude;
            newEntity.MediaDate = model.MediaDate;
            newEntity.OriginalBytes = model.OriginalBytes;
            newEntity.OriginalFileFormat = model.OriginalFileFormat;
            newEntity.OriginalFileName = model.OriginalFileName;
            newEntity.OriginalHeight = model.OriginalHeight;
            newEntity.OriginalIsStoredInDB = model.OriginalIsStoredInDB;
            newEntity.OriginalWidth = model.OriginalWidth;
            newEntity.SeoTitle = model.SeoTitle;
            newEntity.SortOrder = model.SortOrder;
            newEntity.ThumbnailBytes = model.ThumbnailBytes;
            newEntity.ThumbnailFileFormat = model.ThumbnailFileFormat;
            newEntity.ThumbnailFileName = model.ThumbnailFileName;
            newEntity.ThumbnailHeight = model.ThumbnailHeight;
            newEntity.ThumbnailIsStoredInDB = model.ThumbnailIsStoredInDB;
            newEntity.ThumbnailWidth = model.ThumbnailWidth;
            // Exit
            return Task.CompletedTask;
        }
    }
}
