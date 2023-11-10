// <copyright file="SageECheckAccount.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage check account class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage check account.</summary>
    [PublicAPI, Serializable]
    public class SageECheckAccount
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        public string? type { get; set; }

        /// <summary>Gets or sets the routing number.</summary>
        /// <value>The routing number.</value>
        public string? routingNumber { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        public string? accountNumber { get; set; }
    }
}
