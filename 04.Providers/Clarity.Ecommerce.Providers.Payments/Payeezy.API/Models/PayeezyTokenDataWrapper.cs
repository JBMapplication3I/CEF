// <copyright file="PayeezyTokenDataWrapper.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy token data wrapper class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy token data wrapper.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyTokenDataWrapper
    {
        /// <summary>Gets or sets the type of the token.</summary>
        /// <value>The type of the token.</value>
        public string? token_type { get; set; }

        /// <summary>Gets or sets information describing the token.</summary>
        /// <value>Information describing the token.</value>
        public PayeezyTokenData? token_data { get; set; }
    }
}
