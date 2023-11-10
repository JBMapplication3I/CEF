// <copyright file="PayFabricRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric request class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>PayFabricCreateAndProcessTransactionRequest.</summary>
    public class PayFabricCreateAndProcessTransactionRequest
    {
        /// <summary>Amount.</summary>
        /// <value>The amount.</value>
        public string Amount { get; set; }

        /// <summary>Card.</summary>
        /// <value>The card.</value>
        public PayFabricCard Card { get; set; }

        /// <summary>Currency.</summary>
        /// <value>The currency.</value>
        public string Currency { get; set; }

        /// <summary>Customer.</summary>
        /// <value>The customer.</value>
        public string Customer { get; set; }

        /// <summary>SetupId.</summary>
        /// <value>The identifier of the setup.</value>
        public string SetupId { get; set; }

        /// <summary>Type.</summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>ServiceId.</summary>
        /// <value>The identifier of the service.</value>
        public string ServiceId { get; set; }

        /// <summary>Shipto.</summary>
        /// <value>The shipto.</value>
        public PayFabricAddress Shipto { get; set; }
    }
}
