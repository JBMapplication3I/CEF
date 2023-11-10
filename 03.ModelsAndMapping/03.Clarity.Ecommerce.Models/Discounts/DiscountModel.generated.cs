// <autogenerated>
// <copyright file="DiscountModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Model Interfaces generated to provide base setups</summary>
// <remarks>This file was auto-generated by Models.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#pragma warning disable 618 // Ignore Obsolete warnings
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data transfer model for Discount.</summary>
    public partial class DiscountModel
        : NameableBaseModel
            , IDiscountModel
    {
        #region IAmFilterableByAccountModel<IDiscountAccountModel> Properties
        /// <inheritdoc cref="IAmFilterableByAccountModel{IDiscountAccountModel}.Accounts"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Accounts), DataType = "List<DiscountAccountModel>", ParameterType = "body", IsRequired = false,
                Description = "Accounts this object is associated with")]
        public List<DiscountAccountModel>? Accounts { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountAccountModel>? IAmFilterableByAccountModel<IDiscountAccountModel>.Accounts { get => Accounts?.ToList<IDiscountAccountModel>(); set => Accounts = value?.Cast<DiscountAccountModel>().ToList(); }
        #endregion
        #region IAmFilterableByBrandModel<> Properties
        /// <inheritdoc cref="IAmFilterableByBrandModel{IDiscountBrandModel}.Brands"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Brands), DataType = "List<DiscountBrandModel>", ParameterType = "body", IsRequired = false,
                Description = "Brands this object is associated with")]
        public List<DiscountBrandModel>? Brands { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountBrandModel>? IAmFilterableByBrandModel<IDiscountBrandModel>.Brands { get => Brands?.ToList<IDiscountBrandModel>(); set => Brands = value?.Cast<DiscountBrandModel>().ToList(); }
        #endregion
        #region IAmFilterableByFranchiseModel<> Properties
        /// <inheritdoc cref="IAmFilterableByFranchiseModel{IDiscountFranchiseModel}.Franchises"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Franchises), DataType = "List<DiscountFranchiseModel>", ParameterType = "body", IsRequired = false,
                Description = "Franchises this object is associated with")]
        public List<DiscountFranchiseModel>? Franchises { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountFranchiseModel>? IAmFilterableByFranchiseModel<IDiscountFranchiseModel>.Franchises { get => Franchises?.ToList<IDiscountFranchiseModel>(); set => Franchises = value?.Cast<DiscountFranchiseModel>().ToList(); }
        #endregion
        #region IAmFilterableByManufacturerModel<> Properties
        /// <inheritdoc cref="IAmFilterableByManufacturerModel{IDiscountManufacturerModel}.Manufacturers"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Manufacturers), DataType = "List<DiscountManufacturerModel>", ParameterType = "body", IsRequired = false,
                Description = "Manufacturers this object is associated with")]
        public List<DiscountManufacturerModel>? Manufacturers { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountManufacturerModel>? IAmFilterableByManufacturerModel<IDiscountManufacturerModel>.Manufacturers { get => Manufacturers?.ToList<IDiscountManufacturerModel>(); set => Manufacturers = value?.Cast<DiscountManufacturerModel>().ToList(); }
        #endregion
        #region IAmFilterableByProductModel<> Properties
        /// <inheritdoc cref="IAmFilterableByProductModel{IDiscountProductModel}.Products"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Products), DataType = "List<DiscountProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Products this object is associated with")]
        public List<DiscountProductModel>? Products { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountProductModel>? IAmFilterableByProductModel<IDiscountProductModel>.Products { get => Products?.ToList<IDiscountProductModel>(); set => Products = value?.Cast<DiscountProductModel>().ToList(); }
        #endregion
        #region IAmFilterableByStoreModel<> Properties
        /// <inheritdoc cref="IAmFilterableByStoreModel{IDiscountStoreModel}.Stores"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Stores), DataType = "List<DiscountStoreModel>", ParameterType = "body", IsRequired = false,
                Description = "Stores this object is associated with")]
        public List<DiscountStoreModel>? Stores { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountStoreModel>? IAmFilterableByStoreModel<IDiscountStoreModel>.Stores { get => Stores?.ToList<IDiscountStoreModel>(); set => Stores = value?.Cast<DiscountStoreModel>().ToList(); }
        #endregion
        #region IAmFilterableByUserModel<> Properties
        /// <inheritdoc cref="IAmFilterableByUserModel{IDiscountUserModel}.Users"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Users), DataType = "List<DiscountUserModel>", ParameterType = "body", IsRequired = false,
                Description = "Users this object is associated with")]
        public List<DiscountUserModel>? Users { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountUserModel>? IAmFilterableByUserModel<IDiscountUserModel>.Users { get => Users?.ToList<IDiscountUserModel>(); set => Users = value?.Cast<DiscountUserModel>().ToList(); }
        #endregion
        #region IAmFilterableByVendorModel<> Properties
        /// <inheritdoc cref="IAmFilterableByVendorModel{IDiscountVendorModel}.Vendors"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Vendors), DataType = "List<DiscountVendorModel>", ParameterType = "body", IsRequired = false,
                Description = "Vendors this object is associated with")]
        public List<DiscountVendorModel>? Vendors { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IDiscountVendorModel>? IAmFilterableByVendorModel<IDiscountVendorModel>.Vendors { get => Vendors?.ToList<IDiscountVendorModel>(); set => Vendors = value?.Cast<DiscountVendorModel>().ToList(); }
        #endregion
    }
}
