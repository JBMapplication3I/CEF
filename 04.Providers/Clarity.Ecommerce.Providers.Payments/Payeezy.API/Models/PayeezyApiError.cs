// <copyright file="PayeezyApiError.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy API error class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy API error.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyApiError
    {
        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        public PayeezyErrorMessage[]? messages { get; set; }
    }
}
