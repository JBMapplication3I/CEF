// <copyright file="IServiceAreaModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service area model interface.</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IServiceAreaModel
    {
        #region ServiceArea Properties
        /// <summary>Gets or sets the radius (in miles) of this service area.</summary>
        /// <value>The radius (in miles) of this service area.</value>
        decimal? Radius { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the ID of the contractor.</summary>
        /// <value>The ID of the contractor.</value>
        int ContractorID { get; set; }

        /// <summary>Gets or sets the custom key of the contractor.</summary>
        /// <value>The custom key of the contractor.</value>
        string? ContractorKey { get; set; }

        /// <summary>Gets or sets the contractor.</summary>
        /// <value>The contractor.</value>
        IContractorModel? Contractor { get; set; }

        /// <summary>Gets or sets the ID of the address where this service area is centered.</summary>
        /// <value>The ID of the address where this service area is centered.</value>
        int AddressID { get; set; }

        /// <summary>Gets or sets the custom key of the address where this service area is centered.</summary>
        /// <value>The custom key of the address where this service area is centered.</value>
        string? AddressKey { get; set; }

        /// <summary>Gets or sets the address where this service area is centered.</summary>
        /// <value>The address where this service area is centered.</value>
        IAddressModel? Address { get; set; }
        #endregion
    }
}
