// <copyright file="ZipCodeCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zip code workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Utilities;

    public partial class ZipCodeWorkflow
    {
        /// <inheritdoc/>
        public Task<IZipCodeModel?> GetByZipCodeValueAsync(string? zipCode, string? contextProfileName)
        {
            if (!Contract.CheckValidKey(zipCode))
            {
                return Task.FromResult<IZipCodeModel?>(null);
            }
            var zipCodeClean = zipCode!.Trim();
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.ZipCodes
                    .AsNoTracking()
                    .Where(x => x.ZipCodeValue == zipCodeClean)
                    .SelectFirstFullZipCodeAndMapToZipCodeModel(contextProfileName));
        }

        /// <inheritdoc/>
        public async Task UpdateLatitudeLongitudeBasedOnZipCodeAsync(
            IAddress addressEntity,
            IAddressModel addressModel,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.DoAutoUpdateLatitudeLongitude)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(addressEntity?.PostalCode))
            {
                return;
            }
            var postalCode = addressEntity!.PostalCode;
            var zipCode = await GetByZipCodeValueAsync(postalCode, contextProfileName).ConfigureAwait(false);
            if (zipCode?.Latitude == null || zipCode.Longitude == null)
            {
                return;
            }
            addressEntity.Latitude = zipCode.Latitude;
            addressEntity.Longitude = zipCode.Longitude;
        }
    }
}
