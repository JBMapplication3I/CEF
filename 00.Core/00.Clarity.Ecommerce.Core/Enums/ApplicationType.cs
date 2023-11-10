// <copyright file="ApplicationType.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application type class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent application types.</summary>
    public enum ApplicationType
    {
        /// <summary>An enum constant representing the store option.</summary>
        Store = 0,

        /// <summary>An enum constant representing the admin option.</summary>
        Admin,

        /// <summary>An enum constant representing the store admin option.</summary>
        StoreAdmin,

        /// <summary>An enum constant representing the franchise admin option.</summary>
        FranchiseAdmin,

        /// <summary>An enum constant representing the brand admin option.</summary>
        BrandAdmin,

        /// <summary>An enum constant representing the vendor admin option.</summary>
        VendorAdmin,
    }
}
