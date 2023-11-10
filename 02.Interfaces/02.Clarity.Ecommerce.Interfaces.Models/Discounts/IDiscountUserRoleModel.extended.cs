// <copyright file="IDiscountUserRoleModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountUserRoleModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for discount user role model.</summary>
    public partial interface IDiscountUserRoleModel
    {
        /// <summary>Gets or sets the name of the role.</summary>
        /// <value>The name of the role.</value>
        string? RoleName { get; set; }

        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the master key.</summary>
        /// <value>The master key.</value>
        string? MasterKey { get; set; }

        /// <summary>Gets or sets the name of the master.</summary>
        /// <value>The name of the master.</value>
        string? MasterName { get; set; }

        /// <summary>Gets or sets the master.</summary>
        /// <value>The master.</value>
        IDiscountModel? Master { get; set; }
    }
}
