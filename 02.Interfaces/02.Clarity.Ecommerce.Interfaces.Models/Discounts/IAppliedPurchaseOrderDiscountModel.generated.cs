// <autogenerated>
// <copyright file="IAppliedPurchaseOrderDiscountModel.generated.cs" company="clarity-ventures.com">
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
    /// <summary>Interface for applied purchase order discount model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseModel"/>
    /// <seealso cref="IAmARelationshipTableBaseModel{IDiscountModel}"/>
    /// <seealso cref="IAppliedDiscountBaseModel"/>
    public partial interface IAppliedPurchaseOrderDiscountModel
        : IAmARelationshipTableBaseModel
            , IAmARelationshipTableBaseModel<IDiscountModel>
            , IAppliedDiscountBaseModel
    {
        #region IAmARelationshipTable
        #endregion
    }
}
