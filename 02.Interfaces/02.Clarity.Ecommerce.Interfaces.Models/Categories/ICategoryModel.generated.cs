// <autogenerated>
// <copyright file="ICategoryModel.generated.cs" company="clarity-ventures.com">
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
    /// <summary>Interface for category model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    /// <seealso cref="IAmFilterableByBrandModel{IBrandCategoryModel}"/>
    /// <seealso cref="IAmFilterableByFranchiseModel{IFranchiseCategoryModel}"/>
    /// <seealso cref="IAmFilterableByProductModel{IProductCategoryModel}"/>
    /// <seealso cref="IAmFilterableByStoreModel{IStoreCategoryModel}"/>
    /// <seealso cref="IHaveATypeBaseModel{ITypeModel}"/>
    /// <seealso cref="IHaveAParentBaseModel{ICategoryModel}"/>
    /// <seealso cref="IHaveRequiresRolesBaseModel"/>
    /// <seealso cref="IHaveReviewsBaseModel"/>
    /// <seealso cref="IHaveSeoBaseModel"/>
    /// <seealso cref="IHaveOrderMinimumsBaseModel"/>
    /// <seealso cref="IHaveFreeShippingMinimumsBaseModel"/>
    /// <seealso cref="IHaveImagesBaseModel{ICategoryImageModel,ITypeModel}"/>
    /// <seealso cref="IHaveStoredFilesBaseModel{ICategoryFileModel}"/>
    public partial interface ICategoryModel
        : INameableBaseModel
            , IAmFilterableByBrandModel<IBrandCategoryModel>
            , IAmFilterableByFranchiseModel<IFranchiseCategoryModel>
            , IAmFilterableByProductModel<IProductCategoryModel>
            , IAmFilterableByStoreModel<IStoreCategoryModel>
            , IHaveATypeBaseModel<ITypeModel>
            , IHaveAParentBaseModel<ICategoryModel>
            , IHaveRequiresRolesBaseModel
            , IHaveReviewsBaseModel
            , IHaveSeoBaseModel
            , IHaveOrderMinimumsBaseModel
            , IHaveFreeShippingMinimumsBaseModel
            , IHaveImagesBaseModel<ICategoryImageModel, ITypeModel>
            , IHaveStoredFilesBaseModel<ICategoryFileModel>
    {
    }
}
