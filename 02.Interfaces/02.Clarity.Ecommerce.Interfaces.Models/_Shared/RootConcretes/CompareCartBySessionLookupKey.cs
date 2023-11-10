// <copyright file="CompareCartBySessionLookupKey.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the compare cart by session lookup key class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>A compare cart by session lookup key.</summary>
    /// <seealso cref="SessionCartBySessionAndTypeLookupKey"/>
    public class CompareCartBySessionLookupKey : SessionCartBySessionAndTypeLookupKey
    {
        /// <summary>Initializes a new instance of the <see cref="CompareCartBySessionLookupKey"/> class.</summary>
        /// <param name="source">Source for the data.</param>
        public CompareCartBySessionLookupKey(string source)
        {
            var parsed = FromString(source);
            SessionID = parsed.SessionID;
            UserID = parsed.UserID;
            AccountID = parsed.AccountID;
            BrandID = parsed.BrandID;
            FranchiseID = parsed.FranchiseID;
            StoreID = parsed.StoreID;
        }

        /// <summary>Initializes a new instance of the <see cref="CompareCartBySessionLookupKey"/> class.</summary>
        /// <param name="sessionID">  Identifier for the session.</param>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="accountID">  Identifier for the account.</param>
        /// <param name="brandID">    Identifier for the brand.</param>
        /// <param name="franchiseID">Identifier for the franchise.</param>
        /// <param name="storeID">    Identifier for the store.</param>
        public CompareCartBySessionLookupKey(
            Guid sessionID,
            int? userID = null,
            int? accountID = null,
            int? brandID = null,
            int? franchiseID = null,
            int? storeID = null)
        {
            SessionID = sessionID;
            UserID = userID;
            AccountID = accountID;
            BrandID = brandID;
            FranchiseID = franchiseID;
            StoreID = storeID;
        }

        /// <inheritdoc/>
        public override string TypeKey
        {
            get => "Compare Cart";
            // ReSharper disable once ValueParameterNotUsed
            set { /* Do Nothing */ }
        }

        /// <summary>Initializes a <see cref="CompareCartBySessionLookupKey"/> from the given string.</summary>
        /// <param name="source">Source for the data.</param>
        /// <returns>A <see cref="CompareCartBySessionLookupKey"/>.</returns>
        public static new CompareCartBySessionLookupKey FromString(string source)
        {
            return JsonConvert.DeserializeObject<CompareCartBySessionLookupKey>(
                source,
                SerializableAttributesDictionaryExtensions.JsonSettings)!;
        }
    }
}
