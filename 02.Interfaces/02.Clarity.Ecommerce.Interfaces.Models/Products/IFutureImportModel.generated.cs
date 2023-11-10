// <autogenerated>
// <copyright file="IFutureImportModel.generated.cs" company="clarity-ventures.com">
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
    /// <summary>Interface for future import model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    /// <seealso cref="IAmFilterableByNullableStoreModel"/>
    /// <seealso cref="IAmFilterableByNullableVendorModel"/>
    /// <seealso cref="IHaveAStatusBaseModel{IStatusModel}"/>
    public partial interface IFutureImportModel
        : INameableBaseModel
            , IAmFilterableByNullableStoreModel
            , IAmFilterableByNullableVendorModel
            , IHaveAStatusBaseModel<IStatusModel>
    {
    }
}
