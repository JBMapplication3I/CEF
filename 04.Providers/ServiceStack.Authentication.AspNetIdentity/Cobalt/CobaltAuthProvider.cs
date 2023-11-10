// <copyright file="CobaltAuthProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Cobalt authentication provider class</summary>
namespace ServiceStack.Auth
{
#if NET5_0_OR_GREATER
    using System.Threading;
    using System.Threading.Tasks;
#endif
    using Configuration;
    using FluentValidation;
    using JetBrains.Annotations;

    /// <summary>An Cobalt authentication provider.</summary>
    /// <seealso cref="CEFAuthProviderBase"/>
    [PublicAPI]
    public class CobaltAuthProvider : CEFAuthProviderBase
    {
        /// <summary>Initializes a new instance of the <see cref="CobaltAuthProvider"/> class.</summary>
        public CobaltAuthProvider()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CobaltAuthProvider"/> class.</summary>
        /// <param name="appSettings">The application settings.</param>
        public CobaltAuthProvider(IAppSettings appSettings)
            : this(appSettings, StaticRealm, StaticName)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CobaltAuthProvider"/> class.</summary>
        /// <param name="appSettings">  The application settings.</param>
        /// <param name="authRealm">    The authentication realm.</param>
        /// <param name="oAuthProvider">The authentication provider.</param>
        public CobaltAuthProvider(IAppSettings appSettings, string authRealm, string oAuthProvider)
            : base(appSettings, authRealm, oAuthProvider)
        {
        }

        /// <summary>Gets the name of the static.</summary>
        /// <value>The name of the static.</value>
        public static string StaticName => "cobalt";

        /// <summary>Gets the static realm.</summary>
        /// <value>The static realm.</value>
        public static string StaticRealm => "/auth/cobalt";

        /// <inheritdoc/>
        public override string Name => StaticName;

        /// <inheritdoc/>
        public override string Realm => StaticRealm;

        /// <inheritdoc/>
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
#if NET5_0_OR_GREATER
        public override async Task<object> AuthenticateAsync(
            IServiceBase authService,
            IAuthSession session,
            Authenticate request,
            CancellationToken token = default)
#else
        public override object Authenticate(
            IServiceBase authService,
            IAuthSession session,
            Authenticate request)
#endif
        {
#if NET5_0_OR_GREATER
            await new CobaltAuthValidator().ValidateAndThrowAsync(request, token).ConfigureAwait(false);
            return await AuthenticateAsync(
                    authService,
                    session,
                    request.UserName,
                    request.Password,
                    request.uri,
                    token: token)
                .ConfigureAwait(false);
#else
            new CobaltAuthValidator().ValidateAndThrow(request);
            return Authenticate(
                authService: authService,
                session: session,
                userName: request.UserName,
                password: request.Password,
                referrerUrl: request.Continue);
#endif
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        /// <summary>An cobalt authentication validator.</summary>
        /// <seealso cref="AbstractValidator{Authenticate}"/>
        private class CobaltAuthValidator : AbstractValidator<Authenticate>
        {
            public CobaltAuthValidator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                ////RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
