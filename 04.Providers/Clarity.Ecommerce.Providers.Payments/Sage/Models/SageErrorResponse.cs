// <copyright file="SageErrorResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage error response class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage error response.</summary>
    [PublicAPI, Serializable]
    public class SageErrorResponse
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        public string? code { get; set; } // "400000",

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string? message { get; set; } // "There was a problem with the request. Please see 'detail' for more.",

        /// <summary>Gets or sets the information.</summary>
        /// <value>The information.</value>
        public string? info { get; set; } // "https://developer.sagepayments.com/docs/errors#400000",

        /// <summary>Gets or sets the detail.</summary>
        /// <value>The detail.</value>
        public string? detail { get; set; } // "INVALID CARDNUMBER"
    }
}
