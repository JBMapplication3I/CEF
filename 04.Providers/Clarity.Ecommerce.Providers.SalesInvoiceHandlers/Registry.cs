// <copyright file="Registry.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice handlers registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers
{
    using Actions.Standard;
    using Interfaces.Providers.SalesInvoiceHandlers.Actions;
    using Interfaces.Providers.SalesInvoiceHandlers.Queries;
    using Lamar;
    using Queries.Standard;

    /// <summary>The sales invoice handlers provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesInvoiceHandlersRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="SalesInvoiceHandlersRegistry"/> class.</summary>
        public SalesInvoiceHandlersRegistry()
        {
            Use<StandardSalesInvoiceActionsProvider>().Singleton().For<ISalesInvoiceActionsProviderBase>();
            Use<StandardSalesInvoiceQueriesProvider>().Singleton().For<ISalesInvoiceQueriesProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers
{
    using Actions.Standard;
    using Interfaces.Providers.SalesInvoiceHandlers.Actions;
    using Interfaces.Providers.SalesInvoiceHandlers.Queries;
    using Queries.Standard;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The sales invoice handlers provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesInvoiceHandlersRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="SalesInvoiceHandlersRegistry"/> class.</summary>
        public SalesInvoiceHandlersRegistry()
        {
            For<ISalesInvoiceActionsProviderBase>(new SingletonLifecycle()).Use<StandardSalesInvoiceActionsProvider>();
            For<ISalesInvoiceQueriesProviderBase>(new SingletonLifecycle()).Use<StandardSalesInvoiceQueriesProvider>();
        }
    }
}
#endif
