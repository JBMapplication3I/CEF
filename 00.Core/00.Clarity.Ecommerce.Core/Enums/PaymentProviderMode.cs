// <copyright file="PaymentProviderMode.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment provider mode class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent payment provider modes.</summary>
    public enum PaymentProviderMode
    {
        /// <summary>An enum constant representing the development option.</summary>
        Development = 0,

        /// <summary>An enum constant representing the testing option.</summary>
        Testing,

        /// <summary>An enum constant representing the production option.</summary>
        Production,
    }
}
