// <copyright file="ProductSubscriptionTypeCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product subscription type workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Mapper;

    public partial class ProductSubscriptionTypeWorkflow
    {
        /// <inheritdoc/>
        public Task<List<IProductSubscriptionTypeModel>> GetBySubscriptionTypeAsync(
            int subscriptionTypeID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.ProductSubscriptionTypes
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterProductSubscriptionTypesBySubscriptionTypeID(subscriptionTypeID)
                    .SelectListProductSubscriptionTypeAndMapToProductSubscriptionTypeModel(contextProfileName)
                    .ToList());
        }
    }
}
