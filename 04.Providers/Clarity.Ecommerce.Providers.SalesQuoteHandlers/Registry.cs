// <copyright file="Registry.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote handlers registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers
{
    using Actions.Standard;
    using Interfaces.Providers.SalesQuoteHandlers.Actions;
    using Interfaces.Providers.SalesQuoteHandlers.Checkouts;
    using Interfaces.Providers.SalesQuoteHandlers.Queries;
    using Lamar;
    using Providers.Checkouts.SingleQuote;
    using Providers.Checkouts.TargetQuote;
    using Queries.Standard;

    /// <summary>The sales quote handlers provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesQuoteHandlersRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="SalesQuoteHandlersRegistry"/> class.</summary>
        public SalesQuoteHandlersRegistry()
        {
            Use<StandardSalesQuoteActionsProvider>().Singleton().For<ISalesQuoteActionsProviderBase>();
            Use<StandardSalesQuoteQueriesProvider>().Singleton().For<ISalesQuoteQueriesProviderBase>();
            // Submit Quote
            if (TargetQuoteSubmitProviderConfig.IsValid(false))
            {
                Use<TargetQuoteSubmitProvider>().Singleton().For<ISalesQuoteSubmitProviderBase>();
                return;
            }
            if (SingleQuoteSubmitProviderConfig.IsValid(false))
            {
                Use<SingleQuoteSubmitProvider>().Singleton().For<ISalesQuoteSubmitProviderBase>();
                return;
            }
            // Assign default
            Use<SingleQuoteSubmitProvider>().Singleton().For<ISalesQuoteSubmitProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers
{
    using Actions.Standard;
    using Interfaces.Providers.SalesQuoteHandlers.Actions;
    using Interfaces.Providers.SalesQuoteHandlers.Checkouts;
    using Interfaces.Providers.SalesQuoteHandlers.Queries;
    using Providers.Checkouts.SingleQuote;
    using Providers.Checkouts.TargetQuote;
    using Queries.Standard;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The sales quote handlers provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesQuoteHandlersRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="SalesQuoteHandlersRegistry"/> class.</summary>
        public SalesQuoteHandlersRegistry()
        {
            For<ISalesQuoteActionsProviderBase>(new SingletonLifecycle()).Use<StandardSalesQuoteActionsProvider>();
            For<ISalesQuoteQueriesProviderBase>(new SingletonLifecycle()).Use<StandardSalesQuoteQueriesProvider>();
            // Submit Quote
            var found = false;
            For<ISalesQuoteSubmitProviderBase>(new SingletonLifecycle()).Use<SingleQuoteSubmitProvider>();
            For<ISalesQuoteSubmitProviderBase>(new SingletonLifecycle()).Use<TargetQuoteSubmitProvider>();
            if (TargetQuoteSubmitProviderConfig.IsValid(false))
            {
                For<ISalesQuoteSubmitProviderBase>(new SingletonLifecycle()).Add<TargetQuoteSubmitProvider>();
                found = true;
            }
            if (!found && SingleQuoteSubmitProviderConfig.IsValid(false))
            {
                For<ISalesQuoteSubmitProviderBase>(new SingletonLifecycle()).Add<SingleQuoteSubmitProvider>();
            }
            if (found)
            {
                return;
            }
            // Assign default
            For<ISalesQuoteSubmitProviderBase>(new SingletonLifecycle()).Add<SingleQuoteSubmitProvider>();
        }
    }
}
#endif
