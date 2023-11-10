// <copyright file="BasicAddressValidationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic address validation provider class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Basic
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Models;
    using Utilities;

    /// <summary>A basic address validation provider.</summary>
    /// <seealso cref="AddressValidationProviderBase"/>
    public class BasicAddressValidationProvider : AddressValidationProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BasicAddressValidationProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<IAddressValidationResultModel> ValidateAddressAsync(
            IAddressValidationRequestModel request,
            string? contextProfileName)
        {
            var resultModel = new AddressValidationResultModel(request);
            if (!Contract.CheckValidIDOrKey(request.Address?.CountryID, request.Address?.CountryCode))
            {
                return resultModel.WithFailure("Address isn't populated");
            }
            ICountryModel? country;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var query = context.Countries.AsNoTracking();
                query = Contract.CheckValidKey(request.Address!.CountryCode)
                    ? query.FilterByActive(true).FilterCountriesByCode(request.Address.CountryCode)
                    : query.FilterByID(request.Address.CountryID);
                country = (await query
                        .Select(x => new
                        {
                            x.ID,
                            x.CustomKey,
                            x.Name,
                            x.Code,
                        })
                        .ToListAsync()
                    .ConfigureAwait(false))
                    .Select(x => new CountryModel
                    {
                        ID = x.ID,
                        CustomKey = x.CustomKey,
                        Name = x.Name,
                        Code = x.Code,
                    })
                    .SingleOrDefault();
                if (country == null)
                {
                    return resultModel.WithFailure("Invalid Country");
                }
                country.Regions = (await context.Regions
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterRegionsByCountryID(Contract.RequiresValidID(country.ID))
                        .Select(x => new
                        {
                            x.ID,
                            x.CustomKey,
                            x.Name,
                            x.Code,
                        })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new RegionModel
                    {
                        ID = x.ID,
                        CustomKey = x.CustomKey,
                        Name = x.Name,
                        Code = x.Code,
                    })
                    .ToList<IRegionModel>();
            }
            request.Address.Country = country;
            request.Address.CountryID = country.ID;
            request.Address.CountryKey = country.CustomKey;
            request.Address.CountryName = country.Name;
            request.Address.CountryCode = country.Code;
            if (country.Regions.Count == 0 && Contract.CheckValidID(request.Address.RegionID))
            {
                return resultModel.WithFailure("Selected Country does not have Regions but a Region is selected");
            }
            if (country.Regions.Count > 0 && !Contract.CheckValidID(request.Address.RegionID))
            {
                return resultModel.WithFailure("No Region selected when selected Country has Regions");
            }
            if (country.Regions.Count > 0
                && Contract.CheckValidID(request.Address.RegionID)
                && country.Regions.All(x => x.ID != request.Address.RegionID))
            {
                return resultModel.WithFailure("Region selected isn't for the selected Country");
            }
            var region = country.Regions.Single(x => x.ID == request.Address.RegionID);
            request.Address.Region = region;
            request.Address.RegionID = region.ID;
            request.Address.RegionKey = region.CustomKey;
            request.Address.RegionName = region.Name;
            request.Address.RegionCode = region.Code;
            IAddressModel resolvedAddress;
            if (Contract.CheckValidID(request.AccountContactID))
            {
                var ac = (await Workflows.AccountContacts.GetAsync(
                        request.AccountContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false))!;
                ac.Slave!.Address!.SerializableAttributes![$"Validated-By-{nameof(BasicAddressValidationProvider)}"]
                    = new()
                    {
                        Key = $"Validated-By-{nameof(BasicAddressValidationProvider)}",
                        Value = Newtonsoft.Json.JsonConvert.SerializeObject(
                            new { Timestamp = DateExtensions.GenDateTime }),
                    };
                ac.Slave.Address.Street1 = request.Address.Street1;
                ac.Slave.Address.Street2 = request.Address.Street2;
                ac.Slave.Address.Street3 = request.Address.Street3;
                ac.Slave.Address.City = request.Address.City;
                ac.Slave.Address.PostalCode = request.Address.PostalCode;
                ac.Slave.Address = await Workflows.Addresses.ResolveAddressAsync(
                        ac.Slave.Address,
                        contextProfileName)
                    .ConfigureAwait(false);
                var updateResponse = await Workflows.Addresses.UpdateAsync(
                        ac.Slave.Address,
                        contextProfileName)
                    .ConfigureAwait(false);
                resolvedAddress = ac.Slave.Address = (await Workflows.Addresses.GetAsync(
                        updateResponse.Result,
                        contextProfileName)
                    .ConfigureAwait(false))!;
            }
            else if (Contract.CheckValidID(request.ContactID))
            {
                var c = (await Workflows.Contacts.GetAsync(
                        request.ContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false))!;
                c.Address!.SerializableAttributes![$"Validated-By-{nameof(BasicAddressValidationProvider)}"]
                    = new()
                    {
                        Key = $"Validated-By-{nameof(BasicAddressValidationProvider)}",
                        Value = Newtonsoft.Json.JsonConvert.SerializeObject(
                            new { Timestamp = DateExtensions.GenDateTime }),
                    };
                c.Address = await Workflows.Addresses.ResolveAddressAsync(
                        c.Address,
                        contextProfileName)
                    .ConfigureAwait(false);
                c.Address.Street1 = request.Address.Street1;
                c.Address.Street2 = request.Address.Street2;
                c.Address.Street3 = request.Address.Street3;
                c.Address.City = request.Address.City;
                c.Address.PostalCode = request.Address.PostalCode;
                var updateResponse = await Workflows.Addresses.UpdateAsync(
                        c.Address,
                        contextProfileName)
                    .ConfigureAwait(false);
                resolvedAddress = c.Address = (await Workflows.Addresses.GetAsync(
                        updateResponse.Result,
                        contextProfileName)
                    .ConfigureAwait(false))!;
            }
            else if (Contract.CheckValidID(request.AddressID))
            {
                var a = (await Workflows.Addresses.GetAsync(
                        request.AddressID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false))!;
                a.SerializableAttributes![$"Validated-By-{nameof(BasicAddressValidationProvider)}"]
                    = new()
                    {
                        Key = $"Validated-By-{nameof(BasicAddressValidationProvider)}",
                        Value = Newtonsoft.Json.JsonConvert.SerializeObject(
                            new { Timestamp = DateExtensions.GenDateTime }),
                    };
                a = await Workflows.Addresses.ResolveAddressAsync(
                        a,
                        contextProfileName)
                    .ConfigureAwait(false);
                a.Street1 = request.Address.Street1;
                a.Street2 = request.Address.Street2;
                a.Street3 = request.Address.Street3;
                a.City = request.Address.City;
                a.PostalCode = request.Address.PostalCode;
                var updateResponse = await Workflows.Addresses.UpdateAsync(
                        a,
                        contextProfileName)
                    .ConfigureAwait(false);
                resolvedAddress = (await Workflows.Addresses.GetAsync(
                        updateResponse.Result,
                        contextProfileName)
                    .ConfigureAwait(false))!;
            }
            else
            {
                // No prior DB values
                var a = await Workflows.Addresses.ResolveAddressAsync(
                        request.Address,
                        contextProfileName)
                    .ConfigureAwait(false);
                a.SerializableAttributes ??= new SerializableAttributesDictionary();
                a.SerializableAttributes[$"Validated-By-{nameof(BasicAddressValidationProvider)}"]
                    = new()
                    {
                        Key = $"Validated-By-{nameof(BasicAddressValidationProvider)}",
                        Value = Newtonsoft.Json.JsonConvert.SerializeObject(
                            new { Timestamp = DateExtensions.GenDateTime }),
                    };
                resolvedAddress = a;
            }
            return resultModel.WithSuccess(resolvedAddress, "Basic Service");
        }
    }
}
