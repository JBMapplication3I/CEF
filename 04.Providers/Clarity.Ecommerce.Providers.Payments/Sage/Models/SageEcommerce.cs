// <copyright file="SageEcommerce.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage ecommerce class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage ecommerce.</summary>
    [PublicAPI, Serializable]
    public class SageEcommerce
    {
        /// <summary>Gets or sets the order number.</summary>
        /// <value>The order number.</value>
        public string? orderNumber { get; set; }

        /// <summary>Gets or sets the amounts.</summary>
        /// <value>The amounts.</value>
        public SageAmounts? amounts { get; set; }

        /// <summary>Gets or sets information describing the card.</summary>
        /// <value>Information describing the card.</value>
        public SageCreditCardAccount? cardData { get; set; }
    }
}
