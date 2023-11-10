// <copyright file="Registry.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample request handlers registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers
{
    using Actions.Standard;
    using Checkouts.Standard;
    using Interfaces.Providers.SampleRequestHandlers.Actions;
    using Interfaces.Providers.SampleRequestHandlers.Checkouts;
    using Interfaces.Providers.SampleRequestHandlers.Queries;
    using Lamar;
    using Queries.Standard;

    /// <summary>The sample request handlers provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SampleRequestHandlersRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="SampleRequestHandlersRegistry"/> class.</summary>
        public SampleRequestHandlersRegistry()
        {
            Use<StandardSampleRequestCheckoutProvider>().Singleton().For<ISampleRequestCheckoutProviderBase>();
            Use<StandardSampleRequestActionsProvider>().Singleton().For<ISampleRequestActionsProviderBase>();
            Use<StandardSampleRequestQueriesProvider>().Singleton().For<ISampleRequestQueriesProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers
{
    using Actions.Standard;
    using Checkouts.Standard;
    using Interfaces.Providers.SampleRequestHandlers.Actions;
    using Interfaces.Providers.SampleRequestHandlers.Checkouts;
    using Interfaces.Providers.SampleRequestHandlers.Queries;
    using Queries.Standard;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The sample request handlers provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SampleRequestHandlersRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="SampleRequestHandlersRegistry"/> class.</summary>
        public SampleRequestHandlersRegistry()
        {
            For<ISampleRequestCheckoutProviderBase>(new SingletonLifecycle()).Use<StandardSampleRequestCheckoutProvider>();
            For<ISampleRequestActionsProviderBase>(new SingletonLifecycle()).Use<StandardSampleRequestActionsProvider>();
            For<ISampleRequestQueriesProviderBase>(new SingletonLifecycle()).Use<StandardSampleRequestQueriesProvider>();
        }
    }
}
#endif
