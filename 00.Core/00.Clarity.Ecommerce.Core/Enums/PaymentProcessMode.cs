// <copyright file="PaymentProcessMode.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment process mode class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent payment process modes.</summary>
    public enum PaymentProcessMode
    {
        /// <summary>An enum constant representing the authorize and capture option.</summary>
        AuthorizeAndCapture = 0,

        /// <summary>An enum constant representing the authorize only option.</summary>
        AuthorizeOnly,
    }
}
