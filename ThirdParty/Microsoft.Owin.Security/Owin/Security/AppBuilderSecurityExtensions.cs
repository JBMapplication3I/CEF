// <copyright file="AppBuilderSecurityExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application builder security extensions class</summary>
namespace Microsoft.Owin.Security
{
    using System;

    /// <summary>Provides extensions methods for app.Property values that are only needed by implementations of
    /// authentication middleware.</summary>
    public static class AppBuilderSecurityExtensions
    {
        /// <summary>Returns the previously set AuthenticationType that external sign in middleware should use when the
        /// browser navigates back to their return url.</summary>
        /// <param name="app">App builder passed to the application startup code.</param>
        /// <returns>The default sign in as authentication type.</returns>
        public static string GetDefaultSignInAsAuthenticationType(this global::Owin.IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (app.Properties.TryGetValue(
                "Microsoft.Owin.Security.Constants.DefaultSignInAsAuthenticationType",
                out var obj))
            {
                var str = obj as string;
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            throw new InvalidOperationException(Resources.Exception_MissingDefaultSignInAsAuthenticationType);
        }

        /// <summary>Called by middleware to change the name of the AuthenticationType that external middleware should
        /// use when the browser navigates back to their return url.</summary>
        /// <param name="app">               App builder passed to the application startup code.</param>
        /// <param name="authenticationType">AuthenticationType that external middleware should sign in as.</param>
        public static void SetDefaultSignInAsAuthenticationType(this global::Owin.IAppBuilder app, string authenticationType)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.Properties["Microsoft.Owin.Security.Constants.DefaultSignInAsAuthenticationType"] = authenticationType ?? throw new ArgumentNullException(nameof(authenticationType));
        }
    }
}
