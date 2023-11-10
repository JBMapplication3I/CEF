// <copyright file="PayFabricGatewayAccountProfile.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric gateway account profile class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>A pay fabric gateway account profile.</summary>
    public class PayFabricGatewayAccountProfile
    {
        /// <summary>GUID   Unique identifier for this record, provided by the merchant.</summary>
        /// <value>The identifier.</value>
        public string? ID { get; set; }

        /// <summary>String Gateway account profile name. nvarchar(64)</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>String PayFabric gateway connector name. nvarchar(64)</summary>
        /// <value>The connector.</value>
        public string? Connector { get; set; }

        /// <summary>String Bank processor name. nvarchar(64)</summary>
        /// <value>The processor.</value>
        public string? Processor { get; set; }

        /// <summary>String Bank processor ID.  tinyint.</summary>
        /// <value>The identifier of the processor.</value>
        public string? ProcessorID { get; set; }

        /// <summary>Gets or sets the card class.</summary>
        /// <value>The card class.</value>
        public string? CardClass { get; set; }

        /// <summary>String ID of CardClass tinyint.</summary>
        /// <value>The identifier of the card class.</value>
        public string? CardClassID { get; set; }
    }
}
