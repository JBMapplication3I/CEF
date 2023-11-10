// <autogenerated>
// <copyright file="IProductModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IModel Interfaces generated to provide base setups.</summary>
// <remarks>This file was auto-generated by IModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable BadEmptyBracesLineBreaks
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for product model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    /// <seealso cref="IAmFilterableByAccountModel{IAccountProductModel}"/>
    /// <seealso cref="IAmFilterableByBrandModel{IBrandProductModel}"/>
    /// <seealso cref="IAmFilterableByCategoryModel{IProductCategoryModel}"/>
    /// <seealso cref="IAmFilterableByFranchiseModel{IFranchiseProductModel}"/>
    /// <seealso cref="IAmFilterableByManufacturerModel{IManufacturerProductModel}"/>
    /// <seealso cref="IAmFilterableByStoreModel{IStoreProductModel}"/>
    /// <seealso cref="IAmFilterableByVendorModel{IVendorProductModel}"/>
    /// <seealso cref="IHaveATypeBaseModel{ITypeModel}"/>
    /// <seealso cref="IHaveAStatusBaseModel{IStatusModel}"/>
    /// <seealso cref="IHaveRequiresRolesBaseModel"/>
    /// <seealso cref="IHaveReviewsBaseModel"/>
    /// <seealso cref="IHaveSeoBaseModel"/>
    /// <seealso cref="IHaveImagesBaseModel{IProductImageModel,ITypeModel}"/>
    /// <seealso cref="IHaveStoredFilesBaseModel{IProductFileModel}"/>
    public partial interface IProductModel
        : INameableBaseModel
            , IAmFilterableByAccountModel<IAccountProductModel>
            , IAmFilterableByBrandModel<IBrandProductModel>
            , IAmFilterableByFranchiseModel<IFranchiseProductModel>
            , IAmFilterableByCategoryModel<IProductCategoryModel>
            , IAmFilterableByManufacturerModel<IManufacturerProductModel>
            , IAmFilterableByStoreModel<IStoreProductModel>
            , IAmFilterableByVendorModel<IVendorProductModel>
            , IHaveATypeBaseModel<ITypeModel>
            , IHaveAStatusBaseModel<IStatusModel>
            , IHaveRequiresRolesBaseModel
            , IHaveReviewsBaseModel
            , IHaveSeoBaseModel
            , IHaveImagesBaseModel<IProductImageModel, ITypeModel>
            , IHaveStoredFilesBaseModel<IProductFileModel>
    {
    }
}
