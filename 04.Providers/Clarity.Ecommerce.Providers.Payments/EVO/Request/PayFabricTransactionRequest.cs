// <copyright file="PayFabricTransactionRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric transaction request class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>A pay fabric transaction request.</summary>
    public class PayFabricTransactionRequest
    {
        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public string? Amount { get; set; }

        /// <summary>Gets or sets the card.</summary>
        /// <value>The card.</value>
        public PayFabricCard? Card { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        public string? Currency { get; set; }

        /// <summary>Gets or sets the customer.</summary>
        /// <value>The customer.</value>
        public string? Customer { get; set; }

        /// <summary>Gets or sets the identifier of the setup.</summary>
        /// <value>The identifier of the setup.</value>
        public string? SetupId { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        public string? Type { get; set; }

        /// <summary>Gets or sets the identifier of the service.</summary>
        /// <value>The identifier of the service.</value>
        public string? ServiceId { get; set; }

        /// <summary>Gets or sets the ship to.</summary>
        /// <value>The ship to.</value>
        public PayFabricAddress? Shipto { get; set; }
    }
}
