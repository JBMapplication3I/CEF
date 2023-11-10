// <copyright file="SageWalletResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage wallet response class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage wallet response.</summary>
    [PublicAPI, Serializable]
    public class SageWalletResponse
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        public string? code { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public string? status { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public string? token { get; set; }

        /// <summary>Gets or sets the reference a.</summary>
        /// <value>The reference a.</value>
        public string? referencea { get; set; }

        /// <summary>Gets or sets the vault response.</summary>
        /// <value>The vault response.</value>
        public SageVaultResponse? vaultResponse { get; set; }
    }
}
