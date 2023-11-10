// <copyright file="PayFabricCardHolder.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric card holder class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>A pay fabric card holder.</summary>
    public class PayFabricCardHolder
    {
        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        public string? FirstName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        public string? LastName { get; set; }
    }
}
