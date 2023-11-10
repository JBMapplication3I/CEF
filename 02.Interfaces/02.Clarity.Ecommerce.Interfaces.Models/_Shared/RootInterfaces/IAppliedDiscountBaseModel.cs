// <copyright file="IAppliedDiscountBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAppliedDiscountBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for applied discount base model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseModel{Discount}"/>
    public interface IAppliedDiscountBaseModel
        : IAmARelationshipTableBaseModel<IDiscountModel>
    {
        #region Applied Discounts Base Properties
        /// <summary>Gets or sets the discount total.</summary>
        /// <value>The discount total.</value>
        decimal DiscountTotal { get; set; }

        /// <summary>Gets or sets the applications used.</summary>
        /// <value>The applications used.</value>
        int? ApplicationsUsed { get; set; }

        /// <summary>Gets or sets target applications used.</summary>
        /// <value>The target applications used.</value>
        int? TargetApplicationsUsed { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the discount.</summary>
        /// <value>The identifier of the discount.</value>
        int DiscountID { get; set; }

        /// <summary>Gets or sets the discount key.</summary>
        /// <value>The discount key.</value>
        string? DiscountKey { get; set; }

        /// <summary>Gets or sets the name of the discount.</summary>
        /// <value>The name of the discount.</value>
        string? DiscountName { get; set; }

        /// <summary>Gets or sets the name of the slave.</summary>
        /// <value>The name of the slave.</value>
        string? SlaveName { get; set; }

        /// <summary>Gets or sets the discount value.</summary>
        /// <value>The discount value.</value>
        decimal DiscountValue { get; set; }

        /// <summary>Gets or sets the type of the discount value.</summary>
        /// <value>The type of the discount value.</value>
        int DiscountValueType { get; set; }

        /// <summary>Gets or sets the discount priority.</summary>
        /// <value>The discount priority.</value>
        int? DiscountPriority { get; set; }

        /// <summary>Gets or sets the identifier of the discount type.</summary>
        /// <value>The identifier of the discount type.</value>
        int DiscountTypeID { get; set; }

        /// <summary>Gets or sets the discount.</summary>
        /// <value>The discount.</value>
        IDiscountModel? Discount { get; set; }

        /// <summary>Gets or sets a value indicating whether the discount can combine.</summary>
        /// <value>Can be combined or not.</value>
        bool DiscountCanCombine { get; set; }

        /// <summary>Gets or sets a value indicating whether the discount is automatic applied.</summary>
        /// <value>True if discount is automatic applied, false if not.</value>
        bool DiscountIsAutoApplied { get; set; }
        #endregion
    }
}
