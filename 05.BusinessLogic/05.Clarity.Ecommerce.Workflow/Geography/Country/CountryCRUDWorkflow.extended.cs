// <copyright file="CountryCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class CountryWorkflow
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
            return context.Countries
                .FilterCountriesByCode(Contract.RequiresValidKey(code), true)
                .OrderByDescending(x => x.Active)
                .ThenBy(x => x.ID)
                .Select(x => (int?)x.ID)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<ICountryModel?> GetByCodeAsync(string code, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetByCodeAsync(code, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<ICountryModel?> GetByCodeAsync(string code, IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCountriesByCode(Contract.RequiresValidKey(code), true)
                    .SelectFirstFullCountryAndMapToCountryModel(context.ContextProfileName));
        }
    }
}
