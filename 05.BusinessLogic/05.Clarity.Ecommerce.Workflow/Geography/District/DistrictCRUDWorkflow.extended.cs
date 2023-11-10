// <copyright file="DistrictCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the district workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class DistrictWorkflow
    {
        /// <inheritdoc/>
        public override Task<IDistrictModel?> GetByNameAsync(string name, IClarityEcommerceEntities context)
        {
            throw new InvalidOperationException("This operation requires additional information. See GetDistrictByNameAndRegionIDAsync");
        }

        /// <inheritdoc/>
        public virtual Task<IDistrictModel?> GetDistrictByNameAndRegionIDAsync(int regionID, string name, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(context.Districts
                .FilterByActive(true)
                .FilterByName(name, true)
                .Where(x => x.RegionID == regionID)
                .SelectFirstFullDistrictAndMapToDistrictModel(contextProfileName));
        }
    }
}
