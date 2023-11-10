// <copyright file="ApprovalStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the approval status class</summary>
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    /// <summary>Values that represent approval status.</summary>
    public enum ApprovalStatus
    {
        /// <summary>An enum constant representing the approved option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"A")]
        Approved = 0,

        /// <summary>An enum constant representing the retry option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"B")]
        Retry = 1,

        /// <summary>An enum constant representing the declined option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"C")]
        Declined = 2,
    }
}
