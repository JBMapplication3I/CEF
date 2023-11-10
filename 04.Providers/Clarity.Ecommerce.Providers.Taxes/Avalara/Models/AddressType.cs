// <copyright file="AddressType.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address type class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    /// <summary>Values that represent address types.</summary>
    public enum AddressType
    {
        /// <summary>Firm or company address.</summary>
        F,

        /// <summary>General Delivery address.</summary>
        G,

        /// <summary>High-rise or business complex.</summary>
        H,

        /// <summary>PO box address.</summary>
        P,

        /// <summary>Rural route address.</summary>
        R,

        /// <summary>Street or residential address.</summary>
        S,
    }
}
