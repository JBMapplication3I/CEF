// <copyright file="PayeezyErrorMessage.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Payeezy error message class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy error message.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyErrorMessage
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        public string? code { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string? description { get; set; }
    }
}
