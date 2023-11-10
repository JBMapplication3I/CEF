// <copyright file="CartByIDLookupKey.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart by identifier lookup key class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.ComponentModel;
    using DataModel;
    using Newtonsoft.Json;

    /// <summary>A cart by identifier lookup key.</summary>
    public class CartByIDLookupKey : CartLookupKeyBase
    {
        /// <summary>Initializes a new instance of the <see cref="CartByIDLookupKey"/> class.</summary>
        public CartByIDLookupKey()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CartByIDLookupKey"/> class.</summary>
        /// <param name="source">Source for the data.</param>
        public CartByIDLookupKey(string source)
        {
            var parsed = FromString(source);
            CartID = parsed.CartID;
            UserID = parsed.UserID;
            AccountID = parsed.AccountID;
            BrandID = parsed.BrandID;
            FranchiseID = parsed.FranchiseID;
            StoreID = parsed.StoreID;
        }

        /// <summary>Initializes a new instance of the <see cref="CartByIDLookupKey"/> class.</summary>
        /// <param name="cartID">     Identifier for the cart.</param>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="accountID">  Identifier for the account.</param>
        /// <param name="brandID">    Identifier for the brand.</param>
        /// <param name="franchiseID">Identifier for the franchise.</param>
        /// <param name="storeID">    Identifier for the store.</param>
        public CartByIDLookupKey(
            int cartID,
            int? userID = null,
            int? accountID = null,
            int? brandID = null,
            int? franchiseID = null,
            int? storeID = null)
        {
            CartID = cartID;
            UserID = userID;
            AccountID = accountID;
            BrandID = brandID;
            FranchiseID = franchiseID;
            StoreID = storeID;
        }

        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        [DefaultValue(0), JsonProperty("ID")]
        public int CartID { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [DefaultValue(null), JsonProperty("UID")]
        public int? UserID { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [DefaultValue(null), JsonProperty("AltAccountID")]
        public int? AltAccountID { get; set; }

        /// <summary>Initializes a <see cref="CartByIDLookupKey"/> from the given string.</summary>
        /// <param name="source">Source for the data.</param>
        /// <returns>A <see cref="CartByIDLookupKey"/>.</returns>
        public static CartByIDLookupKey FromString(string source)
        {
            return JsonConvert.DeserializeObject<CartByIDLookupKey>(
                source,
                SerializableAttributesDictionaryExtensions.JsonSettings)!;
        }

        /// <summary>Initializes a <see cref="CartByIDLookupKey"/> from the given cart model.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="CartByIDLookupKey"/>.</returns>
        public static CartByIDLookupKey FromCart(ICartModel cart)
        {
            return new(
                cartID: cart.ID,
                userID: cart.UserID,
                accountID: cart.AccountID,
                brandID: cart.BrandID,
                franchiseID: cart.FranchiseID,
                storeID: cart.StoreID);
        }

        /// <summary>Converts this <see cref="CartByIDLookupKey"/> to a <see cref="SessionCartBySessionAndTypeLookupKey"/>.</summary>
        /// <param name="typeKey">  The type key.</param>
        /// <param name="sessionID">Identifier for the session.</param>
        /// <returns>The given data converted to a <see cref="SessionCartBySessionAndTypeLookupKey"/>.</returns>
        public SessionCartBySessionAndTypeLookupKey ToSessionLookupKey(string typeKey, Guid sessionID)
        {
            return new(
                typeKey: typeKey,
                sessionID: sessionID,
                userID: UserID,
                accountID: AccountID,
                brandID: BrandID,
                franchiseID: FranchiseID,
                storeID: StoreID);
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
