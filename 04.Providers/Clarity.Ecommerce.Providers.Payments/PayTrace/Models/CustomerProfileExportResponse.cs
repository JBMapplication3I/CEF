// <copyright file="CustomerProfileExportResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace CustomerProfileExportResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>A customer profile export response.</summary>
    /// <seealso cref="PayTraceBasicResponse"/>
    public class CustomerProfileExportResponse : PayTraceBasicResponse
    {
        /// <summary>Gets or sets Customers.</summary>
        /// <value>The Customers.</value>
        [JsonProperty("customers")]
        public List<Customer>? Customers { get; set; }
    }
}
