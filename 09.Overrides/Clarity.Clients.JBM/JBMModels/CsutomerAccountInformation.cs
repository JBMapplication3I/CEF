// <copyright file="CsutomerAccountInformation.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CustomerAccountInformation class</summary>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using Newtonsoft.Json;

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class ShipMethod
    {
        [JsonProperty("DefautlShipMethod")]
        public string? DefaultShipMethod { get; set; }

        [JsonProperty("OrderType")]
        public string? OrderType { get; set; }
    }

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class CustomerAddress
    {
        [JsonProperty("PartyId")]
        public long? PartyId { get; set; }

        [JsonProperty("AddressNumber")]
        public string? AddressNumber { get; set; }

        [JsonProperty("AddressId")]
        public long? AddressId { get; set; }

        [JsonProperty("Address1")]
        public string? Address1 { get; set; }

        [JsonProperty("Address2")]
        public string? Address2 { get; set; }

        [JsonProperty("City")]
        public string? City { get; set; }

        [JsonProperty("Country")]
        public string? Country { get; set; }

        [JsonProperty("PostalCode")]
        public string? PostalCode { get; set; }

        [JsonProperty("State")]
        public string? State { get; set; }

        [JsonProperty("AddressType")]
        public string? AddressType { get; set; }
    }

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class SiteUse
    {
        [JsonProperty("SiteUseId")]
        public string? SiteUseId { get; set; }

        [JsonProperty("SiteUseCode")]
        public string? SiteUseCode { get; set; }
    }

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class CustomerAccountSiteInformation
    {
        [JsonProperty("AddressId")]
        public string? AddressId { get; set; }

        [JsonProperty("SiteUses")]
        public List<SiteUse>? SiteUses { get; set; }

        [JsonProperty("Type")]
        public string? Type { get; set; }

        [JsonProperty("ContactFirstName")]
        public string? ContactFirstName { get; set; }

        [JsonProperty("ContactLastName")]
        public string? ContactLastName { get; set; }

        [JsonProperty("FreightTerm")]
        public string? FreightTerm { get; set; }

        [JsonProperty("SetCode")]
        public string? SetCode { get; set; }

        [JsonProperty("ContactNumber")]
        public string? ContactNumber { get; set; }

        [JsonProperty("EndDate")]
        public string? EndDate { get; set; }

        [JsonProperty("ShipMethods")]
        public List<ShipMethod>? ShipMethods { get; set; }
    }

    public class CustomerAccountInformation
    {
        [JsonProperty("Sites")]
        public List<CustomerAccountSiteInformation>? Sites { get; set; }

        [JsonProperty("CustomerAddresses")]
        public List<CustomerAddress>? CustomerAddresses { get; set; }
    }
}