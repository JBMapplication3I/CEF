// <copyright file="AppBuilderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application builder extensions class</summary>
namespace Owin
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>Extensions off of IAppBuilder to make it easier to configure the SignInCookies.</summary>
    public static class AppBuilderExtensions
    {
        /// <summary>The cookie prefix.</summary>
        private const string CookiePrefix = ".AspNet.";

        /// <summary>Registers a callback that will be invoked to create an instance of type T that will be stored in the
        /// OwinContext which can fetched via context.Get.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="app">           The <see cref="IAppBuilder" /> passed to the configuration method.</param>
        /// <param name="createCallback">Invoked to create an instance of T.</param>
        /// <returns>The updated <see cref="IAppBuilder" /></returns>
        public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<T> createCallback)
            where T : class, IDisposable
        {
            return app.CreatePerOwinContext<T>((options, context) => createCallback());
        }

        /// <summary>Registers a callback that will be invoked to create an instance of type T that will be stored in the
        /// OwinContext which can fetched via context.Get.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="app">           .</param>
        /// <param name="createCallback">.</param>
        /// <returns>The new per owin context.</returns>
        public static IAppBuilder CreatePerOwinContext<T>(
            this IAppBuilder app,
            Func<IdentityFactoryOptions<T>, IOwinContext, T> createCallback)
            where T : class, IDisposable
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.CreatePerOwinContext(
                createCallback,
                (options, instance) => instance.Dispose());
        }

        /// <summary>Registers a callback that will be invoked to create an instance of type T that will be stored in the
        /// OwinContext which can fetched via context.Get.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="app">            .</param>
        /// <param name="createCallback"> .</param>
        /// <param name="disposeCallback">.</param>
        /// <returns>The new per owin context.</returns>
        public static IAppBuilder CreatePerOwinContext<T>(
            this IAppBuilder app,
            Func<IdentityFactoryOptions<T>, IOwinContext, T> createCallback,
            Action<IdentityFactoryOptions<T>, T> disposeCallback)
            where T : class, IDisposable
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (createCallback == null)
            {
                throw new ArgumentNullException(nameof(createCallback));
            }
            if (disposeCallback == null)
            {
                throw new ArgumentNullException(nameof(disposeCallback));
            }
            app.Use(
                typeof(IdentityFactoryMiddleware<T, IdentityFactoryOptions<T>>),
                new IdentityFactoryOptions<T>
                {
                    DataProtectionProvider = Microsoft.Owin.Security.DataProtection.AppBuilderExtensions.GetDataProtectionProvider(app),
                    Provider = new IdentityFactoryProvider<T>
                    {
                        OnCreate = createCallback,
                        OnDispose = disposeCallback,
                    },
                });
            return app;
        }

        /// <summary>Configure the app to use owin middleware based cookie authentication for external identities.</summary>
        /// <param name="app">.</param>
        public static void UseExternalSignInCookie(this IAppBuilder app)
        {
            app.UseExternalSignInCookie("ExternalCookie");
        }

        /// <summary>Configure the app to use owin middleware based cookie authentication for external identities.</summary>
        /// <param name="app">                       .</param>
        /// <param name="externalAuthenticationType">.</param>
        public static void UseExternalSignInCookie(this IAppBuilder app, string externalAuthenticationType)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.SetDefaultSignInAsAuthenticationType(externalAuthenticationType);
            var cookieAuthenticationOption = new CookieAuthenticationOptions
            {
                AuthenticationType = externalAuthenticationType,
                AuthenticationMode = (AuthenticationMode)1,
                CookieName = string.Concat(".AspNet.", externalAuthenticationType),
                ExpireTimeSpan = TimeSpan.FromMinutes(5)
            };
            app.UseCookieAuthentication(cookieAuthenticationOption);
        }

        /// <summary>Configure the app to use owin middleware based oauth bearer tokens.</summary>
        /// <param name="app">    .</param>
        /// <param name="options">.</param>
        public static void UseOAuthBearerTokens(this IAppBuilder app, OAuthAuthorizationServerOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            app.UseOAuthAuthorizationServer(options);
            var oAuthBearerAuthenticationOption = new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = options.AccessTokenFormat,
                AccessTokenProvider = options.AccessTokenProvider,
                AuthenticationMode = options.AuthenticationMode,
                AuthenticationType = options.AuthenticationType,
                Description = options.Description,
                Provider = new ApplicationOAuthBearerProvider(),
                SystemClock = options.SystemClock
            };
            app.UseOAuthBearerAuthentication(oAuthBearerAuthenticationOption);
            var oAuthBearerAuthenticationOption1 = new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = options.AccessTokenFormat,
                AccessTokenProvider = options.AccessTokenProvider,
                AuthenticationMode = (AuthenticationMode)1,
                AuthenticationType = "ExternalBearer",
                Description = options.Description,
                Provider = new ExternalOAuthBearerProvider(),
                SystemClock = options.SystemClock
            };
            app.UseOAuthBearerAuthentication(oAuthBearerAuthenticationOption1);
        }

        /// <summary>Configures a cookie intended to be used to store whether two factor authentication has been done
        /// already.</summary>
        /// <param name="app">               .</param>
        /// <param name="authenticationType">.</param>
        public static void UseTwoFactorRememberBrowserCookie(this IAppBuilder app, string authenticationType)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var cookieAuthenticationOption = new CookieAuthenticationOptions
            {
                AuthenticationType = authenticationType,
                AuthenticationMode = (AuthenticationMode)1,
                CookieName = string.Concat(".AspNet.", authenticationType)
            };
            app.UseCookieAuthentication(cookieAuthenticationOption);
        }

        /// <summary>Configures a cookie intended to be used to store the partial credentials for two factor
        /// authentication.</summary>
        /// <param name="app">               .</param>
        /// <param name="authenticationType">.</param>
        /// <param name="expires">           .</param>
        public static void UseTwoFactorSignInCookie(this IAppBuilder app, string authenticationType, TimeSpan expires)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var cookieAuthenticationOption = new CookieAuthenticationOptions
            {
                AuthenticationType = authenticationType,
                AuthenticationMode = (AuthenticationMode)1,
                CookieName = string.Concat(".AspNet.", authenticationType),
                ExpireTimeSpan = expires
            };
            app.UseCookieAuthentication(cookieAuthenticationOption);
        }

        /// <summary>An application o authentication bearer provider.</summary>
        /// <seealso cref="OAuthBearerAuthenticationProvider"/>
        private class ApplicationOAuthBearerProvider : OAuthBearerAuthenticationProvider
        {
            /// <inheritdoc/>
            public override Task ValidateIdentity(OAuthValidateIdentityContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                if (context.Ticket.Identity.Claims.Any((Claim c) => c.Issuer != "LOCAL AUTHORITY"))
                {
                    context.Rejected();
                }
                return Task.FromResult<object>(null);
            }
        }

        /// <summary>An external o authentication bearer provider.</summary>
        /// <seealso cref="OAuthBearerAuthenticationProvider"/>
        private class ExternalOAuthBearerProvider : OAuthBearerAuthenticationProvider
        {
            /// <inheritdoc/>
            public override Task ValidateIdentity(OAuthValidateIdentityContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                if (!context.Ticket.Identity.Claims.Any())
                {
                    context.Rejected();
                }
                else if (context.Ticket.Identity.Claims.All((Claim c) => c.Issuer == "LOCAL AUTHORITY"))
                {
                    context.Rejected();
                }
                return Task.FromResult<object>(null);
            }
        }
    }
}
