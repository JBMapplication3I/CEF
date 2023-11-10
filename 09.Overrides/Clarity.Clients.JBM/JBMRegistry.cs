// <copyright file="JBMRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// ReSharper disable EmptyConstructor
#if NET5_0_OR_GREATER
namespace Clarity.Clients.JBM
{
    using Lamar;

    public class JBMRegistry : ServiceRegistry
    {
        public JBMRegistry()
        {
            // Intentionally left blank
        }
    }
}
#else
namespace Clarity.Clients.JBM
{
    using Ecommerce.Interfaces.Providers.Checkouts;
    using Ecommerce.Interfaces.Workflow;
    using Ecommerce.Providers.Checkouts.TargetOrder;
    using StructureMap;

    public class JBMRegistry : Registry
    {
        public JBMRegistry()
        {
            For<ICheckoutProviderBase>().ClearAll().Use<JBMTargetOrderCheckoutProvider>();
            For<ICartItemWorkflow>().ClearAll().Use<JBMCartItemWorkflow>();
        }
    }
}
#endif
