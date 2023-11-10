// <copyright file="AttributeTypeCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the attribute type workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class AttributeTypeWorkflow
    {
        /// <inheritdoc/>
        public override Task<IAttributeTypeModel?> GetAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var model = context.AttributeTypes
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByCustomKey(Contract.RequiresValidKey(key), true)
                .SelectSingleFullAttributeTypeAndMapToAttributeTypeModel(contextProfileName);
            if (model != null && context.GeneralAttributes != null)
            {
                model.GeneralAttributes = context.GeneralAttributes
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByTypeID(model.ID)
                    .SelectListGeneralAttributeAndMapToGeneralAttributeModel(contextProfileName)
                    .ToList();
            }
            return Task.FromResult(model);
        }
    }
}
