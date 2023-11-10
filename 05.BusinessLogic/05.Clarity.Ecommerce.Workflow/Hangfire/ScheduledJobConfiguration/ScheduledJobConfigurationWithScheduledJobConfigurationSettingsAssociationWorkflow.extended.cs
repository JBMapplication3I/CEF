// <copyright file="ScheduledJobConfigurationWithScheduledJobConfigurationSettingsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate scheduled job configuration settings workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class ScheduledJobConfigurationWithScheduledJobConfigurationSettingsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IScheduledJobConfigurationSettingModel model,
            IScheduledJobConfigurationSetting entity,
            IClarityEcommerceEntities context)
        {
            return entity.SlaveID == model.SlaveID
                && entity.MasterID == model.MasterID
                && entity.Slave!.TypeID == model.Slave!.TypeID
                && entity.Slave.Value == model.Slave.Value;
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(
            IScheduledJobConfigurationSettingModel model)
        {
            return (model.Slave?.ID ?? model.SlaveID) > 0
                || model.Slave?.TypeID > 0
                || !string.IsNullOrWhiteSpace(model.Slave?.TypeKey ?? model.Slave?.Type?.CustomKey);
        }

        /// <inheritdoc/>
        protected override Task DeactivateObjectAdditionalPropertiesAsync(
            IScheduledJobConfigurationSetting entity,
            DateTime timestamp)
        {
            if (entity.Slave == null)
            {
                return Task.CompletedTask;
            }
            entity.Slave.UpdatedDate = timestamp;
            entity.Slave.Active = false;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IScheduledJobConfigurationSetting newEntity,
            IScheduledJobConfigurationSettingModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            var setting = RegistryLoaderWrapper.GetInstance<ISetting>(context.ContextProfileName);
            setting.Active = true;
            setting.CreatedDate = timestamp;
            setting.CustomKey = model.SlaveKey ?? model.Slave!.CustomKey;
            setting.Value = model.Slave!.Value;
            setting.TypeID = await Workflows.SettingTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: model.Slave.TypeID,
                    byKey: model.Slave.TypeKey,
                    byName: model.Slave.TypeName,
                    byDisplayName: model.Slave.TypeDisplayName,
                    model: model.Slave.Type,
                    context: context)
                .ConfigureAwait(false);
            newEntity.Slave = (Setting)setting;
        }
    }
}
