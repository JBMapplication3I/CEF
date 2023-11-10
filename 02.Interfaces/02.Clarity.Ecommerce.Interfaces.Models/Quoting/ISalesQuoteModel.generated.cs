// <autogenerated>
// <copyright file="ISalesQuoteModel.generated.cs" company="clarity-ventures.com">
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
    /// <summary>Interface for sales quote model.</summary>
    /// <seealso cref="ISalesCollectionBaseModel{ITypeModel, ISalesQuoteFileModel, ISalesQuoteContactModel, ISalesQuoteEventModel, IAppliedSalesQuoteDiscountModel, IAppliedSalesQuoteItemDiscountModel}"/>
    /// <seealso cref="IAmFilterableByNullableAccountModel"/>
    /// <seealso cref="IAmFilterableByNullableBrandModel"/>
    /// <seealso cref="IAmFilterableByNullableFranchiseModel"/>
    /// <seealso cref="IAmFilterableByNullableStoreModel"/>
    /// <seealso cref="IAmFilterableByNullableUserModel"/>
    /// <seealso cref="IHaveATypeBaseModel{ITypeModel}"/>
    /// <seealso cref="IHaveAStatusBaseModel{IStatusModel}"/>
    /// <seealso cref="IHaveAStateBaseModel{IStateModel}"/>
    /// <seealso cref="IHaveStoredFilesBaseModel{ISalesQuoteFileModel}"/>
    /// <seealso cref="IHaveNotesBaseModel"/>
    public partial interface ISalesQuoteModel
        : ISalesCollectionBaseModel<ITypeModel, ISalesQuoteFileModel, ISalesQuoteContactModel, ISalesQuoteEventModel, IAppliedSalesQuoteDiscountModel, IAppliedSalesQuoteItemDiscountModel>
            , IAmFilterableByNullableAccountModel
            , IAmFilterableByNullableBrandModel
            , IAmFilterableByNullableFranchiseModel
            , IAmFilterableByNullableStoreModel
            , IAmFilterableByNullableUserModel
            , IHaveATypeBaseModel<ITypeModel>
            , IHaveAStatusBaseModel<IStatusModel>
            , IHaveAStateBaseModel<IStateModel>
            , IHaveStoredFilesBaseModel<ISalesQuoteFileModel>
            , IHaveNotesBaseModel
    {
    }
}
