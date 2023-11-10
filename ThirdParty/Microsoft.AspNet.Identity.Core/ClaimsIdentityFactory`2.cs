// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.ClaimsIdentityFactory`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>Creates a ClaimsIdentity from a User.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="Microsoft.AspNet.Identity.IClaimsIdentityFactory{TUser,TKey}"/>
    /// <seealso cref="IClaimsIdentityFactory{TUser,TKey}"/>
    public class ClaimsIdentityFactory<TUser, TKey> : IClaimsIdentityFactory<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>The default identity provider claim value.</summary>
        internal const string DefaultIdentityProviderClaimValue = "ASP.NET Identity";

        /// <summary>Type of the identity provider claim.</summary>
        internal const string IdentityProviderClaimType =
            "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        /// <summary>Constructor.</summary>
        public ClaimsIdentityFactory()
        {
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            SecurityStampClaimType = "AspNet.Identity.SecurityStamp";
        }

        /// <summary>Claim type used for role claims.</summary>
        /// <value>The type of the role claim.</value>
        public string RoleClaimType { get; set; }

        /// <summary>Claim type used for the user security stamp.</summary>
        /// <value>The type of the security stamp claim.</value>
        public string SecurityStampClaimType { get; set; }

        /// <summary>Claim type used for the user id.</summary>
        /// <value>The type of the user identifier claim.</value>
        public string UserIdClaimType { get; set; }

        /// <summary>Claim type used for the user name.</summary>
        /// <value>The type of the user name claim.</value>
        public string UserNameClaimType { get; set; }

        /// <summary>Convert the key to a string, by default just calls .ToString()</summary>
        /// <param name="key">.</param>
        /// <returns>The identifier converted to string.</returns>
        public virtual string ConvertIdToString(TKey key)
        {
            return (object)key != null ? key.ToString() : throw new ArgumentNullException(nameof(key));
        }

        /// <summary>Create a ClaimsIdentity from a user.</summary>
        /// <param name="manager">           .</param>
        /// <param name="user">              .</param>
        /// <param name="authenticationType">.</param>
        /// <returns>The new asynchronous.</returns>
        public virtual async Task<ClaimsIdentity> CreateAsync(
            UserManager<TUser, TKey> manager,
            TUser user,
            string authenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var id = new ClaimsIdentity(authenticationType, UserNameClaimType, RoleClaimType);
            id.AddClaim(
                new Claim(UserIdClaimType, ConvertIdToString(user.Id), "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim(UserNameClaimType, user.UserName, "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(
                new Claim(
                    "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "ASP.NET Identity",
                    "http://www.w3.org/2001/XMLSchema#string"));
            ClaimsIdentity claimsIdentity;
            if (manager.SupportsUserSecurityStamp)
            {
                claimsIdentity = id;
                var type = SecurityStampClaimType;
                claimsIdentity.AddClaim(
                    new Claim(type, await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()));
                claimsIdentity = null;
                type = null;
            }
            if (manager.SupportsUserRole)
            {
                foreach (var str in await manager.GetRolesAsync(user.Id).WithCurrentCulture())
                {
                    id.AddClaim(new Claim(RoleClaimType, str, "http://www.w3.org/2001/XMLSchema#string"));
                }
            }
            if (manager.SupportsUserClaim)
            {
                claimsIdentity = id;
                claimsIdentity.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture());
                claimsIdentity = null;
            }
            return id;
        }
    }
}
