// <copyright file="ReferralCodeGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the referral code generator class</summary>
namespace Clarity.Ecommerce.Utilities
{
    using System;

    /// <summary>A referral code generator.</summary>
    public static class ReferralCodeGenerator
    {
        /// <summary>Generates a code for user.</summary>
        /// <param name="userID">Identifier for the user.</param>
        /// <returns>The code for user.</returns>
        public static string GenerateCodeForUser(int userID)
        {
            var code = Guid.NewGuid().ToString();
            code = code[..12];
            code = code + "-" + userID;
            return code;
        }
    }
}
