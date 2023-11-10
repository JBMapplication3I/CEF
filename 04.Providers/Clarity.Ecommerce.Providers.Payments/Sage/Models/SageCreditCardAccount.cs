// <copyright file="SageCreditCardAccount.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage credit card account class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage credit card account.</summary>
    [PublicAPI, Serializable]
    public class SageCreditCardAccount
    {
        /// <summary>Gets or sets the number of. </summary>
        /// <value>The number.</value>
        public string? number { get; set; }

        /// <summary>Gets or sets the expiration.</summary>
        /// <value>The expiration.</value>
        public string? expiration { get; set; }
    }
}
