// <copyright file="AddressValidationService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address validation service class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.AddressValidation
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>A validate address.</summary>
    /// <seealso cref="IReturn{AddressValidationModel}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInStoreAdmin,
     Route("/Providers/Geography/ValidateAddress", "POST", Summary = "Use to validate address with AddressProvider")]
    public class ValidateAddress : IAddressValidationRequestModel, IReturn<AddressValidationResultModel>
    {
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? AccountContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? AddressID { get; set; }

        /// <inheritdoc cref="IAddressValidationRequestModel.Address"/>
        [DefaultValue(null)]
        public AddressModel? Address { get; set; }

        /// <inheritdoc/>
        IAddressModel? IAddressValidationRequestModel.Address { get => Address; set => Address = (AddressModel?)value; }
    }

    /// <summary>The address validation service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class AddressValidationService : ClarityEcommerceServiceBase
    {
        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>An AddressValidationModel.</returns>
        public async Task<object> Post(ValidateAddress request)
        {
            var provider = RegistryLoaderWrapper.GetAddressValidationProvider(ServiceContextProfileName);
            if (provider == null)
            {
                return (AddressValidationResultModel)new AddressValidationResultModel(request)
                    .WithSuccess(null, "AddressValidationProvider was null");
            }
            var result = await provider.ValidateAddressAsync(request, null).ConfigureAwait(false);
            if (result == null)
            {
                return (AddressValidationResultModel)new AddressValidationResultModel(request)
                    .WithFailure("Address could not be validated");
            }
            if (!result.IsValid)
            {
                // Mark Address as neither primary billing nor primary shipping if the address is bad.
                if (Contract.CheckValidID(request.AccountContactID))
                {
                    await Workflows.AccountContacts.MarkAccountContactAsNeitherBillingNorShippingAsync(
                            request.AccountContactID!.Value,
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                }
                return (AddressValidationResultModel)result;
            }
            result.AccountContactID = request.AccountContactID;
            result.ContactID = request.ContactID;
            result.AddressID = request.AddressID;
            // ReSharper disable once InvertIf
            if (result.MergedAddress != null)
            {
                result.MergedAddress.Region = await Workflows.Regions.GetByCodeAsync(
                        result.MergedAddress.RegionCode!,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
                result.MergedAddress.Country = await Workflows.Countries.GetByCodeAsync(
                        result.MergedAddress.CountryCode == "US"
                            ? "USA"
                            : result.MergedAddress.CountryCode!,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            return (AddressValidationResultModel)result;
        }
    }

    /// <summary>A target order checkout feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class AddressValidationFeature : IPlugin
    {
        /// <summary>Registers this AddressValidationFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
