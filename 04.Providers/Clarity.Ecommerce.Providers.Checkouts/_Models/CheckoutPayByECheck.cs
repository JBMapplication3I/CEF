// <copyright file="CheckoutPayByECheck.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout pay by e check class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A checkout pay by e check.</summary>
    /// <seealso cref="ICheckoutPayByECheck"/>
    public class CheckoutPayByECheck : ICheckoutPayByECheck
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountReferenceName), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "eCheck Payment Method: Account Reference Name (for adding to Wallet)"), DefaultValue(null)]
        public string? AccountReferenceName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountHolderName), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "eCheck Payment Method: Name on the Account"), DefaultValue(null)]
        public string? AccountHolderName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountNumber), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "eCheck Payment Method: Account Number"), DefaultValue(null)]
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RoutingNumber), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "eCheck Payment Method: Routing Number"), DefaultValue(null)]
        public string? RoutingNumber { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BankName), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "eCheck Payment Method: Bank Name"), DefaultValue(null)]
        public string? BankName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountType), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "eCheck Payment Method: Account Type (Checking/Savings)"), DefaultValue(null)]
        public string? AccountType { get; set; }
    }
}
