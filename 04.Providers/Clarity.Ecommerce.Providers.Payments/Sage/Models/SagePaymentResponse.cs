// <copyright file="SagePaymentResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage payment response class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage payment response.</summary>
    [PublicAPI, Serializable]
    public class SagePaymentResponse
    {
        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public string? status { get; set; }

        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        public string? code { get; set; }

        /// <summary>Gets or sets the referencea.</summary>
        /// <value>The referencea.</value>
        public string? referencea { get; set; }
    }
}
