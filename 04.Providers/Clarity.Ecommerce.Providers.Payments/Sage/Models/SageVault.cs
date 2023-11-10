// <copyright file="SageVault.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage vault class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage vault.</summary>
    [PublicAPI, Serializable]
    public class SageVault
    {
        /// <summary>Gets or sets the operation.</summary>
        /// <value>The operation.</value>
        public string? operation { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public string? token { get; set; }
    }
}
