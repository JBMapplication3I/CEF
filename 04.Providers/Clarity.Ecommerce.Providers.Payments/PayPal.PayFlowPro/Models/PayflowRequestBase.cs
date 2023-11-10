// <copyright file="PayflowRequestBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payflow request base class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>(Serializable) A PayPal Payflow request.</summary>
    [PublicAPI, Serializable, DataContract]
    public abstract class PayflowRequestBase
    {
        /// <summary>USER (Required)<br/>
        /// If you set up one or more additional users on the account, this value is the ID of the user authorized to
        /// process transactions. If, however, you have not set up additional users on the account, USER has the same
        /// value as VENDOR.<br/>
        /// Limitations: 64 alphanumeric, case-sensitive characters.</summary>
        /// <value>The user.</value>
        [JsonProperty("USER"), DataMember(Name = "USER"), ApiMember(Name = "USER",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "USER (Required): If you set up one or more additional users on the account, this value "
                 + "is the ID of the user authorized to process transactions. If, however, you have not set up "
                 + "additional users on the account, USER has the same value as VENDOR. Limitations: 64 "
                 + "alphanumeric, case-sensitive characters.")]
        public string User { get; set; } = PayPalPayflowProPaymentsProviderConfig.LoginUserName;

        /// <summary>VENDOR (Required)<br/>
        /// Your merchant login ID that you created when you registered for the account.<br/>
        /// Limitations: 64 alphanumeric, case-sensitive characters.</summary>
        /// <value>The vendor.</value>
        [JsonProperty("VENDOR"), DataMember(Name = "VENDOR"), ApiMember(Name = "VENDOR",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "VENDOR (Required): Your merchant login ID that you created when you registered for the "
                 + "account. Limitations: 64 alphanumeric, case-sensitive characters.")]
        public string Vendor { get; set; } = PayPalPayflowProPaymentsProviderConfig.LoginVendor;

        /// <summary>PARTNER (Required)<br/>
        /// The ID provided to you by the authorized PayPal Reseller who registered you for the Gateway gateway. If you
        /// purchased your account directly from PayPal, use PayPal.<br/>
        /// Limitations: 64 alphanumeric, case-sensitive characters.</summary>
        /// <value>The partner.</value>
        [JsonProperty("PARTNER"), DataMember(Name = "PARTNER"), ApiMember(Name = "PARTNER",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "PARTNER (Required): The ID provided to you by the authorized PayPal Reseller who "
                 + "registered you for the Gateway gateway. If you purchased your account directly from PayPal, use "
                 + "PayPal. Limitations: 64 alphanumeric, case-sensitive characters.")]
        public string Partner { get; set; } = PayPalPayflowProPaymentsProviderConfig.LoginPartner;

        /// <summary>PWD (Required)<br/>
        /// The password that you defined while registering for the account.<br/>
        /// Limitations: 6 to 32 alphanumeric, case-sensitive characters.</summary>
        /// <value>The password.</value>
        [JsonProperty("PWD"), DataMember(Name = "PWD"), ApiMember(Name = "PWD",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "PWD (Required): The password that you defined while registering for the account. "
                 + "Limitations: 6 to 32 alphanumeric, case-sensitive characters.")]
        public string Password { get; set; } = PayPalPayflowProPaymentsProviderConfig.LoginPassword;

        /// <summary>VERBOSITY (Required)<br/>
        /// Set to HIGH to obtain information about a partial authorization in the response.</summary>
        /// <value>The password.</value>
        [JsonProperty("VERBOSITY"), DataMember(Name = "VERBOSITY"), ApiMember(Name = "VERBOSITY",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "VERBOSITY (Required): The password that you defined while registering for the account. "
                 + "Limitations: 6 to 32 alphanumeric, case-sensitive characters.")]
        public string Verbosity { get; set; } = "HIGH";
    }
}
