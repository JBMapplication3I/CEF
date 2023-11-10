// <copyright file="CheckoutWithCartInfo.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout with cart information class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.ComponentModel;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>Information about the checkout with cart.</summary>
    /// <seealso cref="ICheckoutWithCartInfo"/>
    public class CheckoutWithCartInfo : ICheckoutWithCartInfo
    {
        /// <inheritdoc/>
        [DefaultValue(null),
            JsonProperty(nameof(CartID)),
            ApiMember(Name = nameof(CartID), DataType = "int?", ParameterType = "body", IsRequired = true,
                Description = "Cart ID")]
        public int? CartID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            JsonProperty(nameof(CartTypeName)),
            ApiMember(Name = nameof(CartTypeName), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Type of the cart to check out to an order")]
        public string? CartTypeName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            JsonProperty(nameof(CartSessionID)),
            ApiMember(Name = nameof(CartSessionID), DataType = "Guid?", ParameterType = "body", IsRequired = true,
                Description = "Cart Session ID")]
        public Guid? CartSessionID { get; set; }
    }
}
