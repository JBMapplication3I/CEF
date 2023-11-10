// <copyright file="TargetCartLookupKey.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target cart lookup key class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A target cart lookup key.</summary>
    /// <seealso cref="SessionCartBySessionAndTypeLookupKey"/>
    public class TargetCartLookupKey : SessionCartBySessionAndTypeLookupKey
    {
        /// <summary>Initializes a new instance of the <see cref="TargetCartLookupKey"/> class.</summary>
        /// <param name="source">Source for the data.</param>
        public TargetCartLookupKey(string source)
        {
            var parsed = FromString(source);
            Contract.Requires<ArgumentException>(parsed.TypeKey.StartsWith("Target-Grouping-"));
            TypeKey = parsed.TypeKey;
            SessionID = parsed.SessionID;
            UserID = parsed.UserID;
            AccountID = parsed.AccountID;
            BrandID = parsed.BrandID;
            FranchiseID = parsed.FranchiseID;
            StoreID = parsed.StoreID;
        }

        /// <summary>Initializes a new instance of the <see cref="TargetCartLookupKey"/> class.</summary>
        /// <param name="typeKey">    The type key.</param>
        /// <param name="sessionID">  Identifier for the session.</param>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="accountID">  Identifier for the account.</param>
        /// <param name="brandID">    Identifier for the brand.</param>
        /// <param name="franchiseID">Identifier for the franchise.</param>
        /// <param name="storeID">    Identifier for the store.</param>
        public TargetCartLookupKey(
            string typeKey,
            Guid sessionID,
            int? userID = null,
            int? accountID = null,
            int? brandID = null,
            int? franchiseID = null,
            int? storeID = null)
        {
            Contract.Requires<ArgumentException>(typeKey.StartsWith("Target-Grouping-"));
            TypeKey = typeKey;
            SessionID = sessionID;
            UserID = userID;
            AccountID = accountID;
            BrandID = brandID;
            FranchiseID = franchiseID;
            StoreID = storeID;
        }

        /// <summary>Initializes this TargetCartLookupKey from the given from session lookup key.</summary>
        /// <param name="sourceKey">Source key.</param>
        /// <returns>A TargetCartLookupKey.</returns>
        public static TargetCartLookupKey FromSessionLookupKey(SessionCartBySessionAndTypeLookupKey sourceKey)
        {
            return new(
                typeKey: sourceKey.TypeKey.StartsWith("Target-Grouping-") ? sourceKey.TypeKey : "Target-Grouping-",
                sessionID: sourceKey.SessionID,
                userID: sourceKey.UserID,
                accountID: sourceKey.AccountID,
                brandID: sourceKey.BrandID,
                franchiseID: sourceKey.FranchiseID,
                storeID: sourceKey.StoreID);
        }

        /// <summary>Initializes a <see cref="TargetCartLookupKey"/> from the given string.</summary>
        /// <param name="source">Source for the data.</param>
        /// <returns>A <see cref="TargetCartLookupKey"/>.</returns>
        public static new TargetCartLookupKey FromString(string source)
        {
            return JsonConvert.DeserializeObject<TargetCartLookupKey>(
                source,
                SerializableAttributesDictionaryExtensions.JsonSettings)!;
        }
    }
}
