// <copyright file="AddressValidationResultModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address validation result model class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation
{
    using System.ComponentModel;
    using Interfaces.Models;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using Models;

    /// <summary>A data Model for the address validation result.</summary>
    /// <seealso cref="IAddressValidationResultModel"/>
    [PublicAPI]
    public class AddressValidationResultModel : IAddressValidationResultModel
    {
        /// <summary>Initializes a new instance of the <see cref="AddressValidationResultModel"/> class.</summary>
        /// <param name="request">The request.</param>
        public AddressValidationResultModel(IAddressValidationRequestModel request)
        {
            AccountContactID = request.AccountContactID;
            ContactID = request.ContactID;
            AddressID = request.AddressID;
            SourceAddress = (AddressModel?)request.Address;
        }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? AccountContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        public bool IsValid { get; set; }

        /// <inheritdoc/>
        public string? Message { get; set; }

        /// <inheritdoc cref="IAddressValidationResultModel.SourceAddress"/>
        public AddressModel? SourceAddress { get; set; }

        /// <inheritdoc/>
        IAddressModel? IAddressValidationResultModel.SourceAddress { get => SourceAddress; set => SourceAddress = (AddressModel?)value; }

        /// <inheritdoc cref="IAddressValidationResultModel.MergedAddress"/>
        public AddressModel? MergedAddress { get; set; }

        /// <inheritdoc/>
        IAddressModel? IAddressValidationResultModel.MergedAddress { get => MergedAddress; set => MergedAddress = (AddressModel?)value; }

        /// <inheritdoc cref="IAddressValidationResultModel.ValidatedAddress"/>
        public AddressModel? ValidatedAddress { get; set; }

        /// <inheritdoc/>
        IAddressModel? IAddressValidationResultModel.ValidatedAddress { get => ValidatedAddress; set => ValidatedAddress = (AddressModel?)value; }

        /// <inheritdoc/>
        public IAddressValidationResultModel WithSuccess(IAddressModel? validatedAddress, string? message = null)
        {
            IsValid = true;
            Message = message;
            if (validatedAddress == null)
            {
                return this;
            }
            ValidatedAddress = (AddressModel)validatedAddress;
            MergedAddress = SourceAddress;
            MergedAddress!.Street1 = validatedAddress.Street1;
            MergedAddress.Street2 = validatedAddress.Street2;
            MergedAddress.Street3 = validatedAddress.Street3;
            MergedAddress.City = validatedAddress.City;
            MergedAddress.PostalCode = validatedAddress.PostalCode;
            MergedAddress.RegionCode = validatedAddress.RegionCode;
            MergedAddress.RegionName = validatedAddress.RegionName;
            MergedAddress.CountryCode = validatedAddress.CountryCode;
            MergedAddress.CountryName = validatedAddress.CountryName;
            MergedAddress.SerializableAttributes = validatedAddress.SerializableAttributes;
            return this;
        }

        /// <inheritdoc/>
        public IAddressValidationResultModel WithFailure(string? message = null)
        {
            Message = message;
            return this;
        }
    }
}
