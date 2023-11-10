// <copyright file="AuctionRoutes.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the auction routes class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin,
     Route("/Auctions/Auctions/ByIDs", "GET",
        Summary = "Provides the same results as calling GetAuctionByID multiple times with separate IDs.")]
    public class GetAuctionsByIDs : IReturn<List<AuctionModel>>
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The store the user has selected if present")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The brand the user has selected if present")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the is vendor admin.</summary>
        /// <value>The is vendor admin.</value>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "A flag indicating that this is a vendor admin request. This can only be set by the server.")]
        public bool? IsVendorAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the vendor admin.</summary>
        /// <value>The identifier of the vendor admin.</value>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The identifier of the vendor which is logged in. This can only be set by the server.")]
        public int? VendorAdminID { get; set; }

        /// <summary>Gets or sets the auction IDs.</summary>
        /// <value>The auction IDs.</value>
        [ApiMember(Name = nameof(IDs), DataType = "List<int>", ParameterType = "query", IsRequired = true,
            Description = "The identifiers of auctions to read out")]
        // ReSharper disable once InconsistentNaming
        public List<int> IDs { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin,
     Route("/Auctions/Lots/ByIDs", "GET",
        Summary = "Provides the same results as calling GetLotByID multiple times with separate IDs.")]
    public class GetLotsByIDs : IReturn<List<LotModel>>
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The store the user has selected if present")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The brand the user has selected if present")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the is vendor admin.</summary>
        /// <value>The is vendor admin.</value>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "A flag indicating that this is a vendor admin request. This can only be set by the server.")]
        public bool? IsVendorAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the vendor admin.</summary>
        /// <value>The identifier of the vendor admin.</value>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The identifier of the vendor which is logged in. This can only be set by the server.")]
        public int? VendorAdminID { get; set; }

        /// <summary>Gets or sets the lot IDs.</summary>
        /// <value>The lot IDs.</value>
        [ApiMember(Name = nameof(IDs), DataType = "List<int>", ParameterType = "query", IsRequired = true,
            Description = "The identifiers of lots to read out")]
        // ReSharper disable once InconsistentNaming
        public List<int> IDs { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront,
     Route("/Auctions/Lots/ValidateVinNumber", "GET",
        Summary = "Validates the VIN number and if valid, then sets the VIN on the product description.")]
    public class ValidateVinNumber : IReturn<CEFActionResponse<bool>>
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "The product ID")]
        public int ProductID { get; set; }

        /// <summary>Gets or sets the VIN Number of the Vehicle.</summary>
        /// <value>The VIN Number of the Vehicle.</value>
        [ApiMember(Name = nameof(VinNumber), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The VIN Number of the Vehicle")]
        public string? VinNumber { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Auctions/Bid/BidOnGroupedLots", "POST")]
    public partial class BidOnGroupedLots : IReturn<BidModel>
    {
        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        [ApiMember(Name = nameof(GroupID), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "The group ID")]
        public int GroupID { get; set; }

        /// <summary>Gets or sets the amount of the bid.</summary>
        /// <value>The amount of the bid.</value>
        [ApiMember(Name = nameof(Amount), DataType = "decimal", ParameterType = "query", IsRequired = true,
            Description = "The amount of the bid")]
        public decimal Amount { get; set; }
    }
}
