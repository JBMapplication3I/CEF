// <copyright file="StaticCartLookupKey.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the static cart lookup key class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.ComponentModel;
    using DataModel;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A static cart by session lookup key.</summary>
    /// <seealso cref="CartLookupKeyBase"/>
    public class StaticCartLookupKey : CartLookupKeyBase
    {
        /// <summary>Initializes a new instance of the <see cref="StaticCartLookupKey"/> class.</summary>
        public StaticCartLookupKey()
        {
            TypeKey = string.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="StaticCartLookupKey"/> class.</summary>
        /// <param name="source">Source for the data.</param>
        public StaticCartLookupKey(string source)
        {
            var parsed = FromString(source);
            TypeKey = parsed.TypeKey;
            UserID = parsed.UserID;
            AccountID = parsed.AccountID;
            BrandID = parsed.BrandID;
            FranchiseID = parsed.FranchiseID;
            StoreID = parsed.StoreID;
        }

        /// <summary>Initializes a new instance of the <see cref="StaticCartLookupKey"/> class.</summary>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="typeKey">    The type key.</param>
        /// <param name="accountID">  Identifier for the account.</param>
        /// <param name="brandID">    Identifier for the brand.</param>
        /// <param name="franchiseID">Identifier for the franchise.</param>
        /// <param name="storeID">    Identifier for the store.</param>
        public StaticCartLookupKey(
            int userID,
            string typeKey,
            int? accountID = null,
            int? brandID = null,
            int? franchiseID = null,
            int? storeID = null)
        {
            TypeKey = typeKey;
            UserID = userID;
            AccountID = accountID;
            BrandID = brandID;
            FranchiseID = franchiseID;
            StoreID = storeID;
        }

        /// <summary>Gets or sets the type key.</summary>
        /// <value>The type key.</value>
        [DefaultValue(null), JsonProperty("TK")]
        public virtual string TypeKey { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [DefaultValue(null), JsonProperty("UID")]
        public int UserID { get; set; }

        /// <summary>Initializes a <see cref="StaticCartLookupKey"/> from the given string.</summary>
        /// <param name="source">Source for the data.</param>
        /// <returns>A <see cref="StaticCartLookupKey"/>.</returns>
        public static StaticCartLookupKey FromString(string source)
        {
            return JsonConvert.DeserializeObject<StaticCartLookupKey>(
                source,
                SerializableAttributesDictionaryExtensions.JsonSettings)!;
        }

        /// <summary>Initializes a <see cref="StaticCartLookupKey"/> from the given cart model.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="StaticCartLookupKey"/>.</returns>
        public static StaticCartLookupKey FromCart(ICartModel cart)
        {
            return new(
                typeKey: Contract.RequiresValidKey(cart.TypeName ?? cart.Type?.Name),
                userID: Contract.RequiresValidID(cart.UserID),
                accountID: cart.AccountID,
                brandID: cart.BrandID,
                franchiseID: cart.FranchiseID,
                storeID: cart.StoreID);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(
                this,
                SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
