// <copyright file="IReferralCodeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IReferralCodeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for referral code model.</summary>
    public partial interface IReferralCodeModel
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserUserName { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }
        #endregion
    }
}
