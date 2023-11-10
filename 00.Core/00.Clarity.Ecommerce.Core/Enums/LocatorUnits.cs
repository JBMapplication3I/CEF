// <copyright file="LocatorUnits.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the locator units class</summary>
namespace Clarity.Ecommerce.Enums
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>Values that represent locator units.</summary>
    public enum LocatorUnits
    {
        /// <summary>An enum constant representing the miles option.</summary>
        [JsonProperty("mi"), DataMember(Name = "mi")]
        Miles,

        /// <summary>An enum constant representing the kilometers option.</summary>
        [JsonProperty("km"), DataMember(Name = "km")]
        Kilometers,

        /// <summary>An enum constant representing the meters option.</summary>
        [JsonProperty("m"), DataMember(Name = "m")]
        Meters,
    }
}
