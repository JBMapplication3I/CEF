// <copyright file="SageECheckRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage check request class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage check request.</summary>
    [PublicAPI, Serializable]
    public class SageECheckRequest
    {
        /// <summary>Gets or sets the security code.</summary>
        /// <value>The security code.</value>
        public string? secCode { get; set; }

        /// <summary>Gets or sets the identifier of the originator.</summary>
        /// <value>The identifier of the originator.</value>
        public string? originatorId { get; set; }

        /// <summary>Gets or sets the order number.</summary>
        /// <value>The order number.</value>
        public string? orderNumber { get; set; }

        /// <summary>Gets or sets the amounts.</summary>
        /// <value>The amounts.</value>
        public SageAmounts? amounts { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        public SageECheckAccount? account { get; set; }

        /// <summary>Gets or sets the billing.</summary>
        /// <value>The billing.</value>
        public SageAddress? billing { get; set; }

        /// <summary>Gets or sets the vault.</summary>
        /// <value>The vault.</value>
        public SageVault? vault { get; set; }
    }
}
