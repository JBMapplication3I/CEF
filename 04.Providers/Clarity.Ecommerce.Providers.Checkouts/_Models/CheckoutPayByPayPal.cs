// <copyright file="CheckoutPayByPayPal.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout pay by pay palette class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A checkout pay by pay palette.</summary>
    /// <seealso cref="ICheckoutPayByPayPal"/>
    public class CheckoutPayByPayPal : ICheckoutPayByPayPal
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CancelUrl), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "PayPal Checkout Method: Cancel Url"), DefaultValue(null)]
        public string? CancelUrl { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ReturnUrl), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "PayPal Checkout Method: Return Url"), DefaultValue(null)]
        public string? ReturnUrl { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PayerID), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "PayPal Checkout Method: Payer ID"), DefaultValue(null)]
        public string? PayerID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PayPalToken), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "PayPal Checkout Method: Token"), DefaultValue(null)]
        public string? PayPalToken { get; set; }
    }
}
