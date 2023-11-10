// <copyright file="ProductWithProductSubscriptionTypesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate product subscription types workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithProductSubscriptionTypesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IProductSubscriptionTypeModel model,
            IProductSubscriptionType entity,
            IClarityEcommerceEntities context)
        {
            return entity.SortOrder == model.SortOrder;
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IProductSubscriptionTypeModel model)
        {
            // Must have something to recognize the Subscription Type by
            return Contract.CheckAnyValidID(model.SlaveID, model.Slave?.ID)
                || Contract.CheckAnyValidKey(model.SlaveKey, model.SlaveName, model.Slave?.CustomKey, model.Slave?.Name);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IProductSubscriptionType newEntity,
            IProductSubscriptionTypeModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.SubscriptionTypes.ResolveWithAutoGenerateToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
            newEntity.SortOrder = model.SortOrder;
        }
    }
}
