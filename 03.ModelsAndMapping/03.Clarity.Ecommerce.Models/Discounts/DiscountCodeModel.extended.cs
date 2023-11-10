// <copyright file="DiscountCodeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount code model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the discount code.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IDiscountCodeModel"/>
    public partial class DiscountCodeModel
    {
        /// <inheritdoc/>
        public string? Code { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int DiscountID { get; set; }

        /// <inheritdoc/>
        public string? DiscountKey { get; set; }

        /// <inheritdoc/>
        public string? DiscountName { get; set; }

        /// <inheritdoc cref="IDiscountCodeModel.Discount"/>
        public DiscountModel? Discount { get; set; }

        /// <inheritdoc/>
        IDiscountModel? IDiscountCodeModel.Discount { get => Discount; set => Discount = (DiscountModel?)value; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc cref="IDiscountCodeModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IDiscountCodeModel.User { get => User; set => User = (UserModel?)value; }
        #endregion
    }
}
