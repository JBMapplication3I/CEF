// <copyright file="SessionCartBySessionAndTypeLookupKey.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the session cart by session and type lookup key class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.ComponentModel;
    using DataModel;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A cart by session lookup key.</summary>
    public class SessionCartBySessionAndTypeLookupKey : CartLookupKeyBase
    {
        /// <summary>Initializes a new instance of the <see cref="SessionCartBySessionAndTypeLookupKey"/> class.</summary>
        /// <param name="source">Source for the data.</param>
        public SessionCartBySessionAndTypeLookupKey(string source)
        {
            var parsed = FromString(source);
            SessionID = parsed.SessionID;
            TypeKey = parsed.TypeKey;
            UserID = parsed.UserID;
            AccountID = parsed.AccountID;
            BrandID = parsed.BrandID;
            FranchiseID = parsed.FranchiseID;
            StoreID = parsed.StoreID;
        }

        /// <summary>Initializes a new instance of the <see cref="SessionCartBySessionAndTypeLookupKey"/> class.</summary>
        /// <param name="sessionID">  Identifier for the session.</param>
        /// <param name="typeKey">    The type key.</param>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="accountID">  Identifier for the account.</param>
        /// <param name="brandID">    Identifier for the brand.</param>
        /// <param name="franchiseID">Identifier for the franchise.</param>
        /// <param name="storeID">    Identifier for the store.</param>
        public SessionCartBySessionAndTypeLookupKey(
            Guid sessionID,
            string typeKey,
            int? userID = null,
            int? accountID = null,
            int? brandID = null,
            int? franchiseID = null,
            int? storeID = null)
        {
            SessionID = sessionID;
            TypeKey = typeKey;
            UserID = userID;
            AccountID = accountID;
            BrandID = brandID;
            FranchiseID = franchiseID;
            StoreID = storeID;
        }

        /// <summary>Initializes a new instance of the <see cref="SessionCartBySessionAndTypeLookupKey"/> class.</summary>
        protected SessionCartBySessionAndTypeLookupKey()
        {
            TypeKey = string.Empty;
        }

        /// <summary>Gets or sets the type key.</summary>
        /// <value>The type key.</value>
        [DefaultValue(null), JsonProperty("TK")]
        public virtual string TypeKey { get; set; }

        /// <summary>Gets or sets the identifier of the session.</summary>
        /// <value>The identifier of the session.</value>
        [DefaultValue(null), JsonProperty("SeID")]
        public Guid SessionID { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [DefaultValue(null), JsonProperty("UID")]
        public int? UserID { get; set; }

        /// <summary>Gets or sets the identifier of the alternate account.</summary>
        /// <value>The identifier of the alternate account.</value>
        public int? AltAccountID { get; set; }

        /// <summary>Initializes a <see cref="SessionCartBySessionAndTypeLookupKey"/> from the given string.</summary>
        /// <param name="source">Source for the data.</param>
        /// <returns>A <see cref="SessionCartBySessionAndTypeLookupKey"/>.</returns>
        public static SessionCartBySessionAndTypeLookupKey FromString(string source)
        {
            return JsonConvert.DeserializeObject<SessionCartBySessionAndTypeLookupKey>(
                source,
                SerializableAttributesDictionaryExtensions.JsonSettings)!;
        }

        /// <summary>Initializes this <see cref="SessionCartBySessionAndTypeLookupKey"/> from the given from cart model.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="SessionCartBySessionAndTypeLookupKey"/>.</returns>
        public static SessionCartBySessionAndTypeLookupKey FromCart(ICartModel cart)
        {
            return new(
                typeKey: Contract.RequiresValidKey(cart.Type?.Name ?? cart.TypeName),
                sessionID: Contract.RequiresValidID(cart.SessionID),
                userID: cart.UserID,
                accountID: cart.AccountID,
                brandID: cart.BrandID,
                franchiseID: cart.FranchiseID,
                storeID: cart.StoreID);
        }

        /// <summary>Converts this <see cref="SessionCartBySessionAndTypeLookupKey"/> to a
        /// <see cref="CartByIDLookupKey"/>.</summary>
        /// <param name="cartID">Identifier for the cart.</param>
        /// <returns>A <see cref="CartByIDLookupKey"/>.</returns>
        public CartByIDLookupKey ToIDLookupKey(int cartID)
        {
            return new(
                cartID: cartID,
                userID: UserID,
                accountID: AccountID,
                brandID: BrandID,
                franchiseID: FranchiseID,
                storeID: StoreID);
        }

        /// <summary>Creates a copy of this lookup key, but ignores the session identifier.</summary>
        /// <returns>A <see cref="SessionCartBySessionAndTypeLookupKey"/>.</returns>
        public SessionCartBySessionAndTypeLookupKey ButIgnoreSessionID()
        {
            return new(
                sessionID: default,
                typeKey: TypeKey,
                userID: UserID,
                accountID: AccountID,
                brandID: BrandID,
                franchiseID: FranchiseID,
                storeID: StoreID);
        }

        /// <summary>Creates a copy of this lookup key, but ignores the user and account identifiers.</summary>
        /// <returns>A <see cref="SessionCartBySessionAndTypeLookupKey"/>.</returns>
        public SessionCartBySessionAndTypeLookupKey ButIgnoreUserAndAccountID()
        {
            return new(
                sessionID: SessionID,
                typeKey: TypeKey,
                userID: null,
                accountID: null,
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
