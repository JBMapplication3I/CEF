// <copyright file="AppBuilderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application builder extensions class</summary>
namespace Microsoft.Owin.Security.DataProtection
{
    using System;

    /// <summary>An application builder extensions.</summary>
    public static class AppBuilderExtensions
    {
        /// <summary>An IAppBuilder extension method that creates data protector.</summary>
        /// <param name="app">     The app to act on.</param>
        /// <param name="purposes">A variable-length parameters list containing purposes.</param>
        /// <returns>The new data protector.</returns>
        public static IDataProtector CreateDataProtector(this global::Owin.IAppBuilder app, params string[] purposes)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var dataProtectionProvider = app.GetDataProtectionProvider() ?? FallbackDataProtectionProvider(app);
            return dataProtectionProvider.Create(purposes);
        }

        /// <summary>An IAppBuilder extension method that gets data protection provider.</summary>
        /// <param name="app">The app to act on.</param>
        /// <returns>The data protection provider.</returns>
        public static IDataProtectionProvider GetDataProtectionProvider(this global::Owin.IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (app.Properties.TryGetValue("security.DataProtectionProvider", out var obj))
            {
                if (obj is Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> func)
                {
                    return new CallDataProtectionProvider(func);
                }
            }
            return null;
        }

        /// <summary>An IAppBuilder extension method that sets data protection provider.</summary>
        /// <param name="app">                   The app to act on.</param>
        /// <param name="dataProtectionProvider">The data protection provider.</param>
        public static void SetDataProtectionProvider(
            this global::Owin.IAppBuilder app,
            IDataProtectionProvider dataProtectionProvider)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (dataProtectionProvider == null)
            {
                app.Properties.Remove("security.DataProtectionProvider");
                return;
            }
            app.Properties["security.DataProtectionProvider"] =
                (Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>)(purposes =>
                {
                    var dataProtector = dataProtectionProvider.Create(purposes);
                    return new Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>(
                        dataProtector.Protect,
                        dataProtector.Unprotect);
                });
        }

        /// <summary>Fallback data protection provider.</summary>
        /// <param name="app">The application.</param>
        /// <returns>An IDataProtectionProvider.</returns>
        private static IDataProtectionProvider FallbackDataProtectionProvider(global::Owin.IAppBuilder app)
        {
            return new DpapiDataProtectionProvider(GetAppName(app));
        }

        /// <summary>Gets application name.</summary>
        /// <param name="app">The application.</param>
        /// <returns>The application name.</returns>
        private static string GetAppName(global::Owin.IAppBuilder app)
        {
            if (app.Properties.TryGetValue("host.AppName", out var obj))
            {
                var str = obj as string;
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            throw new NotSupportedException(Resources.Exception_DefaultDpapiRequiresAppNameKey);
        }

        /// <summary>A call data protection provider.</summary>
        /// <seealso cref="IDataProtectionProvider"/>
        /// <seealso cref="IDataProtectionProvider"/>
        private class CallDataProtectionProvider : IDataProtectionProvider
        {
            /// <summary>The create.</summary>
            private readonly Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> _create;

            /// <summary>Initializes a new instance of the
            /// <see cref="CallDataProtectionProvider" />
            /// class.</summary>
            /// <param name="create">The create.</param>
            public CallDataProtectionProvider(Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> create)
            {
                _create = create;
            }

            /// <inheritdoc/>
            public IDataProtector Create(params string[] purposes)
            {
                var tuple = _create(purposes);
                return new CallDataProtection(tuple.Item1, tuple.Item2);
            }

            /// <summary>A call data protection.</summary>
            /// <seealso cref="IDataProtector"/>
            /// <seealso cref="IDataProtector"/>
            private class CallDataProtection : IDataProtector
            {
                /// <summary>The protect.</summary>
                private readonly Func<byte[], byte[]> _protect;

                /// <summary>The unprotect.</summary>
                private readonly Func<byte[], byte[]> _unprotect;

                /// <summary>Initializes a new instance of the
                /// <see
                /// cref="CallDataProtection"
                /// />
                /// class.</summary>
                /// <param name="protect">  The protect.</param>
                /// <param name="unprotect">The unprotect.</param>
                public CallDataProtection(Func<byte[], byte[]> protect, Func<byte[], byte[]> unprotect)
                {
                    _protect = protect;
                    _unprotect = unprotect;
                }

                /// <inheritdoc/>
                public byte[] Protect(byte[] userData)
                {
                    return _protect(userData);
                }

                /// <inheritdoc/>
                public byte[] Unprotect(byte[] protectedData)
                {
                    return _unprotect(protectedData);
                }
            }
        }
    }
}
