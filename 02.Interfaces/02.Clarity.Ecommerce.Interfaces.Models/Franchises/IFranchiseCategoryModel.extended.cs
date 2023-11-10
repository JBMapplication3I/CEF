// <copyright file="IFranchiseCategoryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFranchiseCategoryModel.extended interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IFranchiseCategoryModel
    {
        /// <summary>Gets or sets a value indicating whether this record is visible in the owner.</summary>
        /// <value>True if this record is visible in the owner, false if not.</value>
        bool IsVisibleIn { get; set; }
    }
}
