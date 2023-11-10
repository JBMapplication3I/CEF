// <copyright file="CheckoutPayByCreditCard.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout pay by credit card class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A checkout pay by credit card.</summary>
    /// <seealso cref="ICheckoutPayByCreditCard"/>
    public class CheckoutPayByCreditCard : ICheckoutPayByCreditCard
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CardType), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: Card Type"), DefaultValue(null)]
        public string? CardType { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CardReferenceName), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: Card Reference Name (for adding to Wallet)"), DefaultValue(null)]
        public string? CardReferenceName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CardHolderName), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: Name on the Card"), DefaultValue(null)]
        public string? CardHolderName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CardNumber), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: Card Number"), DefaultValue(null)]
        public string? CardNumber { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CVV), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: CVV"), DefaultValue(null)]
        public string? CVV { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExpirationMonth), DataType = "int?", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: Expiration Month"), DefaultValue(null)]
        public int? ExpirationMonth { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExpirationYear), DataType = "int?", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: Expiration Year"), DefaultValue(null)]
        public int? ExpirationYear { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CardToken), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Credit Card Payment Method: Token"), DefaultValue(null)]
        public string? CardToken { get; set; }
    }
}
