// <copyright file="SystemCustomerUsageType.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the system customer usage type class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    /// <summary>Values that represent system customer usage types.</summary>
    public enum SystemCustomerUsageType
    {
        /// <summary>"Other" type.</summary>
        L,

        /// <summary>"Federal government",.</summary>
        A,

        /// <summary>"State government",.</summary>
        B,

        /// <summary>"Tribe / Status Indian / Indian Band",.</summary>
        C,

        /// <summary>"Foreign diplomat",.</summary>
        D,

        /// <summary>"Charitable or benevolent organization",.</summary>
        E,

        /// <summary>"Religious or educational organization",.</summary>
        F,

        /// <summary>"Resale" type.</summary>
        G,

        /// <summary>"Commercial agricultural production",.</summary>
        H,

        /// <summary>"Industrial production / manufacturer",.</summary>
        I,

        /// <summary>"Direct pay permit",.</summary>
        J,

        /// <summary>"Direct Mail",.</summary>
        K,

        /// <summary>"Local Government",.</summary>
        N,

        /// <summary>"Commercial Aquaculture",.</summary>
        P,

        /// <summary>"Commercial Fishery",.</summary>
        Q,

        /// <summary>"Non-resident" type.</summary>
        R,
    }
}
