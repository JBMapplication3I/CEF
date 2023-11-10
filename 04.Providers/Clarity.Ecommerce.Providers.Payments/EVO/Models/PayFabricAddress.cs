// <copyright file="PayFabricAddress.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric address class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>A pay fabric address.</summary>
    public class PayFabricAddress
    {
        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        public string? City { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public string? Country { get; set; }

        /// <summary>Gets or sets the customer.</summary>
        /// <value>The customer.</value>
        public string? Customer { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        public string? Email { get; set; }

        /// <summary>Gets or sets the line 1.</summary>
        /// <value>The line 1.</value>
        public string? Line1 { get; set; }

        /// <summary>Gets or sets the line 2.</summary>
        /// <value>The line 2.</value>
        public string? Line2 { get; set; }

        /// <summary>Gets or sets the line 3.</summary>
        /// <value>The line 3.</value>
        public string? Line3 { get; set; }

        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        public string? Phone { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        public string? State { get; set; }

        /// <summary>Gets or sets the zip.</summary>
        /// <value>The zip.</value>
        public string? Zip { get; set; }
    }
}
