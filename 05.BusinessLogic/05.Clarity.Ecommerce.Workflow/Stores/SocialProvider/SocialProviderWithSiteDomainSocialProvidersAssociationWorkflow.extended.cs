// <copyright file="SocialProviderWithSiteDomainSocialProvidersAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate store brands workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class SocialProviderWithSiteDomainSocialProvidersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            ISiteDomainSocialProviderModel model,
            ISiteDomainSocialProvider entity,
            IClarityEcommerceEntities context)
        {
            return entity.Script == model.Script
                && entity.UrlValues == model.UrlValues;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            ISiteDomainSocialProvider newEntity,
            ISiteDomainSocialProviderModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.SiteDomains.ResolveWithAutoGenerateToIDAsync(
                    model.MasterID,
                    model.MasterKey,
                    model.MasterName,
                    null,
                    context)
                .ConfigureAwait(false);
        }
    }
}
