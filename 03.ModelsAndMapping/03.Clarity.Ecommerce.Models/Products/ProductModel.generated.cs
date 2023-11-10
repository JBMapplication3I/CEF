// <autogenerated>
// <copyright file="ProductModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Product.</summary>
    public partial class ProductModel
        : NameableBaseModel
            , IProductModel
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
        #region IAmFilterableByAccountModel<IAccountProductModel> Properties
        /// <inheritdoc cref="IAmFilterableByAccountModel{IAccountProductModel}.Accounts"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Accounts), DataType = "List<AccountProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Accounts this object is associated with")]
        public List<AccountProductModel>? Accounts { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IAccountProductModel>? IAmFilterableByAccountModel<IAccountProductModel>.Accounts { get => Accounts?.ToList<IAccountProductModel>(); set => Accounts = value?.Cast<AccountProductModel>().ToList(); }
        #endregion
        #region IAmFilterableByBrandModel<> Properties
        /// <inheritdoc cref="IAmFilterableByBrandModel{IBrandProductModel}.Brands"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Brands), DataType = "List<BrandProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Brands this object is associated with")]
        public List<BrandProductModel>? Brands { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IBrandProductModel>? IAmFilterableByBrandModel<IBrandProductModel>.Brands { get => Brands?.ToList<IBrandProductModel>(); set => Brands = value?.Cast<BrandProductModel>().ToList(); }
        #endregion
        #region IAmFilterableByCategoryModel<> Properties
        /// <inheritdoc cref="IAmFilterableByCategoryModel{IProductCategoryModel}.Categories"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Categories), DataType = "List<ProductCategoryModel>", ParameterType = "body", IsRequired = false,
                Description = "Categories this object is associated with")]
        public List<ProductCategoryModel>? Categories { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IProductCategoryModel>? IAmFilterableByCategoryModel<IProductCategoryModel>.Categories { get => Categories?.ToList<IProductCategoryModel>(); set => Categories = value?.Cast<ProductCategoryModel>().ToList(); }
        #endregion
        #region IAmFilterableByFranchiseModel<> Properties
        /// <inheritdoc cref="IAmFilterableByFranchiseModel{IFranchiseProductModel}.Franchises"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Franchises), DataType = "List<FranchiseProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Franchises this object is associated with")]
        public List<FranchiseProductModel>? Franchises { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IFranchiseProductModel>? IAmFilterableByFranchiseModel<IFranchiseProductModel>.Franchises { get => Franchises?.ToList<IFranchiseProductModel>(); set => Franchises = value?.Cast<FranchiseProductModel>().ToList(); }
        #endregion
        #region IAmFilterableByManufacturerModel<> Properties
        /// <inheritdoc cref="IAmFilterableByManufacturerModel{IManufacturerProductModel}.Manufacturers"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Manufacturers), DataType = "List<ManufacturerProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Manufacturers this object is associated with")]
        public List<ManufacturerProductModel>? Manufacturers { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IManufacturerProductModel>? IAmFilterableByManufacturerModel<IManufacturerProductModel>.Manufacturers { get => Manufacturers?.ToList<IManufacturerProductModel>(); set => Manufacturers = value?.Cast<ManufacturerProductModel>().ToList(); }
        #endregion
        #region IAmFilterableByStoreModel<> Properties
        /// <inheritdoc cref="IAmFilterableByStoreModel{IStoreProductModel}.Stores"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Stores), DataType = "List<StoreProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Stores this object is associated with")]
        public List<StoreProductModel>? Stores { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IStoreProductModel>? IAmFilterableByStoreModel<IStoreProductModel>.Stores { get => Stores?.ToList<IStoreProductModel>(); set => Stores = value?.Cast<StoreProductModel>().ToList(); }
        #endregion
        #region IAmFilterableByVendorModel<> Properties
        /// <inheritdoc cref="IAmFilterableByVendorModel{IVendorProductModel}.Vendors"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Vendors), DataType = "List<VendorProductModel>", ParameterType = "body", IsRequired = false,
                Description = "Vendors this object is associated with")]
        public List<VendorProductModel>? Vendors { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IVendorProductModel>? IAmFilterableByVendorModel<IVendorProductModel>.Vendors { get => Vendors?.ToList<IVendorProductModel>(); set => Vendors = value?.Cast<VendorProductModel>().ToList(); }
        #endregion
        #region IHaveImagesBaseModel
        /// <inheritdoc cref="IHaveImagesBaseModel{IProductImageModel, ITypeModel}.Images"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Images), DataType = "List<ProductImageModel>", ParameterType = "body", IsRequired = false,
                Description = "Images for the object, optional")]
        public List<ProductImageModel>? Images { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IProductImageModel>? IHaveImagesBaseModel<IProductImageModel, ITypeModel>.Images { get => Images?.ToList<IProductImageModel>(); set => Images = value?.Cast<ProductImageModel>().ToList(); }

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
            ApiMember(Name = nameof(StoredFiles), IsRequired = false, DataType = "List<ProductFileModel>", ParameterType = "body",
                Description = "Stored Files for the object, optional")]
        public List<ProductFileModel>? StoredFiles { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IProductFileModel>? IHaveStoredFilesBaseModel<IProductFileModel>.StoredFiles { get => StoredFiles?.ToList<IProductFileModel>(); set => StoredFiles = value?.Cast<ProductFileModel>().ToList(); }
        #endregion
        #region IHaveSeoBaseModel
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoKeywords), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO Keywords to use in the Meta tags of the page for this object")]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO URL to use to link to the page for this object")]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoDescription), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO Description to use in the Meta tags of the page for this object")]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoMetaData), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO General Meta Data to use in the Meta tags of the page for this object")]
        public string? SeoMetaData { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoPageTitle), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO Page Title to use in the Meta tags of the page for this object")]
        public string? SeoPageTitle { get; set; }
        #endregion
        #region IHaveRequiresRolesBaseModel
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(RequiresRoles), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "A comma delimited string? for Role Names that are required for access")]
        public string? RequiresRoles { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(RequiresRolesList), DataType = "List<string>", ParameterType = "body", IsRequired = false,
                Description = "A List string? for Role Names that are required for access")]
        public List<string> RequiresRolesList => RequiresRoles?.Split(',').Select(s => s.Trim()).ToList()
            ?? new List<string>();

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(RequiresRolesAlt), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "A comma delimited string? for Role Names that are required for access. This is an alternate list for additional purposes.")]
        public string? RequiresRolesAlt { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(RequiresRolesListAlt), DataType = "List<string>", ParameterType = "body", IsRequired = false,
                Description = "A List string? for Role Names that are required for access. This is an alternate list for additional purposes.")]
        public List<string> RequiresRolesListAlt => RequiresRolesAlt?.Split(',').Select(s => s.Trim()).ToList()
            ?? new List<string>();
        #endregion
        #region IHaveReviewsBaseModel
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Reviews), DataType = "List<ReviewModel>", ParameterType = "body", IsRequired = false,
                Description = "Product Reviews")]
        public List<ReviewModel>? Reviews { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        List<IReviewModel>? IHaveReviewsBaseModel.Reviews { get => Reviews?.ToList<IReviewModel>(); set => Reviews = value?.Cast<ReviewModel>().ToList(); }
        #endregion
    }
}
