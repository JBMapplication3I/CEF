// <copyright file="CookieAuthenticationDefaults.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie authentication defaults class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    /// <summary>Default values related to cookie-based authentication middleware.</summary>
    public static class CookieAuthenticationDefaults
    {
        /// <summary>The default value used for CookieAuthenticationOptions.AuthenticationType.</summary>
        public const string AuthenticationType = "Cookies";

        /// <summary>The prefix used to provide a default CookieAuthenticationOptions.CookieName.</summary>
        public const string CookiePrefix = ".AspNet.";

        /// <summary>The default value of the CookieAuthenticationOptions.ReturnUrlParameter.</summary>
        public const string ReturnUrlParameter = "ReturnUrl";

        /// <summary>The default value used by UseApplicationSignInCookie for the CookieAuthenticationOptions.LoginPath.</summary>
        public static readonly PathString LoginPath;

        /// <summary>The default value used by UseApplicationSignInCookie for the CookieAuthenticationOptions.LogoutPath.</summary>
        public static readonly PathString LogoutPath;

        /// <summary>Initializes static members of the Microsoft.Owin.Security.Cookies.CookieAuthenticationDefaults class.</summary>
        static CookieAuthenticationDefaults()
        {
            LoginPath = new PathString("/Account/Login");
            LogoutPath = new PathString("/Account/Logout");
        }
    }
}
