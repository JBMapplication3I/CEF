// <copyright file="PaymentOrigin.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment origin class</summary>
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    /// <summary>Values that represent payment origins.</summary>
    public enum PaymentOrigin
    {
        /// <summary>An enum constant representing the telephone or mail option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"T")]
        TelephoneOrMail = 0,

        /// <summary>An enum constant representing the recurring option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"R")]
        Recurring = 1,

        /// <summary>An enum constant representing the eCommerce or Internet option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"E")]
        Ecommerce = 2,
    }
}
