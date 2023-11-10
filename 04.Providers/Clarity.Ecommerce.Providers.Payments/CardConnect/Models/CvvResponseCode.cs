// <copyright file="CvvResponseCode.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cvv response code class</summary>
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    /// <summary>Values that represent cvv response codes.</summary>
    public enum CvvResponseCode
    {
        /// <summary>An enum constant representing the valid cvv match option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"M")]
        ValidCvvMatch = 0,

        /// <summary>An enum constant representing the invalid cvv option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"N")]
        InvalidCvv = 1,

        /// <summary>An enum constant representing the cvv not processed option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"P")]
        CvvNotProcessed = 2,

        /// <summary>An enum constant representing the cvv not present option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"S")]
        CvvNotPresent = 3,

        /// <summary>An enum constant representing the card issuer not certified option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"U")]
        CardIssuerNotCertified = 4,

        /// <summary>An enum constant representing the no response option.</summary>
        [System.Runtime.Serialization.EnumMember(Value = @"X")]
        NoResponse = 5,
    }
}
