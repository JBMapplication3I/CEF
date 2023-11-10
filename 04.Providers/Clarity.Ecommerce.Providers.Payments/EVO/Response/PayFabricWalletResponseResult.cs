// <copyright file="PayFabricWalletResponseResult.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric wallet response result class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>Encapsulates the result of a pay fabric wallet response.</summary>
    public class PayFabricWalletResponseResult
    {
        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string? Message { get; set; }

        /// <summary>Gets or sets the result.</summary>
        /// <value>The result.</value>
        public string? Result { get; set; }
    }
}
