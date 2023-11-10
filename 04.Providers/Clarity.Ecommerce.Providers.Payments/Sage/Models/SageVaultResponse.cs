// <copyright file="SageVaultResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage vault response class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage vault response.</summary>
    [PublicAPI, Serializable]
    public class SageVaultResponse
    {
        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public string? status { get; set; }

        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        public string? data { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string? message { get; set; }
    }
}
