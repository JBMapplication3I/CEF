// <copyright file="SettingCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Mapper;

    public partial class SettingWorkflow
    {
        /// <inheritdoc/>
        public Task<List<ISettingModel?>> GetSettingByTypeNameAsync(string name, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.Settings
                    .AsNoTracking()
                    .Include(x => x.Type)
                    .Include(x => x.SettingGroup)
                    .FilterByActive(true)
                    .FilterByTypeName<Setting, SettingType>(name, true)
                    .Select(x => ModelMapperForSetting.MapSettingModelFromEntityLite(x, contextProfileName))
                    .ToList());
        }

        /// <inheritdoc/>
        public Task<List<ISettingModel?>> GetSettingsByGroupNameAsync(string name, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.Settings
                    .AsNoTracking()
                    .Include(x => x.Type)
                    .Include(x => x.SettingGroup)
                    .FilterByActive(true)
                    .Where(x => x.SettingGroupID.HasValue && x.SettingGroup != null && x.SettingGroup.Name == name)
                    .Select(x => ModelMapperForSetting.MapSettingModelFromEntityLite(x, contextProfileName))
                    .ToList());
        }
    }
}
