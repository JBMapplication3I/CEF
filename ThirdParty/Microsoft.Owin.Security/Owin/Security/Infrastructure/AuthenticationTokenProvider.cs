// <copyright file="AuthenticationTokenProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication token provider class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    /// <summary>An authentication token provider.</summary>
    /// <seealso cref="IAuthenticationTokenProvider"/>
    /// <seealso cref="IAuthenticationTokenProvider"/>
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        /// <summary>Gets or sets the on create.</summary>
        /// <value>The on create.</value>
        public Action<AuthenticationTokenCreateContext> OnCreate
        {
            get;
            set;
        }

        /// <summary>Gets or sets the on create asynchronous.</summary>
        /// <value>The on create asynchronous.</value>
        public Func<AuthenticationTokenCreateContext, Task> OnCreateAsync
        {
            get;
            set;
        }

        /// <summary>Gets or sets the on receive.</summary>
        /// <value>The on receive.</value>
        public Action<AuthenticationTokenReceiveContext> OnReceive
        {
            get;
            set;
        }

        /// <summary>Gets or sets the on receive asynchronous.</summary>
        /// <value>The on receive asynchronous.</value>
        public Func<AuthenticationTokenReceiveContext, Task> OnReceiveAsync
        {
            get;
            set;
        }

        /// <summary>Creates this AuthenticationTokenProvider.</summary>
        /// <param name="context">The context.</param>
        public virtual void Create(AuthenticationTokenCreateContext context)
        {
            if (OnCreateAsync != null && OnCreate == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            OnCreate?.Invoke(context);
        }

        /// <summary>Creates an asynchronous.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The new asynchronous.</returns>
        public virtual async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            if (OnCreateAsync != null && OnCreate == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            if (OnCreateAsync == null)
            {
                Create(context);
            }
            else
            {
                await OnCreateAsync(context);
            }
        }

        /// <summary>Receives the given context.</summary>
        /// <param name="context">The context.</param>
        public virtual void Receive(AuthenticationTokenReceiveContext context)
        {
            if (OnReceiveAsync != null && OnReceive == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            OnReceive?.Invoke(context);
        }

        /// <summary>Receive asynchronous.</summary>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        public virtual async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            if (OnReceiveAsync != null && OnReceive == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            if (OnReceiveAsync == null)
            {
                Receive(context);
            }
            else
            {
                await OnReceiveAsync(context);
            }
        }
    }
}
