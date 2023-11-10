// <copyright file="OpenIDService.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the open identifier service class</summary>
namespace ServiceStack.Auth
{
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Threading.Tasks;
    using Auth;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.JSConfigs;
    using Clarity.Ecommerce.Service;
    using JetBrains.Annotations;

    /// <summary>An open identifier service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class OpenIDService : ClarityEcommerceServiceBase
    {
        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>The result.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public async Task Post(OpenIDConnectCodeCallback request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                Response.Redirect(CEFConfigDictionary.SiteRouteRelativePath ?? "/");
                return;
            }
            try
            {
                var token = await OpenIDRequestHelper.RequestTokenAsync(Logger, request.Code).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(token?.Error))
                {
                    throw new InvalidOperationException(token!.Error);
                }
                var claimsPrincipal = await OpenIDRequestHelper.ValidateJwtAsync(token!.IdToken).ConfigureAwait(false);
                var email = claimsPrincipal!.FindFirst(CEFConfigDictionary.AuthProviderEmailClaimName!)?.Value;
                if (string.IsNullOrEmpty(email))
                {
                    Response.Redirect(CEFConfigDictionary.SiteRouteRelativePath ?? "/");
                    return;
                }
                var globalAccountNumber = email!.Substring(0, email.IndexOf('@'));
                Clarity.Ecommerce.DataModel.User user;
                using (var context = RegistryLoaderWrapper.GetContext(null))
                {
                    user = await context.Users
                        .SingleOrDefaultAsync(x => x.CustomKey == globalAccountNumber)
                        .ConfigureAwait(false);
                }
                if (user == null)
                {
                    Response.Redirect(CEFConfigDictionary.SiteRouteRelativePath ?? "/");
                    return;
                }
                var authRepository = RegistryLoaderWrapper.GetInstance<ICEFUserAuthRepository>();
                var auth = authRepository.GetUserAuthByUserName(user.Email);
                if (auth == null)
                {
                    Response.Redirect(CEFConfigDictionary.SiteRouteRelativePath ?? "/");
                    return;
                }
                var session = GetSession();
                var sessionId = session.Id;
                session.PopulateWith(auth);
                session.Id = sessionId;
                session.IsAuthenticated = true;
                session.UserAuthId = auth.Id.ToString(CultureInfo.InvariantCulture);
                session.UserName = user.UserName ?? email;
                if (string.IsNullOrEmpty(session.UserAuthName))
                {
                    session.UserAuthName = session.UserName;
                }
                session.ProviderOAuthAccess = authRepository.GetUserAuthDetails(session.UserAuthId)
                    .ConvertAll(x => (IAuthTokens)x);
#if NET5_0_OR_GREATER
                await this.SaveSessionAsync(session).ConfigureAwait(false);
#else
                this.SaveSession(session);
#endif
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(nameof(OpenIDConnectCodeCallback), ex.Message, ex, null).ConfigureAwait(false);
            }
            Response.Redirect(CEFConfigDictionary.SiteRouteRelativePath ?? "/");
        }
    }
}
