// <copyright file="UOMResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UOMResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class UOMResponseItem
    {
        [JsonProperty("UOMCode", NullValueHandling = NullValueHandling.Ignore)]
        public string? UOMCode { get; set; }

        // public string? UOMClass { get; set; }

        // public string? UOMClassName { get; set; }

        // public string? UOM { get; set; }

        // public string? Description { get; set; }

        // public string? UOMReciprocalDescription { get; set; }

        // public string? UOMPluralDescription { get; set; }

        // public bool? BaseUnitFlag { get; set; }

        // public DateTime? DisableDate { get; set; }

        // public int? StandardConversion { get; set; }

        // public DateTime? StandardConversionEndDate { get; set; }

        // public string? GlobalAttributeCategory { get; set; }

        // public string? OkeiCode { get; set; }

        // public string? DerivedUnitParentCode { get; set; }

        // public string? CreatedBy { get; set; }

        // public DateTime CreationDate { get; set; }

        // public DateTime LastUpdateDate { get; set; }

        // public string? LastUpdatedBy { get; set; }
    }

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class UOMRespsonse
    {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<UOMResponseItem>? items { get; set; }
    }
}