// <autogenerated>
// <copyright file="IVendorModel.generated.cs" company="clarity-ventures.com">
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
    /// <summary>Interface for vendor model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    /// <seealso cref="IAmFilterableByAccountModel{IVendorAccountModel}"/>
    /// <seealso cref="IAmFilterableByBrandModel{IBrandVendorModel}"/>
    /// <seealso cref="IAmFilterableByFranchiseModel{IFranchiseVendorModel}"/>
    /// <seealso cref="IAmFilterableByManufacturerModel{IVendorManufacturerModel}"/>
    /// <seealso cref="IAmFilterableByProductModel{IVendorProductModel}"/>
    /// <seealso cref="IAmFilterableByStoreModel{IStoreVendorModel}"/>
    /// <seealso cref="IHaveATypeBaseModel{ITypeModel}"/>
    /// <seealso cref="IHaveANullableContactBaseModel"/>
    /// <seealso cref="IHaveReviewsBaseModel"/>
    /// <seealso cref="IHaveOrderMinimumsBaseModel"/>
    /// <seealso cref="IHaveFreeShippingMinimumsBaseModel"/>
    /// <seealso cref="IHaveImagesBaseModel{IVendorImageModel,ITypeModel}"/>
    /// <seealso cref="IHaveNotesBaseModel"/>
    public partial interface IVendorModel
        : INameableBaseModel
            , IAmFilterableByAccountModel<IVendorAccountModel>
            , IAmFilterableByBrandModel<IBrandVendorModel>
            , IAmFilterableByFranchiseModel<IFranchiseVendorModel>
            , IAmFilterableByManufacturerModel<IVendorManufacturerModel>
            , IAmFilterableByProductModel<IVendorProductModel>
            , IAmFilterableByStoreModel<IStoreVendorModel>
            , IHaveATypeBaseModel<ITypeModel>
            , IHaveANullableContactBaseModel
            , IHaveReviewsBaseModel
            , IHaveOrderMinimumsBaseModel
            , IHaveFreeShippingMinimumsBaseModel
            , IHaveImagesBaseModel<IVendorImageModel, ITypeModel>
            , IHaveNotesBaseModel
    {
    }
}
