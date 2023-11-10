// <copyright file="CheckoutPayByWalletEntry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout pay by wallet entry class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A checkout pay by wallet entry.</summary>
    /// <seealso cref="ICheckoutPayByWalletEntry"/>
    public class CheckoutPayByWalletEntry : ICheckoutPayByWalletEntry
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(WalletID), DataType = "int?", ParameterType = "body", IsRequired = true,
             Description = "Wallet ID"), DefaultValue(null)]
        public int? WalletID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(WalletToken), DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Wallet Token"), DefaultValue(null)]
        public string? WalletToken { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(WalletCVV), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: CVV (Use with Wallet Entry"), DefaultValue(null)]
        public string? WalletCVV { get; set; }
    }
}
