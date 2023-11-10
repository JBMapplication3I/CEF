// <copyright file="SageAmounts.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage amounts class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage amounts.</summary>
    [PublicAPI, Serializable]
    public class SageAmounts
    {
        /// <summary>Gets or sets the shipping.</summary>
        /// <value>The shipping.</value>
        public decimal shipping { get; set; }

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        public decimal tax { get; set; }

        /// <summary>Gets or sets the number of. </summary>
        /// <value>The total.</value>
        public decimal total { get; set; }
    }
}
