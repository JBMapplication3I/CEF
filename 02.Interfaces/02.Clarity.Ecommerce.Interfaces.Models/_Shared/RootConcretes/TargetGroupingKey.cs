// <copyright file="TargetGroupingKey.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target grouping key class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    /// <summary>A target grouping key.</summary>
    public class TargetGroupingKey
    {
        /// <summary>Initializes a new instance of the <see cref="TargetGroupingKey"/> class.</summary>
        public TargetGroupingKey()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TargetGroupingKey"/> class.</summary>
        /// <param name="source">Source for the JSON.</param>
        public TargetGroupingKey(string source)
        {
            var parsed = FromString(source);
            TypeKey = parsed.TypeKey;
            StoreID = parsed.StoreID;
            BrandID = parsed.BrandID;
            VendorID = parsed.VendorID;
            InventoryLocationID = parsed.InventoryLocationID;
            NothingToShip = parsed.NothingToShip;
            CustomSplitKey = parsed.CustomSplitKey;
            HashedDestination = parsed.HashedDestination;
        }

        /// <summary>Initializes a new instance of the <see cref="TargetGroupingKey"/> class.</summary>
        /// <param name="typeKey">          The type key.</param>
        /// <param name="storeID">          Identifier for the store.</param>
        /// <param name="brandID">          Identifier for the brand.</param>
        /// <param name="vendorID">         Identifier for the vendor.</param>
        /// <param name="ilID">             Identifier for the inventory location.</param>
        /// <param name="nothingToShip">    True to nothing to ship.</param>
        /// <param name="customSplitKey">   The custom split key.</param>
        /// <param name="hashedDestination">The hashed destination.</param>
        public TargetGroupingKey(
            string? typeKey = null,
            int? storeID = null,
            int? brandID = null,
            int? vendorID = null,
            int? ilID = null,
            bool nothingToShip = false,
            string? customSplitKey = null,
            long? hashedDestination = null)
        {
            TypeKey = typeKey;
            StoreID = storeID;
            BrandID = brandID;
            VendorID = vendorID;
            InventoryLocationID = ilID;
            NothingToShip = nothingToShip;
            CustomSplitKey = customSplitKey;
            HashedDestination = hashedDestination;
        }

        /// <summary>Gets or sets the type key.</summary>
        /// <value>The type key.</value>
        [DefaultValue(null), JsonProperty("TK")]
        public string? TypeKey { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [DefaultValue(null), JsonProperty("SID")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [DefaultValue(null), JsonProperty("BID")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        [DefaultValue(null), JsonProperty("VID")]
        public int? VendorID { get; set; }

        /// <summary>Gets or sets the identifier of the inventory location.</summary>
        /// <value>The identifier of the inventory location.</value>
        [DefaultValue(null), JsonProperty("ILID")]
        public int? InventoryLocationID { get; set; }

        /// <summary>Gets or sets a value indicating whether the nothing to ship.</summary>
        /// <value>True if nothing to ship, false if not.</value>
        [DefaultValue(false), JsonProperty("NTS")]
        public bool NothingToShip { get; set; }

        /// <summary>Gets or sets the custom split key.</summary>
        /// <value>The custom split key.</value>
        [DefaultValue(null), JsonProperty("CSK")]
        public string? CustomSplitKey { get; set; }

        /// <summary>Gets or sets the hashed destination.</summary>
        /// <value>The hashed destination.</value>
        [DefaultValue(null), JsonProperty("HD")]
        public long? HashedDestination { get; set; }

        /// <summary>Initializes this TargetGroupingKey from the given from string.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>A TargetGroupingKey.</returns>
        public static TargetGroupingKey FromString(string source)
        {
            return JsonConvert.DeserializeObject<TargetGroupingKey>(
                source,
                SerializableAttributesDictionaryExtensions.JsonSettings)!;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(
                this,
                SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
