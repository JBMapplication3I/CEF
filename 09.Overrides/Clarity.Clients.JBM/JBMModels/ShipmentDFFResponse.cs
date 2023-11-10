// <copyright file="ShipmentDFFResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ShipmentDFFResponse class</summary>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ShipmentDFF
    {
        public string? JbmTrackingNumber { get; set; }

        public string? JbmTrackingNumber1 { get; set; }

        public string? JbmTrackingNumber2 { get; set; }

        public string? JbmTrackingNumber3 { get; set; }

        public string? JbmTrackingNumber4 { get; set; }
    }

    public class ShipmentDFFs : FusionResponseBase
    {
        [JsonProperty("items")]
        public List<ShipmentDFF>? Items { get; set; }
    }



}
