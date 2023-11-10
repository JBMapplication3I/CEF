// <copyright file="BrandWithBrandSiteDomainsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate Brand SiteDomains workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class BrandWithBrandSiteDomainsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IBrandSiteDomain newEntity,
            IBrandSiteDomainModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.SiteDomains.ResolveWithAutoGenerateToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
        }
    }
}
