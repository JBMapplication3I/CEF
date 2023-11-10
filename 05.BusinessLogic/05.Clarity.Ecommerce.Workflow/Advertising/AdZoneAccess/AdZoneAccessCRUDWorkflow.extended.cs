// <copyright file="AdZoneAccessCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad zone access workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Mapper;

    public partial class AdZoneAccessWorkflow
    {
        /// <inheritdoc/>
        public Task<List<IAdZoneAccessModel>> GetByUserIDAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.AdZoneAccesses
                    .FilterByActive(true)
                    .Where(a => a.Subscription != null && a.Subscription.UserID == userID)
                    .SelectFullAdZoneAccessAndMapToAdZoneAccessModel(contextProfileName)
                    .ToList());
        }
    }
}
