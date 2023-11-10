// <copyright file="OwinRequestExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the owin request extensions class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading.Tasks;

    /// <summary>An owin request extensions.</summary>
    internal static class OwinRequestExtensions
    {
        /// <summary>An IOwinRequest extension method that registers the authentication handler.</summary>
        /// <param name="request">The request to act on.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>An object.</returns>
        public static object RegisterAuthenticationHandler(this IOwinRequest request, AuthenticationHandler handler)
        {
            var hook = new Hook(
                handler,
                request
                    .Get<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>(Constants.SecurityAuthenticate));
            request.Set(
                Constants.SecurityAuthenticate,
                new Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>,
                    object, Task>(hook.AuthenticateAsync));
            return hook;
        }

        /// <summary>An IOwinRequest extension method that unregisters the authentication handler.</summary>
        /// <param name="request">     The request to act on.</param>
        /// <param name="registration">The registration.</param>
        public static void UnregisterAuthenticationHandler(this IOwinRequest request, object registration)
        {
            if (registration is not Hook hook)
            {
                throw new InvalidOperationException(Resources.Exception_UnhookAuthenticationStateType);
            }
            request.Set(Constants.SecurityAuthenticate, hook.Chained);
        }

        /// <summary>A hook.</summary>
        private class Hook
        {
            /// <summary>The handler.</summary>
            private readonly AuthenticationHandler _handler;

            /// <summary>Initializes a new instance of the
            /// <see cref="Hook" /> class.</summary>
            /// <param name="handler">The handler.</param>
            /// <param name="chained">The chained.</param>
            public Hook(
                AuthenticationHandler handler,
                Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> chained)
            {
                _handler = handler;
                Chained = chained;
            }

            /// <summary>Gets the chained.</summary>
            /// <value>The chained.</value>
            public Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> Chained
            {
                get;
            }

            /// <summary>Authenticate asynchronous.</summary>
            /// <param name="authenticationTypes">List of types of the authentications.</param>
            /// <param name="callback">           The callback.</param>
            /// <param name="state">              The state.</param>
            /// <returns>A Task.</returns>
            public async Task AuthenticateAsync(
                string[] authenticationTypes,
                Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object> callback,
                object state)
            {
                if (authenticationTypes == null)
                {
                    callback(null, null, _handler.BaseOptions.Description.Properties, state);
                }
                else if (authenticationTypes.Contains(_handler.BaseOptions.AuthenticationType, StringComparer.Ordinal))
                {
                    var authenticationTicket = await _handler.AuthenticateAsync();
                    if (authenticationTicket != null && authenticationTicket.Identity != null)
                    {
                        callback(
                            authenticationTicket.Identity,
                            authenticationTicket.Properties.Dictionary,
                            _handler.BaseOptions.Description.Properties,
                            state);
                    }
                }
                if (Chained != null)
                {
                    await Chained(authenticationTypes, callback, state);
                }
            }
        }
    }
}
