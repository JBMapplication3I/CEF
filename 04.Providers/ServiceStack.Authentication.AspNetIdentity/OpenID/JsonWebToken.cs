// <copyright file="JsonWebToken.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the JsonWebToken class</summary>
namespace ServiceStack.Auth
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using Clarity.Ecommerce.JSConfigs;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>The JsonWebToken class.</summary>
    public static class JsonWebToken
    {
        private const string NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        private const string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        private const string ActorClaimType = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
        private const string StringClaimValueType = "http://www.w3.org/2001/XMLSchema#string";

        /// <summary>Validate the Token.</summary>
        /// <param name="token">The API token.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="audience">The token audience.</param>
        /// <param name="issuer">The token issuer.</param>
        /// <returns>A ClaimsPrincipal.</returns>
        public static ClaimsPrincipal ValidateToken(
            string token,
            X509Certificate2 certificate,
            string? audience = null,
            string? issuer = null)
        {
            var claims = ValidateIdentityToken(token, audience, certificate);
            return new(ClaimsIdentityFromJwt(claims, issuer));
        }

        private static ClaimsIdentity ClaimsIdentityFromJwt(IEnumerable<Claim> claims, string? issuer)
        {
            var subject = new ClaimsIdentity("Federation", NameClaimType, RoleClaimType);
            foreach (var claim in claims)
            {
                var type = claim.Type;
                switch (type)
                {
                    case ActorClaimType when subject.Actor != null:
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                "Jwt10401: Only a single 'Actor' is supported. Found second claim of type: '{0}', value: '{1}'",
                                new object[] { "actor", claim.Value }));
                    }
                    case ActorClaimType:
                    {
                        subject.AddClaim(new(type, claim.Value, claim.ValueType, issuer, issuer, subject));
                        continue;
                    }
                    case "name":
                    {
                        subject.AddClaim(new(NameClaimType, claim.Value, StringClaimValueType, issuer, issuer));
                        continue;
                    }
                    case "role":
                    {
                        subject.AddClaim(new(RoleClaimType, claim.Value, StringClaimValueType, issuer, issuer));
                        continue;
                    }
                }
                var newClaim = new Claim(type, claim.Value, claim.ValueType, issuer, issuer, subject);
                foreach (var prop in claim.Properties)
                {
                    newClaim.Properties.Add(prop);
                }
                subject.AddClaim(newClaim);
            }
            return subject;
        }

        private static IEnumerable<Claim> ValidateIdentityToken(
            string token,
            string? audience,
            X509Certificate2 certificate)
        {
            var parameters = new TokenValidationParameters
            {
                ValidAudience = audience,
                ValidIssuer = CEFConfigDictionary.AuthProviderIssuer,
                IssuerSigningKey = new X509SecurityKey(certificate),
            };
            var handler = new JwtSecurityTokenHandler();
            var id = handler.ValidateToken(token, parameters, out _);
            return id.Claims;
        }
    }
}
