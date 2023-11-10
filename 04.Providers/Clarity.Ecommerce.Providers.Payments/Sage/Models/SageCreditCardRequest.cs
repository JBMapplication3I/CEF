// <copyright file="SageCreditCardRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage credit card request class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage credit card request.</summary>
    [PublicAPI, Serializable]
    public class SageCreditCardRequest
    {
        /// <summary>Gets or sets the ecommerce.</summary>
        /// <value>The ecommerce.</value>
        public SageEcommerce? ecommerce { get; set; }

        /// <summary>Gets or sets the vault.</summary>
        /// <value>The vault.</value>
        public SageVault? vault { get; set; }
    }
}
