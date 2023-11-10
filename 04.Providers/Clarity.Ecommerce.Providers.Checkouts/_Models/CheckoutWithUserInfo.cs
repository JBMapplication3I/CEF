// <copyright file="CheckoutWithUserInfo.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout with user information class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>Information about the checkout with user.</summary>
    /// <seealso cref="ICheckoutWithUserInfo"/>
    public class CheckoutWithUserInfo : ICheckoutWithUserInfo
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "body", IsRequired = true,
             Description = "User ID"), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsNewAccount), DataType = "bool", ParameterType = "body", IsRequired = true,
             Description = "Is New Account"), DefaultValue(false)]
        public bool IsNewAccount { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Account Username"), DefaultValue(null)]
        public string? UserName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Account Password"), DefaultValue(null)]
        public string? Password { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExternalUserID), DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Account ExternalUser ID"), DefaultValue(null)]
        public string? ExternalUserID { get; set; }
    }
}
