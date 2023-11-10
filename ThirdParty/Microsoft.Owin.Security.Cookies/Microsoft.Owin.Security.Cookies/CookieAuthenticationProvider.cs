// <copyright file="CookieAuthenticationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie authentication provider class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System;
    using System.Threading.Tasks;

    /// <summary>This default implementation of the ICookieAuthenticationProvider may be used if the application only
    /// needs to override a few of the interface methods. This may be used as a base class or may be instantiated
    /// directly.</summary>
    /// <seealso cref="ICookieAuthenticationProvider"/>
    /// <seealso cref="ICookieAuthenticationProvider"/>
    public class CookieAuthenticationProvider : ICookieAuthenticationProvider
    {
        /// <summary>Create a new instance of the default provider.</summary>
        public CookieAuthenticationProvider()
        {
            OnValidateIdentity = context => Task.FromResult<object>(null);
            OnResponseSignIn = context => { };
            OnResponseSignedIn = context => { };
            OnResponseSignOut = context => { };
            OnApplyRedirect = DefaultBehavior.ApplyRedirect;
            OnException = context => { };
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on apply redirect.</value>
        public Action<CookieApplyRedirectContext> OnApplyRedirect
        {
            get;
            set;
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on exception.</value>
        public Action<CookieExceptionContext> OnException
        {
            get;
            set;
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on response signed in.</value>
        public Action<CookieResponseSignedInContext> OnResponseSignedIn
        {
            get;
            set;
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on response sign in.</value>
        public Action<CookieResponseSignInContext> OnResponseSignIn
        {
            get;
            set;
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on response sign out.</value>
        public Action<CookieResponseSignOutContext> OnResponseSignOut
        {
            get;
            set;
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on validate identity.</value>
        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity
        {
            get;
            set;
        }

        /// <summary>Implements the interface method by invoking the related delegate method.</summary>
        /// <param name="context">Contains information about the event.</param>
        public virtual void ApplyRedirect(CookieApplyRedirectContext context)
        {
            OnApplyRedirect(context);
        }

        /// <summary>Implements the interface method by invoking the related delegate method.</summary>
        /// <param name="context">Contains information about the event.</param>
        public virtual void Exception(CookieExceptionContext context)
        {
            OnException(context);
        }

        /// <summary>Implements the interface method by invoking the related delegate method.</summary>
        /// <param name="context">.</param>
        public virtual void ResponseSignedIn(CookieResponseSignedInContext context)
        {
            OnResponseSignedIn(context);
        }

        /// <summary>Implements the interface method by invoking the related delegate method.</summary>
        /// <param name="context">.</param>
        public virtual void ResponseSignIn(CookieResponseSignInContext context)
        {
            OnResponseSignIn(context);
        }

        /// <summary>Implements the interface method by invoking the related delegate method.</summary>
        /// <param name="context">.</param>
        public virtual void ResponseSignOut(CookieResponseSignOutContext context)
        {
            OnResponseSignOut(context);
        }

        /// <summary>Implements the interface method by invoking the related delegate method.</summary>
        /// <param name="context">.</param>
        /// <returns>A Task.</returns>
        public virtual Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            return OnValidateIdentity(context);
        }
    }
}
