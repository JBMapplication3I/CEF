// <copyright file="DataProtectorTokenProvider`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the data protector token provider` 1 class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using Microsoft.Owin.Security.DataProtection;

    /// <summary>Token provider that uses an IDataProtector to generate encrypted tokens based off of the security
    /// stamp.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <seealso cref="DataProtectorTokenProvider{TUser,String}"/>
    public class DataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser, string>
        where TUser : class, IUser<string>
    {
        /// <summary>Constructor.</summary>
        /// <param name="protector">.</param>
        public DataProtectorTokenProvider(IDataProtector protector) : base(protector) { }
    }
}
