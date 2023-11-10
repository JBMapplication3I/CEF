// <copyright file="GeneralAttributeCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the general attribute workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class GeneralAttributeWorkflow
    {
        /// <inheritdoc/>
        public Task<List<IGeneralAttributeModel>> GetAllAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.GeneralAttributes
                    .AsNoTracking()
                    .FilterByActive(true)
                    .SelectListGeneralAttributeAndMapToGeneralAttributeModel(contextProfileName)
                    .ToList());
        }

        /// <inheritdoc/>
        protected override Task AssignAdditionalPropertiesAsync(
            IGeneralAttribute entity,
            IGeneralAttributeModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.TypeDisplayName))
            {
                model.TypeKey = "GENERAL";
                model.TypeName = "General";
            }
            return base.AssignAdditionalPropertiesAsync(entity, model, timestamp, context);
        }
    }
}
