// <copyright file="AdWithCampaignAdsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate ad campaigns workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class AdWithCampaignAdsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            ICampaignAd newEntity,
            ICampaignAdModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Campaigns.ResolveWithAutoGenerateToIDAsync(
                    model.MasterID,
                    model.MasterKey,
                    model.MasterName,
                    null,
                    context)
                .ConfigureAwait(false);
        }
    }
}
