#if !NETSTANDARD1_6

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
#if NET_4_0
using System.Diagnostics.Contracts;
#endif
using System.Globalization;
using System.Security.Principal;
using System.Web;

namespace ServiceStack.Html.AntiXsrf
{
    internal sealed class TokenValidator : ITokenValidator
    {
        private readonly IClaimUidExtractor _claimUidExtractor;
        private readonly IAntiForgeryConfig _config;

        internal TokenValidator(IAntiForgeryConfig config, IClaimUidExtractor claimUidExtractor)
        {
            _config = config;
            _claimUidExtractor = claimUidExtractor;
        }

        public AntiForgeryToken GenerateCookieToken()
        {
            return new()
            {
                // SecurityToken will be populated automatically.
                IsSessionToken = true
            };
        }

        public AntiForgeryToken GenerateFormToken(HttpContextBase httpContext, IIdentity identity, AntiForgeryToken cookieToken)
        {
#if NET_4_0
            Contract.Assert(IsCookieTokenValid(cookieToken));
#endif
            var formToken = new AntiForgeryToken()
            {
                SecurityToken = cookieToken.SecurityToken,
                IsSessionToken = false
            };

            var requireAuthenticatedUserHeuristicChecks = false;
            // populate Username and ClaimUid
            if (identity?.IsAuthenticated == true)
            {
                if (!_config.SuppressIdentityHeuristicChecks)
                {
                    // If the user is authenticated and heuristic checks are not suppressed,
                    // then Username, ClaimUid, or AdditionalData must be set.
                    requireAuthenticatedUserHeuristicChecks = true;
                }

                formToken.ClaimUid = _claimUidExtractor.ExtractClaimUid(identity);
                if (formToken.ClaimUid == null)
                {
                    formToken.Username = identity.Name;
                }
            }

            // populate AdditionalData
            if (_config.AdditionalDataProvider != null)
            {
                formToken.AdditionalData = _config.AdditionalDataProvider.GetAdditionalData(httpContext);
            }

            if (requireAuthenticatedUserHeuristicChecks
                && string.IsNullOrEmpty(formToken.Username)
                && formToken.ClaimUid == null
                && string.IsNullOrEmpty(formToken.AdditionalData))
            {
                // Application says user is authenticated, but we have no identifier for the user.
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                    MvcResources.TokenValidator_AuthenticatedUserWithoutUsername, identity.GetType()));
            }

            return formToken;
        }

        public bool IsCookieTokenValid(AntiForgeryToken cookieToken)
        {
            return cookieToken?.IsSessionToken == true;
        }

        public void ValidateTokens(HttpContextBase httpContext, IIdentity identity, AntiForgeryToken sessionToken, AntiForgeryToken fieldToken)
        {
            // Were the tokens even present at all?
            if (sessionToken == null)
            {
                throw HttpAntiForgeryException.CreateCookieMissingException(_config.CookieName);
            }
            if (fieldToken == null)
            {
                throw HttpAntiForgeryException.CreateFormFieldMissingException(_config.FormFieldName);
            }

            // Do the tokens have the correct format?
            if (!sessionToken.IsSessionToken || fieldToken.IsSessionToken)
            {
                throw HttpAntiForgeryException.CreateTokensSwappedException(_config.CookieName, _config.FormFieldName);
            }

            // Are the security tokens embedded in each incoming token identical?
            if (!Equals(sessionToken.SecurityToken, fieldToken.SecurityToken))
            {
                throw HttpAntiForgeryException.CreateSecurityTokenMismatchException();
            }

            // Is the incoming token meant for the current user?
            var currentUsername = string.Empty;
            BinaryBlob currentClaimUid = null;

            if (identity?.IsAuthenticated == true)
            {
                currentClaimUid = _claimUidExtractor.ExtractClaimUid(identity);
                if (currentClaimUid == null)
                {
                    currentUsername = identity.Name ?? string.Empty;
                }
            }

            // OpenID and other similar authentication schemes use URIs for the username.
            // These should be treated as case-sensitive.
            var useCaseSensitiveUsernameComparison = currentUsername.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || currentUsername.StartsWith("https://", StringComparison.OrdinalIgnoreCase);

            if (!string.Equals(fieldToken.Username, currentUsername, useCaseSensitiveUsernameComparison ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw HttpAntiForgeryException.CreateUsernameMismatchException(fieldToken.Username, currentUsername);
            }
            if (!Equals(fieldToken.ClaimUid, currentClaimUid))
            {
                throw HttpAntiForgeryException.CreateClaimUidMismatchException();
            }

            // Is the AdditionalData valid?
            if (_config.AdditionalDataProvider?.ValidateAdditionalData(httpContext, fieldToken.AdditionalData) == false)
            {
                throw HttpAntiForgeryException.CreateAdditionalDataCheckFailedException();
            }
        }
    }
}

#endif
