// <copyright file="EvoRequestBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the evo request base class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>(Serializable) An EVO request.</summary>
    [PublicAPI, Serializable, DataContract]
    public abstract class EvoRequestBase
    {
        /// <summary>Gets or sets the identifier of the device.</summary>
        /// <value>The identifier of the device.</value>
        [JsonProperty("DEVICEID"), DataMember(Name = "DEVICEID"), ApiMember(Name = "DEVICEID",
            DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "A unique & secure identifier to identify different applications under a"
                + "single PayFabric merchant account")]
        public string? DeviceID { get; set; } = EvoPaymentProviderConfig.DeviceID;

        /// <summary>Gets or sets the device password.</summary>
        /// <value>The device password.</value>
        [JsonProperty("DEVICEPASSWORD"), DataMember(Name = "DEVICEPASSWORD"), ApiMember(Name = "DEVICEPASSWORD",
            DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Assigned to a single device. Both device Id and device password are "
                + "required when integrating with PayFabric.")]
        public string? DevicePassword { get; set; } = EvoPaymentProviderConfig.DevicePassword;

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
        public string? User { get; set; } = EvoPaymentProviderConfig.LoginUserName;

        /// <summary>VENDOR (Required)<br/>
        /// Your merchant login ID that you created when you registered for the account.<br/>
        /// Limitations: 64 alphanumeric, case-sensitive characters.</summary>
        /// <value>The vendor.</value>
        [JsonProperty("VENDOR"), DataMember(Name = "VENDOR"), ApiMember(Name = "VENDOR",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "VENDOR (Required): Your merchant login ID that you created when you registered for the "
                 + "account. Limitations: 64 alphanumeric, case-sensitive characters.")]
        public string? Vendor { get; set; } = EvoPaymentProviderConfig.LoginVendor;

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
        public string? Partner { get; set; } = EvoPaymentProviderConfig.LoginPartner;

        /// <summary>PWD (Required)<br/>
        /// The password that you defined while registering for the account.<br/>
        /// Limitations: 6 to 32 alphanumeric, case-sensitive characters.</summary>
        /// <value>The password.</value>
        [JsonProperty("PWD"), DataMember(Name = "PWD"), ApiMember(Name = "PWD",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "PWD (Required): The password that you defined while registering for the account. "
                 + "Limitations: 6 to 32 alphanumeric, case-sensitive characters.")]
        public string? Password { get; set; } = EvoPaymentProviderConfig.LoginPassword;

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
