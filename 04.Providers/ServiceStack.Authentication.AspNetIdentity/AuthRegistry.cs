// <copyright file="AuthRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication registry class</summary>
#if NET5_0_OR_GREATER
namespace ServiceStack.Auth
{
    using Clarity.Ecommerce.Interfaces.Workflow;
    using Clarity.Ecommerce.Workflow;
    using Lamar;

    /// <summary>An ASP net identity authentication registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class AspNetIdentityAuthRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="AspNetIdentityAuthRegistry"/> class.</summary>
        public AspNetIdentityAuthRegistry()
        {
            // Use(new AspNetIdentityAuthProvider(new AppSettings())).Singleton().For<ICEFAuthProvider>();
            // Use<AspNetIdentityUserAuthRepository>().Singleton().For<ICEFUserAuthRepository>();
            // For<ITokenizedCredentialsAuthProvider>(new SingletonLifecycle()).Use<TokenizedCredentialsAuthProvider>();
            // For<ICobaltAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new CobaltAuthProvider(new AppSettings())));
            // For<ICobaltUserAuthRepository>(new SingletonLifecycle()).Use<CobaltUserAuthRepository>();
            // For<IDotNetNukeSSOAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new DotNetNukeSSOAuthProvider(new AppSettings())));
            // For<IDotNetNukeSSOUserAuthRepository>(new SingletonLifecycle()).Use<DotNetNukeSSOUserAuthRepository>();
            // For<IOpenIDAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new OpenIDAuthProvider(new AppSettings())));
            // For<IOpenIDUserAuthRepository>(new SingletonLifecycle()).Use<OpenIDUserAuthRepository>();
            // Use(new OktaAuthProvider(new AppSettings())).Singleton().For<IOktaAuthProvider>();
            // Use<OktaUserAuthRepository>().Singleton().For<IOktaUserAuthRepository>();
            Use<AuthenticationWorkflow>().Singleton().For<IAuthenticationWorkflow>();
            For<ICMSAuthUserSession>().Use<CMSAuthUserSession>();
        }
    }
}
#else
namespace ServiceStack.Auth
{
    using Clarity.Ecommerce.Interfaces.Workflow;
    using Clarity.Ecommerce.Workflow;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>An ASP net identity authentication registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class AspNetIdentityAuthRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="AspNetIdentityAuthRegistry"/> class.</summary>
        public AspNetIdentityAuthRegistry()
        {
            // For<IAspNetIdentityAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new AspNetIdentityAuthProvider(new AppSettings())));
            // For<IAspNetIdentityUserAuthRepository>(new SingletonLifecycle()).Use<AspNetIdentityUserAuthRepository>();
            // For<ITokenizedCredentialsAuthProvider>(new SingletonLifecycle()).Use<TokenizedCredentialsAuthProvider>();
            // For<ICobaltAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new CobaltAuthProvider(new AppSettings())));
            // For<ICobaltUserAuthRepository>(new SingletonLifecycle()).Use<CobaltUserAuthRepository>();
            // For<IDotNetNukeSSOAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new DotNetNukeSSOAuthProvider(new AppSettings())));
            // For<IDotNetNukeSSOUserAuthRepository>(new SingletonLifecycle()).Use<DotNetNukeSSOUserAuthRepository>();
            // For<IOpenIDAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new OpenIDAuthProvider(new AppSettings())));
            // For<IOpenIDUserAuthRepository>(new SingletonLifecycle()).Use<OpenIDUserAuthRepository>();
            // For<IOktaAuthProvider>(new SingletonLifecycle()).UseInstance(new ObjectInstance(new OktaAuthProvider(new AppSettings())));
            // For<IOktaUserAuthRepository>(new SingletonLifecycle()).Use<OktaUserAuthRepository>();
            For<IAuthenticationWorkflow>(new SingletonLifecycle()).Use<AuthenticationWorkflow>();
            For<ICMSAuthUserSession>().Use<CMSAuthUserSession>();
        }
    }
}
#endif
