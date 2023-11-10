// <copyright file="AddressCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address workflow class</summary>
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

    public partial class AddressWorkflow
    {
        /// <inheritdoc/>
        public async Task<IAddressModel> ResolveAddressAsync(IAddressModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveAddressAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAddressModel> ResolveAddressAsync(IAddressModel model, IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(model);
            // Used for lookups
            await RelateRegionAsync(model, context).ConfigureAwait(false);
            await RelateCountryAsync(model, context).ConfigureAwait(false);
            return model;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<(decimal lat, decimal lon)>> GetLatLongForAddress(
            IAddressModel model,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(model);
            if (model.Latitude != null && model.Longitude != null)
            {
                return (model.Latitude.Value, model.Longitude.Value).WrapInPassingCEFAR();
            }
            if (!Contract.CheckValidKey(model.PostalCode))
            {
                return CEFAR.FailingCEFAR<(decimal, decimal)>(
                    "ERROR! Unable to determine latitude/longitude coordinates");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var zipLatLon = await context.ZipCodes
                .FilterByActive(true)
                .FilterZipCodesByZipCode(model.PostalCode)
                .Select(x => new
                {
                    x.Latitude,
                    x.Longitude,
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (zipLatLon?.Latitude == null || zipLatLon.Longitude == null)
            {
                return CEFAR.FailingCEFAR<(decimal, decimal)>(
                    "ERROR! Unable to determine latitude/longitude coordinates");
            }
            return (zipLatLon.Latitude.Value, zipLatLon.Longitude.Value).WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Address>> FilterQueryByModelCustomAsync(
            IQueryable<Address> query,
            IAddressSearchModel search,
            IClarityEcommerceEntities context)
        {
            var zipCode = await Workflows.ZipCodes.GetByZipCodeValueAsync(
                    search.ZipCode,
                    context.ContextProfileName)
                .ConfigureAwait(false);
            return query
                .FilterAddressesByZipCodeRadius(zipCode?.Longitude, zipCode?.Latitude, search.Radius, search.Units)
                .FilterAddressesByLatitudeLongitudeRadius(search.Latitude, search.Longitude, search.Radius, search.Units);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IAddress entity,
            IAddressModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateAddressFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await RelateRegionAsync(model, context).ConfigureAwait(false);
            await RelateCountryAsync(model, context).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <summary>Relate country.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        private static async Task RelateCountryAsync(IAddressModel model, IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(model);
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.CountryID,
                    model.CountryCode,
                    model.Country?.CustomKey,
                    model.CountryKey,
                    model.Country?.Code))
            {
                if (string.IsNullOrWhiteSpace(model.Region?.Country?.Code))
                {
                    return;
                }
                model.CountryCode = model.Region?.Country?.Code;
            }
            // Try to find it
            ICountryModel? country = null;
            if (Contract.CheckValidID(model.CountryID))
            {
                country = context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(model.CountryID!.Value)
                    .SelectFirstListCountryAndMapToCountryModel(context.ContextProfileName);
            }
            if (country == null && Contract.CheckValidKey(model.CountryCode))
            {
                country = context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCountriesByCode(model.CountryCode!, true)
                    .SelectFirstListCountryAndMapToCountryModel(context.ContextProfileName);
            }
            if (country == null && Contract.CheckValidKey(model.Country?.Code))
            {
                country = context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCountriesByCode(model.Country!.Code, true)
                    .SelectFirstListCountryAndMapToCountryModel(context.ContextProfileName);
            }
            if (country == null && Contract.CheckValidKey(model.Country?.CustomKey))
            {
                country = context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(model.Country!.CustomKey, true)
                    .SelectFirstListCountryAndMapToCountryModel(context.ContextProfileName);
            }
            if (country == null && Contract.CheckValidKey(model.CountryKey))
            {
                country = context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(model.CountryKey!, true)
                    .SelectFirstListCountryAndMapToCountryModel(context.ContextProfileName);
            }
            if (country == null && Contract.CheckValidKey(model.CountryName))
            {
                country = context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByName(model.CountryName!)
                    .SelectFirstListCountryAndMapToCountryModel(context.ContextProfileName);
            }
            if (country == null && Contract.CheckValidKey(model.Country?.Name))
            {
                country = context.Countries
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByName(model.Country!.Name)
                    .SelectFirstListCountryAndMapToCountryModel(context.ContextProfileName);
            }
            model.Country = country
                ?? throw new ArgumentException("Must provide a CountryID that matches an existing Country");
            model.CountryID = country.ID;
            model.CountryName = country.Name;
            model.CountryCode = country.Code;
            model.CountryKey = country.CustomKey;
        }

        /// <summary>Relate region.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        private static async Task RelateRegionAsync(IAddressModel model, IClarityEcommerceEntities context)
        {
            if (!Contract.CheckValidIDOrAnyValidKey(
                    Contract.RequiresNotNull(model).RegionID,
                    model.RegionCode,
                    model.Region?.CustomKey,
                    model.RegionKey,
                    model.Region?.Code))
            {
                return;
            }
            // Try to find it
            IRegionModel? region = null;
            if (Contract.CheckValidID(model.RegionID))
            {
                region = context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(model.RegionID!.Value)
                    .SelectFirstListRegionAndMapToRegionModel(context.ContextProfileName);
            }
            if (region == null && Contract.CheckValidKey(model.RegionCode))
            {
                region = context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterRegionsByCode(model.RegionCode, true, false)
                    .SelectFirstListRegionAndMapToRegionModel(context.ContextProfileName);
            }
            if (region == null && Contract.CheckValidKey(model.Region?.Code))
            {
                region = context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterRegionsByCode(model.Region!.Code, true, false)
                    .SelectFirstListRegionAndMapToRegionModel(context.ContextProfileName);
            }
            if (region == null && Contract.CheckValidKey(model.Region?.CustomKey))
            {
                region = context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(model.Region!.CustomKey, true)
                    .SelectFirstListRegionAndMapToRegionModel(context.ContextProfileName);
            }
            if (region == null && Contract.CheckValidKey(model.RegionKey))
            {
                region = context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(model.RegionKey!, true)
                    .SelectFirstListRegionAndMapToRegionModel(context.ContextProfileName);
            }
            if (region == null && Contract.CheckValidKey(model.RegionName))
            {
                region = context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByName(model.RegionName!)
                    .SelectFirstListRegionAndMapToRegionModel(context.ContextProfileName);
            }
            if (region == null && Contract.CheckValidKey(model.Region?.Name))
            {
                region = context.Regions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByName(model.Region!.Name)
                    .SelectFirstListRegionAndMapToRegionModel(context.ContextProfileName);
            }
            model.Region = region
                ?? throw new ArgumentException("Must provide a RegionID that matches an existing Region");
            model.RegionID = region.ID;
            model.RegionName = region.Name;
            model.RegionCode = region.Code;
            model.RegionKey = region.CustomKey;
        }
    }
}
