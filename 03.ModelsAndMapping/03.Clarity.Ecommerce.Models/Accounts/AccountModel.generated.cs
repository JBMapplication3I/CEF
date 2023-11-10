// <autogenerated>
// <copyright file="AccountModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Account.</summary>
    public partial class AccountModel
        : NameableBaseModel
            , IAccountModel
    {
        #region IHaveATypeBaseModel<ITypeModel>
        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeID), DataType = "int", ParameterType = "body", IsRequired = true,
                Description = "Identifier for the Type of this Account, required if no TypeModel present")]
        public int TypeID { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel{ITypeModel}.Type"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Type), IsRequired = true, DataType = "TypeModel", ParameterType = "body",
                Description = "Model for Type of this Account, required if no TypeID present")]
        public TypeModel? Type { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        ITypeModel? IHaveATypeBaseModel<ITypeModel>.Type { get => Type; set => Type = (TypeModel?)value; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Key for the Type of this Account, read-only")]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Name for the Type of this Account, read-only")]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Display Name for the Type of this object, read-only")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Translation Key for the Type of this object, read-only")]
        public string? TypeTranslationKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "Sort Order for the Type of this object, read-only")]
        public int? TypeSortOrder { get; set; }
        #endregion
        #region IHaveAStatusBaseModel<IStatusModel>
        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusID), DataType = "int", ParameterType = "body", IsRequired = true,
                Description = "Identifier for the Status of this object, required if no StatusModel present")]
        public int StatusID { get; set; }

        /// <inheritdoc cref="IHaveAStatusBaseModel{IStatusModel}.Status"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Status), DataType = "StatusModel", ParameterType = "body", IsRequired = true,
                Description = "Model for Status of this object, required if no StatusID present")]
        public StatusModel? Status { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IStatusModel? IHaveAStatusBaseModel<IStatusModel>.Status { get => Status; set => Status = (StatusModel?)value; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusKey), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Key for the Status of this object, read-only")]
        public string? StatusKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusName), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Name for the Status of this object, read-only")]
        public string? StatusName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Display Name for the Status of this object, read-only")]
        public string? StatusDisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Translation Key for the Status of this object, read-only")]
        public string? StatusTranslationKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "Sort Order for the Status of this object, read-only")]
        public int? StatusSortOrder { get; set; }
        #endregion
        #region IAmFilterableByBrandModel<> Properties
        /// <inheritdoc cref="IAmFilterableByBrandModel{IBrandAccountModel}.Brands"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Brands), DataType = "List<BrandAccountModel>", ParameterType = "body", IsRequired = false,
                Description = "Brands this object is associated with")]
        public List<BrandAccountModel>? Brands { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IBrandAccountModel>? IAmFilterableByBrandModel<IBrandAccountModel>.Brands { get => Brands?.ToList<IBrandAccountModel>(); set => Brands = value?.Cast<BrandAccountModel>().ToList(); }
        #endregion
        #region IAmFilterableByFranchiseModel<> Properties
        /// <inheritdoc cref="IAmFilterableByFranchiseModel{IFranchiseAccountModel}.Franchises"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Franchises), DataType = "List<FranchiseAccountModel>", ParameterType = "body", IsRequired = false,
                Description = "Franchises this object is associated with")]
        public List<FranchiseAccountModel>? Franchises { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IFranchiseAccountModel>? IAmFilterableByFranchiseModel<IFranchiseAccountModel>.Franchises { get => Franchises?.ToList<IFranchiseAccountModel>(); set => Franchises = value?.Cast<FranchiseAccountModel>().ToList(); }
        #endregion
        #region IAmFilterableByProductModel<> Properties
        /// <inheritdoc cref="IAmFilterableByProductModel{IAccountProductModel}.Products"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Products), DataType = "List<AccountProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Products this object is associated with")]
        public List<AccountProductModel>? Products { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IAccountProductModel>? IAmFilterableByProductModel<IAccountProductModel>.Products { get => Products?.ToList<IAccountProductModel>(); set => Products = value?.Cast<AccountProductModel>().ToList(); }
        #endregion
        #region IAmFilterableByStoreModel<> Properties
        /// <inheritdoc cref="IAmFilterableByStoreModel{IStoreAccountModel}.Stores"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Stores), DataType = "List<StoreAccountModel>", ParameterType = "body", IsRequired = false,
                Description = "Stores this object is associated with")]
        public List<StoreAccountModel>? Stores { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IStoreAccountModel>? IAmFilterableByStoreModel<IStoreAccountModel>.Stores { get => Stores?.ToList<IStoreAccountModel>(); set => Stores = value?.Cast<StoreAccountModel>().ToList(); }
        #endregion
        #region IAmFilterableByUserModel<> Properties
        /// <inheritdoc cref="IAmFilterableByUserModel{IUserModel}.Users"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Users), DataType = "List<UserModel>", ParameterType = "body", IsRequired = false,
                Description = "Users this object is associated with")]
        public List<UserModel>? Users { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IUserModel>? IAmFilterableByUserModel<IUserModel>.Users { get => Users?.ToList<IUserModel>(); set => Users = value?.Cast<UserModel>().ToList(); }
        #endregion
        #region IAmFilterableByVendorModel<> Properties
        /// <inheritdoc cref="IAmFilterableByVendorModel{IVendorAccountModel}.Vendors"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Vendors), DataType = "List<VendorAccountModel>", ParameterType = "body", IsRequired = false,
                Description = "Vendors this object is associated with")]
        public List<VendorAccountModel>? Vendors { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IVendorAccountModel>? IAmFilterableByVendorModel<IVendorAccountModel>.Vendors { get => Vendors?.ToList<IVendorAccountModel>(); set => Vendors = value?.Cast<VendorAccountModel>().ToList(); }
        #endregion
        #region IHaveNotesBaseModel
        /// <inheritdoc cref="IHaveNotesBaseModel.Notes"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Notes), DataType = "List<NoteModel>", ParameterType = "body", IsRequired = false,
                Description = "Notes for the object, optional")]
        public List<NoteModel>? Notes { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<INoteModel>? IHaveNotesBaseModel.Notes { get => Notes?.ToList<INoteModel>(); set => Notes = value?.Cast<NoteModel>().ToList(); }
        #endregion
        #region IHaveImagesBaseModel
        /// <inheritdoc cref="IHaveImagesBaseModel{IAccountImageModel, ITypeModel}.Images"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Images), DataType = "List<AccountImageModel>", ParameterType = "body", IsRequired = false,
                Description = "Images for the object, optional")]
        public List<AccountImageModel>? Images { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IAccountImageModel>? IHaveImagesBaseModel<IAccountImageModel, ITypeModel>.Images { get => Images?.ToList<IAccountImageModel>(); set => Images = value?.Cast<AccountImageModel>().ToList(); }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(PrimaryImageFileName), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "The primary image from the list of images, or the first image if no primary is set (read-only)")]
        public string? PrimaryImageFileName { get; set; }
        #endregion
        #region IHaveStoredFilesBaseModel
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StoredFiles), IsRequired = false, DataType = "List<AccountFileModel>", ParameterType = "body",
                Description = "Stored Files for the object, optional")]
        public List<AccountFileModel>? StoredFiles { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IAccountFileModel>? IHaveStoredFilesBaseModel<IAccountFileModel>.StoredFiles { get => StoredFiles?.ToList<IAccountFileModel>(); set => StoredFiles = value?.Cast<AccountFileModel>().ToList(); }
        #endregion
    }
}
