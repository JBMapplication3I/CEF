// <copyright file="RegionCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class RegionWorkflow
    {
        /// <inheritdoc/>
        public virtual async Task<int?> CheckExistsByCodeAsync(string code, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CheckExistsByCodeAsync(code, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<int?> CheckExistsByCodeAsync(string code, IClarityEcommerceEntities context)
        {
            return context.Regions
                .FilterRegionsByCode(Contract.RequiresValidKey(code), true, false)
                .OrderByDescending(x => x.Active)
                .ThenBy(x => x.ID)
                .Select(x => (int?)x.ID)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<IRegionModel?> GetByCodeAsync(string code, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetByCodeAsync(code, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<IRegionModel?> GetByCodeAsync(string code, IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterRegionsByCode(code, true, false)
                    .SelectFirstFullRegionAndMapToRegionModel(context.ContextProfileName));
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ValidateRestrictedRegionAsync(int countryID, int regionID, string? contextProfileName)
        {
            if (!Contract.CheckValidID(regionID) || !Contract.CheckValidID(countryID))
            {
                return CEFAR.FailingCEFAR("No region ID or country ID to validate");
            }
            var region = await GetAsync(regionID, contextProfileName).ConfigureAwait(false);
            if (region == null)
            {
                return CEFAR.FailingCEFAR($"Region with ID '{regionID}' not found");
            }
            if (!region.Active || !region.SerializableAttributes!.ContainsKey("Restricted"))
            {
                return CEFAR.FailingCEFAR("No restrictions for this region");
            }
            var restricted = $"{region.SerializableAttributes["Restricted"].Value}";
            if (!bool.TryParse(restricted, out var restrictedBool))
            {
                return CEFAR.FailingCEFAR("Unable to parse restriction value for region");
            }
            if (!restrictedBool || region.CountryID != countryID)
            {
                return CEFAR.FailingCEFAR("Region is not restricted");
            }
            return CEFAR.PassingCEFAR("Region is restricted");
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Region>> FilterQueryByModelCustomAsync(
            IQueryable<Region> query,
            IRegionSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterRegionsByID(search.ID, search.CountryID);
        }
    }
}
