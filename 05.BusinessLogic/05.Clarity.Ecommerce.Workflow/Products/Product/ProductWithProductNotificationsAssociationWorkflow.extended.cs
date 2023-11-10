// <copyright file="ProductWithProductNotificationsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate product notifications workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithProductNotificationsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IProductNotificationModel model)
        {
            return Contract.CheckAllValid(model.Name, model.Description);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IProductNotificationModel model,
            IProductNotification entity,
            IClarityEcommerceEntities context)
        {
            return entity.Name == model.Name
                && entity.Description == model.Description;
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            IProductNotification newEntity,
            IProductNotificationModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.Name = model.Name;
            newEntity.Description = model.Description;
            return Task.CompletedTask;
        }
    }
}
