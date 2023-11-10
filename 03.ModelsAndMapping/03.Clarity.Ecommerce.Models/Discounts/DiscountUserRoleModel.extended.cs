// <copyright file="DiscountUserRoleModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount user role model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the discount user role.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IDiscountUserRoleModel"/>
    public partial class DiscountUserRoleModel
    {
        /// <inheritdoc/>
        public string? RoleName { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int MasterID { get; set; }

        /// <inheritdoc/>
        public string? MasterKey { get; set; }

        /// <inheritdoc/>
        public string? MasterName { get; set; }

        /// <inheritdoc cref="IDiscountUserRoleModel.Master"/>
        public DiscountModel? Master { get; set; }

        /// <inheritdoc/>
        IDiscountModel? IDiscountUserRoleModel.Master { get => Master; set => Master = (DiscountModel?)value; }
        #endregion
    }
}
