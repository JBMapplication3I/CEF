// <copyright file="ServiceAreaModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service model class.</summary>
namespace Clarity.Ecommerce.Models
{
    using Clarity.Ecommerce.Interfaces.Models;

    public partial class ServiceAreaModel
    {
        #region ServiceArea Properties
        /// <inheritdoc/>
        public decimal? Radius { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int ContractorID { get; set; }

        /// <inheritdoc/>
        public string? ContractorKey { get; set; }

        /// <inheritdoc cref="IServiceAreaModel.Contractor"/>
        public ContractorModel? Contractor { get; set; }

        /// <inheritdoc/>
        IContractorModel? IServiceAreaModel.Contractor { get => Contractor; set => Contractor = (ContractorModel?)value; }

        /// <inheritdoc/>
        public int AddressID { get; set; }

        /// <inheritdoc/>
        public string? AddressKey { get; set; }

        /// <inheritdoc cref="IServiceAreaModel.Address"/>
        public AddressModel? Address { get; set; }

        /// <inheritdoc/>
        IAddressModel? IServiceAreaModel.Address { get => Address; set => Address = (AddressModel?)value; }
        #endregion
    }
}
