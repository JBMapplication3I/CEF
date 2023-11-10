// <copyright file="MembershipProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership Provider StructureMap 4 Registry to associate the interfaces with
// their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Memberships
{
    using Basic;
    using Interfaces.Providers.Memberships;
    using Lamar;

    /// <summary>The membership provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class MembershipProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="MembershipProviderRegistry"/> class.</summary>
        public MembershipProviderRegistry()
        {
            if (BasicMembershipProviderConfig.IsValid(false))
            {
                Use<BasicMembershipProvider>().Singleton().For<IMembershipsProviderBase>();
            }
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Memberships
{
    using Basic;
    using Interfaces.Providers.Memberships;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The membership provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class MembershipProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="MembershipProviderRegistry"/> class.</summary>
        public MembershipProviderRegistry()
        {
            if (BasicMembershipProviderConfig.IsValid(false))
            {
                For<IMembershipsProviderBase>(new SingletonLifecycle()).Add<BasicMembershipProvider>();
            }
        }
    }
}
#endif
