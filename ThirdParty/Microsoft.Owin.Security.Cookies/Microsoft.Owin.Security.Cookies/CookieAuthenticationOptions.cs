// <copyright file="CookieAuthenticationOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie authentication options class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System;
    using Owin.Infrastructure;

    /// <summary>Contains the options used by the CookiesAuthenticationMiddleware.</summary>
    /// <seealso cref="AuthenticationOptions"/>
    /// <seealso cref="AuthenticationOptions"/>
    public class CookieAuthenticationOptions : AuthenticationOptions
    {
        /// <summary>Name of the cookie.</summary>
        private string _cookieName;

        /// <summary>Create an instance of the options initialized with the default values.</summary>
        public CookieAuthenticationOptions() : base("Cookies")
        {
            ReturnUrlParameter = "ReturnUrl";
            CookiePath = "/";
            ExpireTimeSpan = TimeSpan.FromDays(14);
            SlidingExpiration = true;
            CookieHttpOnly = true;
            CookieSecure = CookieSecureOption.SameAsRequest;
            SystemClock = new SystemClock();
            Provider = new CookieAuthenticationProvider();
        }

        /// <summary>Determines the domain used to create the cookie. Is not provided by default.</summary>
        /// <value>The cookie domain.</value>
        public string CookieDomain
        {
            get;
            set;
        }

        /// <summary>Determines if the browser should allow the cookie to be accessed by client-side javascript. The
        /// default is true, which means the cookie will only be passed to http requests and is not made available to
        /// script on the page.</summary>
        /// <value>True if cookie HTTP only, false if not.</value>
        public bool CookieHttpOnly
        {
            get;
            set;
        }

        /// <summary>The component used to get cookies from the request or set them on the response.
        /// ChunkingCookieManager will be used by default.</summary>
        /// <value>The cookie manager.</value>
        public ICookieManager CookieManager
        {
            get;
            set;
        }

        /// <summary>Determines the cookie name used to persist the identity. The default value is ".AspNet.Cookies".
        /// This value should be changed if you change the name of the AuthenticationType, especially if your system
        /// uses the cookie authentication middleware multiple times.</summary>
        /// <value>The name of the cookie.</value>
        public string CookieName
        {
            get => _cookieName;
            set
            {
                _cookieName = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Determines the path used to create the cookie. The default value is "/" for highest browser
        /// compatability.</summary>
        /// <value>The full pathname of the cookie file.</value>
        public string CookiePath
        {
            get;
            set;
        }

        /// <summary>Determines if the browser should allow the cookie to be sent with requests initiated from other
        /// sites. The default is 'null' to exclude the setting and let the browser choose the default behavior.</summary>
        /// <value>The cookie same site.</value>
        public SameSiteMode? CookieSameSite
        {
            get;
            set;
        }

        /// <summary>Determines if the cookie should only be transmitted on HTTPS request. The default is to limit the
        /// cookie to HTTPS requests if the page which is doing the SignIn is also HTTPS. If you have an HTTPS sign in
        /// page and portions of your site are HTTP you may need to change this value.</summary>
        /// <value>The cookie secure.</value>
        public CookieSecureOption CookieSecure
        {
            get;
            set;
        }

        /// <summary>Controls how much time the cookie will remain valid from the point it is created. The expiration
        /// information is in the protected cookie ticket. Because of that an expired cookie will be ignored even if it
        /// is passed to the server after the browser should have purged it.</summary>
        /// <value>The expire time span.</value>
        public TimeSpan ExpireTimeSpan
        {
            get;
            set;
        }

        /// <summary>The LoginPath property informs the middleware that it should change an outgoing 401 Unauthorized
        /// status code into a 302 redirection onto the given login path. The current url which generated the 401 is
        /// added to the LoginPath as a query string parameter named by the ReturnUrlParameter. Once a request to the
        /// LoginPath grants a new SignIn identity, the ReturnUrlParameter value is used to redirect the browser back to
        /// the url which caused the original unauthorized status code. If the LoginPath is null or empty, the
        /// middleware will not look for 401 Unauthorized status codes, and it will not redirect automatically when a
        /// login occurs.</summary>
        /// <value>The full pathname of the login file.</value>
        public PathString LoginPath
        {
            get;
            set;
        }

        /// <summary>If the LogoutPath is provided the middleware then a request to that path will redirect based on the
        /// ReturnUrlParameter.</summary>
        /// <value>The full pathname of the logout file.</value>
        public PathString LogoutPath
        {
            get;
            set;
        }

        /// <summary>The Provider may be assigned to an instance of an object created by the application at startup time.
        /// The middleware calls methods on the provider which give the application control at certain points where
        /// processing is occuring. If it is not provided a default instance is supplied which does nothing when the
        /// methods are called.</summary>
        /// <value>The provider.</value>
        public ICookieAuthenticationProvider Provider
        {
            get;
            set;
        }

        /// <summary>The ReturnUrlParameter determines the name of the query string parameter which is appended by the
        /// middleware when a 401 Unauthorized status code is changed to a 302 redirect onto the login path. This is
        /// also the query string parameter looked for when a request arrives on the login path or logout path, in order
        /// to return to the original url after the action is performed.</summary>
        /// <value>The return URL parameter.</value>
        public string ReturnUrlParameter
        {
            get;
            set;
        }

        /// <summary>An optional container in which to store the identity across requests. When used, only a session
        /// identifier is sent to the client. This can be used to mitigate potential problems with very large identities.</summary>
        /// <value>The session store.</value>
        public IAuthenticationSessionStore SessionStore
        {
            get;
            set;
        }

        /// <summary>The SlidingExpiration is set to true to instruct the middleware to re-issue a new cookie with a new
        /// expiration time any time it processes a request which is more than halfway through the expiration window.</summary>
        /// <value>True if sliding expiration, false if not.</value>
        public bool SlidingExpiration
        {
            get;
            set;
        }

        /// <summary>The SystemClock provides access to the system's current time coordinates. If it is not provided a
        /// default instance is used which calls DateTimeOffset.UtcNow. This is typically not replaced except for unit
        /// testing.</summary>
        /// <value>The system clock.</value>
        public ISystemClock SystemClock
        {
            get;
            set;
        }

        /// <summary>The TicketDataFormat is used to protect and unprotect the identity and other properties which are
        /// stored in the cookie value. If it is not provided a default data handler is created using the data
        /// protection service contained in the IAppBuilder.Properties. The default data protection service is based on
        /// machine key when running on ASP.NET, and on DPAPI when running in a different process.</summary>
        /// <value>The ticket data format.</value>
        public ISecureDataFormat<AuthenticationTicket> TicketDataFormat
        {
            get;
            set;
        }
    }
}
