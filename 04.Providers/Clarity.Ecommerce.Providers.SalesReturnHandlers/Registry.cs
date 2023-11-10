// <copyright file="Registry.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return handlers registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers
{
    using Actions.Standard;
    using Interfaces.Providers.SalesReturnHandlers.Actions;
    using Interfaces.Providers.SalesReturnHandlers.Queries;
    using Lamar;
    using Queries.Standard;

    /// <summary>The sales return handlers provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesReturnHandlersRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="SalesReturnHandlersRegistry"/> class.</summary>
        public SalesReturnHandlersRegistry()
        {
            Use<StandardSalesReturnActionsProvider>().Singleton().For<ISalesReturnActionsProviderBase>();
            Use<StandardSalesReturnQueriesProvider>().Singleton().For<ISalesReturnQueriesProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers
{
    using Actions.Standard;
    using Interfaces.Providers.SalesReturnHandlers.Actions;
    using Interfaces.Providers.SalesReturnHandlers.Queries;
    using Queries.Standard;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The sales return handlers provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesReturnHandlersRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="SalesReturnHandlersRegistry"/> class.</summary>
        public SalesReturnHandlersRegistry()
        {
            For<ISalesReturnActionsProviderBase>(new SingletonLifecycle()).Use<StandardSalesReturnActionsProvider>();
            For<ISalesReturnQueriesProviderBase>(new SingletonLifecycle()).Use<StandardSalesReturnQueriesProvider>();
        }
    }
}
#endif
