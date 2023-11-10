// <copyright file="ReferralCodeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the referral code model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the referral code.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IReferralCodeModel"/>
    public partial class ReferralCodeModel
    {
        /// <inheritdoc/>
        public string? Code { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        public string? UserUserName { get; set; }

        /// <inheritdoc cref="IReferralCodeModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IReferralCodeModel.User { get => User; set => User = (UserModel?)value; }
        #endregion
    }
}
