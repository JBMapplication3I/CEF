// <copyright file="CartLookupKeyBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart lookup key base class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    /// <summary>A cart lookup key base.</summary>
    public abstract class CartLookupKeyBase
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [DefaultValue(null), JsonProperty("AID")]
        public int? AccountID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [DefaultValue(null), JsonProperty("BID")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        [DefaultValue(null), JsonProperty("FID")]
        public int? FranchiseID { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [DefaultValue(null), JsonProperty("SID")]
        public int? StoreID { get; set; }
    }
}
