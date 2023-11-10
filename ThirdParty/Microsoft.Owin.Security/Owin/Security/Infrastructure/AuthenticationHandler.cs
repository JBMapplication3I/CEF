// <copyright file="AuthenticationHandler.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication handler class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;
    using DataHandler.Encoder;
    using Logging;
    using Owin.Infrastructure;

    /// <summary>Base class for the per-request work performed by most authentication middleware.</summary>
    public abstract class AuthenticationHandler
    {
        /// <summary>The random.</summary>
        private static readonly RNGCryptoServiceProvider Random;

        /// <summary>The apply response.</summary>
        private Task _applyResponse;

        /// <summary>True if apply response initialized.</summary>
        private bool _applyResponseInitialized;

        /// <summary>The apply response synchronise lock.</summary>
        private object _applyResponseSyncLock;

        /// <summary>The authenticate.</summary>
        private Task<AuthenticationTicket> _authenticate;

        /// <summary>True if authenticate initialized.</summary>
        private bool _authenticateInitialized;

        /// <summary>The authenticate synchronise lock.</summary>
        private object _authenticateSyncLock;

        /// <summary>The registration.</summary>
        private object _registration;

        /// <summary>Initializes static members of the Microsoft.Owin.Security.Infrastructure.AuthenticationHandler class.</summary>
        static AuthenticationHandler()
        {
            Random = new RNGCryptoServiceProvider();
        }

        /// <summary>Gets options for controlling the base.</summary>
        /// <value>Options that control the base.</value>
        internal AuthenticationOptions BaseOptions { get; private set; }

        /// <summary>Gets the context.</summary>
        /// <value>The context.</value>
        protected IOwinContext Context
        {
            get;
            private set;
        }

        /// <summary>Gets or sets a value indicating whether the faulted.</summary>
        /// <value>True if faulted, false if not.</value>
        protected bool Faulted
        {
            get;
            set;
        }

        /// <summary>Gets the helper.</summary>
        /// <value>The helper.</value>
        protected SecurityHelper Helper
        {
            get;
            private set;
        }

        /// <summary>Gets the request.</summary>
        /// <value>The request.</value>
        protected IOwinRequest Request => Context.Request;

        /// <summary>Gets the request path base.</summary>
        /// <value>The request path base.</value>
        protected PathString RequestPathBase
        {
            get;
            private set;
        }

        /// <summary>Gets the response.</summary>
        /// <value>The response.</value>
        protected IOwinResponse Response => Context.Response;

        /// <summary>Causes the authentication logic in AuthenticateCore to be performed for the current request at most
        /// once and returns the results. Calling Authenticate more than once will always return the original value.
        /// This method should always be called instead of calling AuthenticateCore directly.</summary>
        /// <returns>The ticket data provided by the authentication logic.</returns>
        public Task<AuthenticationTicket> AuthenticateAsync()
        {
            var authenticationHandler = this;
            return LazyInitializer.EnsureInitialized(
                ref _authenticate,
                ref _authenticateInitialized,
                ref _authenticateSyncLock,
                authenticationHandler.AuthenticateCoreAsync);
        }

        /// <summary>Called once by common code after initialization. If an authentication middleware responds directly
        /// to specifically known paths it must override this virtual, compare the request path to it's known paths,
        /// provide any response information as appropriate, and true to stop further processing.</summary>
        /// <returns>Returning false will cause the common code to call the next middleware in line. Returning true will
        /// cause the common code to begin the async completion journey without calling the rest of the middleware
        /// pipeline.</returns>
        public virtual Task<bool> InvokeAsync()
        {
            return Task.FromResult(false);
        }

        /// <summary>Called once per request after Initialize and Invoke.</summary>
        /// <returns>async completion.</returns>
        internal async Task TeardownAsync()
        {
            await ApplyResponseAsync();
            await TeardownCoreAsync();
            Request.UnregisterAuthenticationHandler(_registration);
        }

        /// <summary>Override this method to deal with 401 challenge concerns, if an authentication scheme in question
        /// deals an authentication interaction as part of it's request flow. (like adding a response header, or
        /// changing the 401 result to 302 of a login page or external sign-in location.)</summary>
        /// <returns>A Task.</returns>
        protected virtual Task ApplyResponseChallengeAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>Core method that may be overridden by handler. The default behavior is to call two common response
        /// activities, one that deals with sign-in/sign-out concerns, and a second to deal with 401 challenges.</summary>
        /// <returns>A Task.</returns>
        protected virtual async Task ApplyResponseCoreAsync()
        {
            await ApplyResponseGrantAsync();
            await ApplyResponseChallengeAsync();
        }

        /// <summary>Override this method to dela with sign-in/sign-out concerns, if an authentication scheme in question
        /// deals with grant/revoke as part of it's request flow. (like setting/deleting cookies)</summary>
        /// <returns>A Task.</returns>
        protected virtual Task ApplyResponseGrantAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>The core authentication logic which must be provided by the handler. Will be invoked at most once
        /// per request. Do not call directly, call the wrapping Authenticate method instead.</summary>
        /// <returns>The ticket data provided by the authentication logic.</returns>
        protected abstract Task<AuthenticationTicket> AuthenticateCoreAsync();

        /// <summary>Base initialize asynchronous.</summary>
        /// <param name="options">Options for controlling the operation.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        protected async Task BaseInitializeAsync(AuthenticationOptions options, IOwinContext context)
        {
            BaseOptions = options;
            Context = context;
            Helper = new SecurityHelper(context);
            RequestPathBase = Request.PathBase;
            _registration = Request.RegisterAuthenticationHandler(this);
            Response.OnSendingHeaders(OnSendingHeaderCallback, this);
            await InitializeCoreAsync();
            if (BaseOptions.AuthenticationMode == AuthenticationMode.Active)
            {
                var authenticationTicket = await AuthenticateAsync();
                if (authenticationTicket != null && authenticationTicket.Identity != null)
                {
                    Helper.AddUserIdentity(authenticationTicket.Identity);
                }
            }
        }

        /// <summary>Generates a correlation identifier.</summary>
        /// <param name="properties">The properties.</param>
        protected void GenerateCorrelationId(AuthenticationProperties properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            var str = string.Concat(".AspNet.Correlation.", BaseOptions.AuthenticationType);
            var numArray = new byte[32];
            Random.GetBytes(numArray);
            var str1 = TextEncodings.Base64Url.Encode(numArray);
            var cookieOption = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Secure = Request.IsSecure,
            };
            properties.Dictionary[str] = str1;
            Response.Cookies.Append(str, str1, cookieOption);
        }

        /// <summary>Generates a correlation identifier.</summary>
        /// <param name="cookieManager">Manager for cookie.</param>
        /// <param name="properties">   The properties.</param>
        protected void GenerateCorrelationId(ICookieManager cookieManager, AuthenticationProperties properties)
        {
            if (cookieManager == null)
            {
                throw new ArgumentNullException(nameof(cookieManager));
            }
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            var str = string.Concat(".AspNet.Correlation.", BaseOptions.AuthenticationType);
            var numArray = new byte[32];
            Random.GetBytes(numArray);
            var str1 = TextEncodings.Base64Url.Encode(numArray);
            var cookieOption = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Secure = Request.IsSecure,
            };
            properties.Dictionary[str] = str1;
            cookieManager.AppendResponseCookie(Context, str, str1, cookieOption);
        }

        /// <summary>Initializes the core asynchronous.</summary>
        /// <returns>A Task.</returns>
        protected virtual Task InitializeCoreAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>Teardown core asynchronous.</summary>
        /// <returns>A Task.</returns>
        protected virtual Task TeardownCoreAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>Validates the correlation identifier.</summary>
        /// <param name="properties">The properties.</param>
        /// <param name="logger">    The logger.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected bool ValidateCorrelationId(AuthenticationProperties properties, ILogger logger)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            var str1 = string.Concat(".AspNet.Correlation.", BaseOptions.AuthenticationType);
            var item = Request.Cookies[str1];
            if (string.IsNullOrWhiteSpace(item))
            {
                logger.WriteWarning("{0} cookie not found.", str1);
                return false;
            }
            var cookieOption = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Secure = Request.IsSecure,
            };
            Response.Cookies.Delete(str1, cookieOption);
            if (!properties.Dictionary.TryGetValue(str1, out var str))
            {
                logger.WriteWarning("{0} state property not found.", str1);
                return false;
            }
            properties.Dictionary.Remove(str1);
            if (string.Equals(item, str, StringComparison.Ordinal))
            {
                return true;
            }
            logger.WriteWarning("{0} correlation cookie and state property mismatch.", str1);
            return false;
        }

        /// <summary>Validates the correlation identifier.</summary>
        /// <param name="cookieManager">Manager for cookie.</param>
        /// <param name="properties">   The properties.</param>
        /// <param name="logger">       The logger.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected bool ValidateCorrelationId(
            ICookieManager cookieManager,
            AuthenticationProperties properties,
            ILogger logger)
        {
            if (cookieManager == null)
            {
                throw new ArgumentNullException(nameof(cookieManager));
            }
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            var str1 = string.Concat(".AspNet.Correlation.", BaseOptions.AuthenticationType);
            var requestCookie = cookieManager.GetRequestCookie(Context, str1);
            if (string.IsNullOrWhiteSpace(requestCookie))
            {
                logger.WriteWarning("{0} cookie not found.", str1);
                return false;
            }
            var cookieOption = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Secure = Request.IsSecure,
            };
            cookieManager.DeleteCookie(Context, str1, cookieOption);
            if (!properties.Dictionary.TryGetValue(str1, out var str))
            {
                logger.WriteWarning("{0} state property not found.", str1);
                return false;
            }
            properties.Dictionary.Remove(str1);
            if (string.Equals(requestCookie, str, StringComparison.Ordinal))
            {
                return true;
            }
            logger.WriteWarning("{0} correlation cookie and state property mismatch.", str1);
            return false;
        }

        /// <summary>Executes the sending header callback action.</summary>
        /// <param name="state">The state.</param>
        private static void OnSendingHeaderCallback(object state)
        {
            ((AuthenticationHandler)state).ApplyResponseAsync().Wait();
        }

        /// <summary>Causes the ApplyResponseCore to be invoked at most once per request. This method will be invoked
        /// either earlier, when the response headers are sent as a result of a response write or flush, or later, as
        /// the last step when the original async call to the middleware is returning.</summary>
        /// <returns>A Task.</returns>
        private async Task ApplyResponseAsync()
        {
            try
            {
                if (!Faulted)
                {
                    var authenticationHandler = this;
                    await LazyInitializer.EnsureInitialized(
                        ref _applyResponse,
                        ref _applyResponseInitialized,
                        ref _applyResponseSyncLock,
                        authenticationHandler.ApplyResponseCoreAsync);
                }
            }
            catch (Exception)
            {
                Faulted = true;
                throw;
            }
        }
    }
}
