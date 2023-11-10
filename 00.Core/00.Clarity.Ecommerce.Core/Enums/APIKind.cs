// <copyright file="APIKind.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the API kind class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent API kinds.</summary>
    public enum APIKind
    {
        /// <summary>An enum constant representing the global option.</summary>
        Global = 0,

        /// <summary>An enum constant representing the admin option.</summary>
        Admin,

        /// <summary>An enum constant representing the brand admin option.</summary>
        BrandAdmin,

        /// <summary>An enum constant representing the franchise admin option.</summary>
        FranchiseAdmin,

        /// <summary>An enum constant representing the manufacturer admin option.</summary>
        ManufacturerAdmin,

        /// <summary>An enum constant representing the store admin option.</summary>
        StoreAdmin,

        /// <summary>An enum constant representing the storefront option.</summary>
        Storefront,

        /// <summary>An enum constant representing the vendor admin option.</summary>
        VendorAdmin,
    }
}
